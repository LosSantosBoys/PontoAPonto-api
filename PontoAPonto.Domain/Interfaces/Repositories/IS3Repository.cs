using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Interfaces.Repositories
{
    public interface IS3Repository
    {
        Task<CustomActionResult> UploadFileAsync(string bucketName, string base64, string path);
        Task<CustomActionResult<byte[]>> GetFileAsync(string bucketName, string keyName, string destPath);
    }
}
