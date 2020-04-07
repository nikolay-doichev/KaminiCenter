namespace KaminiCenter.Web.ViewModels.FinishedModels
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class IndexFinishedModelViewModel : IMapFrom<Finished_Model>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string TypeProject { get; set; }

        public string Description { get; set; }

        public string ShortDesciption
        {
            get
            {
                var shortDescription = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]+>", string.Empty));
                return shortDescription.Length > 200
                    ? shortDescription.Substring(0, 200) + "..."
                    : shortDescription;
            }
        }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Finished_Model, IndexFinishedModelViewModel>()
               .ForMember(
               destination => destination.Name,
               opts => opts.MapFrom(origin => origin.Product.Name));
        }
    }
}
