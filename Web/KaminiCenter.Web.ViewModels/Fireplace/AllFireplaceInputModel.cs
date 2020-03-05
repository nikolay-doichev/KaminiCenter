using KaminiCenter.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace KaminiCenter.Web.ViewModels.Fireplace
{
    public class AllFireplaceInputModel : IMapFrom<AddFireplaceInputModel>
    {
        public string Name { get; set; }

        public string Power { get; set; }

        public string ImagePath { get; set; }
    }
}
