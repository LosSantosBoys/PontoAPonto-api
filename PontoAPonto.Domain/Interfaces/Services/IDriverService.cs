using PontoAPonto.Domain.Models;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Domain.Interfaces.Services
{
    public interface IDriverService
    {
        Task<CustomActionResult> AddDriverAsync(Driver driver);
        Task<CustomActionResult> DeleteDriverByEmailAsync(string email);
        Task<CustomActionResult<Driver>> GetDriverByEmailAsync(string email);
        Task<CustomActionResult> UpdateDriverAsync(Driver driver);
        Task<CustomActionResult> CaptureProfilePictureAsync(string email, string imageBase64);
        Task<CustomActionResult> CaptureDocumentPictureAsync(string email, string imageBase64);
        Task<CustomActionResult> InsertCarInfoAsync(CarInfo request, string email);
    }
}
