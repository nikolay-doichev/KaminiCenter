namespace KaminiCenter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using KaminiCenter.Common;
    using KaminiCenter.Services;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class FireplaceController : Controller
    {
        private readonly IFireplaceService fireplaceService;
        private readonly IMapper mapper;

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
            var fireplaceInputModel = inputModel.To<AddFireplaceInputModel>();
            await this.fireplaceService.AddFireplaceAsync(fireplaceInputModel);

            string filePath = $@"C:\Temp\{inputModel.Name}.jpeg";
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await inputModel.ImagePath.CopyToAsync(fileStream);
            }

            fireplaceInputModel.ImagePath = filePath;

            return this.Redirect("/Fireplace/All");
        }

        public async Task<IActionResult> Details()
        {
            //return this.PhysicalFile();

            return this.View();
        }
    }
}
