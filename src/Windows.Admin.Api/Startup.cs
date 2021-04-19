using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windows.Admin.Infrastructure.Configuration;
using Windows.Admin.Infrastructure.EFCore;
using Windows.Api.Shared;
using Windows.Api.Shared.Extensions;
using Windows.Infrastructure.EFCore;

namespace Windows.Api.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            ServiceInfo = ServiceInfo.Create(Assembly.GetExecutingAssembly());
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public ServiceInfo ServiceInfo { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //appsettings.json转AppSetting
            services.AddAppSettingsSetup<AppSetting>(Configuration);
            //注入DbContext
            services.AddDbContext<AdminDbContext>(option => option.UseSqlServer(AppSetting.Database.SqlServer.ConnectionString));
            services.AddControllers();
            //Api列表界面
            services.AddSwaggerGen(c =>
            {
                var openApiInfo = new OpenApiInfo { Title = ServiceInfo.ShortName, Version = ServiceInfo.Version };
                c.SwaggerDoc(openApiInfo.Version, openApiInfo);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint($"/{ServiceInfo.ShortName}/swagger/{ServiceInfo.Version}/swagger.json", $"{ServiceInfo.FullName}-{ServiceInfo.Version}"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
