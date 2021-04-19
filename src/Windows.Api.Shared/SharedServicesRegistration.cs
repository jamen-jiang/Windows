using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.Infrastructure.EFCore;

namespace Windows.Api.Shared
{
    public abstract class SharedServicesRegistration
    {
        protected readonly IConfiguration _configuration;
        protected readonly IServiceCollection _services;
        protected readonly IWebHostEnvironment _environment;
        /// <summary>
        /// 服务注册与系统配置
        /// </summary>
        /// <param name="cfg"><see cref="IConfiguration"/></param>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="env"><see cref="IWebHostEnvironment"/></param>
        /// <param name="serviceInfo"><see cref="ServiceInfo"/></param>
        public SharedServicesRegistration(IConfiguration cfg, IServiceCollection services, IWebHostEnvironment env)
        {
            _configuration = cfg;
            _services = services;
            _environment = env;
        }
        /// <summary>
        /// 注册EfcoreContext
        /// </summary>
        public virtual void AddEfCoreContext()
        {
            //_services.AddDbContext<WindowsContext>(options =>
            //{
            //    options.UseSqlServer(_mysqlConfig.ConnectionString, mySqlOptions =>
            //    {
            //        mySqlOptions.ServerVersion(new ServerVersion(new Version(10, 5, 4), ServerType.MariaDb));
            //        mySqlOptions.MinBatchSize(2);
            //        mySqlOptions.MigrationsAssembly(_serviceInfo.AssemblyName.Replace("WebApi", "Migrations"));
            //        mySqlOptions.CharSet(CharSet.Utf8Mb4);
            //    });

            //    if (_env.IsDevelopment())
            //    {
            //        options.EnableSensitiveDataLogging();
            //        options.EnableDetailedErrors();
            //    }

            //    //替换默认查询sql生成器,如果通过mycat中间件实现读写分离需要替换默认SQL工厂。
            //    //options.ReplaceService<IQuerySqlGeneratorFactory, AdncMySqlQuerySqlGeneratorFactory>();
            //});
        }
        /// <summary>
        /// 注册跨域组件
        /// </summary>
        public virtual void AddCors()
        {
            //_services.AddCors(options =>
            //{
            //    var _corsHosts = _configuration.GetAllowCorsHosts().Split(",", StringSplitOptions.RemoveEmptyEntries);
            //    options.AddPolicy(_serviceInfo.CorsPolicy, policy =>
            //    {
            //        policy.WithOrigins(_corsHosts)
            //        .AllowAnyHeader()
            //        .AllowAnyMethod()
            //        .AllowCredentials();
            //    });
            //});
        }
    }
}
