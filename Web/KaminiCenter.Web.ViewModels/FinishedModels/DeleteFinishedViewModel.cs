namespace KaminiCenter.Web.ViewModels.FinishedModels
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using KaminiCenter.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class DeleteFinishedViewModel : IMapFrom<KaminiCenter.Data.Models.Finished_Model>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Display(Name = "Име на продукта")]
        public string Name { get; set; }

        [Display(Name = "Тип на камерата")]
        public string TypeProject { get; set; }

        [Display(Name = "Снимка на продукта")]
        public string ImagePath { get; set; }

        [Display(Name = "Описание на продукта")]
        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<KaminiCenter.Data.Models.Finished_Model, DeleteFinishedViewModel>()
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
