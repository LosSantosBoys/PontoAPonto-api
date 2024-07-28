using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Models;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Service.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<CustomActionResult> AddDriverAsync(Driver driver)
        {
            return await _driverRepository.AddDriverAsync(driver);
        }

        public async Task<CustomActionResult> DeleteDriverByEmailAsync(string email)
        {
            return await _driverRepository.DeleteDriverByEmailAsync(email);
        }
    }
}
