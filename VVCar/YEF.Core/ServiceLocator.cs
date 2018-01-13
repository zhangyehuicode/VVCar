using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core
{
    /// <summary>
    /// 服务对象定位器
    /// </summary>
    public sealed class ServiceLocator : IServiceProvider
    {
        #region fields

        Func<ILifetimeScope> _GetCurrentScopeProvider;

        #endregion fields

        #region ctor.

        private ServiceLocator()
        {
        }

        #endregion ctor.

        #region properties

        #region Instance

        private static readonly ServiceLocator s_Instance = new ServiceLocator();

        /// <summary>
        /// 服务对象定位器实例
        /// </summary>
        public static ServiceLocator Instance
        {
            get { return s_Instance; }
        }

        #endregion Instance

        /// <summary>
        /// IoC容器
        /// </summary>
        public IContainer Container
        {
            get;
            private set;
        }

        #endregion properties

        #region methods

        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="extendRegister">自定义初始化委托</param>
        public void Initialize(Action<ContainerBuilder> extendRegister)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<YEF.Core.Caching.RuntimeMemoryCache>().As<YEF.Core.Caching.ICache>();

            Type baseType = typeof(IDependency);
            Assembly[] assemblies = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll").Select(m => Assembly.LoadFrom(m)).ToArray();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                .AsSelf()   //自身服务，用于没有接口的类
                .AsImplementedInterfaces()  //接口服务
                                            //.PropertiesAutowired()  //属性注入
                .InstancePerLifetimeScope();    //保证生命周期基于请求

            if (extendRegister != null)
            {
                extendRegister(builder);
            }
            Container = builder.Build();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="implementer"></param>
        /// <param name="services"></param>
        public void RegisterType(Type implementer, params Type[] services)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(implementer)
                .As(services)
                .InstancePerLifetimeScope();
            builder.Update(this.Container, Autofac.Builder.ContainerBuildOptions.None);
        }

        /// <summary>
        /// 泛型注册
        /// </summary>
        /// <param name="implementer"></param>
        /// <param name="services"></param>
        public void RegisterGeneric(Type implementer, params Type[] services)
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(implementer)
                .As(services)
                .InstancePerLifetimeScope();
            builder.Update(this.Container, Autofac.Builder.ContainerBuildOptions.None);
        }

        /// <summary>
        /// 获取泛型服务对象
        /// </summary>
        /// <typeparam name="T">服务对象的类型</typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            var currentScope = this.GetCurrentScope();
            if (false == currentScope.IsRegistered<T>())
                return default(T);
            return currentScope.Resolve<T>();
        }

        /// <summary>
        /// 获取泛型服务对象
        /// </summary>
        /// <typeparam name="T">服务对象的类型</typeparam>
        /// <param name="arguments">服务对象构造器参数</param>
        /// <returns></returns>
        public T GetService<T>(object arguments)
        {
            var currentScope = this.GetCurrentScope();
            if (false == currentScope.IsRegistered<T>())
                return default(T);
            if (arguments == null)
            {
                return currentScope.Resolve<T>();
            }
            var parameters = GetParameters(arguments);
            return currentScope.Resolve<T>(parameters);
        }

        /// <summary>
        /// 获取指定类型的服务对象。
        /// </summary>
        /// <param name="serviceType">一个对象，它指定要获取的服务对象的类型。</param>
        /// <returns>
        ///   <paramref name="serviceType" /> 类型的服务对象。 - 或 - 如果没有 <paramref name="serviceType" /> 类型的服务对象，则为 null。
        /// </returns>
        public object GetService(Type serviceType)
        {
            var currentScope = this.GetCurrentScope();
            if (false == currentScope.IsRegistered(serviceType))
                return null;
            return currentScope.Resolve(serviceType);
        }

        public object GetService(Type serviceType, object arguments)
        {
            var currentScope = this.GetCurrentScope();
            if (false == currentScope.IsRegistered(serviceType))
                return null;
            if (arguments == null)
            {
                return currentScope.Resolve(serviceType);
            }
            var parameters = GetParameters(arguments);
            return currentScope.Resolve(serviceType, parameters);
        }

        private IEnumerable<Autofac.Core.Parameter> GetParameters(object overridedArguments)
        {
            var parameters = new List<Autofac.Core.Parameter>();
            Type argumentsType = overridedArguments.GetType();
            return argumentsType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(property =>
                {
                    var propertyValue = property.GetValue(overridedArguments, null);
                    var propertyName = property.Name;
                    return new NamedParameter(propertyName, propertyValue);
                });
        }

        /// <summary>
        /// 获取当前LifetimeScope
        /// </summary>
        /// <returns></returns>
        ILifetimeScope GetCurrentScope()
        {
            ILifetimeScope currentScope = null;
            if (_GetCurrentScopeProvider != null)
                currentScope = _GetCurrentScopeProvider();
            return currentScope ?? this.Container;
        }

        public void SetScopeProvider(Func<ILifetimeScope> provider)
        {
            _GetCurrentScopeProvider = provider;
        }

        #endregion methods
    }
}