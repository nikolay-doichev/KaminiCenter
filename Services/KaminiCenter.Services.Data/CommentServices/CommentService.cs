namespace KaminiCenter.Services.Data.CommentServices
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services.Data.FireplaceServices;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Messaging;
    using KaminiCenter.Web.ViewModels.Comment;
    using KaminiCenter.Web.ViewModels.Fireplace;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;
        private readonly IProductService productService;
        private readonly IFireplaceService fireplaceService;
        private readonly IEmailSender emailSender;

        public CommentService(
            IDeletableEntityRepository<Comment> commentRepository,
            IProductService productService,
            IFireplaceService fireplaceService,
            IEmailSender emailSender)
        {
            this.commentRepository = commentRepository;
            this.productService = productService;
            this.fireplaceService = fireplaceService;
            this.emailSender = emailSender;
        }

        public async Task Create(CreateCommentInputModel model)
        {
            var product = this.productService.GetIdByNameAndGroup(model.ProductName, GroupType.Fireplace.ToString());
            var fireplace = this.fireplaceService.GetById<DetailsFireplaceViewModel>(model.ProductId);

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

            var contentEmail = new StringBuilder();
            var content = GeneratEmailContent(model, fireplace, contentEmail, comment);

            await this.emailSender.SendEmailAsync(
                model.Email,
                model.FullName,
                "nikolay.doichev@gmail.com",
                "Запитване",
                content);

            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        private static string GeneratEmailContent(CreateCommentInputModel model, DetailsFireplaceViewModel fireplace, StringBuilder contentEmail, Comment comment)
        {
            contentEmail.AppendLine(@$"Получен коментар за: 
            <div style='font - family: inherit; text - align: inherit'><em><strong>{model.ProductName}</strong></em></div> 
            от тип 
            <div style='font - family: inherit; text - align: inherit'><em><strong>{fireplace.TypeOfChamber}</strong></em></div>");

            contentEmail.AppendLine($@"<img src='{fireplace.ImagePath}' alt='{fireplace.Name}'>");

            contentEmail.AppendLine(@$"Съдържание на коментарът: 
            <div style='font - family: inherit; text - align: inherit'><em><strong>{model.Content}</strong></em></div>");

            contentEmail.AppendLine(@$"Коментарът е получен на 
            <div style='font - family: inherit; text - align: inherit'><em><strong>{comment.CreatedOn}</strong></em></div> 
            от 
            <div style='font - family: inherit; text - align: inherit'><em><strong>{model.FullName}</strong></em></div> 
            със Email: 
            <div style='font - family: inherit; text - align: inherit'><em><strong>{model.Email}!</strong></em></div>");

            return contentEmail.ToString().TrimEnd();
        }
    }
}
