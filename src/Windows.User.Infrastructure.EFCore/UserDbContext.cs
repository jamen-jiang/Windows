using Microsoft.EntityFrameworkCore;
using Windows.Infrastructure.EFCore;

namespace Windows.User.Infrastructure.EFCore
{
    public partial class UserDbContext : WindowsDbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string dbConnectionString="Data Source=.;Initial Catalog=System;User ID=sa;Password=19911004";
        //    // 定义要使用的数据库
        //    optionsBuilder.UseSqlServer(dbConnectionString);
        //}

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Domain.User> User { get; set; }
 
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
