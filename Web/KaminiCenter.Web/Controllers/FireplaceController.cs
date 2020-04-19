namespace KaminiCenter.Web.Controllers
{
    using System;
    using System.Linq;

    using KaminiCenter.Common;
    using KaminiCenter.Services;
    using KaminiCenter.Services.Data.CommentServices;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Web.ViewModels.Comment;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using Microsoft.AspNetCore.Mvc;

    public class FireplaceController : Controller
    {
        private readonly IFireplaceService fireplaceService;
        private readonly IEnumParseService enumParseService;
        private readonly ICommentService commentService;

        public FireplaceController(
            IFireplaceService fireplaceService,
            IEnumParseService enumParseService,
            ICommentService commentService)
        {
            this.fireplaceService = fireplaceService;
            this.enumParseService = enumParseService;
            this.commentService = commentService;
        }

        [Route("Fireplace/All/{type}/{page?}")]
        public IActionResult All(string type, int page = 1)
        {
            int count = this.fireplaceService.GetCountByTypeOfChamber(type);

            var viewModel = new AllFireplaceViewModel
            {
                Fireplaces =
                    this.fireplaceService.GetAllFireplaceAsync<IndexFireplaceViewModel>(
                        type,
                        GlobalConstants.ItemsPerPage,
                        (page - 1) * GlobalConstants.ItemsPerPage),
                PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage),
                CurrentPage = page,
            };

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            this.TempData["returnToall"] = type;
            return this.View(viewModel);
        }

        public IActionResult AllWithoutParameter(int page = 1)
        {
            int count = this.fireplaceService.GetAllFireplace<AllFireplaceViewModel>().Count();

            var viewModel = new AllFireplaceViewModel
            {
                Fireplaces = this.fireplaceService.GetAllFireplace<IndexFireplaceViewModel>(
                     GlobalConstants.ItemsPerPage,
                     (page - 1) * GlobalConstants.ItemsPerPage),
                PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage),
                CurrentPage = page,
            };

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            return this.View(viewModel);
        }

        public IActionResult Details(string name)
        {
            var viewModel = this.fireplaceService.GetByName<DetailsFireplaceViewModel>(name);
            var comments = this.commentService.GetAllComments<IndexCommentViewModel>(viewModel.ProductId).ToList();

            viewModel.Comments = comments;

            return this.View(viewModel);
        }
    }
}
