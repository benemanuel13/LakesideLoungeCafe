using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LakesideLoungeWeb.Startup))]
namespace LakesideLoungeWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.MapSignalR();
        }
    }
}
