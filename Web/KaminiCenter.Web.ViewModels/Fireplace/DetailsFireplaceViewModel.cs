namespace KaminiCenter.Web.ViewModels.Fireplace
{
    using AutoMapper;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class DetailsFireplaceViewModel : IMapFrom<Fireplace_chamber>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string TypeOfChamber { get; set; }

        public string Power { get; set; }

        public string Size { get; set; }

        public string Chimney { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Fireplace_chamber, DetailsFireplaceViewModel>()
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
