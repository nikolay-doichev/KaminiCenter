namespace KaminiCenter.Web.ViewModels.Fireplace
{
    using System.Collections.Generic;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class AllFireplaceViewModel : IMapFrom<Fireplace_chamber>
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<IndexFireplaceViewModel> Fireplaces { get; set; }
    }
}
