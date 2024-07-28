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

            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasIndex(u => u.Phone).IsUnique();

            builder.HasIndex(u => u.Cpf).IsUnique();

            builder.OwnsOne(u => u.Otp,
                optBuilder =>
                {
                    optBuilder.ToTable("Users");
                });
        
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.IsFirstAccess)
                .IsRequired();

            builder.Property(u => u.Status)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.PasswordSalt)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.Property(x => x.PasswordResetToken);

            builder.Property(x => x.ResetTokenExpiracy);

            builder.Property(x => x.Birthday)
                .IsRequired();
        }
    }
}
