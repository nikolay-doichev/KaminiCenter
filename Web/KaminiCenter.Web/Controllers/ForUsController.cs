namespace KaminiCenter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using KaminiCenter.Services.Data.ForUs;
    using KaminiCenter.Web.ViewModels.ContactForm;
    using Microsoft.AspNetCore.Mvc;

    public class ForUsController : Controller
    {
        private readonly IForUsService forUsService;

        public ForUsController(IForUsService forUsService)
        {
            this.forUsService = forUsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.forUsService.SendEmailToAdmin(model);

            return this.RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
