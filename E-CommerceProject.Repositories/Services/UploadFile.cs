namespace E_CommerceProject.Repositories.Services;

public interface IUploadFile
{
    Task<string> UploadFileAsync(string filePath, IFormFile file);
}

public class UploadFile(IHostingEnvironment hostingEnvironment) : IUploadFile
{

    private readonly IHostingEnvironment _hostingEnvironment = hostingEnvironment;

    public async Task<string> UploadFileAsync(string path, IFormFile file)
    {
        string uniqueFileName;
        string rootPath = _hostingEnvironment.WebRootPath + path;

        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }

        uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

        string filePath = Path.Combine(rootPath, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
            fileStream.Dispose();
        }

        return path + uniqueFileName;
    }
}