namespace KaminiCenter.Web.ViewModels.Projects
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    using static KaminiCenter.Web.ViewModels.ModelValidation;
    using static KaminiCenter.Web.ViewModels.ModelValidation.Project;

    public class EditProjectViewModel : IMapFrom<KaminiCenter.Data.Models.Project>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Display(Name = "Име на проекта")]
        [Required(ErrorMessage = EmptyFieldLengthError)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = NameError)]
        public string Name { get; set; }

        [Display(Name = "Тип на продукта")]
        public string Group { get; set; }

        [Display(Name = "Разположение на камината")]
        public string TypeLocation { get; set; }

        [Display(Name = "Тип на проекта")]
        public string TypeProject { get; set; }

        [Display(Name = "Снимка на проекта")]
        public IFormFile ImagePath { get; set; }

        [Display(Name = "Описание на проекта")]
        public string Description { get; set; }

        public string ProductProductId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<KaminiCenter.Data.Models.Project, EditProjectViewModel>()
                .ForMember(
                    destination => destination.ImagePath,
                    opts => opts.Ignore())
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
