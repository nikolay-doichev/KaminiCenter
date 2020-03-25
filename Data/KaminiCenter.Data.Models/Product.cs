﻿namespace KaminiCenter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using KaminiCenter.Data.Common.Models;

    public class Product : BaseDeletableModel<string>
    {
        [Required]
        public string Name { get; set; }

        public Product_Group Group { get; set; }

        public string GroupId { get; set; }
    }
}
