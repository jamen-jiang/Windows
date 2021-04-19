using Microsoft.EntityFrameworkCore;
using Windows.Infrastructure.EFCore.Extensions;

namespace Windows.Infrastructure.EFCore
{
    public class WindowsDbContext: DbContext
    {
        public WindowsDbContext(DbContextOptions options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //关闭缓存，每次都会调用OnModelCreating
        //    //用于设置是否启用缓存
        //    //optionsBuilder.EnableServiceProviderCaching(false);
        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //自动映射(免去每加一个mapping就要加一个)
            modelBuilder.ApplyAllConfigurations();
        }
    }
}
