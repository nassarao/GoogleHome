using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoogleHome.Startup))]
namespace GoogleHome
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
