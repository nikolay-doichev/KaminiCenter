namespace KaminiCenter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using KaminiCenter.Data.Common.Models;
    using KaminiCenter.Data.Models.Enums;

    public class Product_Group : BaseDeletableModel<string>
    {
        public GroupType GroupName { get; set; }
    }
}
