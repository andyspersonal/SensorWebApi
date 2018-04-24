using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SensorWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace SensorWebApi
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
            
            if(Program.Configuration["useInMemoryStore"] == "True")
            {
                Console.WriteLine("Using in mmory database");
                services.AddDbContext<TemperatureReadingContext>(opt => opt.UseInMemoryDatabase("InMem"));
            }
            else
            {
            
                Console.WriteLine("Using my sql database with connection string");
                string connectionstring = Program.Configuration["ConnectionString"];
                //connectionstring = "server=piserver;port=3306;database=temperaturereading;uid=andy;password=BraeCottage";
                Console.WriteLine(connectionstring);
                services.AddDbContext<TemperatureReadingContext>(opt => opt.UseMySql(connectionstring));
            }
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            //app.UseMvcWithDefaultRoute();
            //app.UseMvc(routes =>
            //{
              //  routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //});
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
