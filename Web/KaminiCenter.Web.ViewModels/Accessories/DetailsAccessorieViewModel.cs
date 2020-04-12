namespace KaminiCenter.Web.ViewModels.Accessories
{
    using AutoMapper;
    using Ganss.XSS;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class DetailsAccessorieViewModel : IMapFrom<Accessorie>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        public string ImagePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Accessorie, DetailsAccessorieViewModel>()
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
