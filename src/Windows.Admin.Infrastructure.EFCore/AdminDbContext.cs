using Microsoft.EntityFrameworkCore;
using Windows.Admin.Domain;
using Windows.Infrastructure.EFCore;

namespace Windows.Admin.Infrastructure.EFCore
{
    public partial class AdminDbContext : WindowsDbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string dbConnectionString="Data Source=.;Initial Catalog=System;User ID=sa;Password=19911004";
        //    // 定义要使用的数据库
        //    optionsBuilder.UseSqlServer(dbConnectionString);
        //}

        public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Operate> Operate { get; set; }
        public virtual DbSet<Privilege> Privilege { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Role_User> Role_User { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<LogOperate> LogOperate { get; set; }
        public virtual DbSet<LogLogin> LogLogin { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Role_Organization> Role_Organization { get; set; }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<Dictionary> Dictionary { get; set; }
        public virtual DbSet<Organization_User> Organization_User { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    //自动映射
        //    modelBuilder.ApplyAllConfigurations();
        //    //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //    //{
        //    //    entityType.GetOrAddProperty("IsEnable", typeof(bool));
        //    //    var parameter = Expression.Parameter(entityType.ClrType);
        //    //    var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
        //    //    var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsEnable"));
        //    //    BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(true));
        //    //    var lambda = Expression.Lambda(compareExpression, parameter);
        //    //    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
        //    //}
        //}
    }
}
