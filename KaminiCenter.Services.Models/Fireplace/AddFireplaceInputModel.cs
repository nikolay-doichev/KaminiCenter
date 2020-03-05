namespace KaminiCenter.Web.ViewModels.Fireplace
{
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AddFireplaceInputModel : IMapTo<Fireplace_chamber>, IMapFrom<Fireplace_chamber>
    {
        
        public string Name { get; set; }

        public string Group { get; set; }

        public string TypeOfChamber { get; set; }

        public string Power { get; set; }

        public string Size { get; set; }

        public string Chimney { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
        public string ImagePath { get; set; }

    }
}
