using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Windows.Admin.Domain;

namespace Windows.Admin.Infrastructure.EFCore.Mappings
{
    public class DictionaryMapping : IEntityTypeConfiguration<Dictionary>
    {
        public void Configure(EntityTypeBuilder<Dictionary> builder)
        {
            builder.HasQueryFilter(x => x.IsEnable);
            builder.Property(x => x.Category).HasMaxLength(50);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Code).HasMaxLength(50);
            builder.Property(x => x.Value).HasMaxLength(50);
            builder.Property(x => x.Remark).HasMaxLength(500);
        }
    }
}
