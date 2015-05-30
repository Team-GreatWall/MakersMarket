using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MakersMarket.Web.Startup))]
namespace MakersMarket.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
