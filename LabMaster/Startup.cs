using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LabMaster.Startup))]
namespace LabMaster
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
