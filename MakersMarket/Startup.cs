using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MakersMarket.Startup))]
namespace MakersMarket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
