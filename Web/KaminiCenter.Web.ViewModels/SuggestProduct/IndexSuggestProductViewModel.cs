namespace KaminiCenter.Web.ViewModels.SuggestProduct
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class IndexSuggestProductViewModel : IMapFrom<SuggestProduct>, IMapFrom<Fireplace_chamber>, IMapFrom<Finished_Model>, IMapFrom<Project>, IMapFrom<Accessorie>, IHaveCustomMappings
    {
        public string FireplaceId { get; set; }

        public string ProductId { get; set; }

        public string Name { get; set; }

        public string Fireplace_chamberImagePath { get; set; }

        public string Finished_ModelImagePath { get; set; }

        public string ProjectImagePath { get; set; }

        public string AccessorieImagePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, IndexSuggestProductViewModel>()
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Name));

            configuration.CreateMap<Fireplace_chamber, IndexSuggestProductViewModel>()
                .ForMember(
                destination => destination.Fireplace_chamberImagePath,
                opts => opts.MapFrom(origin => origin.ImagePath));

            configuration.CreateMap<Finished_Model, IndexSuggestProductViewModel>()
                .ForMember(
                destination => destination.Finished_ModelImagePath,
                opts => opts.MapFrom(origin => origin.ImagePath));

            configuration.CreateMap<Project, IndexSuggestProductViewModel>()
                .ForMember(
                destination => destination.ProjectImagePath,
                opts => opts.MapFrom(origin => origin.ImagePath));

            configuration.CreateMap<Accessorie, IndexSuggestProductViewModel>()
                .ForMember(
                destination => destination.AccessorieImagePath,
                opts => opts.MapFrom(origin => origin.ImagePath));
        }
    }
}
