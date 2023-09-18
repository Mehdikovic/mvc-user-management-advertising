using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppPortfolio.Startup))]
namespace AppPortfolio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
