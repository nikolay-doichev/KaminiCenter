using KaminiCenter.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KaminiCenter.Data.Models
{
    public class Comment : BaseDeletableModel<string>
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string ProductId { get; set; }

        public Product Product { get; set; }
    }
}
