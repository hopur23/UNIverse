using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UNIverse.Startup))]
namespace UNIverse
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
