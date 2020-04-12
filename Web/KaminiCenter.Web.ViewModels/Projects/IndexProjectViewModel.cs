namespace KaminiCenter.Web.ViewModels.Projects
{
    using System.Net;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using Ganss.XSS;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class IndexProjectViewModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string TypeProject { get; set; }

        public string TypeLocation { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Project, IndexProjectViewModel>()
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
