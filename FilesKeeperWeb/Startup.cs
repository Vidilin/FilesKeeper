using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FilesKeeperWeb.Services;
using Microsoft.AspNetCore.Http.Features;

namespace FilesKeeperWeb
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
            //services.Configure<FormOptions>(x =>
            //{
            //    x.ValueLengthLimit = 50 * 1024;
            //    x.MultipartBodyLengthLimit = 50 * 1024;
            //    x.MultipartHeadersLengthLimit = 50 * 1024;
            //});

            services.AddSingleton(serviceProvider =>
            {
                return new DataBase(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Files}/{action=Index}/{id?}");
            });
        }
    }
}
