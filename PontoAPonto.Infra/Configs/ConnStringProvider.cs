using Microsoft.Extensions.Configuration;
using PontoAPonto.Domain.Interfaces.Infra;

namespace PontoAPonto.Infra.Configs
{
    public class ConnStringProvider : IConnStringProvider
    {
        private readonly IConfiguration _configuration;

        public ConnStringProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString
        {
            get
            {
                var conn = _configuration.GetConnectionString("DefaultConnection");

                if (conn == null)
                {
                    throw new InvalidOperationException("Failed to retrieve ConnectionString from app.settings.");
                }

                return conn;
            }
        }
    }
}
