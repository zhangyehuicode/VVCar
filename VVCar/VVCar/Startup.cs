using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using VVCar.App_Start;

[assembly: OwinStartup(typeof(VVCar.Startup))]

namespace VVCar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
            InitializeConfig.Register(System.Web.Http.GlobalConfiguration.Configuration);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureWebApp(app);
            ConfigureAuth(app);
        }
    }
}
