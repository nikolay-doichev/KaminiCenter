namespace KaminiCenter.Web
{
    using System.Reflection;

    using AutoMapper;
    using CloudinaryDotNet;
    using KaminiCenter.Data;
    using KaminiCenter.Data.Common;
    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Repositories;
    using KaminiCenter.Data.Seeding;
    using KaminiCenter.Services;
    using KaminiCenter.Services.Data;
    using KaminiCenter.Services.Data.AccessoriesService;
    using KaminiCenter.Services.Data.CommentServices;
    using KaminiCenter.Services.Data.FinishedModelService;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Services.Data.ForUs;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Data.ProjectsService;
    using KaminiCenter.Services.Data.SuggestProduct;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Services.Messaging;
    using KaminiCenter.Web.ViewModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            services.AddRazorPages();

            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            services.AddSingleton(this.configuration);

            // Cloudinary
            Account account = new Account(
                this.configuration["Cloudinary:CloudName"],
                this.configuration["Cloudinary:ApiKey"],
                this.configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(account);
            services.AddSingleton(cloudinary);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(
               serviceProvider => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
            services.AddTransient<ISettingsService, SettingsService>();

            // Add AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // My Custom Services
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IFireplaceService, FireplaceService>();
            services.AddTransient<IEnumParseService, EnumParseService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IFinishedModelService, FinishedModelService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IAccessoriesService, AccessoriesService>();
            services.AddTransient<IForUsService, ForUsService>();
            services.AddTransient<ISuggestProduct, SuggestProdcut>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder(this.configuration).SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithRedirects("/Home/HttpError?statusCode={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                        endpoints.MapControllerRoute("fireplaceDetails", "{controller=Fireplace}/{action=Details}/{name:minlength(3)}", new { control = "Fireplace", action = "Details" });
                        endpoints.MapControllerRoute("finishedModelsDetails", "{controller=FinishedModels}/{action=Details}/{name:minlength(3)}", new { control = "FinishedModels", action = "Details" });
                        endpoints.MapControllerRoute("accessorieDetails", "{controller=Accessorie}/{action=Details}/{name:minlength(3)}", new { control = "Accessorie", action = "Details" });

                        endpoints.MapControllerRoute("accessorieEdit", "{controller=Accessorie}/{action=Edit}/{id:minlength(3)}", new { control = "Accessorie", action = "Edit" });

                        endpoints.MapControllerRoute("fireplaceAllWithPage", "{controller=Fireplace}/{action=All}/{type:minlength(3)}/{page?}", new { control = "Fireplace", action = "All" });
                        endpoints.MapControllerRoute("fireplaceAllWithPageAndNoParameter", "{controller=Fireplace}/{action=AllWithoutParameter}/{page?}", new { control = "Fireplace", action = "AllWithoutParameter" });
                        endpoints.MapControllerRoute("finishedModelAllWithPage", "{controller=FinishedModels}/{action=All}/{type:minlength(3)}/{page?}", new { control = "FinishedModels", action = "All" });
                        endpoints.MapControllerRoute("projectWithPage", "{controller=Project}/{action=All}/{typeLocation:minlength(3)}/{type:minlength(3)}/{page?}", new { control = "Project", action = "All" });
                        endpoints.MapControllerRoute("accessorieAllWithPage", "{controller=Accessorie}/{action=All}/{page?}", new { control = "Accessorie", action = "All" });

                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

                        endpoints.MapRazorPages();
                    });
        }
    }
}
