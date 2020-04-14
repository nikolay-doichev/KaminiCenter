namespace KaminiCenter.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using KaminiCenter.Services.Data.CommentServices;
    using Microsoft.AspNetCore.Mvc;

    public class CommentController : AdministrationController
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer(string answer, string commentId, string productName)
        {
            await this.commentService.CreateAnswer(answer, commentId);

            return this.RedirectToAction("Details", "Fireplace", new { name = productName, area = string.Empty });
        }
    }
}
