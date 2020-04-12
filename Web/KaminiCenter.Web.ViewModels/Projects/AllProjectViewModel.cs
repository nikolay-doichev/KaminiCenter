namespace KaminiCenter.Web.ViewModels.Projects
{
    using System.Collections.Generic;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class AllProjectViewModel : IMapFrom<Project>
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<IndexProjectViewModel> Projects { get; set; }
    }
}
