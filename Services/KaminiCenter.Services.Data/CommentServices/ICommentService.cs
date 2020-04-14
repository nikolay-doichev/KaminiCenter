namespace KaminiCenter.Services.Data.CommentServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Web.ViewModels.Comment;

    public interface ICommentService
    {
        Task Create(CreateCommentInputModel model);

        Task CreateAnswer(string contentAnswer, string commentId);

        IEnumerable<T> GetAllComments<T>(string productId);
    }
}
