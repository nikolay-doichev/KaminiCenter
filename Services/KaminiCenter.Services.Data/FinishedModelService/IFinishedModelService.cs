namespace KaminiCenter.Services.Data.FinishedModelService
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Web.ViewModels.FinishedModels;

    public interface IFinishedModelService
    {
        Task<string> AddFinishedModelAsync(AddFinishedModelInputModel finishedModelInputModel, string userId);

        Task<string> EditAsync<T>(EditFinishedModelViewModel editModel);

        Task DeleteAsync<T>(DeleteFinishedViewModel deleteModel);

        IEnumerable<T> GetAllFinishedModelsAsync<T>(string type, int? take = null, int skip = 0);

        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        int GetCountByTypeOfProject(string type);
    }
}
