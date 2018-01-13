using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Config;
using YEF.Core.Context;
using YEF.Core.Logging;
using YEF.Core.Session;

namespace YEF.Core
{
    /// <summary>
    /// 当前App运行上下文
    /// </summary>
    [Serializable]
    public static class AppContext
    {
        #region fields

        private static readonly ISessionProvider _sessionProvider;
        static readonly object _lockObj = new object();

        #endregion

        #region ctor.
        static AppContext()
        {
            _sessionProvider = ServiceLocator.Instance.GetService<ISessionProvider>();
            if (null == _sessionProvider)
            {
                _sessionProvider = new SimpleSession();
            }
            PathInfo = new WebAppPathInfo();
            License = Core.License.LicenseManager.GetLicense();
        }
        #endregion

        #region properties

        /// <summary>
        /// 当前会话
        /// </summary>
        public static ISession CurrentSession
        {
            get
            {
                return _sessionProvider.GetSession();
            }
        }

        /// <summary>
        /// 日志记录器
        /// </summary>
        public static ILogger Logger
        {
            get
            {
                return LoggerManager.GetLogger();
            }
        }

        /// <summary>
        /// 路径信息
        /// </summary>
        public static IAppPathInfo PathInfo { get; private set; }

        /// <summary>
        /// License信息
        /// </summary>
        public static ILicenseInfo License { get; private set; }

        static YEFSettings _settings;

        /// <summary>
        /// 配置信息
        /// </summary>
        public static YEFSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    lock (_lockObj)
                    {
                        if (_settings == null)
                            _settings = YEFSettings.LoadSettings();
                    }
                }
                return _settings;
            }
        }

        /// <summary>
        /// 超级刷卡账户
        /// </summary>
        public static string SuperScanCardAccount
        {
            get { return "-_-YEF_CARD-_-"; }
        }

        /// <summary>
        /// 部门ID
        /// </summary>
        public static Guid? DepartmentID { get; set; }

        /// <summary>
        /// 部门代码
        /// </summary>
        public static string DepartmentCode { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public static string DepartmentName { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// 重新加载License信息
        /// </summary>
        internal static void ReloadLicense()
        {
            License = Core.License.LicenseManager.GetLicense();
        }

        #endregion
    }
}
