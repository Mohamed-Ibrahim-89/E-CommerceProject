using Microsoft.AspNetCore.Http;

namespace E_CommerceProject.Repositories.Interfaces
{
    public interface IUploadFile
    {
        Task<string> UploadFileAsync(string filePath, IFormFile file);
    }
}
