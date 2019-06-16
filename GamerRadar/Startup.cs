using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GamerRadar.Startup))]
namespace GamerRadar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
