using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Windows.Admin.Domain;

namespace Windows.Admin.Infrastructure.EFCore.Mappings
{
    public class OperateMapping : IEntityTypeConfiguration<Operate>
    {
        public void Configure(EntityTypeBuilder<Operate> builder)
        {
            builder.HasQueryFilter(x => x.IsEnable);
            builder.Property(x => x.ModuleId).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Action).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Icon).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.Sort).IsRequired(false);
            builder.Property(x => x.Remark).IsRequired(false).HasMaxLength(500);
        }
    }
}
