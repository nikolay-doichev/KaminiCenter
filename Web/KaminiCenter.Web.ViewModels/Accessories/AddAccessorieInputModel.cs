namespace KaminiCenter.Web.ViewModels.Accessories
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
    using static KaminiCenter.Web.ViewModels.ModelValidation.Accessorie;

    public class AddAccessorieInputModel : IMapFrom<Data.Models.Accessorie>, IHaveCustomMappings
    {
        public AddAccessorieInputModel()
        {
            Group = GroupType.Accessories.ToString();
        }

        public string Id { get; set; }

        [Display(Name = "Име на проекта")]
        [Required(ErrorMessage = EmptyFieldLengthError)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = NameError)]
        public string Name { get; set; }

        [Display(Name = "Тип на продукта")]
        public string Group { get; set; }

        [Display(Name = "Описание на проекта")]
        public string Description { get; set; }

        [Display(Name = "Снимка на проекта")]
        public IFormFile ImagePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AddAccessorieInputModel, Data.Models.Accessorie>()
                .ForMember(
                    destination => destination.ImagePath,
                    opts => opts.Ignore());
        }
    }
}
