namespace KaminiCenter.Services.Data.Tests.Administration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Repositories;
    using KaminiCenter.Services.Data.AccessoriesService;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Data.Tests.Common;
    using KaminiCenter.Services.Data.Tests.Common.Seeder;
    using KaminiCenter.Services.Data.Tests.Extensions;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Accessories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Xunit;

    public class AccessorieServiceTests
    {
        private const string ErrorMessage = "Method: \"{0}\", does not work properly!";

        [Fact]
        public async Task TestGetAllAccessorie_WithGivenType_ShouldReturnAllAccessorie()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var accessorieRepository = new EfDeletableEntityRepository<Accessorie>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var accessorieService = new AccessoriesService(accessorieRepository, prodcutService, groupService, cloudinaryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedAccessorieAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AllAccessorieViewModel).Assembly);
            var result = accessorieService.GetAllAccessorieAsync<AllAccessorieViewModel>();
            var expected = result.ToList().Count;
            var actual = context.Accessories.Count();

            // Assert
            Assert.True(expected == actual, string.Format(ErrorMessage, "GetAllAccessorie"));
        }

        [Fact]
        public void TestGetAllAccessorie_WithoutAnyData_ShouldReturnEmptyList()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var accessorieRepository = new EfDeletableEntityRepository<Accessorie>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var accessorieService = new AccessoriesService(accessorieRepository, prodcutService, groupService, cloudinaryService);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AllAccessorieViewModel).Assembly);
            var result = accessorieService.GetAllAccessorieAsync<AllAccessorieViewModel>();
            var expected = result.ToList().Count;
            var actual = context.Accessories.Count();

            // Assert
            Assert.True(expected == actual, string.Format(ErrorMessage, "GetAllAccessorie empty list"));
        }

        [Fact]
        public async Task TestGetAccessorie_WithExistingAccessorieName_ShouldReturnAccessorie()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var accessorieRepository = new EfDeletableEntityRepository<Accessorie>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var accessorieService = new AccessoriesService(accessorieRepository, prodcutService, groupService, cloudinaryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedAccessorieAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DetailsAccessorieViewModel).Assembly);
            var expected = context.Accessories.SingleOrDefault(accessorie => accessorie.Product.Name == "Аксесоар 1");
            var actualResult = accessorieService.GetByName<DetailsAccessorieViewModel>("Аксесоар 1");

            // Assert
            AssertExtension.EqualsWithMessage(expected.Product.Name, actualResult.Name, string.Format(ErrorMessage, "GetAccessorie with name, returns name"));
            AssertExtension.EqualsWithMessage(expected.Description, actualResult.Description, string.Format(ErrorMessage, "GetAccessorie with name, returns description"));
            AssertExtension.EqualsWithMessage(expected.ImagePath, actualResult.ImagePath, string.Format(ErrorMessage, "GetAccessorie with name, returns ImagePath"));
        }

        [Fact]
        public async Task TestGetAccessorie_WithNoneExistingAccessorieName_ShouldReturnNull()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var accessorieRepository = new EfDeletableEntityRepository<Accessorie>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var accessorieService = new AccessoriesService(accessorieRepository, prodcutService, groupService, cloudinaryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedAccessorieAsync(context);

            AutoMapperConfig.RegisterMappings(typeof(DetailsAccessorieViewModel).Assembly);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => accessorieService.GetByName<DetailsAccessorieViewModel>("Тестово име"));
        }

        [Fact]
        public async Task AddFinishedModel_WithCorrectData_ShouldSuccessfullyAdd()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var accessorieRepository = new EfDeletableEntityRepository<Accessorie>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var accessorieService = new AccessoriesService(accessorieRepository, prodcutService, groupService, cloudinaryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);

            var user = new ApplicationUser
            {
                Id = "abc",
                FirstName = "Nikolay",
                LastName = "Doychev",
                Email = "nikolay.doichev@gmail.com",
                EmailConfirmed = true,
            };

            var fileName = "Img";
            IFormFile file = new FormFile(
                new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                0,
                0,
                fileName,
                "dummy.png");

            var accessorie = new AddAccessorieInputModel
            {
                Id = "abc1",
                Description = "Some description test 1",
                ImagePath = file,
                Name = "Аксесоар 1",
            };

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AddAccessorieInputModel).Assembly);
            var result = await accessorieService.AddAccessoriesAsync(accessorie, user.Id.ToString());

            var actual = context.Accessories.FirstOrDefault(x => x.Product.Name == "Аксесоар 1");

            var expectedName = "Аксесоар 1";
            var expectedDescription = "Some description test 1";

            // Assert
            AssertExtension.EqualsWithMessage(expectedName, actual.Product.Name, string.Format(ErrorMessage, "AddAccessorie returns correct Name"));
            AssertExtension.EqualsWithMessage(expectedDescription, actual.Description, string.Format(ErrorMessage, "AddAccessorie returns correct Description"));
        }

        [Fact]
        public async Task AddProject_WithNullValue_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var accessorieRepository = new EfDeletableEntityRepository<Accessorie>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var accessorieService = new AccessoriesService(accessorieRepository, prodcutService, groupService, cloudinaryService);

            var user = new ApplicationUser
            {
                Id = "abc",
                FirstName = "Nikolay",
                LastName = "Doychev",
                Email = "nikolay.doichev@gmail.com",
                EmailConfirmed = true,
            };

            var fileName = "Img";
            IFormFile file = new FormFile(
                new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                0,
                0,
                fileName,
                "dummy.png");

            var accessorie = new AddAccessorieInputModel
            {
                Id = "abc1",
                Description = "Some description test 1",
                Name = "Аксесоар 1",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AddAccessorieInputModel).Assembly);
            await Assert.ThrowsAsync<ArgumentNullException>(() => accessorieService.AddAccessoriesAsync(accessorie, user.Id.ToString()));
        }

        [Fact]
        public async Task TestGetAccessorieById_WithExistingAccessorieId_ShouldReturnAccessorie()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var accessorieRepository = new EfDeletableEntityRepository<Accessorie>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var accessorieService = new AccessoriesService(accessorieRepository, prodcutService, groupService, cloudinaryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedAccessorieAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DetailsAccessorieViewModel).Assembly);
            var expected = context.Accessories.SingleOrDefault(accessorie => accessorie.Id == "abc1");
            var actualResult = accessorieService.GetById<DetailsAccessorieViewModel>("abc1");

            // Assert
            AssertExtension.EqualsWithMessage(expected.Product.Name, actualResult.Name, string.Format(ErrorMessage, "GetAccessorie with name, returns name"));
            AssertExtension.EqualsWithMessage(expected.Description, actualResult.Description, string.Format(ErrorMessage, "GetAccessorie with name, returns description"));
            AssertExtension.EqualsWithMessage(expected.ImagePath, actualResult.ImagePath, string.Format(ErrorMessage, "GetAccessorie with name, returns ImagePath"));
        }

        [Fact]
        public async Task TestGetAccessorieById_WithNoneExistingAccessorieId_ShouldReturnNull()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var accessorieRepository = new EfDeletableEntityRepository<Accessorie>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var accessorieService = new AccessoriesService(accessorieRepository, prodcutService, groupService, cloudinaryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedAccessorieAsync(context);

            AutoMapperConfig.RegisterMappings(typeof(DetailsAccessorieViewModel).Assembly);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => accessorieService.GetById<DetailsAccessorieViewModel>("Тестово id"));
        }

        [Fact]
        public async Task TestGetCountOfPage_WithCorrectDate_ShouldReturenCorrectCount()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var accessorieRepository = new EfDeletableEntityRepository<Accessorie>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var accessorieService = new AccessoriesService(accessorieRepository, prodcutService, groupService, cloudinaryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedAccessorieAsync(context);

            // Act
            var expected = accessorieService.GetCount();
            var actual = context.Accessories.Count();

            // Assert
            Assert.True(expected == actual, string.Format(ErrorMessage, string.Format(ErrorMessage, "GetCount")));
        }

        [Fact]
        public async Task EditProject_WithCorrectData_ShouldSuccessfullyEdit()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var accessorieRepository = new EfDeletableEntityRepository<Accessorie>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var accessorieService = new AccessoriesService(accessorieRepository, prodcutService, groupService, cloudinaryService);

            var user = new ApplicationUser
            {
                Id = "abc",
                FirstName = "Nikolay",
                LastName = "Doychev",
                Email = "nikolay.doichev@gmail.com",
                EmailConfirmed = true,
            };

            var fileName = "Img";
            IFormFile file = new FormFile(
                new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                0,
                0,
                fileName,
                "dummy.png");

            var accessorie = new EditAccessorieViewModel
            {
                Id = "abc1",
                Description = "Some description test 1",
                ImagePath = file,
                Name = "Аксесоар 1 сменено",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedAccessorieAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(EditAccessorieViewModel).Assembly);
            var result = await accessorieService.EditAsync<EditAccessorieViewModel>(accessorie);

            var actual = context.Accessories.FirstOrDefault(x => x.Product.Name == "Аксесоар 1 сменено");

            var expectedName = "Аксесоар 1 сменено";
            var expectedDescription = "Some description test 1";

            // Assert
            AssertExtension.EqualsWithMessage(expectedName, actual.Product.Name, string.Format(ErrorMessage, "EditAccessorie returns correct Name"));
            AssertExtension.EqualsWithMessage(expectedDescription, actual.Description, string.Format(ErrorMessage, "EditAccessorie returns correct Description"));
        }

        [Fact]
        public async Task EditAccessorie_WithNonExistingAccessorie_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var accessorieRepository = new EfDeletableEntityRepository<Accessorie>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var accessorieService = new AccessoriesService(accessorieRepository, prodcutService, groupService, cloudinaryService);

            var user = new ApplicationUser
            {
                Id = "abc",
                FirstName = "Nikolay",
                LastName = "Doychev",
                Email = "nikolay.doichev@gmail.com",
                EmailConfirmed = true,
            };

            var fileName = "Img";
            IFormFile file = new FormFile(
                new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                0,
                0,
                fileName,
                "dummy.png");

            var accessorie = new EditAccessorieViewModel
            {
                Id = "Test Id",
                Description = "Some description test 1",
                Name = "Аксесоар 1",
                ImagePath = file,
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProjectAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(EditAccessorieViewModel).Assembly);
            await Assert.ThrowsAsync<NullReferenceException>(() => accessorieService.EditAsync<EditAccessorieViewModel>(accessorie));
        }

        [Fact]
        public async Task DeleteProject_WithCorrectData_ShouldSuccessfullyDelete()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var accessorieRepository = new EfDeletableEntityRepository<Accessorie>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var accessorieService = new AccessoriesService(accessorieRepository, prodcutService, groupService, cloudinaryService);

            var user = new ApplicationUser
            {
                Id = "abc",
                FirstName = "Nikolay",
                LastName = "Doychev",
                Email = "nikolay.doichev@gmail.com",
                EmailConfirmed = true,
            };

            var fileName = "Img";
            IFormFile file = new FormFile(
                new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                0,
                0,
                fileName,
                "dummy.png");

            var project = new DeleteAccessorieViewModel
            {
                Id = "abc1",
                Description = "Some description test 1",
                Name = "Проект 1",
                ImagePath = fileName,
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedAccessorieAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DeleteAccessorieViewModel).Assembly);
            await accessorieService.DeleteAsync<DeleteAccessorieViewModel>(project);
            int actual = context.Accessories.Count();

            // Assert
            Assert.True(actual == 1, string.Format(ErrorMessage, "DeleteAccessorie method"));
        }

        [Fact]
        public async Task DeleteAccessorie_WithNonExistingAccessorie_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var accessorieRepository = new EfDeletableEntityRepository<Accessorie>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var accessorieService = new AccessoriesService(accessorieRepository, prodcutService, groupService, cloudinaryService);

            var user = new ApplicationUser
            {
                Id = "abc",
                FirstName = "Nikolay",
                LastName = "Doychev",
                Email = "nikolay.doichev@gmail.com",
                EmailConfirmed = true,
            };

            var fileName = "Img";
            IFormFile file = new FormFile(
                new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")),
                0,
                0,
                fileName,
                "dummy.png");

            var accessorie = new DeleteAccessorieViewModel
            {
                Id = "Test Id",
                Description = "Some description test 1",
                ImagePath = "Some dummy data",
                Name = "Проект 1",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedAccessorieAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DeleteAccessorieViewModel).Assembly);
            await Assert.ThrowsAsync<NullReferenceException>(() => accessorieService.DeleteAsync<DeleteAccessorieViewModel>(accessorie));
        }
    }
}
