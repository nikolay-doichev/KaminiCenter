﻿namespace KaminiCenter.Services.Data.ProductService
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Data;
    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Models.Product;

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
                Name = name,
                GroupId = group.Id,
            };

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
        }
    }
}
