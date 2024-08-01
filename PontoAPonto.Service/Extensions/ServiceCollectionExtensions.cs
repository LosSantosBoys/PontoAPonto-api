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
using PontoAPonto.Domain.Interfaces.UseCase;
using PontoAPonto.Service.UseCases;
using Microsoft.Extensions.Options;
using PontoAPonto.Domain.Models.Configs;

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
            services.AddScoped<IS3Repository, S3Repository>();

            //Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IMapsService, MapsService>();
            services.AddScoped<ISignUpService, SignUpService>();
            services.AddScoped<ISignInService, SignInService>();

            //UseCases
            services.AddScoped<ISignUpUseCase, SignUpUseCase>();
            services.AddScoped<ISignInUseCase, SignInUseCase>();
            services.AddScoped<IUserUseCase, UserUseCase>();
            services.AddScoped<IDriverUseCase, DriverUseCase>();

            AddSingletonConfigObjects(services, configuration);

            return services;
        }

        private static void AddSingletonConfigObjects(IServiceCollection services, IConfiguration configuration)
        {
            // Configure appsettings objects
            services.Configure<KeysConfig>(configuration.GetSection("ConnectionStrings"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<KeysConfig>>().Value);

            services.Configure<EmailConfig>(configuration.GetSection("EmailConfig"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailConfig>>().Value);

            services.Configure<JwtConfig>(configuration.GetSection("Jwt"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtConfig>>().Value);

            services.Configure<ApiKeys>(configuration.GetSection("ApiKeys"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<ApiKeys>>().Value);

            services.Configure<RedisConfig>(configuration.GetSection("RedisConfig"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<RedisConfig>>().Value);

            services.Configure<S3Config>(configuration.GetSection("S3Config"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<S3Config>>().Value);
        }
    }
}