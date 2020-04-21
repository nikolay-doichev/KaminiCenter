namespace KaminiCenter.Services.Data.Tests.Common.Seeder
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Data;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Data.Repositories;

    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.ProductService;

    public class DbContextTestsSeeder
    {
        public async Task<int> SeedGroupAsync(ApplicationDbContext context)
        {
            var groups = new Product_Group[]
            {
                new Product_Group() { Id = "abc1", GroupName = Enum.Parse<GroupType>("Fireplace") },
                new Product_Group() { Id = "abc2", GroupName = Enum.Parse<GroupType>("Finished_Models") },
                new Product_Group() { Id = "abc3", GroupName = Enum.Parse<GroupType>("Project") },
                new Product_Group() { Id = "abc4", GroupName = Enum.Parse<GroupType>("Accessories") },
            };

            foreach (var group in groups)
            {
                await context.Product_Groups.AddAsync(group);
            }

            await context.SaveChangesAsync();

            return groups.Length;
        }

        public async Task<string> SeedUsersAsync(ApplicationDbContext context)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Nikolay",
                LastName = "Doychev",
                Email = "nikolay.doichev@gmail.com",
                EmailConfirmed = true,
            };

            context.Users.Add(user);

            await context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> SeedProdcutAsync(ApplicationDbContext context)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Nikolay",
                LastName = "Doychev",
                Email = "nikolay.doichev@gmail.com",
                EmailConfirmed = true,
            };

            var groups = new Product_Group[]
            {
                new Product_Group() { Id = "abc1", GroupName = Enum.Parse<GroupType>("Fireplace") },
                new Product_Group() { Id = "abc2", GroupName = Enum.Parse<GroupType>("Finished_Models") },
                new Product_Group() { Id = "abc3", GroupName = Enum.Parse<GroupType>("Project") },
                new Product_Group() { Id = "abc4", GroupName = Enum.Parse<GroupType>("Accessories") },
            };

            var products = new Product[]
            {
                new Product()
                {
                    Id = "abc",
                    CreatedOn = DateTime.UtcNow,
                    GroupId = groups[0].Id,
                    Name = "Гк Мая",
                    UserId = user.Id,
                },

                new Product()
                {
                    Id = "abc2",
                    CreatedOn = DateTime.UtcNow,
                    GroupId = groups[0].Id,
                    Name = "Гк Оливия",
                    UserId = user.Id,
                },
            };

            foreach (var product in products)
            {
                await context.Products.AddAsync(product);
            }

            await context.SaveChangesAsync();

            return groups.Length;
        }

        public async Task<int> SeedFireplacesAsync(ApplicationDbContext context)
        {
            var user = new ApplicationUser
            {
                Id = "abc",
                FirstName = "Nikolay",
                LastName = "Doychev",
                Email = "nikolay.doichev@gmail.com",
                EmailConfirmed = true,
            };

            var groups = new Product_Group[]
            {
                new Product_Group() { Id = "abc1", GroupName = Enum.Parse<GroupType>("Fireplace") },
                new Product_Group() { Id = "abc2", GroupName = Enum.Parse<GroupType>("Finished_Models") },
                new Product_Group() { Id = "abc3", GroupName = Enum.Parse<GroupType>("Project") },
                new Product_Group() { Id = "abc4", GroupName = Enum.Parse<GroupType>("Accessories") },
            };

            var products = new Product[]
            {
                new Product()
                {
                    Id = "abc",
                    CreatedOn = DateTime.UtcNow,
                    GroupId = groups[0].Id,
                    Name = "Гк Мая",
                    UserId = user.Id,
                },

                new Product()
                {
                    Id = "abc2",
                    CreatedOn = DateTime.UtcNow,
                    GroupId = groups[0].Id,
                    Name = "Гк Оливия",
                    UserId = user.Id,
                },
            };

            var fireplaces = new List<Fireplace_chamber>
            {
                new Fireplace_chamber
                {
                    Id = "abc1",
                    Power = "10w",
                    Chimney = "200Ф",
                    CreatedOn = DateTime.UtcNow,
                    Description = "Some description test 1",
                    GroupId = groups[0].Id,
                    ImagePath = "https://cdn11.bigcommerce.com/s-j2bzz1q4/images/stencil/1280x1280/products/206/4809/Biltmore-Wood-Burning-Fireplace-2-main__66102.1541203308.jpg?c=2&imbypass=on",
                    Price = 1800.00M,
                    Size = "60 / 40 / h50",
                    TypeOfChamber = TypeOfChamber.Basic,
                    ProductId = products[0].Id,
                },
                new Fireplace_chamber
                {
                    Id = Guid.NewGuid().ToString(),
                    Power = "15w",
                    Chimney = "200Ф",
                    CreatedOn = DateTime.UtcNow,
                    Description = "Some description test 2",
                    GroupId = groups[0].Id,
                    ImagePath = "https://cdn11.bigcommerce.com/s-j2bzz1q4/images/stencil/1280x1280/products/206/4809/Biltmore-Wood-Burning-Fireplace-2-main__66102.1541203308.jpg?c=2&imbypass=on",
                    Price = 1200.00M,
                    Size = "40 / 20 / h30",
                    TypeOfChamber = TypeOfChamber.Basic,
                    ProductId = products[1].Id,
                },
            };

            foreach (var fireplace in fireplaces)
            {
                await context.Fireplace_Chambers.AddAsync(fireplace);
            }

            await context.SaveChangesAsync();

            return fireplaces.Count;
        }
    }
}
