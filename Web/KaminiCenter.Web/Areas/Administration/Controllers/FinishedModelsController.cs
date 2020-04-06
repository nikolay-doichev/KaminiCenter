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

            return this.RedirectToAction("All", "FinishedModel", new { type = typeOfProject, area = string.Empty });
        }
    }
}
