namespace KaminiCenter.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using KaminiCenter.Services.Data;
    using KaminiCenter.Services.Data.AccessoriesService;
    using KaminiCenter.Services.Data.FinishedModelService;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Services.Data.ForUs;
    using KaminiCenter.Services.Data.ProjectsService;
    using KaminiCenter.Web.ViewModels.Accessories;
    using KaminiCenter.Web.ViewModels.Administration.Dashboard;
    using KaminiCenter.Web.ViewModels.FinishedModels;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using KaminiCenter.Web.ViewModels.Product;
    using KaminiCenter.Web.ViewModels.Projects;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IForUsService forUsService;
        private readonly IFireplaceService fireplaceService;
        private readonly IFinishedModelService finishedModelService;
        private readonly IProjectService projectService;
        private readonly IAccessoriesService accessoriesService;

        public DashboardController(
            ISettingsService settingsService,
            IForUsService forUsService,
            IFireplaceService fireplaceService,
            IFinishedModelService finishedModelService,
            IProjectService projectService,
            IAccessoriesService accessoriesService)
        {
            this.settingsService = settingsService;
            this.forUsService = forUsService;
            this.fireplaceService = fireplaceService;
            this.finishedModelService = finishedModelService;
            this.projectService = projectService;
            this.accessoriesService = accessoriesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new AllSendEmailsViewModel
            {
                SendEmails = this.forUsService.GetAllSendEmails<IndexSendEmailViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer(string answerContent, string commentId)
        {
            await this.forUsService.SendAnswer(answerContent, commentId);

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FindFireplaceForFilling(string searchTerm)
        {
            this.ViewData["GetFireplacesDetails"] = searchTerm;

            var allFireplaces = this.fireplaceService.GetAllFireplace<IndexFireplaceViewModel>();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                allFireplaces = allFireplaces.Where(f => f.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return this.View(allFireplaces);
        }

        public IActionResult FillingSuggestion(string productName)
        {
            var allFireplaces = this.fireplaceService.GetAllFireplace<IndexFireplaceViewModel>();
            var allFinishedModels = this.finishedModelService.GetAll<IndexFinishedModelViewModel>();
            var allProjects = this.projectService.GetAll<IndexProjectViewModel>();
            var allAccessorie = this.accessoriesService.GetAllAccessorieAsync<IndexAccessorieViewModel>();

            this.TempData["fireplaceForFill"] = productName;

            var model = this.fireplaceService.GetByName<DetailsFireplaceViewModel>(productName);

            var allProducts = new AllProductsViewModel
            {
                Fireplaces = allFireplaces,
                FinishedModels = allFinishedModels,
                Projects = allProjects,
                Accessories = allAccessorie,
            };

            return this.View(allProducts);
        }

        [HttpPost]
        public async Task<IActionResult> FillingSuggestion(
            string productName,
            string fireplaceId,
            string[] selectedFireplaces,
            string[] selectedFinishedModels,
            string[] selectedProjects,
            string[] selectedAccessories)
        {
            await this.fireplaceService.AddSuggestionToFireplaceAsync(fireplaceId, selectedFireplaces, selectedFinishedModels, selectedProjects, selectedAccessories);

            return this.RedirectToAction("Details", "Fireplace", new { name = productName, area = string.Empty });
        }
    }
}
