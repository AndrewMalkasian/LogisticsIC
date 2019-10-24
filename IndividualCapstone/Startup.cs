using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IndividualCapstone.Startup))]
namespace IndividualCapstone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
