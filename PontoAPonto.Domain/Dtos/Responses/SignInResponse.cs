namespace PontoAPonto.Domain.Dtos.Responses
{
    public class SignInResponse
    {
        public string TokenType { get; set; }
        public string Token { get; set; }
        public bool IsFirstAccess { get; set; }
    }
}
