using Windows.Admin.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Windows.Admin.Infrastructure.EFCore.Mappings
{
    public class FileMapping : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.HasQueryFilter(x => x.IsEnable);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Extension).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Path).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Md5).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Remark).HasMaxLength(500);
        }
    }
}
