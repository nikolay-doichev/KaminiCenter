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
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Data.ProjectsService;
    using KaminiCenter.Services.Data.Tests.Common;
    using KaminiCenter.Services.Data.Tests.Common.Seeder;
    using KaminiCenter.Services.Data.Tests.Extensions;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Projects;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Xunit;

    public class ProjectServiceTest
    {
        private const string ErrorMessage = "Method: \"{0}\", does not work properly!";

        [Fact]
        public async Task TestGetAllProjects_WithGivenType_ShouldReturnAllFireplacesFromThatType()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var projectRepository = new EfDeletableEntityRepository<Project>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var projectService = new ProjectService(projectRepository, cloudinaryService, prodcutService, groupService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProjectAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AllProjectViewModel).Assembly);
            var result = projectService.GetAllProjectAsync<AllProjectViewModel>(TypeLocation.Corner.ToString(), TypeProject.Classic.ToString());
            var expected = result.ToList().Count;
            var actual = context.Projects.Count();

            // Assert
            Assert.True(expected == actual, string.Format(ErrorMessage, "GetAllProject"));
        }

        [Fact]
        public void TestGetAllProjects_WithoutAnyData_ShouldReturnEmptyList()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var projectRepository = new EfDeletableEntityRepository<Project>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var projectService = new ProjectService(projectRepository, cloudinaryService, prodcutService, groupService);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AllProjectViewModel).Assembly);
            var result = projectService.GetAllProjectAsync<AllProjectViewModel>(
                TypeLocation.Corner.ToString(),
                TypeProject.Classic.ToString());
            var expected = result.ToList().Count;
            var actual = context.Projects.Count();

            // Assert
            Assert.True(expected == actual, string.Format(ErrorMessage, "GetAllProjects empty list"));
        }

        [Fact]
        public async Task TestGetProject_WithExistingProjectName_ShouldReturnProject()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var projectRepository = new EfDeletableEntityRepository<Project>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var projectService = new ProjectService(projectRepository, cloudinaryService, prodcutService, groupService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProjectAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DetailsProjectViewModel).Assembly);
            var expected = context.Projects.SingleOrDefault(finishedModel => finishedModel.Product.Name == "Проект 1");
            var actualResult = projectService.GetByName<DetailsProjectViewModel>("Проект 1");

            // Assert
            AssertExtension.EqualsWithMessage(expected.Product.Name, actualResult.Name, string.Format(ErrorMessage, "GetProject with name, returns name"));
            AssertExtension.EqualsWithMessage(expected.TypeProject.ToString(), actualResult.TypeProject, string.Format(ErrorMessage, "GetFinishedModel with name, returns Type Of Project"));
            AssertExtension.EqualsWithMessage(expected.TypeLocation.ToString(), actualResult.TypeLocation, string.Format(ErrorMessage, "GetFinishedModel with name, returns Type Of Location"));
            AssertExtension.EqualsWithMessage(expected.Description, actualResult.Description, string.Format(ErrorMessage, "GetProject with name, returns description"));
            AssertExtension.EqualsWithMessage(expected.ImagePath, actualResult.ImagePath, string.Format(ErrorMessage, "GetProject with name, returns ImagePath"));
        }

        [Fact]
        public async Task TestGetFinishedModels_WithNoneExistingireplaceName_ShouldReturnNull()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var projectRepository = new EfDeletableEntityRepository<Project>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var projectService = new ProjectService(projectRepository, cloudinaryService, prodcutService, groupService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProjectAsync(context);

            AutoMapperConfig.RegisterMappings(typeof(DetailsProjectViewModel).Assembly);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => projectService.GetByName<DetailsProjectViewModel>("Тестово име"));
        }

        [Fact]
        public async Task AddFinishedModel_WithCorrectData_ShouldSuccessfullyAdd()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var projectRepository = new EfDeletableEntityRepository<Project>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var projectService = new ProjectService(projectRepository, cloudinaryService, prodcutService, groupService);

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

            var project = new AddProjectInputModel
            {
                Id = "abc1",
                Description = "Some description test 1",
                ImagePath = file,
                TypeProject = TypeProject.Classic.ToString(),
                TypeLocation = TypeLocation.Corner.ToString(),
                Name = "Проект 1",
            };

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AddProjectInputModel).Assembly);
            var result = await projectService.AddProjectAsync(project, user.Id.ToString());

            var actual = context.Projects.FirstOrDefault(x => x.Product.Name == "Проект 1");

            var expectedName = "Проект 1";
            var expectedDescription = "Some description test 1";
            var expectedTypeOfProject = TypeProject.Classic.ToString();
            var expectedTypeOfLocation = TypeLocation.Corner.ToString();

            // Assert
            AssertExtension.EqualsWithMessage(expectedName, actual.Product.Name, string.Format(ErrorMessage, "AddProject returns correct Name"));
            AssertExtension.EqualsWithMessage(expectedDescription, actual.Description, string.Format(ErrorMessage, "AddProject returns correct Description"));
            AssertExtension.EqualsWithMessage(expectedTypeOfProject, actual.TypeProject.ToString(), string.Format(ErrorMessage, "AddProject returns correct TypeOfProject"));
            AssertExtension.EqualsWithMessage(expectedTypeOfLocation, actual.TypeLocation.ToString(), string.Format(ErrorMessage, "AddProject returns correct TypeOfLoaction"));
        }

        [Fact]
        public async Task AddProject_WithNullValue_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var projectRepository = new EfDeletableEntityRepository<Project>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var projectService = new ProjectService(projectRepository, cloudinaryService, prodcutService, groupService);

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

            var project = new AddProjectInputModel
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
            AutoMapperConfig.RegisterMappings(typeof(AddProjectInputModel).Assembly);
            await Assert.ThrowsAsync<ArgumentNullException>(() => projectService.AddProjectAsync(project, user.Id.ToString()));
        }

        [Fact]
        public async Task TestGetProjectById_WithExistingProjectId_ShouldReturnProject()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var projectRepository = new EfDeletableEntityRepository<Project>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var projectService = new ProjectService(projectRepository, cloudinaryService, prodcutService, groupService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProjectAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DetailsProjectViewModel).Assembly);
            var expected = context.Projects.SingleOrDefault(project => project.Id == "abc1");
            var actualResult = projectService.GetById<DetailsProjectViewModel>("abc1");

            // Assert
            AssertExtension.EqualsWithMessage(expected.Product.Name, actualResult.Name, string.Format(ErrorMessage, "GetProject with name, returns name"));
            AssertExtension.EqualsWithMessage(expected.TypeProject.ToString(), actualResult.TypeProject, string.Format(ErrorMessage, "GetFinishedModel with name, returns TypoOfProject"));
            AssertExtension.EqualsWithMessage(expected.TypeLocation.ToString(), actualResult.TypeLocation, string.Format(ErrorMessage, "GetFinishedModel with name, returns TypoOfLocation"));
            AssertExtension.EqualsWithMessage(expected.Description, actualResult.Description, string.Format(ErrorMessage, "GetProject with name, returns description"));
            AssertExtension.EqualsWithMessage(expected.ImagePath, actualResult.ImagePath, string.Format(ErrorMessage, "GetProject with name, returns ImagePath"));
        }

        [Fact]
        public async Task TestGetProjectById_WithNoneExistingProjectId_ShouldReturnNull()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var projectRepository = new EfDeletableEntityRepository<Project>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var projectService = new ProjectService(projectRepository, cloudinaryService, prodcutService, groupService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProjectAsync(context);

            AutoMapperConfig.RegisterMappings(typeof(DetailsProjectViewModel).Assembly);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => projectService.GetById<DetailsProjectViewModel>("Тестово id"));
        }

        [Fact]
        public async Task TestGetCountOfPageByTypeOfProjectAndTypeOfLoaction_WithCorrectDate_ShouldReturenCorrectCount()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var projectRepository = new EfDeletableEntityRepository<Project>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var projectService = new ProjectService(projectRepository, cloudinaryService, prodcutService, groupService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProjectAsync(context);

            // Act
            var expected = projectService.GetCountByTypeOfChamber(TypeLocation.Corner.ToString(), TypeProject.Classic.ToString());
            var actual = context.Projects.Count();

            // Assert
            Assert.True(expected == actual, string.Format(ErrorMessage, string.Format(ErrorMessage, "GetCountByTypeOfProjectAndTypeOfLocation")));
        }

        [Fact]
        public async Task EditProject_WithCorrectData_ShouldSuccessfullyEdit()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var projectRepository = new EfDeletableEntityRepository<Project>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var projectService = new ProjectService(projectRepository, cloudinaryService, prodcutService, groupService);

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

            var project = new EditProjectViewModel
            {
                Id = "abc1",
                Description = "Some description test 1",
                ImagePath = file,
                TypeProject = TypeProject.Classic.ToString(),
                TypeLocation = TypeLocation.Corner.ToString(),
                Name = "Проект 1 сменено",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProjectAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(EditProjectViewModel).Assembly);
            var result = await projectService.EditAsync<EditProjectViewModel>(project);

            var actual = context.Projects.FirstOrDefault(x => x.Product.Name == "Проект 1 сменено");

            var expectedName = "Проект 1 сменено";
            var expectedDescription = "Some description test 1";
            var expectedTypeOfProject = TypeProject.Classic.ToString();
            var expectedTypeLocation = TypeLocation.Corner.ToString();

            // Assert
            AssertExtension.EqualsWithMessage(expectedName, actual.Product.Name, string.Format(ErrorMessage, "EditProject returns correct Name"));
            AssertExtension.EqualsWithMessage(expectedDescription, actual.Description, string.Format(ErrorMessage, "EditProject returns correct Description"));
            AssertExtension.EqualsWithMessage(expectedTypeOfProject, actual.TypeProject.ToString(), string.Format(ErrorMessage, "EditProject returns correct TypeOfProject"));
            AssertExtension.EqualsWithMessage(expectedTypeLocation, actual.TypeLocation.ToString(), string.Format(ErrorMessage, "EditProject returns correct TypeLocation"));
        }

        [Fact]
        public async Task EditProject_WithNonExistingFireplace_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var projectRepository = new EfDeletableEntityRepository<Project>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var projectService = new ProjectService(projectRepository, cloudinaryService, prodcutService, groupService);

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

            var project = new EditProjectViewModel
            {
                Id = "Test Id",
                Description = "Some description test 1",
                TypeProject = TypeProject.Classic.ToString(),
                TypeLocation = TypeLocation.Corner.ToString(),
                Name = "Проект 1",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProjectAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(EditProjectViewModel).Assembly);
            await Assert.ThrowsAsync<NullReferenceException>(() => projectService.EditAsync<EditProjectViewModel>(project));
        }

        [Fact]
        public async Task DeleteProject_WithCorrectData_ShouldSuccessfullyDelete()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var projectRepository = new EfDeletableEntityRepository<Project>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var projectService = new ProjectService(projectRepository, cloudinaryService, prodcutService, groupService);

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

            var project = new DeleteProjectViewModel
            {
                Id = "abc1",
                Description = "Some description test 1",
                TypeProject = TypeProject.Classic.ToString(),
                TypeLocation = TypeLocation.Corner.ToString(),
                Name = "Проект 1",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProjectAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DeleteProjectViewModel).Assembly);
            await projectService.DeleteAsync<DeleteProjectViewModel>(project);
            int actual = context.Projects.Count();

            // Assert
            Assert.True(actual == 1, string.Format(ErrorMessage, "DeleteProject method"));
        }

        [Fact]
        public async Task DeleteProject_WithNonExistingProject_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var projectRepository = new EfDeletableEntityRepository<Project>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var projectService = new ProjectService(projectRepository, cloudinaryService, prodcutService, groupService);

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

            var project = new DeleteProjectViewModel
            {
                Id = "Test Id",
                Description = "Some description test 1",
                ImagePath = "Some dummy data",
                TypeProject = TypeProject.Classic.ToString(),
                TypeLocation = TypeLocation.Corner.ToString(),
                Name = "Проект 1",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProjectAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(DeleteProjectViewModel).Assembly);
            await Assert.ThrowsAsync<NullReferenceException>(() => projectService.DeleteAsync<DeleteProjectViewModel>(project));
        }

        [Fact]
        public async Task TestGetAllrojects_WithoutGivenType_ShouldReturnAllProject()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var projectRepository = new EfDeletableEntityRepository<Project>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var projectService = new ProjectService(projectRepository, cloudinaryService, prodcutService, groupService);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProjectAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AllProjectViewModel).Assembly);
            var result = projectService.GetAll<AllProjectViewModel>();
            var count = result.ToList().Count;
            var actual = context.Projects.Count();

            // Assert
            Assert.True(count == actual, string.Format(ErrorMessage, "GetAllProject"));
        }
    }
}
