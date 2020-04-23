using KaminiCenter.Services.Data.Tests.Common.Seeder;
using KaminiCenter.Services.Data.Tests.Extensions;

namespace KaminiCenter.Services.Data.Tests.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Data.Repositories;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.Tests.Common;
    using Xunit;

    public class ProductServiceTest
    {
        private const string ErrorMessage = "Method: \"{0}\", does not work properly!";

        [Fact]
        public async Task AddProductToDataBase_WithCorrectData_ShouldSuccessfullyAdd()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);

            var groupService = new GroupService(groupRepository);
            var productService = new ProductService(productRepository, groupService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedGroupAsync(context);

            // Act
            await productService.AddProductAsync("product", GroupType.Fireplace.ToString(), "abc1");

            var actual = context.Products.Count();

            // Assert
            Assert.True(actual == 1, string.Format(ErrorMessage, "ProductAdd"));
        }

        [Fact]
        public async Task FindProductIdBy_WithExistingNameAndGroup_ShouldSuccessfullyFind()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);

            var groupService = new GroupService(groupRepository);
            var productService = new ProductService(productRepository, groupService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);

            // Act
            var result = productService.GetIdByNameAndGroup("Гк Мая", GroupType.Fireplace.ToString());

            var actual = context.Products.SingleOrDefault(p => p.Name == "Гк Мая").Id;

            // Assert
            AssertExtension.EqualsWithMessage(actual, result, string.Format(ErrorMessage, "ProductGetIdByNameAndGroup"));
        }

        [Fact]
        public async Task FindProductIdBy_WithNoneExistingName_ShouldreturnNull()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);

            var groupService = new GroupService(groupRepository);
            var productService = new ProductService(productRepository, groupService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);

            // Act
            var result = productService.GetIdByNameAndGroup("Test Name", GroupType.Fireplace.ToString());

            // Assert
            AssertExtension.EqualsWithMessage(null, result, string.Format(ErrorMessage, "ProductGetIdByNameAndGroup return null"));
        }

        [Fact]
        public async Task FindProduct_WithExistingId_ShouldSuccessfullyFind()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);

            var groupService = new GroupService(groupRepository);
            var productService = new ProductService(productRepository, groupService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);

            // Act
            var result = productService.GetById("abc").Name;

            var actual = context.Products.SingleOrDefault(p => p.Id == "abc").Name;

            // Assert
            AssertExtension.EqualsWithMessage(actual, result, string.Format(ErrorMessage, "ProductGetIdByNameAndGroup"));
        }

        [Fact]
        public async Task FindProduct_WithNoneExistingId_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);

            var groupService = new GroupService(groupRepository);
            var productService = new ProductService(productRepository, groupService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => productService.GetById("Тестово id"));
        }

        [Fact]
        public async Task AddProductToDataBase_WithExistingName_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);

            var groupService = new GroupService(groupRepository);
            var productService = new ProductService(productRepository, groupService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => productService.AddProductAsync("Гк Мая", GroupType.Fireplace.ToString(), "userId"));
        }

        [Fact]
        public async Task AddProductToDataBase_WithNoneExistingGroupName_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);

            var groupService = new GroupService(groupRepository);
            var productService = new ProductService(productRepository, groupService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => productService.AddProductAsync("Гк Зузуя", "Test group", "userId"));
        }
    }
}
