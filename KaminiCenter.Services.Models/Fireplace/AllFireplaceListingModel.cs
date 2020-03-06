using KaminiCenter.Data.Models;
using KaminiCenter.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace KaminiCenter.Services.Models.Fireplace
{
    public class AllFireplaceViewModel : IMapTo<Fireplace_chamber>, IMapFrom<Fireplace_chamber>
    {
        public string Name { get; set; }

        public string Power { get; set; }

        public string ImagePath { get; set; }
    }
}
