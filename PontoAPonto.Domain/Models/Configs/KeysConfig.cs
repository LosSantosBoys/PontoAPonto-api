namespace PontoAPonto.Domain.Models.Configs
{
    public class KeysConfig
    {
        public JwtConfig JwtConfig { get; set; }
        public EmailConfig EmailConfig { get; set; }
        public RedisConfig RedisConfig { get; set; }
        public ApiKeys ApiKeys { get; set; }
        public string DefaultConnection { get; set; }
    }

    public class ApiKeys
    {
        public string Maps { get; set; }
    }
}
