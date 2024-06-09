using Microsoft.AspNetCore.Http;

namespace HumanResources.Domain.Repository
{
    public interface IBlobClientReposiotry
    {
        Task<bool> UploadImage(IFormFile file, CancellationToken token);

        Task<bool> UpdateImage(IFormFile file, string oldFileName, CancellationToken token); 
    }
}
