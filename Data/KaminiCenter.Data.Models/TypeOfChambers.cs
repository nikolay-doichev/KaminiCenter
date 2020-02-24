using KaminiCenter.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KaminiCenter.Data.Models
{
    public class TypeOfChambers : BaseDeletableModel<string>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
