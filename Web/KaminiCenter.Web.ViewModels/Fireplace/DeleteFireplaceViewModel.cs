namespace KaminiCenter.Web.ViewModels.Fireplace
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AutoMapper;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class DeleteFireplaceViewModel : IMapFrom<Fireplace_chamber>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Display(Name = "Име на продукта")]
        public string Name { get; set; }

        [Display(Name = "Тип на камерата")]

        public string TypeOfChamber { get; set; }

        [Display(Name = "Мощност")]

        public string Power { get; set; }

        [Display(Name = "Размери")]

        public string Size { get; set; }

        [Display(Name = "Димоотвод")]

        public string Chimney { get; set; }

        [Display(Name = "Цена на продукта")]

        public decimal Price { get; set; }

        [Display(Name = "Описание на продукта")]

        public string Description { get; set; }

        [Display(Name = "Снимка на продукта")]
        public string ImagePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Fireplace_chamber, DeleteFireplaceViewModel>()
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
