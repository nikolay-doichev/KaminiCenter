namespace KaminiCenter.Web.ViewModels.Accessories
{
    using System.Collections.Generic;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class AllAccessorieViewModel : IMapFrom<Accessorie>
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<IndexAccessorieViewModel> Accessorie { get; set; }
    }
}
