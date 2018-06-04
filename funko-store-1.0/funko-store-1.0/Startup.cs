using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(funko_store_1._0.Startup))]
namespace funko_store_1._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
