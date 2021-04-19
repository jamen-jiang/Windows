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
            //���ocelot����
            services.AddOcelot(Configuration)
            //���Consul֧��
            .AddConsul()
            //��ӻ���
            .AddCacheManager(x => x.WithDictionaryHandle())
            //��ʱ/�۶�
            //��ʱ���������������ʱ�����̵����Ӧʱ�䡣�۶ϵ���˼���ǵ�����ĳ��������쳣�����ﵽһ����ʱ����ô������һ��ʱ���ھͲ��ٶ���������������ˣ�ֱ���۶ϡ�
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
