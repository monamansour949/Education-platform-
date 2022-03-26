using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.SignalR;
using ProjectItiTeam.Data;
using ProjectItiTeam.Models.Identity.Services;
using ProjectItiTeam.Models.Identity;
using ProjectItiTeam.Models.Identity.Repositery;
using ProjectItiTeam.Repository;

namespace ProjectItiTeam
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
            // Settings to send email 
          
            // services.AddSignalR();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddRazorPages().AddRazorRuntimeCompilation();


            //services.AddIdentity<IdentityUser, IdentityRole>()
            //  .AddDefaultTokenProviders().AddDefaultUI()
            //  .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentity<IdentityUser, IdentityRole>(
             option =>
             {
                 option.Password.RequireUppercase = false;
                 option.Password.RequiredLength = 4;
                 option.Password.RequireDigit = false;
                 option.SignIn.RequireConfirmedAccount = false;
             })
            .AddDefaultTokenProviders().AddDefaultUI()
            .AddEntityFrameworkStores<ApplicationDbContext>();

          
            services.AddControllersWithViews();
           // services.AddSingleton<IEmailSender, EmailSender>();
           //  services.AdHttpContextAccessor();
            services.AddSession(
              options => {
                  options.Cookie.IsEssential = true;
                  options.IdleTimeout = TimeSpan.FromMinutes(10);
                  options.Cookie.HttpOnly = true;
              });

            services.AddScoped<IRepositery, RepositeryUser>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IBAox,basketRepo>();
            //services.AddTransient<IEmailSender, EmailSender>();
            services.AddAuthentication();

            services.AddAuthentication()
                .AddGoogle(options =>
               {
                   options.ClientId = "209335769669-srqi3f3kntddisdlfdq68p0ni3gqd4jf.apps.googleusercontent.com";
                   options.ClientSecret = "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";
               });
        }
        // dotnet user-secrets set "1w93753039045-9bi5alng6f249p163k5vobgvi8q3t82s.apps.googleusercontent.com"
        // dotnet user-secrets set "Authentication:Google:ClientSecret" "<client-secret>"

        // dotnet user-secrets set "1w93753039045-9bi5alng6f249p163k5vobgvi8q3t82s.apps.googleusercontent.com" "<client-id>"
        // dotnet user-secrets set "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" "<client-secret>"

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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
            // StripeConfiguration.ApiKey=Configuration.GetSection("Stripe")["Secretkey"];
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapHub("/NotificationHub");
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
