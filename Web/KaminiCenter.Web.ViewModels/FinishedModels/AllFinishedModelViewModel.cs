namespace KaminiCenter.Web.ViewModels.FinishedModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class AllFinishedModelViewModel : IMapFrom<Finished_Model>
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<IndexFinishedModelViewModel> FinishedModels { get; set; }
    }
}
