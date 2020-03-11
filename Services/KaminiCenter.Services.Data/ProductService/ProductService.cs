namespace KaminiCenter.Services.Data.ProductService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Data;
    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Data.GroupService;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IGroupService groupService;

        public ProductService(IDeletableEntityRepository<Product> productRepository, IGroupService groupService)
        {
            this.productRepository = productRepository;
            this.groupService = groupService;
        }

        public async Task AddProductAsync(string name, string groupName)
        {
            var group = this.groupService.FindByGroupName(groupName);

            if (group == null)
            {
                await this.groupService.CreateAsync(groupName);
            }

            var product = new Product
            {
                // TO Check for Id Initializesion
                Id = Guid.NewGuid().ToString(),
                Name = name,
                GroupId = group.Id,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
            };

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
        }

        public string GetIdByNameAndGroup(string name, string groupName)
        {
            string productId = this.productRepository.All()
                                                  .Where(p => p.Name == name
                                                  && p.Group.GroupName == groupName)
                                                  .Select(p => p.Id).FirstOrDefault();

            return productId;
        }
    }
}
