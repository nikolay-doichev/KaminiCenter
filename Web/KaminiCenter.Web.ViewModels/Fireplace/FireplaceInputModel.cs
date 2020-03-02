using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KaminiCenter.Web.ViewModels.Fireplace
{
    public class FireplaceInputModel
    {
        [Display(Name = "Име на продукта")]
        public string Name { get; set; }

        [Display(Name = "Тип на продукта")]

        public string Group { get; set; }

        [Display(Name = "Тип на камерата")]

        public string TypeOfChamber { get; set; }

        [Display(Name = "Мощност")]

        public string Power { get; set; }

        [Display(Name = "Размери")]

        public string Size { get; set; }

        [Display(Name = "Димоотвод")]

        public string Chimney { get; set; }

        [Display(Name = "Цена на продукта")]

        public decimal Price { get; set; }

        [Display(Name = "Описание на продукта")]

        public string Description { get; set; }

        public IFormFile ImagePath { get; set; }
    }
}
