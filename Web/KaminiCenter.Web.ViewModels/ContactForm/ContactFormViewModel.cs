namespace KaminiCenter.Web.ViewModels.ContactForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class ContactFormViewModel : IMapFrom<ContactFormRecords>
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Име и Фамилия")]
        public string FullName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Вашият Email адрес")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Относно")]
        public string Subject { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }
    }
}
