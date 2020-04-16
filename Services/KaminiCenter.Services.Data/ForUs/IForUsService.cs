namespace KaminiCenter.Services.Data.ForUs
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Web.ViewModels.ContactForm;

    public interface IForUsService
    {
        Task SendEmailToAdmin(ContactFormViewModel model);
    }
}
