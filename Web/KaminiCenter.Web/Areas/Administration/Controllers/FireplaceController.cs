namespace KaminiCenter.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Microsoft.AspNetCore.Mvc;

    public class FireplaceController : AdministrationController
    {
        private readonly IFireplaceService fireplaceService;
        private readonly IEnumParseService enumParseService;

        public FireplaceController(IFireplaceService fireplaceService, IEnumParseService enumParseService)
        {
            this.fireplaceService = fireplaceService;
            this.enumParseService = enumParseService;
        }

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

            await this.fireplaceService.AddFireplaceAsync(inputModel);
            var typeOfChamber = Enum.Parse<TypeOfChamber>(inputModel.TypeOfChamber);

            return this.RedirectToAction("All", new { type = typeOfChamber, area = "Administration" });
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

            return this.RedirectToAction("Details", new { name = model.Name });
        }
    }
}
