using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlayMusic.Startup))]
namespace PlayMusic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
