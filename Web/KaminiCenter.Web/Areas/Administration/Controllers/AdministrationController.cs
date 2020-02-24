namespace KaminiCenter.Web.Areas.Administration.Controllers
{
    using KaminiCenter.Common;
    using KaminiCenter.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
