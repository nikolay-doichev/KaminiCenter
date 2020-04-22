namespace KaminiCenter.Services.Data.Tests.Administration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Data.Repositories;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Data.SuggestProduct;
    using KaminiCenter.Services.Data.Tests.Administration;
    using KaminiCenter.Services.Data.Tests.Common;
    using KaminiCenter.Services.Data.Tests.Common.Seeder;
    using KaminiCenter.Services.Data.Tests.Extensions;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.Extensions.Configuration;

    using Xunit;

    public class FireplaceServiceTest : MapperInitializer
    {
        private const string ErrorMessage = "Method: \"{0}\", does not work properly!";

        [Fact]
        public async Task TestGetAllFireplaces_WithGivenType_ShouldReturnAllFireplacesFromThatType()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AllFireplaceViewModel).Assembly);
            var result = fireplaceService.GetAllFireplaceAsync<AllFireplaceViewModel>(TypeOfChamber.Basic.ToString());
            var count = result.ToList().Count;

            // Assert
            Assert.True(count == 2, string.Format(ErrorMessage, "GetAllFireplaces"));
        }

        [Fact]
        public void TestGetAllFireplaces_WithoutAnyData_ShouldReturnEmptyList()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AllFireplaceViewModel).Assembly);
            var result = fireplaceService.GetAllFireplaceAsync<AllFireplaceViewModel>(TypeOfChamber.Basic.ToString());
            var count = result.ToList().Count;

            // Assert
            Assert.True(count == 0, string.Format(ErrorMessage, "Get all fireplaces empty list"));
        }

        [Fact]
        public async Task TestGetFireplace_WithExistingFireplaceName_ShouldReturnFireplace()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DetailsFireplaceViewModel).Assembly);
            var expected = context.Fireplace_Chambers.SingleOrDefault(fireplace => fireplace.Product.Name == "Гк Мая");
            var actualResult = fireplaceService.GetByName<DetailsFireplaceViewModel>("Гк Мая");

            // Assert
            AssertExtension.EqualsWithMessage(expected.Product.Name, actualResult.Name, string.Format(ErrorMessage, "GetFireplace with name, returns name"));
            AssertExtension.EqualsWithMessage(expected.Power, actualResult.Power, string.Format(ErrorMessage, "GetFireplace with name, returns power"));
            AssertExtension.EqualsWithMessage(expected.Chimney, actualResult.Chimney, string.Format(ErrorMessage, "GetFireplace with name, returns chimney"));
            Assert.Equal(expected.Price, actualResult.Price);
            AssertExtension.EqualsWithMessage(expected.Description, actualResult.Description, string.Format(ErrorMessage, "GetFireplace with name, returns description"));
            AssertExtension.EqualsWithMessage(expected.ImagePath, actualResult.ImagePath, string.Format(ErrorMessage, "GetFireplace with name, returns ImagePath"));
        }

        [Fact]
        public async Task TestGetFireplace_WithNoneExistingireplaceName_ShouldReturnNull()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);
            AutoMapperConfig.RegisterMappings(typeof(DetailsFireplaceViewModel).Assembly);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => fireplaceService.GetByName<DetailsFireplaceViewModel>("Тестово име"));
        }

        [Fact]
        public async Task AddFireplace_WithCorrectData_ShouldSuccessfullyAdd()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

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

            var fireplace = new FireplaceInputModel
            {
                Id = "abc1",
                Power = "10w",
                Chimney = "200Ф",
                Description = "Some description test 1",
                ImagePath = file,
                Price = 1800.00M,
                Size = "60 / 40 / h50",
                TypeOfChamber = TypeOfChamber.Basic.ToString(),
                Name = "Гк Мая",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(FireplaceInputModel).Assembly);
            var result = await fireplaceService.AddFireplaceAsync<FireplaceInputModel>(fireplace, user.Id.ToString());

            var actual = context.Fireplace_Chambers.FirstOrDefault(x => x.Product.Name == "Гк Мая");

            var expectedName = "Гк Мая";
            var expectedPower = "10w";
            var expectedChimney = "200Ф";
            var expectedPrice = 1800.00M;
            var expectedSize = "60 / 40 / h50";
            var expectedTypeOfChamber = TypeOfChamber.Basic.ToString();

            // Assert
            AssertExtension.EqualsWithMessage(expectedName, actual.Product.Name, string.Format(ErrorMessage, "AddFireplace returns correct Name"));
            AssertExtension.EqualsWithMessage(expectedPower, actual.Power, string.Format(ErrorMessage, "AddFireplace returns correct Power"));
            AssertExtension.EqualsWithMessage(expectedChimney, actual.Chimney, string.Format(ErrorMessage, "AddFireplace returns correct Chimney"));
            Assert.Equal(expectedPrice, actual.Price);
            AssertExtension.EqualsWithMessage(expectedSize, actual.Size, string.Format(ErrorMessage, "AddFireplace returns correct Size"));
            AssertExtension.EqualsWithMessage(expectedTypeOfChamber, actual.TypeOfChamber.ToString(), string.Format(ErrorMessage, "AddFireplace returns correct TypeOfChamber"));
        }

        [Fact]
        public async Task AddFireplace_WithNullValue_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

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

            var fireplace = new FireplaceInputModel
            {
                Id = "abc1",
                Chimney = "200Ф",
                Description = "Some description test 1",
                ImagePath = file,
                Price = 1800.00M,
                Size = "60 / 40 / h50",
                TypeOfChamber = TypeOfChamber.Basic.ToString(),
                Name = "Гк Мая",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(FireplaceInputModel).Assembly);
            await Assert.ThrowsAsync<ArgumentNullException>(() => fireplaceService.AddFireplaceAsync<FireplaceInputModel>(fireplace, user.Id.ToString()));
        }

        [Fact]
        public async Task TestGetFireplaceById_WithExistingFireplaceId_ShouldReturnFireplace()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DetailsFireplaceViewModel).Assembly);
            var expected = context.Fireplace_Chambers.SingleOrDefault(fireplace => fireplace.Id == "abc1");
            var actualResult = fireplaceService.GetById<DetailsFireplaceViewModel>("abc1");

            // Assert
            AssertExtension.EqualsWithMessage(expected.Product.Name, actualResult.Name, string.Format(ErrorMessage, "GetFireplace with name, returns name"));
            AssertExtension.EqualsWithMessage(expected.Power, actualResult.Power, string.Format(ErrorMessage, "GetFireplace with name, returns power"));
            AssertExtension.EqualsWithMessage(expected.Chimney, actualResult.Chimney, string.Format(ErrorMessage, "GetFireplace with name, returns chimney"));
            Assert.Equal(expected.Price, actualResult.Price);
            AssertExtension.EqualsWithMessage(expected.Description, actualResult.Description, string.Format(ErrorMessage, "GetFireplace with name, returns description"));
            AssertExtension.EqualsWithMessage(expected.ImagePath, actualResult.ImagePath, string.Format(ErrorMessage, "GetFireplace with name, returns ImagePath"));
        }

        [Fact]
        public async Task TestGetFireplaceById_WithNoneExistingireplaceId_ShouldReturnNull()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);
            AutoMapperConfig.RegisterMappings(typeof(DetailsFireplaceViewModel).Assembly);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => fireplaceService.GetById<DetailsFireplaceViewModel>("Тестово id"));
        }

        [Fact]
        public async Task TestGetCountOfPageByTypeOfChamber_WithCorrectDate_ShouldReturenCorrectCount()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);

            // Act
            var result = fireplaceService.GetCountByTypeOfChamber(TypeOfChamber.Basic.ToString());

            // Assert
            Assert.True(result == 2, string.Format(ErrorMessage, string.Format(ErrorMessage, "GetCountByTypeOfChamber")));
        }

        [Fact]
        public async Task EditFireplace_WithCorrectData_ShouldSuccessfullyEdit()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

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

            var fireplace = new EditFireplaceViewModel
            {
                Id = "abc1",
                Power = "10w",
                Chimney = "200Ф",
                Description = "Some description test 1",
                ImagePath = file,
                Price = 1800.00M,
                Size = "60 / 40 / h50",
                TypeOfChamber = TypeOfChamber.Basic.ToString(),
                Name = "Гк Мая сменено",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(EditFireplaceViewModel).Assembly);
            var result = await fireplaceService.EditAsync<EditFireplaceViewModel>(fireplace);

            var actual = context.Fireplace_Chambers.FirstOrDefault(x => x.Product.Name == "Гк Мая сменено");

            var expectedName = "Гк Мая сменено";
            var expectedPower = "10w";
            var expectedChimney = "200Ф";
            var expectedPrice = 1800.00M;
            var expectedSize = "60 / 40 / h50";
            var expectedTypeOfChamber = TypeOfChamber.Basic.ToString();

            // Assert
            AssertExtension.EqualsWithMessage(expectedName, actual.Product.Name, string.Format(ErrorMessage, "AddFireplace returns correct Name"));
            AssertExtension.EqualsWithMessage(expectedPower, actual.Power, string.Format(ErrorMessage, "AddFireplace returns correct Power"));
            AssertExtension.EqualsWithMessage(expectedChimney, actual.Chimney, string.Format(ErrorMessage, "AddFireplace returns correct Chimney"));
            Assert.Equal(expectedPrice, actual.Price);
            AssertExtension.EqualsWithMessage(expectedSize, actual.Size, string.Format(ErrorMessage, "AddFireplace returns correct Size"));
            AssertExtension.EqualsWithMessage(expectedTypeOfChamber, actual.TypeOfChamber.ToString(), string.Format(ErrorMessage, "AddFireplace returns correct TypeOfChamber"));
        }

        [Fact]
        public async Task EditFireplace_WithNonExistingFireplace_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

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

            var fireplace = new EditFireplaceViewModel
            {
                Id = "Test Id",
                Power = "10w",
                Chimney = "200Ф",
                Description = "Some description test 1",
                ImagePath = file,
                Price = 1800.00M,
                Size = "60 / 40 / h50",
                TypeOfChamber = TypeOfChamber.Basic.ToString(),
                Name = "Гк Мая сменено",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(FireplaceInputModel).Assembly);
            await Assert.ThrowsAsync<NullReferenceException>(() => fireplaceService.EditAsync<EditFireplaceViewModel>(fireplace));
        }

        [Fact]
        public async Task DeleteFireplace_WithCorrectData_ShouldSuccessfullyDelete()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

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

            var fireplace = new DeleteFireplaceViewModel
            {
                Id = "abc1",
                Power = "10w",
                Chimney = "200Ф",
                Description = "Some description test 1",
                ImagePath = "Some dummy data",
                Price = 1800.00M,
                Size = "60 / 40 / h50",
                TypeOfChamber = TypeOfChamber.Basic.ToString(),
                Name = "Гк Мая",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DeleteFireplaceViewModel).Assembly);
            await fireplaceService.DeleteAsync<DeleteFireplaceViewModel>(fireplace);
            int actual = context.Fireplace_Chambers.Count();

            // Assert
            Assert.True(actual == 1, string.Format(ErrorMessage, "DeleteFireplace method"));
        }

        [Fact]
        public async Task DeleteFireplace_WithNonExistingFireplace_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

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

            var fireplace = new DeleteFireplaceViewModel
            {
                Id = "Test Id",
                Power = "10w",
                Chimney = "200Ф",
                Description = "Some description test 1",
                ImagePath = "Some dummy data",
                Price = 1800.00M,
                Size = "60 / 40 / h50",
                TypeOfChamber = TypeOfChamber.Basic.ToString(),
                Name = "Гк Мая",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DeleteFireplaceViewModel).Assembly);
            await Assert.ThrowsAsync<NullReferenceException>(() => fireplaceService.DeleteAsync<DeleteFireplaceViewModel>(fireplace));
        }

        [Fact]
        public async Task TestGetAllFireplaces_WithoutGivenType_ShouldReturnAllFireplaces()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AllFireplaceViewModel).Assembly);
            var result = fireplaceService.GetAllFireplace<AllFireplaceViewModel>();
            var count = result.ToList().Count;
            var actual = context.Fireplace_Chambers.Count();

            // Assert
            Assert.True(count == actual, string.Format(ErrorMessage, "Get all user with data"));
        }

        [Fact]
        public async Task AddSuggestProduct_WithCorrectData_ShouldSuccessfullyAddSuggestion()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);
            string[] selectedFireplace = new string[] { "Гк Оливия" };

            // Act
            AutoMapperConfig.RegisterMappings(typeof(FireplaceInputModel).Assembly);
            await fireplaceService.AddSuggestionToFireplaceAsync("abc1", selectedFireplace, null, null, null);
            var count = context.SuggestProducts.Count();

            // Assert
            Assert.True(count == 1, string.Format(ErrorMessage, "Add suggestion method"));
        }

        [Fact]
        public async Task AddSuggestProduct_ToNoneExistingFireplaceId_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);
            string[] selectedFireplace = new string[] { "Гк Оливия" };

            // Act and Asseart
            AutoMapperConfig.RegisterMappings(typeof(DeleteFireplaceViewModel).Assembly);
            await Assert.ThrowsAsync<NullReferenceException>(() => fireplaceService.AddSuggestionToFireplaceAsync("Test id", selectedFireplace, null, null, null));
        }

        [Fact]
        public async Task AddSuggestProduct_WithNoneExistingProduct_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplacesRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var cloudinaryService = new FakeCloudinary();
            var fireplaceService = new FireplaceService(fireplacesRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);
            string[] selectedFireplace = new string[] { "Test Id" };

            // Act and Asseart
            AutoMapperConfig.RegisterMappings(typeof(DeleteFireplaceViewModel).Assembly);
            await Assert.ThrowsAsync<NullReferenceException>(() => fireplaceService.AddSuggestionToFireplaceAsync("abc1", selectedFireplace, null, null, null));
        }
    }
}
