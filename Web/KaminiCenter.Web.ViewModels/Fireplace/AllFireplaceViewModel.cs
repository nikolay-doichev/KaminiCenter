namespace KaminiCenter.Web.ViewModels.Fireplace
{
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Services.Models.Fireplace;

    public class AllFireplaceViewModel : IMapFrom<AllFireplaceViewModel>
    {
        public string Name { get; set; }

        public string Power { get; set; }

        public string ImagePath { get; set; }
    }
}
