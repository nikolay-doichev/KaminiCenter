namespace KaminiCenter.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Microsoft.AspNetCore.Mvc;

    public class FireplaceController : Controller
    {
        private readonly IFireplaceService fireplaceService;
        private readonly IMapper mapper;
        private readonly IDeletableEntityRepository<Fireplace_chamber> fireplaceRepository;

        public FireplaceController(IFireplaceService fireplaceService, IMapper mapper, IDeletableEntityRepository<Fireplace_chamber> fireplaceRepository)
        {
            this.fireplaceService = fireplaceService;
            this.mapper = mapper;
            this.fireplaceRepository = fireplaceRepository;
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

            string filePath = $@"C:\Temp\{inputModel.Name}";
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await inputModel.ImagePath.CopyToAsync(fileStream);
            }

            fireplaceInputModel.ImagePath = filePath;

            return this.Redirect("/Fireplace/All");
        }

        public IActionResult All()
        {
            var viewModel = new AllFireplaceViewModel 
            {
                Fireplaces = 
                    this.fireplaceService.GetAllFireplaceAsync<IndexFireplaceViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Details(string name)
        {
            return this.View();
        }
    }
}
