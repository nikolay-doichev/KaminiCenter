namespace KaminiCenter.Services.Data.FireplaceServices
{
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Models.Fireplace;
    using KaminiCenter.Web.ViewModels.Fireplace;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFireplaceService
    {
        Task AddFireplaceAsync(AddFireplaceInputModel fireplaceInputModel);

        IEnumerable<AllFireplaceViewModel> GetAllFireplaceAsync();
    }
}
