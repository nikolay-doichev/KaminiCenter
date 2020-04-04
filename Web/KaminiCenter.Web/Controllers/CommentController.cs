namespace KaminiCenter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using KaminiCenter.Services.Data.CommentServices;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Web.ViewModels.Comment;
    using Microsoft.AspNetCore.Mvc;

    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IFireplaceService fireplaceService;

        public CommentController(
            ICommentService commentService,
            IFireplaceService fireplaceService)
        {
            this.commentService = commentService;
            this.fireplaceService = fireplaceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentInputModel model)
        {
            await this.commentService.Create(model);

            return this.RedirectToAction("Details", "Fireplace", new { name = model.ProductName, area = string.Empty });
        }
    }
}
