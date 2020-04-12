namespace KaminiCenter.Web.ViewModels.Accessories
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class DeleteAccessorieViewModel : IMapFrom<Accessorie>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Display(Name = "Име на проекта")]
        public string Name { get; set; }

        [Display(Name = "Тип на продукта")]
        public string Group { get; set; }

        [Display(Name = "Описание на проекта")]
        public string Description { get; set; }

        [Display(Name = "Снимка на проекта")]
        public string ImagePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Accessorie, DeleteAccessorieViewModel>()
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
