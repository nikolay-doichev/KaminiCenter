﻿namespace KaminiCenter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using KaminiCenter.Data.Common.Models;

    public class Product_Group : BaseDeletableModel<string>
    {
        public string GroupName { get; set; }
    }
}