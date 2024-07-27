using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using PontoAPonto.Domain.Interfaces.Rest;
using PontoAPonto.Data.Rest;
using PontoAPonto.Domain.Models.Configs;
using Microsoft.EntityFrameworkCore;
using PontoAPonto.Data.Contexts;
using PontoAPonto.Service.Extensions;
using PontoAPonto.Api.Handlers;

namespace PontoAPonto.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Configure Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                           }
                       },
                       new string[] {}
                    }
                });
            });

            // Add services
            var configuration = builder.Configuration;

            // Add database context
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<UserContext>(options =>
                options.UseMySQL(connectionString));

            // Configure appsettings objects
            builder.Services.Configure<KeysConfig>(configuration.GetSection("ConnectionStrings"));
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<KeysConfig>>().Value);

            builder.Services.Configure<EmailConfig>(configuration.GetSection("EmailConfig"));
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailConfig>>().Value);

            builder.Services.Configure<JwtConfig>(configuration.GetSection("Jwt"));
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtConfig>>().Value);

            builder.Services.Configure<ApiKeys>(configuration.GetSection("ApiKeys"));
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<ApiKeys>>().Value);

            builder.Services.Configure<RedisConfig>(configuration.GetSection("RedisConfig"));
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<RedisConfig>>().Value);

            // Configure JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddScheme<JwtBearerOptions, JwtCustomHandler>(JwtBearerDefaults.AuthenticationScheme,
            options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    LogValidationExceptions = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
                };
            });

            builder.Services.AddAuthorization();

            builder.Services.AddHttpClient<IMapsApi, MapsApi>();

            builder.Services.AddServices(configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
