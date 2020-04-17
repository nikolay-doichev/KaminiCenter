namespace KaminiCenter.Web.Areas.Administration.Controllers
{
    using KaminiCenter.Services.Data;
    using KaminiCenter.Services.Data.ForUs;
    using KaminiCenter.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IForUsService forUsService;

        public DashboardController(
            ISettingsService settingsService,
            IForUsService forUsService)
        {
            this.settingsService = settingsService;
            this.forUsService = forUsService;
        }

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
    }
}
