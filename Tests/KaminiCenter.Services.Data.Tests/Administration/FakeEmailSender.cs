namespace KaminiCenter.Services.Data.Tests.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Services.Messaging;

    public class FakeEmailSender : IEmailSender
    {
        public bool ReturnNullOnCreate { get; set; } = false;

        public Task SendEmailAsync(
            string @from,
            string fromName,
            string to,
            string subject,
            string htmlContent,
            IEnumerable<EmailAttachment> attachments = null)
        {
            if (this.ReturnNullOnCreate)
            {
                return null;
            }

            return Task.CompletedTask;
        }
    }
}
