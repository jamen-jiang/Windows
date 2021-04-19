using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Windows.Admin.Domain;

namespace Windows.Admin.Infrastructure.EFCore.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasQueryFilter(x => x.IsEnable);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PassWord).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.MobilePhone).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(50);
            builder.Property(x => x.Avatar).HasMaxLength(500);
            builder.Property(x => x.Remark).HasMaxLength(500);
        }
    }
}
