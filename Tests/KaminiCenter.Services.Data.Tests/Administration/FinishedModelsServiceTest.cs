namespace KaminiCenter.Services.Data.Tests.Administration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Data.Repositories;
    using KaminiCenter.Services.Data.FinishedModelService;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Data.Tests.Common;
    using KaminiCenter.Services.Data.Tests.Common.Seeder;
    using KaminiCenter.Services.Data.Tests.Extensions;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.FinishedModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Xunit;

    public class FinishedModelsServiceTest : MapperInitializer
    {
        private const string ErrorMessage = "Method: \"{0}\", does not work properly!";

        [Fact]
        public async Task TestGetAllFinishedModels_WithGivenType_ShouldReturnAllFireplacesFromThatType()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var finishedModelsRepository = new EfDeletableEntityRepository<Finished_Model>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var finishedModelsService = new FinishedModelService(finishedModelsRepository, prodcutService, groupService, cloudinaryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedFinishedModelssAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AllFinishedModelViewModel).Assembly);
            var result = finishedModelsService.GetAllFinishedModelsAsync<AllFinishedModelViewModel>(TypeProject.Classic.ToString());
            var count = result.ToList().Count;

            // Assert
            Assert.True(count == 2, string.Format(ErrorMessage, "GetAllFinishedModels"));
        }

        [Fact]
        public void TestGetAllFinishedModel_WithoutAnyData_ShouldReturnEmptyList()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var finishedModelsRepository = new EfDeletableEntityRepository<Finished_Model>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var finishedModelsService = new FinishedModelService(finishedModelsRepository, prodcutService, groupService, cloudinaryService);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AllFinishedModelViewModel).Assembly);
            var result = finishedModelsService.GetAllFinishedModelsAsync<AllFinishedModelViewModel>(TypeProject.Classic.ToString());
            var count = result.ToList().Count;

            // Assert
            Assert.True(count == 0, string.Format(ErrorMessage, "GetAllFinishedModels empty list"));
        }

        [Fact]
        public async Task TestGetFinishedModel_WithExistingFinishedModelName_ShouldReturnFinishedModel()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var finishedModelsRepository = new EfDeletableEntityRepository<Finished_Model>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var finishedModelsService = new FinishedModelService(finishedModelsRepository, prodcutService, groupService, cloudinaryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedFinishedModelssAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DetailsFinishedModelsViewModel).Assembly);
            var expected = context.Finished_Models.SingleOrDefault(finishedModel => finishedModel.Product.Name == "Модел 1");
            var actualResult = finishedModelsService.GetByName<DetailsFinishedModelsViewModel>("Модел 1");

            // Assert
            AssertExtension.EqualsWithMessage(expected.Product.Name, actualResult.Name, string.Format(ErrorMessage, "GetFinishedModel with name, returns name"));
            AssertExtension.EqualsWithMessage(expected.TypeProject.ToString(), actualResult.TypeProject, string.Format(ErrorMessage, "GetFinishedModel with name, returns Type Of Project"));
            AssertExtension.EqualsWithMessage(expected.Description, actualResult.Description, string.Format(ErrorMessage, "GetFinishedModel with name, returns description"));
            AssertExtension.EqualsWithMessage(expected.ImagePath, actualResult.ImagePath, string.Format(ErrorMessage, "GetFinishedModel with name, returns ImagePath"));
        }

        [Fact]
        public async Task TestGetFinishedModels_WithNoneExistingireplaceName_ShouldReturnNull()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var finishedModelsRepository = new EfDeletableEntityRepository<Finished_Model>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var finishedModelsService = new FinishedModelService(finishedModelsRepository, prodcutService, groupService, cloudinaryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedFinishedModelssAsync(context);

            AutoMapperConfig.RegisterMappings(typeof(DetailsFinishedModelsViewModel).Assembly);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => finishedModelsService.GetByName<DetailsFinishedModelsViewModel>("Тестово име"));
        }

        [Fact]
        public async Task AddFinishedModel_WithCorrectData_ShouldSuccessfullyAdd()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var finishedModelsRepository = new EfDeletableEntityRepository<Finished_Model>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var finishedModelsService = new FinishedModelService(finishedModelsRepository, prodcutService, groupService, cloudinaryService);

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

            var finishedModel = new AddFinishedModelInputModel
            {
                Id = "abc1",
                Description = "Some description test 1",
                ImagePath = file,
                TypeProject = TypeProject.Classic.ToString(),
                Name = "Model 1",
            };

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AddFinishedModelInputModel).Assembly);
            var result = await finishedModelsService.AddFinishedModelAsync(finishedModel, user.Id.ToString());

            var actual = context.Finished_Models.FirstOrDefault(x => x.Product.Name == "Model 1");

            var expectedName = "Model 1";
            var expectedDescription = "Some description test 1";
            var expectedTypeOfProject = TypeProject.Classic.ToString();

            // Assert
            AssertExtension.EqualsWithMessage(expectedName, actual.Product.Name, string.Format(ErrorMessage, "AddFinishedModel returns correct Name"));
            AssertExtension.EqualsWithMessage(expectedDescription, actual.Description, string.Format(ErrorMessage, "AddFinishedModel returns correct Power"));
            AssertExtension.EqualsWithMessage(expectedTypeOfProject, actual.TypeProject.ToString(), string.Format(ErrorMessage, "AddFinishedModel returns correct TypeOfProject"));
        }

        [Fact]
        public async Task AddFinishedModel_WithNullValue_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var finishedModelsRepository = new EfDeletableEntityRepository<Finished_Model>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var finishedModelsService = new FinishedModelService(finishedModelsRepository, prodcutService, groupService, cloudinaryService);

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

            var finishedModel = new AddFinishedModelInputModel
            {
                Id = "abc1",
                Description = "Some description test 1",
                TypeProject = TypeProject.Classic.ToString(),
                Name = "Model 1",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AddFinishedModelInputModel).Assembly);
            await Assert.ThrowsAsync<ArgumentNullException>(() => finishedModelsService.AddFinishedModelAsync(finishedModel, user.Id.ToString()));
        }

        [Fact]
        public async Task TestGetFishedModelById_WithExistingFinishedModelId_ShouldReturnFinisheModel()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var finishedModelsRepository = new EfDeletableEntityRepository<Finished_Model>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var finishedModelsService = new FinishedModelService(finishedModelsRepository, prodcutService, groupService, cloudinaryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedFinishedModelssAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DetailsFinishedModelsViewModel).Assembly);
            var expected = context.Finished_Models.SingleOrDefault(fireplace => fireplace.Id == "abc1");
            var actualResult = finishedModelsService.GetById<DetailsFinishedModelsViewModel>("abc1");

            // Assert
            AssertExtension.EqualsWithMessage(expected.Product.Name, actualResult.Name, string.Format(ErrorMessage, "GetFinishedModel with name, returns name"));
            AssertExtension.EqualsWithMessage(expected.TypeProject.ToString(), actualResult.TypeProject, string.Format(ErrorMessage, "GetFinishedModel with name, returns TypoOfProject"));
            AssertExtension.EqualsWithMessage(expected.Description, actualResult.Description, string.Format(ErrorMessage, "GetFinishedModel with name, returns description"));
            AssertExtension.EqualsWithMessage(expected.ImagePath, actualResult.ImagePath, string.Format(ErrorMessage, "GetFinishedModel with name, returns ImagePath"));
        }

        [Fact]
        public async Task TestGetFinishedModelById_WithNoneExistingfinishedModelId_ShouldReturnNull()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var finishedModelsRepository = new EfDeletableEntityRepository<Finished_Model>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var finishedModelsService = new FinishedModelService(finishedModelsRepository, prodcutService, groupService, cloudinaryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedFinishedModelssAsync(context);

            AutoMapperConfig.RegisterMappings(typeof(DetailsFinishedModelsViewModel).Assembly);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => finishedModelsService.GetById<DetailsFinishedModelsViewModel>("Тестово id"));
        }

        [Fact]
        public async Task TestGetCountOfPageByTypeOfChamber_WithCorrectDate_ShouldReturenCorrectCount()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var finishedModelsRepository = new EfDeletableEntityRepository<Finished_Model>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var finishedModelsService = new FinishedModelService(finishedModelsRepository, prodcutService, groupService, cloudinaryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedFinishedModelssAsync(context);

            // Act
            var result = finishedModelsService.GetCountByTypeOfProject(TypeProject.Classic.ToString());

            // Assert
            Assert.True(result == 2, string.Format(ErrorMessage, string.Format(ErrorMessage, "GetCountByTypeOfProject")));
        }

        [Fact]
        public async Task EditFishedModel_WithCorrectData_ShouldSuccessfullyEdit()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var finishedModelsRepository = new EfDeletableEntityRepository<Finished_Model>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var finishedModelsService = new FinishedModelService(finishedModelsRepository, prodcutService, groupService, cloudinaryService);

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

            var fireplace = new EditFinishedModelViewModel
            {
                Id = "abc1",
                Description = "Some description test 1",
                ImagePath = file,
                TypeProject = TypeProject.Classic.ToString(),
                Name = "Модел 1 сменено",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedFinishedModelssAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(EditFinishedModelViewModel).Assembly);
            var result = await finishedModelsService.EditAsync<EditFinishedModelViewModel>(fireplace);

            var actual = context.Finished_Models.FirstOrDefault(x => x.Product.Name == "Модел 1 сменено");

            var expectedName = "Модел 1 сменено";
            var expectedDescription = "Some description test 1";
            var expectedTypeOfProject = TypeProject.Classic.ToString();

            // Assert
            AssertExtension.EqualsWithMessage(expectedName, actual.Product.Name, string.Format(ErrorMessage, "EditFinishedModel returns correct Name"));
            AssertExtension.EqualsWithMessage(expectedDescription, actual.Description, string.Format(ErrorMessage, "EditFinishedModel returns correct Description"));
            AssertExtension.EqualsWithMessage(expectedTypeOfProject, actual.TypeProject.ToString(), string.Format(ErrorMessage, "EditFinishedModel returns correct TypeOfProject"));
        }

        [Fact]
        public async Task EditFinishedModel_WithNonExistingFireplace_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var finishedModelsRepository = new EfDeletableEntityRepository<Finished_Model>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var finishedModelsService = new FinishedModelService(finishedModelsRepository, prodcutService, groupService, cloudinaryService);

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

            var finishedModel = new EditFinishedModelViewModel
            {
                Id = "Test Id",
                Description = "Some description test 1",
                TypeProject = TypeProject.Classic.ToString(),
                Name = "Model 1",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(EditFinishedModelViewModel).Assembly);
            await Assert.ThrowsAsync<NullReferenceException>(() => finishedModelsService.EditAsync<EditFinishedModelViewModel>(finishedModel));
        }

        [Fact]
        public async Task DeleteFinishedModel_WithCorrectData_ShouldSuccessfullyDelete()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var finishedModelsRepository = new EfDeletableEntityRepository<Finished_Model>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var finishedModelsService = new FinishedModelService(finishedModelsRepository, prodcutService, groupService, cloudinaryService);

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

            var finishedModel = new DeleteFinishedViewModel
            {
                Id = "abc1",
                Description = "Some description test 1",
                TypeProject = TypeProject.Classic.ToString(),
                Name = "Model 1",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedFinishedModelssAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DeleteFinishedViewModel).Assembly);
            await finishedModelsService.DeleteAsync<DeleteFinishedViewModel>(finishedModel);
            int actual = context.Finished_Models.Count();

            // Assert
            Assert.True(actual == 1, string.Format(ErrorMessage, "DeleteFinishedModel method"));
        }

        [Fact]
        public async Task DeleteFinishedModel_WithNonExistingFireplace_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var finishedModelsRepository = new EfDeletableEntityRepository<Finished_Model>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var finishedModelsService = new FinishedModelService(finishedModelsRepository, prodcutService, groupService, cloudinaryService);

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

            var finishedModel = new DeleteFinishedViewModel
            {
                Id = "Test Id",
                Description = "Some description test 1",
                ImagePath = "Some dummy data",
                TypeProject = TypeProject.Classic.ToString(),
                Name = "Модел 1",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedFinishedModelssAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DeleteFinishedViewModel).Assembly);
            await Assert.ThrowsAsync<NullReferenceException>(() => finishedModelsService.DeleteAsync<DeleteFinishedViewModel>(finishedModel));
        }

        [Fact]
        public async Task TestGetAllFinishedModels_WithoutGivenType_ShouldReturnAllFinishedModel()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var finishedModelsRepository = new EfDeletableEntityRepository<Finished_Model>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var finishedModelsService = new FinishedModelService(finishedModelsRepository, prodcutService, groupService, cloudinaryService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedFinishedModelssAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AllFinishedModelViewModel).Assembly);
            var result = finishedModelsService.GetAll<AllFinishedModelViewModel>();
            var count = result.ToList().Count;
            var actual = context.Finished_Models.Count();

            // Assert
            Assert.True(count == actual, string.Format(ErrorMessage, "GetAllFinishedModel"));
        }
    }
}
