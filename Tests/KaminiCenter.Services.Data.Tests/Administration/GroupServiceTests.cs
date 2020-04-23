using KaminiCenter.Data.Models.Enums;

namespace KaminiCenter.Services.Data.Tests.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Repositories;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.Tests.Common;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Comment;
    using KaminiCenter.Web.ViewModels.ContactForm;
    using Xunit;

    public class GroupServiceTests
    {
        private const string ErrorMessage = "Method: \"{0}\", does not work properly!";

        [Fact]
        public async Task AddGroupToDataBase_WithCorrectData_ShouldSuccessfullyAdd()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var groupService = new GroupService(groupRepository);

            // Act
            await groupService.CreateAsync(GroupType.Fireplace.ToString());

            var actual = context.Product_Groups.Count();

            // Assert
            Assert.True(actual == 1, string.Format(ErrorMessage, "GroupAdd"));
        }
    }
}
