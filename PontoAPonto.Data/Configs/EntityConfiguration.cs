﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Data.Configs
{
    public class EntityConfiguration<T> where T : EntityBase
    {
        public void DefaultConfigs(EntityTypeBuilder<T> builder, string tableName)
        {
            builder.ToTable(tableName);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired();
        }
    }
}
