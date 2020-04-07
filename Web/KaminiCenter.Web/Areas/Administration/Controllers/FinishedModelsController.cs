namespace KaminiCenter.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services.Data.FinishedModelService;
    using KaminiCenter.Web.ViewModels.FinishedModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class FinishedModelsController : AdministrationController
    {
        private readonly IFinishedModelService finishedModelService;
        private readonly UserManager<ApplicationUser> userManager;

        public FinishedModelsController(
            IFinishedModelService finishedModelService,
            UserManager<ApplicationUser> userManager)
        {
            this.finishedModelService = finishedModelService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var createFinishedModelInputModel = new AddFinishedModelInputModel();

            return this.View(createFinishedModelInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddFinishedModelInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.finishedModelService.AddFinishedModelAsync(inputModel, user.Id.ToString());

            var typeOfProject = Enum.Parse<TypeProject>(inputModel.TypeProject);
            this.TempData["SuccessfullyCreateFinishedModel"] = $"Успешно създаден модел с име: {inputModel.Name}";

            return this.RedirectToAction("All", "FinishedModels", new { type = typeOfProject, area = string.Empty });
        }

        [HttpGet]
        [Route("FinishedModels/Edit/{id}")]
        public IActionResult Edit(string id)
        {
            var viewModel = this.finishedModelService.GetById<EditFinishedModelViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("FinishedModels/Edit/{id}")]
        public async Task<IActionResult> Edit(EditFinishedModelViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var viewModel = await this.finishedModelService.EditAsync<EditFinishedModelViewModel>(model);

            return this.RedirectToAction("Details", "FinishedModels", new { name = model.Name, area = string.Empty });
        }

        [HttpGet]
        [Route("FinishedModels/Delete/{id}")]
        public IActionResult Delete(string id)
        {
            var viewModel = this.finishedModelService.GetById<DeleteFinishedViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("FinishedModels/Delete/{id}")]
        public async Task<IActionResult> Delete(DeleteFinishedViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var typeOfProject = this.finishedModelService.GetById<DeleteFinishedViewModel>(model.Id).TypeProject;

            await this.finishedModelService.DeleteAsync<DeleteFinishedViewModel>(model);

            var type = Enum.Parse<TypeProject>(typeOfProject);

            return this.RedirectToAction("All", "FinishedModels", new { type = typeOfProject, area = string.Empty });
        }
    }
}
