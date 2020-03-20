namespace KaminiCenter.Web.ViewModels.Fireplace
{
    using System.Net;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using Ganss.XSS;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class IndexFireplaceViewModel : IMapFrom<Fireplace_chamber>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Power { get; set; }

        public string Size { get; set; }

        public string ImagePath { get; set; }

        public string TypeOfChamber { get; set; }

        public string Description { get; set; }

        public string ShortDesciption
        {
            get
            {
                var shortDescription = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]+>", string.Empty));
                return shortDescription.Length > 200
                    ? shortDescription.Substring(0, 200) + "..."
                    : shortDescription;
            }
        }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Fireplace_chamber, IndexFireplaceViewModel>()
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
