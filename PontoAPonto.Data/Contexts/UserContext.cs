using Microsoft.EntityFrameworkCore;
using PontoAPonto.Domain.Interfaces.Infra;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Data.Contexts
{
    public class UserContext : DbContext
    {
        private readonly IConnStringProvider _connStringProvider;
        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> opt, IConnStringProvider connStringProvider) : base(opt)
        {
            _connStringProvider = connStringProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connStringProvider.ConnectionString);
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
