using Azure.Storage.Blobs;
using HumanResources.Domain.Repository;
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

        public async Task<bool> UpdateImage(IFormFile file)
        {
            var client = _blobClientService.GetBlobContainerClient(blobContainerName: _storageAccountSettings.BlobContainerName);
            if(!client.ExistsAsync()

        }

        public Task<bool> UploadImage(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
