namespace KaminiCenter.Web.ViewModels.Fireplace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AddFireplaceInputModel
    {
        public string PorductId { get; set; }
        public string GroupId { get; set; }
        public string Power { get; set; }

        public string Size { get; set; }

        public string Chimney { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string TypeOfChamber { get; set; }

    }
}
