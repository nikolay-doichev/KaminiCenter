namespace KaminiCenter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using KaminiCenter.Data.Common.Models;

    public class TypeOfChamber : BaseDeletableModel<string>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
