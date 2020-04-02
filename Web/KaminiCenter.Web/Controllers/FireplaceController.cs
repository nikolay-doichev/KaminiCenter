namespace KaminiCenter.Web.Controllers
{
    using System;

    using KaminiCenter.Common;
    using KaminiCenter.Services;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Microsoft.AspNetCore.Mvc;

    public class FireplaceController : Controller
    {
        private readonly IFireplaceService fireplaceService;
        private readonly IEnumParseService enumParseService;

        public FireplaceController(
            IFireplaceService fireplaceService,
            IEnumParseService enumParseService)
        {
            this.fireplaceService = fireplaceService;
            this.enumParseService = enumParseService;
        }

        [Route("Fireplace/All/{type}/{page?}")]
        public IActionResult All(string type, int page = 1)
        {
            int count = this.fireplaceService.GetCountByTypeOfChamber(type);

            var viewModel = new AllFireplaceViewModel
            {
                Fireplaces =
                    this.fireplaceService.GetAllFireplaceAsync<IndexFireplaceViewModel>(
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

            this.TempData["returnToall"] = type;
            return this.View(viewModel);
        }

        public IActionResult Details(string name)
        {
            var viewModel = this.fireplaceService.GetByName<DetailsFireplaceViewModel>(name);

            return this.View(viewModel);
        }
    }
}
