namespace KaminiCenter.Services.Data.CommentServices
{
    using System;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Messaging;
    using KaminiCenter.Web.ViewModels.Comment;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;
        private readonly IProductService productService;
        private readonly IEmailSender emailSender;

        public CommentService(
            IDeletableEntityRepository<Comment> commentRepository,
            IProductService productService,
            IEmailSender emailSender)
        {
            this.commentRepository = commentRepository;
            this.productService = productService;
            this.emailSender = emailSender;
        }

        public async Task Create(CreateCommentInputModel model)
        {
            var product = this.productService.GetIdByNameAndGroup(model.ProductName, GroupType.Fireplace.ToString());

            if (product == null)
            {
                throw new ArgumentNullException(string.Format("Product with Id: {0} does not exist.", model.ProductId));
            }

            var comment = new Comment
            {
                Id = Guid.NewGuid().ToString(),
                Content = model.Content,
                ProductId = product,
                IsDeleted = false,
                CreatedOn = DateTime.UtcNow,
            };

            await this.emailSender.SendEmailAsync(
                model.Email,
                model.FullName,
                "nikolay.doichev@gmail.com",
                "Запитване",
                model.Content);

            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }
    }
}
