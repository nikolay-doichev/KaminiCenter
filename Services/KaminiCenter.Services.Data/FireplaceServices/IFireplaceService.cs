namespace KaminiCenter.Services.Data.FireplaceServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using KaminiCenter.Web.ViewModels.Fireplace;

    public interface IFireplaceService
    {
        Task<string> AddFireplaceAsync(FireplaceInputModel fireplaceInputModel);

        IEnumerable<T> GetAllFireplaceAsync<T>(string type);

        T GetByName<T>(string name);

        T GetById<T>(string id);
    }
}
