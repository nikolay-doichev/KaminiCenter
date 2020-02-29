using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace KaminiCenter.Web.ViewModels.Fireplace
{
    public class FireplaceInputModel
    {
        public string Power { get; set; }

        public string Size { get; set; }

        public string Chimney { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public IFormFile ImagePath { get; set; }

        public string TypeOfChamber { get; set; }
    }
}
