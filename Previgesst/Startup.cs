
using Microsoft.Owin;
using NLog;
using Owin;

[assembly: OwinStartupAttribute(nameof(Previgesst), typeof(Previgesst.Startup))]
namespace Previgesst
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IoCConfig.RegisterDependencies(app);
            ConfigureAuth(app);
        }
    }
}
