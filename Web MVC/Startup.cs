using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web_MVC.Startup))]
namespace Web_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
