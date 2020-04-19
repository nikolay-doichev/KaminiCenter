namespace KaminiCenter.Web.ViewModels.Comment
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    using AutoMapper;
    using KaminiCenter.Services.Mapping;

    public class IndexCommentViewModel : IMapFrom<KaminiCenter.Data.Models.Comment>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString =>
            this.CreatedOn.Hour == 0 && this.CreatedOn.Minute == 0
                ? this.CreatedOn.ToString("ddd, dd MMM yyyy", new CultureInfo("bg-BG"))
                : this.CreatedOn.ToString("ddd, dd MMM yyyy HH:mm", new CultureInfo("bg-BG"));

        public string Content { get; set; }

        public string Email { get; set; }

        public string Answer { get; set; }
    }
}
