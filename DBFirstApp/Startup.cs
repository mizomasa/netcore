using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBFirstApp.Models;
using DBFirstApp.Service;
using DBFirstApp.Service.Employees.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DBFirstApp
{
    public class Startup
    {
        public static readonly ILoggerFactory MyLoggerFactory
                = LoggerFactory.Create(builder =>
                {
                    builder
                        .AddFilter((category, level) =>
                            category == DbLoggerCategory.Database.Command.Name
                            && level == LogLevel.Information)
                        .AddConsole();
                });

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<DBFirstApp.Models.MydatabaseContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MvcMovieContext"));
                options.UseLoggerFactory(MyLoggerFactory);
            });


            services.AddScoped<ICreateEmployeeService, CreateEmployeeService>();
            services.AddScoped<IEmployeeListService, EmployeeListService>();

            services.AddScoped<IEmployeeSelfReferenceService, EmployeeSelfReferenceService>();
            services.AddScoped<IEmployeeFamilyReferenceService, EmployeeFamilyReferenceService>();

            services.AddScoped<IEmployeeDetailReferenceService, EmployeeDetailReferenceService>();

            services.AddScoped< IEmployeeFamilySaveService, EmployeeFamilySaveService>();
            services.AddScoped<IEmployeeSelfSaveService, EmployeeSelfSaveService>();


            services.AddScoped<IEmployeeService, EmployeeService>();


            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IHumamRepository, HumanReposiotry>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
