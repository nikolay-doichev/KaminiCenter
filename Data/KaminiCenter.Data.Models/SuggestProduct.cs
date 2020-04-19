namespace KaminiCenter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using KaminiCenter.Data.Common.Models;

    public class SuggestProduct : BaseDeletableModel<string>
    {
        public string FireplaceId { get; set; }

        public Fireplace_chamber Fireplace_Chamber { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }
    }
}
