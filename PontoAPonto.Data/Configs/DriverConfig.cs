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

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasIndex(u => u.Phone).IsUnique();

            builder.HasIndex(u => u.Cpf).IsUnique();

            builder.Property(x => x.Birthday).IsRequired();

            builder.Property(x => x.UrlProfilePicute)
                .HasMaxLength(500);

            builder.Property(u => u.IsFirstAccess)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(u => u.Status)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(x => x.PasswordSalt).IsRequired();

            builder.Property(x => x.PasswordHash).IsRequired();

            builder.Property(x => x.Reputation)
                .IsRequired().
                HasMaxLength(5);

            builder.Property(x => x.Approved)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(x => x.ApprovedAt);

            builder.OwnsOne(u => u.Otp)
                .Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(4);

            builder.OwnsOne(u => u.Otp)
                .Property(p => p.Expiracy)
                .IsRequired();

            builder.OwnsOne(u => u.Otp)
                .Property(p => p.Attempts)
                .IsRequired()
                .HasMaxLength(1);

            builder.OwnsOne(u => u.Otp).
                Property(p => p.IsVerified)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(x => x.PasswordResetToken);

            builder.Property(x => x.ResetTokenExpiracy);

            builder.OwnsOne(u => u.CarInfo)
                .Property(c => c.Renavam)
                .HasMaxLength(11);

            builder.OwnsOne(u => u.CarInfo)
                .Property(c => c.Plate)
                .HasMaxLength(7);

            builder.OwnsOne(u => u.CarInfo)
                .Property(c => c.FabricationYear)
                .HasMaxLength(4);

            builder.OwnsOne(u => u.CarInfo)
                .Property(c => c.ModelYear)
                .HasMaxLength(4);

            builder.OwnsOne(u => u.CarInfo)
                .Property(c => c.Brand)
                .HasMaxLength(100);

            builder.OwnsOne(u => u.CarInfo)
                .Property(c => c.Model)
                .HasMaxLength(100);

            builder.OwnsOne(u => u.CarInfo)
                .Property(c => c.Version)
                .HasMaxLength(100);

            builder.OwnsOne(u => u.CarInfo)
                .Property(c => c.Type)
                .HasMaxLength(50);

            builder.OwnsOne(u => u.CarInfo)
                .Property(c => c.Color)
                .HasMaxLength(50);

            builder.OwnsOne(u => u.CarInfo)
                .Property(c => c.Category)
                .HasMaxLength(50);

            builder.OwnsOne(u => u.CarInfo)
                .Property(c => c.Location)
                .HasMaxLength(255);

            builder.OwnsOne(u => u.CarInfo)
                .Property(c => c.OwnerCpfCnpj)
                .HasMaxLength(14);

            builder.OwnsOne(u => u.Location)
                .Property(c => c.Country)
                .HasMaxLength(255);

            builder.OwnsOne(u => u.Location)
                .Property(c => c.State)
                .HasMaxLength(255);

            builder.OwnsOne(u => u.Location)
                .Property(c => c.City)
                .HasMaxLength(255);
        }
    }
}
