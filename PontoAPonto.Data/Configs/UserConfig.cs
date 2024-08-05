using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoAPonto.Domain.Interfaces.Config;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Data.Configs
{
    public class UserConfig : EntityConfiguration<User>, IEntityTypeConfiguration<User>, IEntityConfig
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            DefaultConfigs(builder, tableName: "Users");

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasIndex(u => u.Phone).IsUnique();

            builder.HasIndex(u => u.Cpf).IsUnique();

            builder.Property(x => x.Birthday).IsRequired();

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

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.PasswordResetToken);

            builder.Property(x => x.ResetTokenExpiracy);
        }
    }
}