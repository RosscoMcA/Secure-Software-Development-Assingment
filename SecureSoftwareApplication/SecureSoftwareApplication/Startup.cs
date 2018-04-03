using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecureSoftwareApplication.Startup))]
namespace SecureSoftwareApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
