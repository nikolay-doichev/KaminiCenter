namespace KaminiCenter.Web.Controllers
{
    using System.IO;
    using System.Threading.Tasks;

    using AutoMapper;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Services.Models.Fireplace;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Microsoft.AspNetCore.Mvc;

    public class FireplaceController : Controller
    {
        private readonly IFireplaceService fireplaceService;
        private readonly IMapper mapper;

        public FireplaceController(IFireplaceService fireplaceService, IMapper mapper)
        {
            this.fireplaceService = fireplaceService;
            this.mapper = mapper;
        }

        public IActionResult Add()
        {
            var createFireplaceInputModel = new FireplaceInputModel();

            return this.View(createFireplaceInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FireplaceInputModel inputModel)
        {
            var fireplaceInputModel = this.mapper.Map<AddFireplaceInputModel>(inputModel);


            await this.fireplaceService.AddFireplaceAsync(fireplaceInputModel);

            string filePath = $@"C:\Temp\{inputModel.Name}.jpeg";
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await inputModel.ImagePath.CopyToAsync(fileStream);
            }

            fireplaceInputModel.ImagePath = filePath;

            return this.Redirect("/Fireplace/All");
        }

        public async Task<IActionResult> All()
        {
            var fireplace = this.fireplaceService.GetAllFireplaceAsync();

            return this.View(fireplace);
        }

        public async Task<IActionResult> Details()
        {
            //return this.PhysicalFile();

            return this.View();
        }
    }
}
