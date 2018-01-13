using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using YEF.Core;

namespace VVCar
{
    public partial class Startup
    {
        void ConfigureWebApp(IAppBuilder app)
        {
            ConfigureAutofac(app);
        }

        /// <summary>
        /// 配置Autofac.
        /// </summary>
        /// <param name="app"></param>
        void ConfigureAutofac(IAppBuilder app)
        {
            //http://docs.autofac.org/en/latest/integration/webapi.html#owin-integration
            //ServiceLocator.Instance.SetScopeProvider(() =>
            //{
            //    var httpContext = HttpContext.Current;
            //    if (httpContext == null)
            //        return null;
            //    try
            //    {
            //        if (httpContext.CurrentHandler is System.Web.Http.WebHost.HttpControllerHandler)
            //        {
            //            var request = httpContext.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
            //            if (request == null)
            //                return null;
            //            return request.GetDependencyScope().GetRequestLifetimeScope();
            //        }
            //        if (httpContext.Items.Contains(_OwinIdentity))
            //        {
            //            var owinContext = httpContext.Request.GetOwinContext();
            //            return owinContext.GetAutofacLifetimeScope();
            //        }
            //    }
            //    catch { }
            //    return null;
            //});
            // Register the Autofac middleware FIRST, then the Autofac Web API middleware,
            // and finally the standard Web API middleware.
            app.UseAutofacMiddleware(ServiceLocator.Instance.Container);
        }
    }
}