using Microsoft.EntityFrameworkCore;
using Windows.Infrastructure.EFCore;
using Windows.Privilege.Domain;

namespace Windows.Privilege.Infrastructure.EFCore
{
    public partial class PrivilegeDbContext : WindowsDbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string dbConnectionString="Data Source=.;Initial Catalog=System;User ID=sa;Password=19911004";
        //    // 定义要使用的数据库
        //    optionsBuilder.UseSqlServer(dbConnectionString);
        //}

        public PrivilegeDbContext(DbContextOptions<PrivilegeDbContext> options) : base(options)
        {
        }

      
        public virtual DbSet<Domain.Privilege> Privilege { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Role_Relation> Role_Relation { get; set; }
    }
}
