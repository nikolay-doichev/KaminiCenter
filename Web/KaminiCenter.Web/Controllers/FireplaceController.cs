namespace KaminiCenter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using KaminiCenter.Common;
    using KaminiCenter.Services;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Microsoft.AspNetCore.Mvc;

    public class FireplaceController : Controller
    {
        private readonly IFireplaceService fireplaceService;

        public FireplaceController(IFireplaceService fireplaceService)
        {
            this.fireplaceService = fireplaceService;
        }

        public IActionResult Add()
        {
            var createFireplaceInputModel = new FireplaceInputModel();

            return this.View(createFireplaceInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FireplaceInputModel inputModel)
        {
            //var photoUrl = await this.cloudinaryService.UploadPhotoAsync(
            //    inputModel.ImagePath,
            //    $"{userId}-{recipeCreateInputModel.Title}",
            //    GlobalConstants.CloudFolderForFireplacePhotos);

            //inputModel.ImagePath = photoUrl;

            return null;
        }
    }
}
