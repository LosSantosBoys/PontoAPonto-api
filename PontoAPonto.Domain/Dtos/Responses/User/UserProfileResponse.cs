namespace PontoAPonto.Domain.Dtos.Responses.User
{
    public class UserProfileResponse
    {
        public string Name { get; set; }
        public string Birthday { get; set; }
        public double Reputation { get; set; }
        public DateTime UserSince { get; set; }
    }
}
