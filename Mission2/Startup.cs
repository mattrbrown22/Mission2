using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mission2.Startup))]
namespace Mission2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
