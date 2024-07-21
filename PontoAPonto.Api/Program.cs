using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PontoAPonto.Service.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using PontoAPonto.Domain.Interfaces.Rest;
using PontoAPonto.Data.Rest;
using PontoAPonto.Domain.Models.Configs;
using Microsoft.EntityFrameworkCore;
using PontoAPonto.Data.Contexts;

namespace PontoAPonto.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
            builder.Services.AddServices(configuration);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<UserContext>(options =>
                options.UseMySQL(connectionString));

            // Add Singleton appsettings objects
            // TODO - DESERIALIZE INTO SINGLE OBJECT
            builder.Services.Configure<KeysConfig>(builder.Configuration.GetSection("ConnectionStrings"));
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<KeysConfig>>().Value);
            builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailConfig>>().Value);
            builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("Jwt"));
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtConfig>>().Value);
            builder.Services.Configure<ApiKeys>(builder.Configuration.GetSection("ApiKeys"));
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<ApiKeys>>().Value);
            builder.Services.Configure<RedisConfig>(builder.Configuration.GetSection("RedisConfig"));
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<RedisConfig>>().Value);

            // Add services
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Null JWT Key in appsettings")))
                    };
                });

            builder.Services.AddHttpClient<IMapsApi, MapsApi>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(builder => builder //TODO - Define Policy
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
