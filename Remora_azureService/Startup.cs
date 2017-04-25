using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Remora_azureService.Startup))]

namespace Remora_azureService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}