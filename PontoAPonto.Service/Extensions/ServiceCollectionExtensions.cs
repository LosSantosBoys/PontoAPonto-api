using PontoAPonto.Data.Repositories;
using PontoAPonto.Domain.Interfaces.Infra;
using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PontoAPonto.Infra.Configs;
using PontoAPonto.Data.Contexts;
using PontoAPonto.Domain.Interfaces.Rest;
using PontoAPonto.Data.Rest;
using PontoAPonto.Domain.Interfaces.WebScrapper;
using PontoAPonto.Data.WebScrapper;

namespace PontoAPonto.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Infra
            services.AddSingleton<IConnStringProvider, ConnStringProvider>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Repositories
            services.AddDbContext<UserContext>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMapsApi, MapsApi>();
            services.AddTransient<IGasPriceScrapper, GasPriceScrapper>();

            //Services
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<IMapsService, MapsService>();

            return services;
        }
    }
}
