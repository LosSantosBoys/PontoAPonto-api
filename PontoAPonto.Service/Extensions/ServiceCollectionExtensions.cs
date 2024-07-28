using PontoAPonto.Data.Repositories;
using PontoAPonto.Domain.Interfaces.Infra;
using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PontoAPonto.Data.Contexts;
using PontoAPonto.Domain.Interfaces.Rest;
using PontoAPonto.Data.Rest;
using PontoAPonto.Domain.Interfaces.WebScrapper;
using PontoAPonto.Data.WebScrapper;
using PontoAPonto.Data.Cache;

namespace PontoAPonto.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Infra
            services.AddSingleton<IEmailService, EmailService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IRedisService, RedisService>();

            //Repositories
            services.AddDbContext<UserContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IGasPriceScrapper, GasPriceScrapper>();
            services.AddScoped<IMapsApi, MapsApi>();

            //Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IMapsService, MapsService>();
            services.AddScoped<ISignUpService, SignUpService>();

            return services;
        }
    }
}
