namespace KaminiCenter.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class FireplaceController : Controller
    {
        private readonly IFireplaceService fireplaceService;
        private readonly IEnumParseService enumParseService;

        public FireplaceController(IFireplaceService fireplaceService, IEnumParseService enumParseService)
        {
            this.fireplaceService = fireplaceService;
            this.enumParseService = enumParseService;
        }

        public IActionResult All(string type)
        {
            var viewModel = new AllFireplaceViewModel
            {
                Fireplaces =
                    this.fireplaceService.GetAllFireplaceAsync<IndexFireplaceViewModel>(type)
                    .Where(x => x.TypeOfChamber == type),
            };

            return this.View(viewModel);
        }

        public IActionResult Details(string name)
        {
            var viewModel = this.fireplaceService.GetByName<DetailsFireplaceViewModel>(name);

            // viewModel.TypeOfChamber = this.enumParseService.GetEnumDescription(viewModel.TypeOfChamber, typeof(Data.Models.Enums.TypeOfChamber));
            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        [Route("Fireplace/Delete/{id}")]
        public IActionResult Delete(string id)
        {
            var viewModel = this.fireplaceService.GetById<DeleteFireplaceViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
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

            return this.RedirectToAction("All", new { type = typeOfChamber });
        }
    }
}
