namespace KaminiCenter.Services.Data.ProjectsService
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using KaminiCenter.Web.ViewModels.Accessorie;
    using KaminiCenter.Web.ViewModels.Projects;

    public interface IProjectService
    {
        Task<string> AddProjectAsync(AddProjectInputModel projectInputModel, string userId);

        Task<string> EditAsync<T>(EditProjectViewModel editModel);

        Task DeleteAsync<T>(DeleteProjectViewModel deleteModel);

        IEnumerable<T> GetAllProjectAsync<T>(string typeLocation, string type, int? take = null, int skip = 0);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        int GetCountByTypeOfChamber(string typeLocation, string type);
    }
}
