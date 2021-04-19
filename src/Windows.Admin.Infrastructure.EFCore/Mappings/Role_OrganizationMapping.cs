using Windows.Admin.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Windows.Admin.Infrastructure.EFCore.Mappings
{
    public class Role_OrganizationMapping : IEntityTypeConfiguration<Role_Organization>
    {
        public void Configure(EntityTypeBuilder<Role_Organization> builder)
        {
            builder.Property(x => x.RoleId).IsRequired() ;
            builder.Property(x => x.OrganizationId).IsRequired() ;
        }
    }
}
