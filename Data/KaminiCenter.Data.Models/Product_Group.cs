using KaminiCenter.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KaminiCenter.Data.Models
{
    public class Product_Group : BaseDeletableModel<string>
    {
        public string Group_Name { get; set; }
    }
}
