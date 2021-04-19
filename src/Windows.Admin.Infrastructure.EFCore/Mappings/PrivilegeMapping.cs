using Windows.Admin.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Windows.Admin.Infrastructure.EFCore.Mappings
{
    public class PrivilegeMapping : IEntityTypeConfiguration<Privilege>
    {
        public void Configure(EntityTypeBuilder<Privilege> builder)
        {
            builder.Property(x => x.Master).IsRequired().HasMaxLength(50);
            builder.Property(x => x.MasterValue).IsRequired().HasColumnType("nvarchar(50)"); ;
            builder.Property(x => x.Access).IsRequired().HasMaxLength(50);
            builder.Property(x => x.AccessValue).IsRequired().HasColumnType("nvarchar(50)"); ;
            builder.Property(x => x.Operation).IsRequired();
        }
    }
}
