namespace KaminiCenter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using KaminiCenter.Data.Common.Models;
    using KaminiCenter.Data.Models.Enums;

    public class Fireplace_chamber : BaseDeletableModel<string>
    {
        [Required]
        public string Power { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public string Chimney { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public TypeOfChamber TypeOfChamber { get; set; }

        [Required]
        public string ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public string GroupId { get; set; }

        public Product_Group Group { get; set; }
    }
}
