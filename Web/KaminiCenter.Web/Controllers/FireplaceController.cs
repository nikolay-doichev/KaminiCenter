namespace KaminiCenter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using KaminiCenter.Services.Data.FireplaceServices;
    using Microsoft.AspNetCore.Mvc;

    public class FireplaceController : Controller
    {
        private readonly IFireplaceService fireplaceService;

        public FireplaceController(IFireplaceService fireplaceService)
        {
            this.fireplaceService = fireplaceService;
        }

        public IActionResult Add()
        {
            return this.View();
        }
    }
}
