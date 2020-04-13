namespace KaminiCenter.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services.Data.ProjectsService;
    using KaminiCenter.Web.ViewModels.Projects;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProjectController : AdministrationController
    {
        private readonly IProjectService projectService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProjectController(
            IProjectService projectService,
            UserManager<ApplicationUser> userManager)
        {
            this.projectService = projectService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var createProjectInputModel = new AddProjectInputModel();

            return this.View(createProjectInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProjectInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.projectService.AddProjectAsync(inputModel, user.Id.ToString());
            var typeOfChamber = Enum.Parse<TypeProject>(inputModel.TypeProject);
            var typeLocation = Enum.Parse<TypeLocation>(inputModel.TypeLocation);

            this.TempData["SuccessfullyCreateProject"] = $"Успешно създаден проект {inputModel.Name}";

            return this.RedirectToAction("All", "Project", new { typeLocation = typeLocation, type = typeOfChamber, area = string.Empty });
        }

        [HttpGet]
        [Route("Project/Edit/{id}")]
        public IActionResult Edit(string id)
        {
            var viewModel = this.projectService.GetById<EditProjectViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("Project/Edit/{id}")]
        public async Task<IActionResult> Edit(EditProjectViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var viewModel = await this.projectService.EditAsync<EditProjectViewModel>(model);

            return this.RedirectToAction("Details", "Project", new { name = model.Name, area = string.Empty });
        }

        [HttpGet]
        [Route("Project/Delete/{id}")]
        public IActionResult Delete(string id)
        {
            var viewModel = this.projectService.GetById<DeleteProjectViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("Project/Delete/{id}")]
        public async Task<IActionResult> Delete(DeleteProjectViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var typeOfProject = this.projectService.GetById<DeleteProjectViewModel>(model.Id).TypeProject;
            var getTypeOfLoaction = this.projectService.GetById<DeleteProjectViewModel>(model.Id).TypeLocation;

            await this.projectService.DeleteAsync<DeleteProjectViewModel>(model);

            var type = Enum.Parse<TypeProject>(typeOfProject);
            var typeLocation = Enum.Parse<TypeLocation>(getTypeOfLoaction);

            return this.RedirectToAction("All", "Project", new { typeLocation = typeLocation, type = typeOfProject, area = string.Empty });
        }
    }
}
