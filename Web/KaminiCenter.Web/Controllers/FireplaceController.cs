namespace KaminiCenter.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;    
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Microsoft.AspNetCore.Mvc;

    public class FireplaceController : Controller
    {
        private readonly IFireplaceService fireplaceService;
        private readonly IMapper mapper;
        private readonly IEnumParseService enumParseService;

        public FireplaceController(IFireplaceService fireplaceService, IMapper mapper, IEnumParseService enumParseService)
        {
            this.fireplaceService = fireplaceService;
            this.mapper = mapper;
            this.enumParseService = enumParseService;
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

            string filePath = $@"C:\Temp\{inputModel.Name}";
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await inputModel.ImagePath.CopyToAsync(fileStream);
            }

            //fireplaceInputModel.ImagePath = filePath;

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
    }
}
