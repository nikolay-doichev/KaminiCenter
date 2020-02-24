using KaminiCenter.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KaminiCenter.Data.Models
{
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

        public int TypeOfChamber_Id { get; set; }

        [Required]
        public string PorductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public string Group_Id { get; set; }

        public Product_Group Group { get; set; }
    }
}
