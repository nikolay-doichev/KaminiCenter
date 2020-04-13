namespace KaminiCenter.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Data.AccessoriesService;
    using KaminiCenter.Web.ViewModels.Accessories;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AccessorieController : AdministrationController
    {
        private readonly IAccessoriesService accessoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public AccessorieController(
            IAccessoriesService accessoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.accessoriesService = accessoriesService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var createAccessorieInputModel = new AddAccessorieInputModel();

            return this.View(createAccessorieInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddAccessorieInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.accessoriesService.AddAccessoriesAsync(inputModel, user.Id.ToString());

            this.TempData["SuccessfullyCreateAccessorie"] = $"Успешно създаден аксесоар {inputModel.Name}";

            return this.RedirectToAction("All", "Accessorie", new { area = string.Empty });
        }

        [HttpGet]
        [Route("Accessorie/Edit/{id}")]
        public IActionResult Edit(string id)
        {
            var viewModel = this.accessoriesService.GetById<EditAccessorieViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("Accessorie/Edit/{id}")]
        public async Task<IActionResult> Edit(EditAccessorieViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var viewModel = await this.accessoriesService.EditAsync<EditAccessorieViewModel>(model);

            return this.RedirectToAction("Details", "Accessorie", new { name = model.Name, area = string.Empty });
        }

        [HttpGet]
        [Route("Accessorie/Delete/{id}")]
        public IActionResult Delete(string id)
        {
            var viewModel = this.accessoriesService.GetById<DeleteAccessorieViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("Accessorie/Delete/{id}")]
        public async Task<IActionResult> Delete(DeleteAccessorieViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.accessoriesService.DeleteAsync<DeleteAccessorieViewModel>(model);

            return this.RedirectToAction("All", new { area = string.Empty });
        }
    }
}
