namespace KaminiCenter.Services.Data.ProductService
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Models.Product;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductService(IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task AddProductAsync(AddProductInputModel model)
        {
            var product = new Product
            {
                // TO Check for Id Initializesion
                Name = model.Name,
                GroupId = model.GroupId,
            };

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
        }
    }
}
