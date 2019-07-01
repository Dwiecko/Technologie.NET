using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DeliverySystem.Data;
using DeliverySystem.Models;
using DeliverySystem.Repositories;
using DeliverySystem.Repository;
using Autofac;
using DeliverySystem.Repository.Doubles;

namespace DeliverySystem
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ApplicationDbInitializer>();
            services.AddTransient<IDeliveriesRepository, DeliveriesRepository>();
            services.AddTransient<ICategoriesRepository, FakeCategoryRepository>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));


            services.AddMvc();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
            app.UseStaticFiles();
            app.UseAuthentication();

            dbInitializer.Seed();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
