using Microsoft.EntityFrameworkCore;
using PontoAPonto.Data.Contexts;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Models.Entities;
using System.Net;
using System.Text;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<BaseResponse<OtpUserResponse>> CreateUserOtpAsync(User user)
        {
            var response = new BaseResponse<OtpUserResponse>();

            try
            {
                await _userContext.Users.AddAsync(user);
                await _userContext.SaveChangesAsync();
                return response;
            }
            catch (DbUpdateException ex)
            {
                var message = new StringBuilder().AppendFormat(ResponseMessages.ErrorCreatingUserOtp, ex.Message).ToString();
                return response.CreateError(HttpStatusCode.BadRequest, message);
            }
        }
    }
}
