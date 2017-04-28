using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrudApp_Web.Startup))]
namespace CrudApp_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
