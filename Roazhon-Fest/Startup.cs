using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Roazhon_Fest.Startup))]
namespace Roazhon_Fest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
