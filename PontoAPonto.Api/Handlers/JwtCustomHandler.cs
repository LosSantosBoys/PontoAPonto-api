using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace PontoAPonto.Api.Handlers
{
    public class JwtCustomHandler : JwtBearerHandler
    {
        public JwtCustomHandler(IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                return await base.HandleAuthenticateAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Logger.LogError(ex, "An error occurred during JWT authentication.");
                return AuthenticateResult.Fail(ex);
            }
        }
    }

}
