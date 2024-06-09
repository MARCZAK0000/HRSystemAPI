using HumanResources.Domain.StorageAccountModel;
using Microsoft.AspNetCore.Http;

namespace HumanResources.Domain.Repository
{
    public interface IBlobClientReposiotry
    {
        Task<BlobResponse> UploadImage(IFormFile file, string fileName, CancellationToken token);

        Task<BlobResponse> UpdateImage(IFormFile file, string fileName, CancellationToken token); 
    }
}
