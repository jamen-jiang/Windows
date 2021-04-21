using Microsoft.EntityFrameworkCore;
using Windows.Infrastructure.EFCore;
using Windows.Module.Domain;

namespace Windows.Module.Infrastructure.EFCore
{
    public partial class ModuleDbContext : WindowsDbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string dbConnectionString="Data Source=.;Initial Catalog=System;User ID=sa;Password=19911004";
        //    // 定义要使用的数据库
        //    optionsBuilder.UseSqlServer(dbConnectionString);
        //}

        public ModuleDbContext(DbContextOptions<ModuleDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Domain.Module> Module { get; set; }
        public virtual DbSet<Operate> Operate { get; set; }
    }
}
