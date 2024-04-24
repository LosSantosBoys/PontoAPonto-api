using Microsoft.Extensions.Options;
using PontoAPonto.Domain.Models;
using PontoAPonto.Service.Extensions;

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
            builder.Services.AddSwaggerGen();

            // Add services
            var configuration = builder.Configuration;
            builder.Services.AddServices(configuration);
            builder.Services.AddOptions<EmailConfig>()
                .Bind(builder.Configuration.GetSection("EmailConfig"));
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailConfig>>().Value);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
