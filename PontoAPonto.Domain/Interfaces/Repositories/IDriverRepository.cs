using PontoAPonto.Domain.Models.Entities;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Interfaces.Repositories
{
    public interface IDriverRepository
    {
        Task<CustomActionResult> AddDriverAsync(Driver driver);
        Task<CustomActionResult> DeleteDriverByEmailAsync(string email);
        Task<CustomActionResult<Driver>> GetDriverByEmailAsync(string email);
        Task<CustomActionResult> UpdateDriverAsync(Driver driver);
    }
}
