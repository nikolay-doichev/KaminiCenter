namespace KaminiCenter.Web.ViewModels.Fireplace
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class AllFireplaceWithoutParameter : IMapFrom<Fireplace_chamber>
    {
        public IEnumerable<IndexFireplaceViewModel> Fireplaces { get; set; }
    }
}
