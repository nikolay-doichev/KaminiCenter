namespace KaminiCenter.Web.ViewModels.Fireplace
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class SuggestProductViewModel : IMapFrom<SuggestProduct>, IMapFrom<Fireplace_chamber>
    {
        public string Name { get; set; }

        public string Power { get; set; }

        public string Size { get; set; }

        public string Chimney { get; set; }

        public decimal Price { get; set; }

        public string TypeProject { get; set; }

        public string ImagePath { get; set; }
    }
}
