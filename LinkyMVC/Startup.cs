using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LinkyMVC.Startup))]
namespace LinkyMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
