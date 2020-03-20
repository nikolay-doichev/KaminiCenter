namespace KaminiCenter.Web.ViewModels.Fireplace
{
    using System.Collections.Generic;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class AllFireplaceViewModel : IMapFrom<Fireplace_chamber>
    {
        public IEnumerable<IndexFireplaceViewModel> Fireplaces { get; set; }
    }
}
