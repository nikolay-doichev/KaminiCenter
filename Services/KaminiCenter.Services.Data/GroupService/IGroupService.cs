namespace KaminiCenter.Services.Data.GroupService
{
    using System.Threading.Tasks;

    using KaminiCenter.Data.Models;

    public interface IGroupService
    {
        Task CreateAsync(string name);

        Product_Group FindByGroupName(string name);
    }
}
