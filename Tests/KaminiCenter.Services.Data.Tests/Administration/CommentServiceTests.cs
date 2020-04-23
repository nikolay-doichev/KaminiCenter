namespace KaminiCenter.Services.Data.Tests.Administration
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Data.Repositories;
    using KaminiCenter.Services.Data.CommentServices;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Data.SuggestProduct;
    using KaminiCenter.Services.Data.Tests.Common;
    using KaminiCenter.Services.Data.Tests.Common.Seeder;
    using KaminiCenter.Services.Data.Tests.Extensions;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Accessories;
    using KaminiCenter.Web.ViewModels.Comment;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Xunit;

    public class CommentServiceTests
    {
        private const string ErrorMessage = "Method: \"{0}\", does not work properly!";

        [Fact]
        public async Task TestGetAllComments_WithoutGivenType_ShouldReturnAllComments()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplaceRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);
            var commentsRepository = new EfDeletableEntityRepository<Comment>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var emailSender = new FakeEmailSender();
            var fireplaceService = new FireplaceService(fireplaceRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);
            var commentServices = new CommentService(commentsRepository, prodcutService, fireplaceService, emailSender);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedCommentsAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(AllAccessorieViewModel).Assembly);
            var result = commentServices.GetAllComments<IndexCommentViewModel>("abc");
            var expected = result.ToList().Count;
            var actual = context.Comments.Count();

            // Assert
            Assert.True(expected == actual, string.Format(ErrorMessage, "GetAllCooments"));
        }

        [Fact]
        public async Task AddComment_WithCorrectData_ShouldSuccessfullyAdd()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplaceRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);
            var commentsRepository = new EfDeletableEntityRepository<Comment>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var emailSender = new FakeEmailSender();
            var fireplaceService = new FireplaceService(fireplaceRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);
            var commentServices = new CommentService(commentsRepository, prodcutService, fireplaceService, emailSender);

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

            var comment = new CreateCommentInputModel()
            {
                FullName = "Павлина Якимова",
                Email = "random@gmail.com",
                Content = "Тестов коментар",
                ProductId = "abc1",
                ProductName = "Гк Мая",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(CreateCommentInputModel).Assembly);
            var result = commentServices.Create(comment);

            var actual = context.Comments.Count();

            // Assert
            Assert.True(actual == 1, string.Format(ErrorMessage, "Create"));
        }

        [Fact]
        public async Task AddComment_WithNoneExistingProduct_ShouldThrowException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplaceRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);
            var commentsRepository = new EfDeletableEntityRepository<Comment>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var emailSender = new FakeEmailSender();
            var fireplaceService = new FireplaceService(fireplaceRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);
            var commentServices = new CommentService(commentsRepository, prodcutService, fireplaceService, emailSender);

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

            var comment = new CreateCommentInputModel()
            {
                FullName = "Павлина Якимова",
                Email = "random@gmail.com",
                Content = "Тестов коментар",
                ProductId = "abc1",
                ProductName = "Тестово име",
            };

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedProdcutAsync(context);
            await seeder.SeedFireplacesAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(CreateCommentInputModel).Assembly);
            await Assert.ThrowsAsync<ArgumentNullException>(() => commentServices.Create(comment));
        }

        [Fact]
        public async Task AddAnswerToComment_WithCorrectData_ShouldSuccessfullyAdd()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var fireplaceRepository = new EfDeletableEntityRepository<Fireplace_chamber>(context);
            var suggestItemsReposotory = new EfDeletableEntityRepository<SuggestProduct>(context);
            var commentsRepository = new EfDeletableEntityRepository<Comment>(context);

            var groupService = new GroupService(groupRepository);
            var prodcutService = new ProductService(productRepository, groupService);
            var cloudinaryService = new FakeCloudinary();
            var sugestItemsRepositoryService = new SuggestProdcut(suggestItemsReposotory);
            var emailSender = new FakeEmailSender();
            var fireplaceService = new FireplaceService(fireplaceRepository, groupService, prodcutService, cloudinaryService, sugestItemsRepositoryService);
            var commentServices = new CommentService(commentsRepository, prodcutService, fireplaceService, emailSender);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedCommentsAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(CreateCommentInputModel).Assembly);
            var result = commentServices.CreateAnswer("Тестов отговор", "comId1");
            var expected = "Тестов отговор";
            var actual = context.Comments.SingleOrDefault(c => c.Id == "comId1").Answer;

            // Assert
            AssertExtension.EqualsWithMessage(actual, expected, string.Format(ErrorMessage, "CreateAnswer"));
        }
    }
}
