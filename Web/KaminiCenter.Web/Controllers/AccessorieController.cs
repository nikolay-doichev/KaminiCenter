namespace KaminiCenter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using KaminiCenter.Common;
    using KaminiCenter.Services.Data.AccessoriesService;
    using KaminiCenter.Web.ViewModels.Accessories;
    using Microsoft.AspNetCore.Mvc;

    public class AccessorieController : Controller
    {
        private readonly IAccessoriesService accessoriesService;

        public AccessorieController(IAccessoriesService accessoriesService)
        {
            this.accessoriesService = accessoriesService;
        }

        [Route("Accessorie/All/{page?}")]
        public IActionResult All(int page = 1)
        {
            int count = this.accessoriesService.GetCount();

            var viewModel = new AllAccessorieViewModel
            {
                Accessorie =
                    this.accessoriesService.GetAllAccessorieAsync<IndexAccessorieViewModel>(
                        GlobalConstants.ItemsPerPage,
                        (page - 1) * GlobalConstants.ItemsPerPage),
                PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage),
                CurrentPage = page,
            };

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            return this.View(viewModel);
        }

        [Route("Accessorie/Details/{name}")]

        public IActionResult Details(string name)
        {
            var viewModel = this.accessoriesService.GetByName<DetailsAccessorieViewModel>(name);

            return this.View(viewModel);
        }
    }
}
