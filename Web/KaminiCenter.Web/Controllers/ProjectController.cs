namespace KaminiCenter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using KaminiCenter.Common;
    using KaminiCenter.Services.Data.ProjectsService;
    using KaminiCenter.Web.ViewModels.Projects;
    using Microsoft.AspNetCore.Mvc;

    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [Route("Project/All/{typeLocation}/{type}/{page?}")]
        public IActionResult All(string typeLocation, string type, int page = 1)
        {
            int count = this.projectService.GetCountByTypeOfChamber(typeLocation, type);

            var viewModel = new AllProjectViewModel
            {
                Projects =
                    this.projectService.GetAllProjectAsync<IndexProjectViewModel>(
                        typeLocation,
                        type,
                        GlobalConstants.ItemsPerPage,
                        (page - 1) * GlobalConstants.ItemsPerPage),
                PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage),
                CurrentPage = page,
            };

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            this.TempData["returnToallProjectstypeLocation"] = typeLocation;
            this.TempData["returnToallProjectsType"] = type;
            return this.View(viewModel);
        }

        public IActionResult Details(string name)
        {
            var viewModel = this.projectService.GetByName<DetailsProjectViewModel>(name);

            return this.View(viewModel);
        }
    }
}
