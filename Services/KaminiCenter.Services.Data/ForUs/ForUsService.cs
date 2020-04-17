namespace KaminiCenter.Services.Data.ForUs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Services.Messaging;
    using KaminiCenter.Web.ViewModels.ContactForm;

    using static KaminiCenter.Common.GlobalConstants;

    public class ForUsService : IForUsService
    {
        private readonly IDeletableEntityRepository<ContactFormRecords> contactFormRepository;
        private readonly IEmailSender emailSender;

        public ForUsService(
            IDeletableEntityRepository<ContactFormRecords> contactFormRepository,
            IEmailSender emailSender)
        {
            this.contactFormRepository = contactFormRepository;
            this.emailSender = emailSender;
        }

        public IEnumerable<T> GetAllSendEmails<T>()
        {
            IQueryable<ContactFormRecords> sendEmails = this.contactFormRepository
                .All()
                .OrderBy(c => c.CreatedOn);

            return sendEmails.To<T>().ToList();
        }

        public async Task SendAnswer(string contentAnswer, string emailId)
        {
            var sendEmail = this.contactFormRepository
                .All()
                .FirstOrDefault(x => x.Id == emailId);

            sendEmail.Answer = contentAnswer;
            sendEmail.DateSendAnswer = DateTime.UtcNow;

            await this.contactFormRepository.SaveChangesAsync();

            await this.emailSender.SendEmailAsync(
                AdminEmail,
                NameForSendingEmails,
                sendEmail.Email,
                SubjectForSendingEmails,
                contentAnswer);
        }

        public async Task SendEmailToAdmin(ContactFormViewModel model)
        {
            var sendEmail = new ContactFormRecords
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
                FullName = model.FullName,
                Email = model.Email,
                Subject = model.Subject,
                Content = model.Content,
            };

            await this.contactFormRepository.AddAsync(sendEmail);
            await this.contactFormRepository.SaveChangesAsync();

            await this.emailSender.SendEmailAsync(
                model.Email,
                model.FullName,
                AdminEmail,
                model.Subject,
                model.Content);
        }
    }
}
