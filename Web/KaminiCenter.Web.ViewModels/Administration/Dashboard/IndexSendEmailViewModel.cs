namespace KaminiCenter.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class IndexSendEmailViewModel : IMapFrom<ContactFormRecords>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public string Answer { get; set; }

        public DateTime? DateSendAnswer { get; set; }
    }
}
