namespace KaminiCenter.Services.Data.ProductService
{
    using System.Threading.Tasks;

    using KaminiCenter.Data.Models;

    public interface IProductService
    {
        Task AddProductAsync(string name, string groupType, string userId);

        string GetIdByNameAndGroup(string name, string groupName);

        Product GetById(string id);

        Task DeleteAsync(string id);
    }
}
