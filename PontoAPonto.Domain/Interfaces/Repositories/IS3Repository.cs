using Microsoft.AspNetCore.Http;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Interfaces.Repositories
{
    public interface IS3Repository
    {
        Task<CustomActionResult> UploadFileAsync(string bucketName, string base64, string path);
        Task<CustomActionResult> UploadFileAsync(string bucketName, IFormFile file, string path);
        Task<CustomActionResult<byte[]>> GetFileAsync(string bucketName, string keyName, string destPath);
        Task<CustomActionResult> UploadPublicFileAsync(string bucketName, string base64, string path);
    }
}
