using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Windows.Api.Shared.Extensions
{
    public class AppSettingsBase
    {
        //public static IConfiguration Configuration { get; private set; }
        //public static ServiceProvider Provider { get; private set; }
        //public static string CurrentPath { get; private set; } = null;

        //public static void Init(IServiceCollection services, IWebHostEnvironment env)
        //{
        //    CurrentPath = env.ContentRootPath;
        //    Configuration = new ConfigurationBuilder()
        //       .SetBasePath(CurrentPath)
        //       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)//这样的话，可以直接读目录里的json文件，而不是 bin 文件夹下的，所以不用修改复制属性
        //       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
        //       .Build();
        //    Provider = services.BuildServiceProvider();

        //    Type appSettings = this.GetType();
        //    PropertyInfo[] pis = appSettings.GetProperties();
        //    foreach (PropertyInfo pi in pis)
        //    {
        //        var obj = Configuration.GetSection(pi.Name).Get(pi.PropertyType);
        //        if (obj != null)
        //            pi.SetValue(appSettings, obj);
        //    }
        //}
        
    }
}
