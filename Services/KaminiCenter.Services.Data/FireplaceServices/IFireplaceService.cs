namespace KaminiCenter.Services.Data.FireplaceServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using KaminiCenter.Web.ViewModels.Fireplace;

    public interface IFireplaceService
    {
        Task<string> AddFireplaceAsync<T>(FireplaceInputModel fireplaceInputModel, string userId);

        Task AddSuggestionToFireplaceAsync(
            string fireplaceId,
            string[]? selectedFireplaces,
            string[]? selectedFinishedModels,
            string[]? selectedProjects,
            string[]? selectedAccessories);

        Task<string> EditAsync<T>(EditFireplaceViewModel editModel);

        Task DeleteAsync<T>(DeleteFireplaceViewModel deleteModel);

        IEnumerable<T> GetAllFireplaceAsync<T>(string type, int? take = null, int skip = 0);

        IEnumerable<T> GetAllFireplace<T>(int? take = null, int skip = 0);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        int GetCountByTypeOfChamber(string type);
    }
}
