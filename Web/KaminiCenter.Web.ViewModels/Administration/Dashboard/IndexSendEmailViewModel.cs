namespace KaminiCenter.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class IndexSendEmailViewModel : IMapFrom<ContactFormRecords>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString =>
            this.CreatedOn.Hour == 0 && this.CreatedOn.Minute == 0
                ? this.CreatedOn.ToString("ddd, dd MMM yyyy", new CultureInfo("bg-BG"))
                : this.CreatedOn.ToString("ddd, dd MMM yyyy HH:mm", new CultureInfo("bg-BG"));

        public string Subject { get; set; }

        public string Content { get; set; }

        public string Answer { get; set; }

        public DateTime? DateSendAnswer { get; set; }

        public string DateSendAnswerAsString =>
            this.DateSendAnswer.Value.Hour == 0 && this.DateSendAnswer.Value.Minute == 0
                ? this.DateSendAnswer.Value.ToString("ddd, dd MMM yyyy", new CultureInfo("bg-BG"))
                : this.DateSendAnswer.Value.ToString("ddd, dd MMM yyyy HH:mm", new CultureInfo("bg-BG"));
    }
}
