namespace KaminiCenter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using KaminiCenter.Data.Common.Models;
    using KaminiCenter.Data.Models.Enums;

    public class Finished_Model : BaseDeletableModel<string>
    {
        public Finished_Model()
        {
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        public string Name { get; set; }

        public TypeProject TypeProject { get; set; }

        public string ImagePath { get; set; }

        [Required]

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
