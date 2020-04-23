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
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Data.SuggestProduct;
    using KaminiCenter.Services.Data.Tests.Common;
    using KaminiCenter.Services.Data.Tests.Common.Seeder;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.SuggestProduct;
    using Xunit;

    public class SuggestServiceTest
    {
        private const string ErrorMessage = "Method: \"{0}\", does not work properly!";

        [Fact]
        public async Task TestGetAllSuggestProducts_WithoutGivenType_ShouldReturnAllSuggestProducts()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();

            var groupRepository = new EfDeletableEntityRepository<Product_Group>(context);
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var suggestProductRepository = new EfDeletableEntityRepository<SuggestProduct>(context);

            var groupService = new GroupService(groupRepository);
            var suggestproductService = new SuggestProdcut(suggestProductRepository);

            var seeder = new DbContextTestsSeeder();
            await seeder.SeedUsersAsync(context);
            await seeder.SeedGroupAsync(context);
            await seeder.SeedSuggestProductAsync(context);

            // Act
            AutoMapperConfig.RegisterMappings(typeof(IndexSuggestProductViewModel).Assembly);
            var result = suggestproductService.GetAllSuggestion<IndexSuggestProductViewModel>("abc1");
            var expected = result.ToList().Count;
            var actual = context.SuggestProducts.Count();

            // Assert
            Assert.True(expected == actual, string.Format(ErrorMessage, "GetAllSuggestion"));
        }
    }
}
