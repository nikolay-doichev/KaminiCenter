namespace KaminiCenter.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class FireplaceController : AdministrationController
    {
        private readonly IFireplaceService fireplaceService;
        private readonly UserManager<ApplicationUser> userManager;

        public FireplaceController(
            IFireplaceService fireplaceService,
            UserManager<ApplicationUser> userManager)
        {
            this.fireplaceService = fireplaceService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var createFireplaceInputModel = new FireplaceInputModel();

            return this.View(createFireplaceInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FireplaceInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.fireplaceService.AddFireplaceAsync<FireplaceInputModel>(inputModel, user.Id.ToString());
            var typeOfChamber = Enum.Parse<TypeOfChamber>(inputModel.TypeOfChamber);
            this.TempData["SuccessfullyCreateFireplace"] = $"Успешно създадена камина {inputModel.Name}";

            return this.RedirectToAction("All", "Fireplace", new { type = typeOfChamber, area = string.Empty });
        }

        [HttpGet]
        [Route("Fireplace/Edit/{id}")]
        public IActionResult Edit(string id)
        {
            var viewModel = this.fireplaceService.GetById<EditFireplaceViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("Fireplace/Edit/{id}")]
        public async Task<IActionResult> Edit(EditFireplaceViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var viewModel = await this.fireplaceService.EditAsync<EditFireplaceViewModel>(model);

            return this.RedirectToAction("Details", new { name = model.Name, area = string.Empty });
        }

        [HttpGet]
        [Route("Fireplace/Delete/{id}")]
        public IActionResult Delete(string id)
        {
            var viewModel = this.fireplaceService.GetById<DeleteFireplaceViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("Fireplace/Delete/{id}")]
        public async Task<IActionResult> Delete(DeleteFireplaceViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var typeOfChamber = this.fireplaceService.GetById<DeleteFireplaceViewModel>(model.Id).TypeOfChamber;

            await this.fireplaceService.DeleteAsync<DeleteFireplaceViewModel>(model);

            var type = Enum.Parse<TypeOfChamber>(typeOfChamber);

            return this.RedirectToAction("All", new { type = typeOfChamber, area = string.Empty });
        }
    }
}
