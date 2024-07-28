using Microsoft.EntityFrameworkCore;
using PontoAPonto.Data.Contexts;
using PontoAPonto.Domain.Errors;
using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Models;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Data.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly UserContext _userContext;

        public DriverRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<CustomActionResult> AddDriverAsync(Driver driver)
        {
            try
            {
                await _userContext.Drivers.AddAsync(driver);

                if (await _userContext.SaveChangesAsync() == 0)
                {
                    return SignUpError.DatabaseError();
                }

                return CustomActionResult.NoContent();
            }
            catch (Exception ex)
            {
                return SignUpError.DatabaseError();
            }
        }

        public async Task<CustomActionResult> DeleteDriverByEmailAsync(string email)
        {
            try
            {
                var rowsAffected = await _userContext.Drivers
                .Where(x => x.Email == email)
                .ExecuteDeleteAsync();

                if (rowsAffected == 0)
                {
                    return SignUpError.UserNotFound();
                }

                return CustomActionResult.NoContent();
            }
            catch (Exception ex)
            {
                return SignUpError.DatabaseError();
            }
        }
    }
}
