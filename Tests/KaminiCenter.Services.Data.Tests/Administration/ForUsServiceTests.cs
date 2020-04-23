namespace KaminiCenter.Services.Data.Tests.Administration
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Repositories;
    using KaminiCenter.Services.Data.CommentServices;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Services.Data.ForUs;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Data.SuggestProduct;
    using KaminiCenter.Services.Data.Tests.Common;
    using KaminiCenter.Services.Data.Tests.Common.Seeder;
    using KaminiCenter.Services.Data.Tests.Extensions;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Accessories;
    using KaminiCenter.Web.ViewModels.Comment;
    using KaminiCenter.Web.ViewModels.ContactForm;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Xunit;

    public class ForUsServiceTests
    {
        private const string ErrorMessage = "Method: \"{0}\", does not work properly!";

        [Fact]
        public async Task TestGetAllComments_WithGivenType_ShouldReturnAllComments()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var contactRecordsRepository = new EfDeletableEntityRepository<ContactFormRecords>(context);

            var emailSender = new FakeEmailSender();
            var forUsService = new ForUsService(contactRecordsRepository, emailSender);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedContactFormRecordsAsync(context);

            AutoMapperConfig.RegisterMappings(typeof(CreateCommentInputModel).Assembly);
            var result = forUsService.GetAllSendEmails<ContactFormViewModel>();
            var expected = result.ToList().Count;
            var actual = context.ContactForms.Count();

            // Assert
            Assert.True(expected == actual, string.Format(ErrorMessage, "GetAllContactForm emails"));
        }

        [Fact]
        public async Task AddComment_WithCorrectData_ShouldSuccessfullyAdd()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var contactRecordsRepository = new EfDeletableEntityRepository<ContactFormRecords>(context);

            var emailSender = new FakeEmailSender();
            var forUsService = new ForUsService(contactRecordsRepository, emailSender);

            var email = new ContactFormViewModel()
            {
                FullName = "Павлина Якимова",
                Email = "random@gmail.com",
                Content = "Some content",
                Subject = "Random subject",
            };

            // Act
            AutoMapperConfig.RegisterMappings(typeof(CreateCommentInputModel).Assembly);
            await forUsService.SendEmailToAdmin(email);

            var actual = context.ContactForms.Count();

            // Assert
            Assert.True(actual == 1, string.Format(ErrorMessage, "SendEmailToAdmin"));
        }

        [Fact]
        public async Task AddAnswerToComment_WithCorrectData_ShouldSuccessfullyAdd()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var contactRecordsRepository = new EfDeletableEntityRepository<ContactFormRecords>(context);

            var emailSender = new FakeEmailSender();
            var forUsService = new ForUsService(contactRecordsRepository, emailSender);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedContactFormRecordsAsync(context);

            // Act
            var result = forUsService.SendAnswer("Тестов отговор", "comId1");
            var expected = "Тестов отговор";
            var actual = context.ContactForms.SingleOrDefault(c => c.Id == "comId1").Answer;

            // Assert
            AssertExtension.EqualsWithMessage(actual, expected, string.Format(ErrorMessage, "CreateAnswer"));
        }

        [Fact]
        public async Task AddAnswerToComment_WithNoneExistingEmailId_ShouldThrowsException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var contactRecordsRepository = new EfDeletableEntityRepository<ContactFormRecords>(context);

            var emailSender = new FakeEmailSender();
            var forUsService = new ForUsService(contactRecordsRepository, emailSender);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedContactFormRecordsAsync(context);

            await Assert.ThrowsAsync<NullReferenceException>(() => forUsService.SendAnswer("Тестов Отговор", "Test Id"));
        }
    }
}
