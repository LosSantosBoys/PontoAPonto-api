using Microsoft.EntityFrameworkCore;
using PontoAPonto.Domain.Interfaces.Config;
using PontoAPonto.Domain.Models.Configs;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Data.Contexts
{
    public class UserContext : DbContext
    {
        private readonly KeysConfig _keyConfig;

        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        public UserContext(DbContextOptions<UserContext> opt, KeysConfig keyConfig) : base(opt)
        {
            _keyConfig = keyConfig;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IEntityConfig).IsAssignableFrom(x) && !x.IsAbstract).ToList();

            foreach (var type in typesToRegister)
            {
                if (type == null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                dynamic configurationInstance = Activator.CreateInstance(type)
                    ?? throw new InvalidOperationException($"Cannot create an instance of type {type.FullName}");
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
