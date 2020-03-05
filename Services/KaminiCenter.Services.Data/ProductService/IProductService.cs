using KaminiCenter.Services.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KaminiCenter.Services.Data.ProductService
{
    public interface IProductService
    {
        Task AddProductAsync(string name, string groupName);

        Task GetIdByName(string name);
    }
}
