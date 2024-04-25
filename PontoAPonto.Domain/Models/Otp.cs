namespace PontoAPonto.Domain.Models
{
    public class Otp
    {
        public Otp()
        {
            Password = new Random().Next(1000, 9999);
            Expiracy = DateTime.Now.AddMinutes(30);
            Attempts = 0;
            IsVerified = false;
        }
        private readonly int maxAttempts = 5;
        public int Password { get; private set; }
        public DateTime Expiracy { get; private set; }
        public int Attempts { get; private set; }
        public bool IsVerified { get; private set; }

        public bool SendOtp(int password)
        {
            if (IsVerified || Attempts >= maxAttempts)
                return false;

            if (DateTime.Now > Expiracy || Password != password)
            {
                Attempts++;
                return false;
            }

            IsVerified = true;
            return IsVerified;
        }

        public bool GenerateNewOtp()
        {
            if (IsVerified || Attempts > maxAttempts) 
                return false;

            Password = new Random().Next(1000, 9999);
            Expiracy = DateTime.Now.AddMinutes(10);
            return true;
        }
    }
}