using KaminiCenter.Web.Infrastructure.CustomAttributes;

namespace KaminiCenter.Web.ViewModels.Fireplace
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    using static KaminiCenter.Web.ViewModels.ModelValidation;
    using static KaminiCenter.Web.ViewModels.ModelValidation.Fireplace;

    public class EditFireplaceViewModel : IMapFrom<Fireplace_chamber>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Display(Name = "Име на продукта")]
        [Required(ErrorMessage = EmptyFieldLengthError)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = NameError)]
        public string Name { get; set; }

        [Display(Name = "Тип на камерата")]
        [Required(ErrorMessage = EmptyFieldLengthError)]
        public string TypeOfChamber { get; set; }

        [Display(Name = "Мощност")]
        [Required(ErrorMessage = EmptyFieldLengthError)]
        [StringLength(PowerMaxLength, MinimumLength = PowerMinLength, ErrorMessage = PowerError)]
        public string Power { get; set; }

        [Display(Name = "Размери")]
        [Required(ErrorMessage = EmptyFieldLengthError)]
        [StringLength(SizeMaxLength, MinimumLength = SizeMinLength, ErrorMessage = SizeError)]
        public string Size { get; set; }

        [Display(Name = "Димоотвод")]

        [Required(ErrorMessage = EmptyFieldLengthError)]
        [StringLength(ChimneyMaxLength, MinimumLength = ChimneyMinLength, ErrorMessage = ChimneyError)]
        public string Chimney { get; set; }

        [Display(Name = "Цена на продукта")]
        [Range(1, int.MaxValue)]
        public decimal Price { get; set; }

        [Display(Name = "Описание на продукта")]
        [Required(ErrorMessage = EmptyFieldLengthError)]
        [StringLength(DescriptionMaxLength, ErrorMessage = DescriptionError)]
        public string Description { get; set; }

        [Display(Name = "Снимка на продукта")]
        [MaxFileSize(FireplaceImageMaxSize)]
        [AllowedExtensions]
        public IFormFile ImagePath { get; set; }

        public string ProductProductId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Fireplace_chamber, EditFireplaceViewModel>()
                .ForMember(
                    destination => destination.ImagePath,
                    opts => opts.Ignore())
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
