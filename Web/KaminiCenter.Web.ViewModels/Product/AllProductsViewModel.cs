namespace KaminiCenter.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using KaminiCenter.Web.ViewModels.Accessories;
    using KaminiCenter.Web.ViewModels.FinishedModels;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using KaminiCenter.Web.ViewModels.Projects;

    public class AllProductsViewModel
    {
        public IEnumerable<IndexFireplaceViewModel> Fireplaces { get; set; }

        public IEnumerable<IndexFinishedModelViewModel> FinishedModels { get; set; }

        public IEnumerable<IndexProjectViewModel> Projects { get; set; }

        public IEnumerable<IndexAccessorieViewModel> Accessories { get; set; }
    }
}
