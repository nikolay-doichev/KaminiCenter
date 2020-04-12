namespace KaminiCenter.Web.ViewModels.Projects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using AutoMapper;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class DeleteProjectViewModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Display(Name = "Име на проекта")]
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
        public string ImagePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Project, DeleteProjectViewModel>()
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
