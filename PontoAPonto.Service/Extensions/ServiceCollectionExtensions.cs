using PontoAPonto.Data.Repositories;
using PontoAPonto.Domain.Interfaces.Infra;
using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PontoAPonto.Infra.Configs;
using PontoAPonto.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace PontoAPonto.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Infra
            services.AddSingleton<IConnStringProvider, ConnStringProvider>();

            //Repositories
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddDbContext<UserContext>();

            //Services
            services.AddSingleton<IUserService, UserService>();

            return services;
        }
    }
}
