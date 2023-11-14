[assembly: HostingStartup(typeof(GBastos.MicroData.UI.Web.Areas.Identity.IdentityHostingStartup))]
namespace GBastos.MicroData.UI.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}