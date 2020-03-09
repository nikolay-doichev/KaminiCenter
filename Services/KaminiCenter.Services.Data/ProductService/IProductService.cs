namespace KaminiCenter.Services.Data.ProductService
{
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task AddProductAsync(string name, string groupName);

        string GetIdByNameAndGroup(string name, string groupName);
    }
}
