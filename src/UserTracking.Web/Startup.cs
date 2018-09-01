using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserTracking.Web.Startup))]
namespace UserTracking.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
