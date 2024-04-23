using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Domain.Dtos.Requests
{
    public class OtpUserRequest
    {
        public OtpUserRequest(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone {  get; set; }

        public User ToEntity()
        {
            return new User().CreateUser(Name, Email, Phone);
        }
    }
}
