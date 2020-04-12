namespace KaminiCenter.Web.ViewModels.Projects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using AutoMapper;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    using static KaminiCenter.Web.ViewModels.ModelValidation;
    using static KaminiCenter.Web.ViewModels.ModelValidation.Project;

    public class AddProjectInputModel : IMapFrom<KaminiCenter.Data.Models.Project>, IHaveCustomMappings
    {
        public AddProjectInputModel()
        {
            this.Group = GroupType.Project.ToString();
        }

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

        [Display(Name = "Описание на проекта")]
        public string Description { get; set; }

        [Display(Name = "Снимка на проекта")]
        public IFormFile ImagePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AddProjectInputModel, KaminiCenter.Data.Models.Project>()
                .ForMember(
                    destination => destination.ImagePath,
                    opts => opts.Ignore());
        }
    }
}
