using Windows.Admin.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Windows.Admin.Infrastructure.EFCore.Mappings
{
    public class OrganizationMapping : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasQueryFilter(x => x.IsEnable);
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Telephone).HasMaxLength(50);
            builder.Property(x => x.Fax).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(50);
            builder.Property(x => x.Remark).IsRequired(false).HasMaxLength(500);
        }
    }
}
