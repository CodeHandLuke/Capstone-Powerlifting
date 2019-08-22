using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PowerliftingCapstone.Startup))]
namespace PowerliftingCapstone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
