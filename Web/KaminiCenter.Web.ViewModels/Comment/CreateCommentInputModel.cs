namespace KaminiCenter.Web.ViewModels.Comment
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using AutoMapper;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    using static KaminiCenter.Web.ViewModels.ModelValidation;
    using static KaminiCenter.Web.ViewModels.ModelValidation.Comment;

    public class CreateCommentInputModel : IMapFrom<KaminiCenter.Data.Models.Comment>, IHaveCustomMappings
    {
        [Display(Name = "Коментар")]
        [Required(ErrorMessage = EmptyFieldLengthError)]
        [StringLength(ContentMaxLength, ErrorMessage = ContentError)]
        public string Content { get; set; }

        [Required]
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        [Display(Name = "Име и Фамилия")]
        [Required(ErrorMessage = EmptyFieldLengthError)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = NameError)]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<KaminiCenter.Data.Models.Comment, CreateCommentInputModel>()
                .ForMember(
                destination => destination.ProductId,
                opts => opts.MapFrom(origin => origin.Product.Id))
                .ForMember(
                destination => destination.ProductName,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
