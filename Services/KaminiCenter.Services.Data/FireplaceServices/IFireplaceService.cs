namespace KaminiCenter.Services.Data.FireplaceServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using KaminiCenter.Web.ViewModels.Fireplace;

    public interface IFireplaceService
    {
        Task<string> AddFireplaceAsync(FireplaceInputModel fireplaceInputModel);

        Task<string> EditAsync<T>(EditFireplaceViewModel editModel);

        Task DeleteAsync<T>(DeleteFireplaceViewModel deleteModel);

        IEnumerable<T> GetAllFireplaceAsync<T>(string type);

        T GetByName<T>(string name);

        T GetById<T>(string id);
    }
}
