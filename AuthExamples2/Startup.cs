using AuthExamples2.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthExamples2
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
            services.AddDbContext<MyDbContext>(
                config =>
                {
                    config.UseInMemoryDatabase("inMemoryDatabase");
                }
            );

            //Registers services
            services
                .AddIdentity<IdentityUser, IdentityRole>(
                    config =>
                    {
                        config.Password.RequireDigit = false;
                        config.Password.RequiredLength = 4;
                        config.Password.RequireNonAlphanumeric = false;
                        config.Password.RequireUppercase = false;
                    }
                )
                .AddEntityFrameworkStores<MyDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(
                config =>
                {
                    config.Cookie.Name = "Identity.Cookie";
                    config.LoginPath = "/home/login";
                }
            );
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //who are you
            app.UseAuthentication();
            //are you allowed
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                }
            );
        }
    }
}
