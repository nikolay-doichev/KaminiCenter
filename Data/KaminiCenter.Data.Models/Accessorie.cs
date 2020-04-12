namespace KaminiCenter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using KaminiCenter.Data.Common.Models;

    public class Accessorie : BaseDeletableModel<string>
    {
        [Required]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public string ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public string GroupId { get; set; }

        public Product_Group Group { get; set; }
    }
}
