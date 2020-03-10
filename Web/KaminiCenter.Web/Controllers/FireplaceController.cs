namespace KaminiCenter.Web.Controllers
{
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
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class FireplaceController : Controller
    {
        private readonly IFireplaceService fireplaceService;
        private readonly IEnumParseService enumParseService;
        private readonly ICloudinaryService cloudinaryService;

        public FireplaceController(IFireplaceService fireplaceService, IEnumParseService enumParseService, ICloudinaryService cloudinaryService)
        {
            this.fireplaceService = fireplaceService;
            this.enumParseService = enumParseService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Add()
        {
            var createFireplaceInputModel = new FireplaceInputModel();

            return this.View(createFireplaceInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FireplaceInputModel inputModel)
        {
            var fireplaceInputModel = this.fireplaceService.AddFireplaceAsync(inputModel);

            var photoUrl = await this.cloudinaryService.UploadPhotoAsync(
               inputModel.ImagePath,
               $"{inputModel.Group}_{inputModel.Name}_{inputModel.Id}",
               GlobalConstants.CloudFolderForFireplacePhotos);

            //inputModel.ImagePath = photoUrl;

            return this.Redirect("/Fireplace/All");
        }

        public IActionResult All(string type)
        {
            var viewModel = new AllFireplaceViewModel
            {
                Fireplaces =
                    this.fireplaceService.GetAllFireplaceAsync<IndexFireplaceViewModel>(type).Where(x => x.TypeOfChamber == type),
            };

            return this.View(viewModel);
        }

        public IActionResult Details(string name)
        {
            var viewModel = this.fireplaceService.GetByName<DetailsFireplaceViewModel>(name);

            viewModel.TypeOfChamber = this.enumParseService.GetEnumDescription(viewModel.TypeOfChamber, typeof(TypeOfChamber));

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile formFile)
        {
            // await this.cloudinary.UploadPhotoAsync(formFile,)
            return this.Redirect("/");
        }
    }
}
