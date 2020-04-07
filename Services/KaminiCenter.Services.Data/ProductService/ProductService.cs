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
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services.Common.Exceptions;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Mapping;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IGroupService groupService;

        public ProductService(IDeletableEntityRepository<Product> productRepository, IGroupService groupService)
        {
            this.productRepository = productRepository;
            this.groupService = groupService;
        }

        public async Task AddProductAsync(string name, string groupType, string userId)
        {
            var group = this.groupService.FindByGroupName(groupType);

            if (group == null)
            {
                throw new ArgumentNullException("There isn`t such group name");
            }

            var product = new Product
            {
                // TO Check for Id Initializesion
                Id = Guid.NewGuid().ToString(),
                Name = name,
                GroupId = group.Id,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
                UserId = userId,
            };

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var product = this.GetById(id);

            product.DeletedOn = DateTime.UtcNow;
            product.IsDeleted = true;
            this.productRepository.Update(product);

            await this.productRepository.SaveChangesAsync();
        }

        public Product GetById(string id)
        {
            var product = this.productRepository
                .All()
                .Where(f => f.Id == id).FirstOrDefault();

            if (product == null)
            {
                throw new ArgumentNullException(
                    string.Format(string.Format(ExceptionsServices.Null_Product_Id_ErrorMessage, id)));
            }

            return product;
        }

        public string GetIdByNameAndGroup(string name, string groupName)
        {
            var inputGroupName = Enum.Parse<GroupType>(groupName);

            string productId = this.productRepository.All()
                                                  .Where(p => p.Name == name
                                                  && p.Group.GroupName == inputGroupName)
                                                  .Select(p => p.Id).FirstOrDefault();

            return productId;
        }
    }
}
