namespace KaminiCenter.Web.Controllers
{
    using System.Diagnostics;

    using KaminiCenter.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet("/Home/Index")]
        public IActionResult IndexFullPage()
        {
            return this.Index();
        }

        public IActionResult HttpError(int statusCode)
        {
            return this.View(statusCode);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        /* https://docs.microsoft.com/en-us/aspnet/web-pages/overview/ui-layouts-and-themes/9-working-with-images */
    }
}
