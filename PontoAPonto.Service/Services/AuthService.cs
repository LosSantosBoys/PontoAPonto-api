using Microsoft.IdentityModel.Tokens;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Models.Configs;
using System.IdentityModel.Tokens.Jwt;
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
            var handler = new JwtSecurityTokenHandler();

            var securityKey = _jwtConfig.Key;
            var issuer = _jwtConfig.Issuer;

            var privateKey = Encoding.UTF8.GetBytes(securityKey);

            var credentials = new SigningCredentials(
                        new SymmetricSecurityKey(privateKey),
                        SecurityAlgorithms.HmacSha256);

            var claim = new ClaimsIdentity();
            claim.AddClaim(new Claim(ClaimTypes.Role, "USER"));

            var descriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Subject = claim,
                Issuer = issuer,
                Audience = _jwtConfig.Audience,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(12),
            };

            return handler.CreateEncodedJwt(descriptor);
        }
    }
}
