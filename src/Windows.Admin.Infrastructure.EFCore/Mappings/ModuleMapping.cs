using Windows.Admin.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Windows.Admin.Infrastructure.EFCore.Mappings
{
    public class ModuleMapping : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.HasQueryFilter(x => x.IsEnable);
            builder.Property(x => x.PId).IsRequired(false);
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Controller).HasMaxLength(50);
            builder.Property(x => x.Icon).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.Sort).IsRequired(false);
            builder.Property(x => x.Uri).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.Remark).IsRequired(false).HasMaxLength(500);
        }
    }
}
