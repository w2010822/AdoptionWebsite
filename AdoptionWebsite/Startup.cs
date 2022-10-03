using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdoptionWebsite.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace AdoptionWebsite
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
            //�s�u�r��
            var connection = Configuration.GetConnectionString("AdoptionDB");
            services.AddDbContext<Animal_AdoptionContext>(option=>option.UseSqlServer(connection));
            services.AddControllersWithViews();

            services.AddControllersWithViews();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Login/Index";
                    });

            services.AddHttpContextAccessor();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Files")),
                RequestPath = new PathString("/Files")
            });

            app.UseRouting();

            app.UseAuthentication(); // ����
            app.UseAuthorization(); // ���v

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                // �Ҧ� Cookie.SamSite �]�m���|�Q���ɬ� Strict
                MinimumSameSitePolicy = SameSiteMode.Strict,
                // Cookie.SamSite �]�m�� None ���ܷ|�Q���ɬ� Lax
                //MinimumSameSitePolicy = SameSiteMode.Lax,  
                // MinimumSameSitePolicy �]�m���̼e�P�A�]�����|�v�T Cookie.SamSite
                //MinimumSameSitePolicy = SameSiteMode.None, 
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
