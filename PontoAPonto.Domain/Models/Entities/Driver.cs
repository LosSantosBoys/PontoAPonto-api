using PontoAPonto.Domain.Enums;
using System;

namespace PontoAPonto.Domain.Models.Entities
{
    public class Driver : UserBase
    {
        public DriverStatus Status { get; protected set; }
        public CarInfo? CarInfo { get; private set; }
        public bool Approved { get; private set; }
        public DateTime? ApprovedAt { get; private set; }
        public string? UrlProfilePicute { get; protected set; }
        public Location? Location { get; private set; }

        public static Driver CreateDriver(string name, string email, string phone, byte[] passwordHash, byte[] passwordSalt, string cpf, DateTime birthday)
        {
            return new Driver
            {
                Name = name,
                Email = email,
                Phone = phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Cpf = cpf,
                Birthday = birthday,
                Otp = new Otp(),
                IsFirstAccess = true,
                Reputation = 5,
                Status = DriverStatus.WAITING_OTP_VERIFICATION,
                Approved = false
            };
        }

        public override CustomActionResult ValidateOtp(int otpCode)
        {
            var result = base.ValidateOtp(otpCode);

            if (result.Success)
            {
                Status = DriverStatus.SIGNIN_AVAILABLE;
            }

            return result;
        }

        public void CaptureFacePicture()
        {
            Status = DriverStatus.WAITING_FACE_CAPTURE;
        }

        public void CaptureDocumentPicture()
        {
            Status = DriverStatus.WAITING_DOCUMENT_CAPTURE;
        }

        public void SetCarInfo(CarInfo carInfo)
        {
            CarInfo = new CarInfo(carInfo.Model, carInfo.Year, carInfo.Plate, carInfo.Color);
            Status = DriverStatus.WAITING_MANUAL_APPROVAL;
        }

        public bool AproveDriver()
        {
            Approved = true;
            ApprovedAt = DateTime.Now;
            Status = DriverStatus.APPROVED;

            return true;
        }

        public void UpdateProfilePicture(string url)
        {
            UrlProfilePicute = url;
        }

        public void UpdateCarInfo(CarInfo carInfo)
        {
            if (CarInfo == null)
            {
                CarInfo = new CarInfo(carInfo.Model, carInfo.Year, carInfo.Plate, carInfo.Color);
                return;
            }

            CarInfo.UpdateCarInfo(carInfo.Model, carInfo.Year, carInfo.Plate, carInfo.Color);
        }

        public void UpdateLocation(Location location)
        {
            if (Location == null)
            {
                Location = new Location(location.Country, location.State, location.City);
                return;
            }
            
            Location.UpdateLocation(Location.Country, location.State, location.City);
        }
    }
}
