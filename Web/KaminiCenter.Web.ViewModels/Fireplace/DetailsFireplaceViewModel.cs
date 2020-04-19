namespace KaminiCenter.Web.ViewModels.Fireplace
{
    using System.Collections.Generic;

    using AutoMapper;
    using Ganss.XSS;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Comment;

    public class DetailsFireplaceViewModel : IMapFrom<Fireplace_chamber>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ProductId { get; set; }

        public string TypeOfChamber { get; set; }

        public string Power { get; set; }

        public string Size { get; set; }

        public string Chimney { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        public string ImagePath { get; set; }

        public IEnumerable<SuggestProductViewModel> SuggestProducts { get; set; }

        public IEnumerable<IndexCommentViewModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Fireplace_chamber, DetailsFireplaceViewModel>()
                .ForMember(
                destination => destination.Name,
                opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
