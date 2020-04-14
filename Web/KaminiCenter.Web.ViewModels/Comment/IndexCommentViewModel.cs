namespace KaminiCenter.Web.ViewModels.Comment
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using KaminiCenter.Services.Mapping;

    public class IndexCommentViewModel : IMapFrom<KaminiCenter.Data.Models.Comment>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string Email { get; set; }

        public string Answer { get; set; }
    }
}
