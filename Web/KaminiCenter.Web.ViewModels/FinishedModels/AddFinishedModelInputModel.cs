﻿namespace KaminiCenter.Web.ViewModels.FinishedModels
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
    using static KaminiCenter.Web.ViewModels.ModelValidation.Finished_Model;

    public class AddFinishedModelInputModel : IMapFrom<KaminiCenter.Data.Models.Finished_Model>, IHaveCustomMappings
    {
        public AddFinishedModelInputModel()
        {
            this.Group = GroupType.Finished_Models.ToString();
        }

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

        [Display(Name = "Тип на продукта")]
        public string Group { get; set; }

        [Display(Name = "Описание на продукта")]
        public string Description { get; set; }

        [Required]

        public string ProductId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AddFinishedModelInputModel, KaminiCenter.Data.Models.Finished_Model>()
                .ForMember(
                    destination => destination.ImagePath,
                    opts => opts.Ignore());
        }
    }
}