using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FIAP.SI.Web.Startup))]
namespace FIAP.SI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
