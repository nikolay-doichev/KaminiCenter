namespace KaminiCenter.Web.ViewModels.Fireplace
{
    using AutoMapper;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class IndexFireplaceViewModel : IMapFrom<Fireplace_chamber>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Power { get; set; }

        public string Size { get; set; }

        public string ImagePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Fireplace_chamber, IndexFireplaceViewModel>()
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
