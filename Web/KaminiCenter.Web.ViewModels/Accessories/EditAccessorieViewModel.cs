using KaminiCenter.Web.Infrastructure.CustomAttributes;

namespace KaminiCenter.Web.ViewModels.Accessories
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    using static KaminiCenter.Web.ViewModels.ModelValidation;
    using static KaminiCenter.Web.ViewModels.ModelValidation.Accessorie;

    public class EditAccessorieViewModel : IMapFrom<KaminiCenter.Data.Models.Accessorie>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Display(Name = "Име на проекта")]
        [Required(ErrorMessage = EmptyFieldLengthError)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = NameError)]
        public string Name { get; set; }

        [Display(Name = "Тип на продукта")]
        public string Group { get; set; }

        [MaxFileSize(AccessorieImageMaxSize)]
        [Display(Name = "Снимка на проекта")]
        [AllowedExtensions]
        public IFormFile ImagePath { get; set; }

        [Display(Name = "Описание на проекта")]
        public string Description { get; set; }

        public string ProductProductId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Accessorie, EditAccessorieViewModel>()
                .ForMember(
                    destination => destination.ImagePath,
                    opts => opts.Ignore())
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
