using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sistema2020.Models;
using Sistema2020.Data;
using System.Threading.Tasks;
using Sistema2020.Services;

namespace Sistema2020
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

            services.AddDbContext<Sistema2020Context>(options =>
            options.UseMySql(Configuration.GetConnectionString("Sistema2020Context")));

            services.AddDbContext<IdentityContext>(options =>
              options.UseMySql(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<Usuario, IdentityRole>(
                 config =>
                 {
                     config.Password.RequiredLength = 6;
                     config.Password.RequireDigit = false;
                     config.Password.RequireLowercase = false;
                     config.Password.RequireUppercase = false;
                     config.Password.RequireNonAlphanumeric = false;
                 }).
                 AddEntityFrameworkStores<IdentityContext>().
                 AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity.Cookie";
                config.LoginPath = "/Home/Login";
            });

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddScoped<SeedingService>();
            services.AddScoped<PaisesService>();
            services.AddScoped<EstadosService>();
            services.AddScoped<MunicipiosService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService ss)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                ss.Seed();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
