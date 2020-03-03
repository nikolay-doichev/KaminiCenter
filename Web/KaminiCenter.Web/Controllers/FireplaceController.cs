namespace KaminiCenter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using KaminiCenter.Common;
    using KaminiCenter.Services;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class FireplaceController : Controller
    {
        private readonly IFireplaceService fireplaceService;
        private readonly IHostingEnvironment environment;

        public FireplaceController(IFireplaceService fireplaceService, IHostingEnvironment environment)
        {
            this.fireplaceService = fireplaceService;
            this.environment = environment;
        }

        public IActionResult Add()
        {
            var createFireplaceInputModel = new FireplaceInputModel();

            return this.View(createFireplaceInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FireplaceInputModel inputModel)
        {
            using (var fileStream = new FileStream($@"C:\Temp\{inputModel.Name}.jpeg", FileMode.Create))
            {
                await inputModel.ImagePath.CopyToAsync(fileStream);
            }

            return null;
        }

        public IActionResult Details()
        {
            //return this.PhysicalFile();

            return this.View();
        }
    }
}
