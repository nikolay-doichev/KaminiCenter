namespace KaminiCenter.Services.Data.FireplaceServices
{
    using KaminiCenter.Web.ViewModels.Fireplace;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFireplaceService
    {
        Task AddFireplaceAsync(AddFireplaceInputModel fireplaceInputModel);
    }
}
