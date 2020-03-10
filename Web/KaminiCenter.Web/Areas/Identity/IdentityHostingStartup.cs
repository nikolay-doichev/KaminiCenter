using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(KaminiCenter.Web.Areas.Identity.IdentityHostingStartup))]

namespace KaminiCenter.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}
