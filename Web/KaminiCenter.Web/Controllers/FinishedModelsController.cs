namespace KaminiCenter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using KaminiCenter.Common;
    using KaminiCenter.Services.Data.FinishedModelService;
    using KaminiCenter.Web.ViewModels.FinishedModels;
    using Microsoft.AspNetCore.Mvc;

    public class FinishedModelsController : Controller
    {
        private readonly IFinishedModelService finishedModel;

        public FinishedModelsController(IFinishedModelService finishedModel)
        {
            this.finishedModel = finishedModel;
        }

        [Route("FinishedModels/All/{type}/{page?}")]
        public IActionResult All(string type, int page = 1)
        {
            int count = this.finishedModel.GetCountByTypeOfProject(type);

            var viewModel = new AllFinishedModelViewModel
            {
                FinishedModels =
                    this.finishedModel.GetAllFinishedModelsAsync<IndexFinishedModelViewModel>(
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

            this.TempData["returnToallModelsType"] = type;
            return this.View(viewModel);
        }

        public IActionResult Details(string name)
        {
            var viewModel = this.finishedModel.GetByName<DetailsFinishedModelsViewModel>(name);

            return this.View(viewModel);
        }
    }
}
