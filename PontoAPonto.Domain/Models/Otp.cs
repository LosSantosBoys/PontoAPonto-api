namespace PontoAPonto.Domain.Models
{
    public class Otp
    {
        public Otp()
        {
            Password = new Random().Next(1000, 9999);
            Expiracy = DateTime.Now.AddMinutes(10);
            Attempts = 0;
            IsVerified = false;
        }

        public int Password { get; private set; }
        public DateTime Expiracy { get; private set; }
        public int Attempts { get; private set; }
        public bool IsVerified { get; private set; }

        public bool SendOtp(int password)
        {
            int maxAttempts = 5;

            if (Attempts > maxAttempts || DateTime.Now > Expiracy || Password != password)
            {
                Attempts++;
                return false;
            }

            IsVerified = true;
            return IsVerified;
        }

        public void GenerateNewOtp()
        {
            Password = new Random().Next(1000, 9999);
            Expiracy = DateTime.Now.AddMinutes(10);
        }
    }
}
