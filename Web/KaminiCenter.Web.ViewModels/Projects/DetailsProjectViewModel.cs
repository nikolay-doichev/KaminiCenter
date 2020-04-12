namespace KaminiCenter.Web.ViewModels.Projects
{
    using AutoMapper;
    using Ganss.XSS;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class DetailsProjectViewModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string TypeProject { get; set; }

        public string TypeLocation { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        public string ImagePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Project, DetailsProjectViewModel>()
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
