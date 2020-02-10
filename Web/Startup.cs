using Core.Interfaces;
using Core.Services;
using Infrastrucure.Models;
using Infrastrucure.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web
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
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IAuthentication, AuthenticationService>();


            services.AddDbContext<TicketDbContext>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                            .AddCookie(opt =>
                            {
                                opt.Cookie.Name = $"TMS-POC";
                                opt.AccessDeniedPath = "/AccessDenied";
                                opt.LoginPath = "/Account/Login/";
                                opt.LogoutPath = "/Account/Logout/";
                            });


            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.Name = $"TMS-TempData";
            });
            //Configure Cookies
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
