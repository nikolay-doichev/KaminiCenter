namespace KaminiCenter.Services.Data.GroupService
{
    using System.Threading.Tasks;

    using KaminiCenter.Data.Models;

    public interface IGroupService
    {
        Task CreateAsync(string name);

        Task FindById(string id);

        Product_Group FindByGroupName(string name);
    }
}
