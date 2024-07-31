using Amazon.S3.Transfer;
using Amazon.S3;
using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Models;
using Amazon;
using System.Net;
using PontoAPonto.Domain.Errors.AWS;

namespace PontoAPonto.Data.Repositories
{
    public class S3Repository : IS3Repository
    {
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;

        public async Task<CustomActionResult> UploadFileAsync(string bucketName, string base64, string path)
        {
            try
            {
                var bytes = Convert.FromBase64String(base64);

                using var memoryStream = new MemoryStream(bytes);

                var s3Client = new AmazonS3Client(bucketRegion);
                var fileTransferUtility = new TransferUtility(s3Client);

                await fileTransferUtility.UploadAsync(memoryStream, bucketName, path);

                return new CustomActionResult(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Console.WriteLine($"S3 Error: '{e.Message}'");
                return S3Error.UploadFail();
            }
        }

        public async Task<CustomActionResult<byte[]>> GetFileAsync(string bucketName, string keyName, string destPath)
        {
            try
            {
                var s3Client = new AmazonS3Client(bucketRegion);
                var fileTransferUtility = new TransferUtility(s3Client);

                await fileTransferUtility.DownloadAsync(destPath, bucketName, keyName);

                byte[] data = await File.ReadAllBytesAsync(destPath);

                if (!data.Any())
                {
                    return S3Error.FileNotFound();
                }

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine($"S3 Error: '{e.Message}'");
                return S3Error.GetFileFail();
            }
        }
    }
}
