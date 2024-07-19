using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Models.Configs;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PontoAPonto.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtConfig _jwtConfig;

        public AuthService(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        public string GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));

            var claims = new Dictionary<string, object>
            {
                [ClaimTypes.Role] = "User",
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                Claims = claims,
                IssuedAt = null,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(180),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var handler = new JsonWebTokenHandler();
            handler.SetDefaultTimesOnTokenCreation = false;
            return handler.CreateToken(descriptor);
        }

        public async Task<bool> ValidateJwtTokenAsync(string jwtToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key)),
                ValidateIssuer = true,
                ValidIssuer = _jwtConfig.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtConfig.Audience,
                ValidateLifetime = true,
            };

            var handler = new JsonWebTokenHandler();
            var result = await handler.ValidateTokenAsync(jwtToken, tokenValidationParameters);
            return result.IsValid;
        }
    }
}
