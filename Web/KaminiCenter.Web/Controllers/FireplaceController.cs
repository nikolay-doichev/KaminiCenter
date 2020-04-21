namespace KaminiCenter.Web.Controllers
{
    using System;
    using System.Linq;

    using KaminiCenter.Common;
    using KaminiCenter.Services;
    using KaminiCenter.Services.Data.CommentServices;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Services.Data.SuggestProduct;
    using KaminiCenter.Web.ViewModels.Comment;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using KaminiCenter.Web.ViewModels.SuggestProduct;
    using Microsoft.AspNetCore.Mvc;

    public class FireplaceController : Controller
    {
        private readonly IFireplaceService fireplaceService;
        private readonly ICommentService commentService;
        private readonly ISuggestProduct suggestService;

        public FireplaceController(
            IFireplaceService fireplaceService,
            ICommentService commentService,
            ISuggestProduct suggestService)
        {
            this.fireplaceService = fireplaceService;
            this.commentService = commentService;
            this.suggestService = suggestService;
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
            var suggestion = this.suggestService.GetAllSuggestion<IndexSuggestProductViewModel>(viewModel.Id);

            viewModel.Comments = comments;
            viewModel.SuggestProducts = suggestion;

            return this.View(viewModel);
        }
    }
}
