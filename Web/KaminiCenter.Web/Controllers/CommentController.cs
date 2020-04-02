namespace KaminiCenter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class CommentController : Controller
    {
        public async Task<IActionResult> CreateComment()
        {
            return this.Ok();
        }
    }
}
