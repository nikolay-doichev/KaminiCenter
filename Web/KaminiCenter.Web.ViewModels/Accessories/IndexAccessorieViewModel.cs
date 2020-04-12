namespace KaminiCenter.Web.ViewModels.Accessories
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using KaminiCenter.Services.Mapping;

    public class IndexAccessorieViewModel : IMapFrom<Data.Models.Accessorie>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Accessorie, IndexAccessorieViewModel>()
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
