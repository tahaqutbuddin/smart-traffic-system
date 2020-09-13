using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Smart_Trafic_Management_System.Startup))]
namespace Smart_Trafic_Management_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
