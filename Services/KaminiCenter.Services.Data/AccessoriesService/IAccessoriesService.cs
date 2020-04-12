namespace KaminiCenter.Services.Data.AccessoriesService
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Web.ViewModels.Accessories;

    public interface IAccessoriesService
    {
        Task<string> AddAccessoriesAsync(AddAccessorieInputModel accessorieInputModel, string userId);

        Task<string> EditAsync<T>(EditAccessorieViewModel editModel);

        Task DeleteAsync<T>(DeleteAccessorieViewModel deleteModel);

        IEnumerable<T> GetAllAccessorieAsync<T>(int? take = null, int skip = 0);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        int GetCount();
    }
}
