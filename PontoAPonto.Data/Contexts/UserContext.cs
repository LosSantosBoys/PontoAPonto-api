using Microsoft.EntityFrameworkCore;
using PontoAPonto.Domain.Models.Configs;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Data.Contexts
{
    public class UserContext : DbContext
    {
        private readonly KeysConfig _keyConfig;

        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> opt, KeysConfig keyConfig) : base(opt)
        {
            _keyConfig = keyConfig;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseMySQL(_keyConfig.DefaultConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasIndex(u => u.Email)
                    .IsUnique();

                entity.HasIndex(u => u.Phone)
                    .IsUnique();

                entity.HasIndex(u => u.Cpf)
                    .IsUnique();

                entity.OwnsOne(u => u.Otp,
                    optBuilder =>
                    {
                        optBuilder.ToTable("Users");
                    });
            });
        }
    }
}
