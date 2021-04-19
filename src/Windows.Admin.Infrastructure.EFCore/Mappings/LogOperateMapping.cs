using Windows.Admin.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Windows.Admin.Infrastructure.EFCore.Mappings
{
    public class LogOperateMapping : IEntityTypeConfiguration<LogOperate>
    {
        public void Configure(EntityTypeBuilder<LogOperate> builder)
        {
            builder.Property(x => x.Type).HasMaxLength(50);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LogOn).IsRequired();
            builder.Property(x => x.IP).HasMaxLength(200);
            builder.Property(x => x.City).HasMaxLength(50);
            builder.Property(x => x.UserAgent).HasMaxLength(500);
            builder.Property(x => x.Browser).HasMaxLength(200);
            builder.Property(x => x.Os).HasMaxLength(200);
            builder.Property(x => x.ApiName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ApiUrl).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ApiMethod).HasMaxLength(50);
        }
    }
}
