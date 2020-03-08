namespace KaminiCenter.Services.Data.FireplaceServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using KaminiCenter.Web.ViewModels.Fireplace;

    public interface IFireplaceService
    {
        Task AddFireplaceAsync(AddFireplaceInputModel fireplaceInputModel);

        IEnumerable<T> GetAllFireplaceAsync<T>();
    }
}
