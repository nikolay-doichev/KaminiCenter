namespace KaminiCenter.Services.Data.ForUs
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Web.ViewModels.ContactForm;

    public interface IForUsService
    {
        IEnumerable<T> GetAllSendEmails<T>();

        Task SendEmailToAdmin(ContactFormViewModel model);

        Task SendAnswer(string contentAnswer, string emailId);
    }
}
