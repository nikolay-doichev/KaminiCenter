namespace KaminiCenter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using KaminiCenter.Data.Common.Models;
    using Microsoft.AspNetCore.Identity;

    public class KaminiCenterUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public KaminiCenterUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
