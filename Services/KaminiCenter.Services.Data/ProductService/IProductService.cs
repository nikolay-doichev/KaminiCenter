namespace KaminiCenter.Services.Data.ProductService
{
    using KaminiCenter.Data.Models;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task AddProductAsync(string name);

        string GetIdByNameAndGroup(string name, string groupName);

        Product GetById(string id);

        Task DeleteAsync(string id);
    }
}
