using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Auction.Application;
using Auction.Application.BrandServices;
using Auction.Application.CategoryServices;
using Auction.Application.CityService;
using Auction.Application.ProductServices;
using Auction.Application.Shared;
using Auction.Application.SubCategoryServices;
using Auction.Application.SubCategoryServices.Dtos;
using Auction.Domain.Identity;
using Auction.EntityFramework.Context;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Auction.WebUI
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            /**********************CONNECTION STRING*****************************/
            services.AddDbContext<ApplicationUserDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("AuctionConnection")
                ));
            /**********************CONNECTION STRING*****************************/



            /**********************SERVICE SCOOPS*****************************/
            services.AddScoped<ICategoryservice, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICityService, CityService>();
            /**********************sERVICE SCOPS*****************************/



            /************************MAPPER*****************************/
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
                
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            /****************MAPPER*****************************/



            /****************FİLE PROVİDER*****************************/
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images")
            )
            );
            /****************FİLE PROVİDER*****************************/



            services.AddIdentity<ApplicationUser, IdentityRole>()
           .AddEntityFrameworkStores<ApplicationUserDbContext>();

            services
                .ConfigureApplicationCookie(options =>
                options.LoginPath = "/Account/Login");

            // kullanici olusturulurken kullanilacak kurallar

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 1;
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Manage",
                    template: "{area:exists}/{controller=Manage}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "Admin",
                    template: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "Category",
                    template: "{area:exists}/{controller=Category}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "SubCategory",
                    template: "{area:exists}/{controller=SubCategory}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "Brand",
                    template: "{area:exists}/{controller=Brand}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "Product",
                    template: "{area:exists}/{controller=Product}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
