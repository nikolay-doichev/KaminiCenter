namespace KaminiCenter.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using KaminiCenter.Services.Data;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Services.Data.ForUs;
    using KaminiCenter.Web.ViewModels.Administration.Dashboard;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IForUsService forUsService;
        private readonly IFireplaceService fireplaceService;

        public DashboardController(
            ISettingsService settingsService,
            IForUsService forUsService,
            IFireplaceService fireplaceService)
        {
            this.settingsService = settingsService;
            this.forUsService = forUsService;
            this.fireplaceService = fireplaceService;
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
                allFireplaces = allFireplaces.Where(f => f.Name.Contains(searchTerm));
            }

            return this.View(allFireplaces);
        }

        public IActionResult FillingSuggestion(string productName)
        {
            var model = this.fireplaceService.GetByName<DetailsFireplaceViewModel>(productName);

            return this.View(model);
        }
    }
}
