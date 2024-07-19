namespace PontoAPonto.Domain.Models.Configs
{
    public class KeysConfig
    {
        public JwtConfig JwtConfig { get; set; }
        public EmailConfig EmailConfig { get; set; }
        public RedisConfig RedisConfig { get; set; }
        public string DefaultConnection { get; set; }
    }
}
