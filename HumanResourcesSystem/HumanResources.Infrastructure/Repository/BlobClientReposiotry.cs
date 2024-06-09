using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.Repository;
using HumanResources.Domain.StorageAccountModel;
using HumanResources.Infrastructure.StorageAccount;
using Microsoft.AspNetCore.Http;

namespace HumanResources.Infrastructure.Repository
{
    public class BlobClientReposiotry : IBlobClientReposiotry
    {
        private readonly BlobServiceClient _blobClientService;
        private readonly StorageAccountSettings _storageAccountSettings;
        public BlobClientReposiotry(BlobServiceClient blobClientService, StorageAccountSettings storageAccountSettings)
        {
            _blobClientService = blobClientService;
            _storageAccountSettings = storageAccountSettings;
        }

        public async Task<BlobResponse> UpdateImage(IFormFile file, string fileName, CancellationToken token)
        {
            var client = _blobClientService.GetBlobContainerClient(blobContainerName: _storageAccountSettings.BlobContainerName);
            if(!await client.ExistsAsync(token)) 
            {
                return new BlobResponse()
                {
                    IsSuccess = false,
                    FileName = fileName,
                    Message = $"Invalid blob container name: " +
                     $"{_storageAccountSettings.BlobContainerName}, contact with admin"
                };
            }
            await client.DeleteBlobIfExistsAsync(fileName, Azure.Storage.Blobs.Models.DeleteSnapshotsOption.None, null, token);

            var newBlob = client.GetBlobClient(fileName);

            var blobHeader = new BlobHttpHeaders()
            {
                ContentType = $"image/jpeg"
            };

            await newBlob.UploadAsync(file.OpenReadStream(),new BlobUploadOptions { HttpHeaders = blobHeader},token);

            return new BlobResponse()
            {
                FileName = fileName,
                IsSuccess = true,
            };


            

        }

        public async Task<BlobResponse> UploadImage(IFormFile file, string fileName,CancellationToken token)
        {
            var client = _blobClientService.GetBlobContainerClient(blobContainerName: _storageAccountSettings.BlobContainerName);
            if (!await client.ExistsAsync(cancellationToken: token))
            {
                return new BlobResponse()
                {
                    IsSuccess = false,
                    FileName = fileName,
                    Message = $"Invalid blob container name: " +
                    $"{_storageAccountSettings.BlobContainerName}, contact with admin"
                };
            }
            var blob = client.GetBlobClient(fileName);

            var blobHeader = new BlobHttpHeaders()
            {
                ContentType = $"image/jpeg"
            };
            await blob.UploadAsync(file.OpenReadStream(), new BlobUploadOptions { HttpHeaders = blobHeader }, token);

            return new BlobResponse()
            {
                IsSuccess = true,
                FileName = fileName,
            };
        }

    }
}
