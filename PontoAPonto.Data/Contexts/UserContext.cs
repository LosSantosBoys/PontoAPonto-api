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

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(u => u.Phone)
                    .IsRequired()
                    .HasMaxLength(13);

                entity.Property(u => u.Cpf)
                    .HasMaxLength(11);

                entity.OwnsOne(u => u.Otp,
                    optBuilder =>
                    {
                        optBuilder.ToTable("Users");
                    });
            });
        }
    }
}
