using PontoAPonto.Domain.Models;

namespace PontoAPonto.Tests.Models
{
    public class OtpTests
    {
        [Fact]
        public void Otp_SendOtp_ReturnsFalse_IfAttemptsExceeded()
        {
            // Arrange
            var otp = new Otp();
            for (int i = 0; i < 6; i++)
            {
                otp.SendOtp(1234);
            }

            // Act
            var result = otp.SendOtp(otp.Password);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Otp_SendOtp_ReturnsFalse_IfIncorrectPassword()
        {
            // Arrange
            var otp = new Otp();

            // Act
            var result = otp.SendOtp(12345);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Otp_SendOtp_ReturnsTrue_IfValid()
        {
            // Arrange
            var otp = new Otp();

            // Act
            var result = otp.SendOtp(otp.Password);

            // Assert
            Assert.True(result);
            Assert.True(otp.IsVerified);
        }

        [Fact]
        public void Otp_GenerateNewOtp_UpdatesValues()
        {
            // Arrange
            var otp = new Otp();
            var initialPassword = otp.Password;
            var initialExpiry = otp.Expiracy;

            // Act
            otp.GenerateNewOtp();

            // Assert
            Assert.NotEqual(initialPassword, otp.Password);
            Assert.NotEqual(initialExpiry, otp.Expiracy);
            Assert.False(otp.IsVerified);
        }
    }
}
