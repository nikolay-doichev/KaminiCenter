namespace KaminiCenter.Web.ViewModels.FinishedModels
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using KaminiCenter.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    using static KaminiCenter.Web.ViewModels.ModelValidation;
    using static KaminiCenter.Web.ViewModels.ModelValidation.Finished_Model;

    public class EditFinishedModelViewModel : IMapFrom<KaminiCenter.Data.Models.Finished_Model>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Display(Name = "Име на продукта")]
        [Required(ErrorMessage = EmptyFieldLengthError)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = NameError)]
        public string Name { get; set; }

        [Display(Name = "Тип на камерата")]
        [Required(ErrorMessage = EmptyFieldLengthError)]
        public string TypeProject { get; set; }

        [Display(Name = "Снимка на продукта")]
        public IFormFile ImagePath { get; set; }

        [Display(Name = "Описание на продукта")]
        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<KaminiCenter.Data.Models.Finished_Model, EditFinishedModelViewModel>()
                .ForMember(
                    destination => destination.ImagePath,
                    opts => opts.Ignore())
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
