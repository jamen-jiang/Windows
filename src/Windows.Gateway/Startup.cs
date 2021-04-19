using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Cache.CacheManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Gateway.Extensions;
using Ocelot.Provider.Polly;
using Windows.Infrastructure.Utils;

namespace Windows.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //添加ocelot服务
            services.AddOcelot(Configuration)
            //添加Consul支持
            .AddConsul()
            //添加缓存
            .AddCacheManager(x => x.WithDictionaryHandle())
            //超时/熔断
            //超时就是网关请求服务时可容忍的最长响应时间。熔断的意思就是当请求某个服务的异常次数达到一定量时，那么网关在一定时间内就不再对这个服务发起请求了，直接熔断。
            .AddPolly();
            services.AddHttpContextAccessor();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            app.UseOcelot().Wait();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // register this service
            ServiceEntity serviceEntity = new ServiceEntity
            {
                IP = NetworkUtils.LocalIPAddress,
                Port = Convert.ToInt32(Configuration["Service:Port"]),
                ServiceName = Configuration["Service:Name"],
                ConsulIP = Configuration["Consul:IP"],
                ConsulPort = Convert.ToInt32(Configuration["Consul:Port"])
            };
            app.RegisterConsul(lifetime, serviceEntity);
        }
    }
}
