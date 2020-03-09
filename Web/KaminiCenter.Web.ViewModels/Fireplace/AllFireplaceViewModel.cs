namespace KaminiCenter.Web.ViewModels.Fireplace
{
    using System.Collections.Generic;

    using KaminiCenter.Services.Mapping;

    public class AllFireplaceViewModel
    {
        public IEnumerable<IndexFireplaceViewModel> Fireplaces { get; set; }

        public string TypeOfChamber { get; set; }
    }
}
