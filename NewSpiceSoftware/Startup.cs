using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewSpiceSoftware.Startup))]
namespace NewSpiceSoftware
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
