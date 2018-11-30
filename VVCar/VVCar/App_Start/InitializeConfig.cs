using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using YEF.Data;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Session;
using Autofac;
using Autofac.Integration.Owin;
using Autofac.Integration.WebApi;
using VVCar.Providers;
using System.Net.Http;
using VVCar.BaseData.Services;
using YEF.Core.TCP;

namespace VVCar.App_Start
{
    /// <summary>
    /// App初始化
    /// </summary>
    public class InitializeConfig
    {
        const string _OwinIdentity = "owin.Environment";

        public static void Register(HttpConfiguration config)
        {
            AutofacRegister(config);
            DatabaseInitializer.Initialize();
            InitConfigData();
            AppContext.Logger.Info("startup app success.");
            DtoMapper.Initialize();
            //TCPService.Run();
        }

        private static void InitConfigData()
        {
            try
            {
                //配置AppContext信息
            }
            catch (Exception ex)
            {
                AppContext.Logger.Error("InitConfigData failure.", ex);
            }
        }

        private static void AutofacRegister(HttpConfiguration config)
        {
            ServiceLocator.Instance.Initialize((builder) =>
            {
                builder.RegisterType<AspNetSessionProvider>().As<ISessionProvider>();
                builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
                builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
                builder.RegisterType<Repository>().As<IRepository>().InstancePerDependency();
                builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
                builder.RegisterWebApiFilterProvider(config);
            });
            config.DependencyResolver = new AutofacWebApiDependencyResolver(ServiceLocator.Instance.Container);
            ServiceLocator.Instance.SetScopeProvider(() =>
            {
                var httpContext = HttpContext.Current;
                if (httpContext == null)
                    return null;
                try
                {
                    if (httpContext.CurrentHandler is System.Web.Http.WebHost.HttpControllerHandler)
                    {
                        var request = httpContext.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
                        if (request == null)
                            return null;
                        return request.GetDependencyScope().GetRequestLifetimeScope();
                    }
                    if (httpContext.Items.Contains(_OwinIdentity))
                    {
                        var owinContext = httpContext.Request.GetOwinContext();
                        return owinContext.GetAutofacLifetimeScope();
                    }
                }
                catch { }
                return null;
            });
        }
    }
}