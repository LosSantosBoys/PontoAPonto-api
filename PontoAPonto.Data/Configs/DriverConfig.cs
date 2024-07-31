using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PontoAPonto.Domain.Interfaces.Config;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Data.Configs
{
    public class DriverConfig : EntityConfiguration<Driver>, IEntityTypeConfiguration<Driver>, IEntityConfig
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            DefaultConfigs(builder, tableName: "Drivers");

            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasIndex(u => u.Phone).IsUnique();

            builder.HasIndex(u => u.Cpf).IsUnique();

            builder.OwnsOne(u => u.Otp,
                optBuilder =>
                {
                    optBuilder.ToTable("Drivers");
                });

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.IsFirstAccess)
                .IsRequired();

            builder.Property(u => u.Status)
                .IsRequired();

            builder.Property(x => x.PasswordSalt)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.Property(x => x.PasswordResetToken);

            builder.Property(x => x.ResetTokenExpiracy);

            builder.Property(x => x.Birthday)
                .IsRequired();

            builder.Property(x => x.Reputation).IsRequired();

            builder.OwnsOne(u => u.CarInfo,
            optBuilder =>
            {
                optBuilder.ToTable("Drivers");
            });

            builder.Property(x => x.Approved).IsRequired();

            builder.Property(x => x.ApprovedAt);
        }
    }
}
