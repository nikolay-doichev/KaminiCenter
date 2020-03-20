namespace KaminiCenter.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using KaminiCenter.Common;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
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

        [Authorize]
        public IActionResult Add()
        {
            var createFireplaceInputModel = new FireplaceInputModel();

            return this.View(createFireplaceInputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(FireplaceInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.fireplaceService.AddFireplaceAsync(inputModel);
            var typeOfChamber = Enum.Parse<TypeOfChamber>(inputModel.TypeOfChamber);

            return this.RedirectToAction("All", new { type = typeOfChamber });
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
        public IActionResult Edit(string name)
        {
            var viewModel = this.fireplaceService.GetByName<DetailsFireplaceViewModel>(name);

            return this.View(viewModel);
        }
    }
}
