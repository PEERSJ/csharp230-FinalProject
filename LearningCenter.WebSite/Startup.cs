using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LearningCenter.WebSite.Startup))]
namespace LearningCenter.WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
