using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SisATU.WebUI.Startup))]
namespace SisATU.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
