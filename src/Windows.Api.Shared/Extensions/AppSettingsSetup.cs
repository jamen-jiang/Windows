using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Windows.Api.Shared.Extensions
{
    public static class AppSettingsSetup
    {
        public static void AddAppSettingsSetup<T>(this IServiceCollection services,IConfiguration configuration) 
        {
            Type appSettings = typeof(T);
            PropertyInfo[] pis = appSettings.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                var obj = configuration.GetSection(pi.Name).Get(pi.PropertyType);
                if (obj != null)
                    pi.SetValue(appSettings, obj);
            }
        }
    }
}
