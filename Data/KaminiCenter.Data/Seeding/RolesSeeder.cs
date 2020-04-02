namespace KaminiCenter.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using KaminiCenter.Common;
    using KaminiCenter.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        private readonly IConfiguration configuration;

        public RolesSeeder(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedRoleAsync(roleManager, userManager, GlobalConstants.AdministratorRoleName, this.configuration);
            await SeedRoleAsync(roleManager, userManager, GlobalConstants.UserRoleName, this.configuration);
        }

        private static async Task SeedRoleAsync(
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            string roleName,
            IConfiguration configuration)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }

            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    FirstName = configuration["Admin:FirstName"],
                    LastName = configuration["Admin:LastName"],
                    UserName = configuration["Admin:UserName"],
                    Email = configuration["Admin:Email"],
                    EmailConfirmed = true,
                };

                var password = configuration["Admin:Password"];

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                }
            }
        }
    }
}
