using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using YEF.Utility;

namespace YEF.Core.Config
{
    /// <summary>
    /// YEF Settings
    /// </summary>
    public sealed class YEFSettings
    {
        /// <summary>
        /// Database Settings
        /// </summary>
        public class DbSettings
        {
            /// <summary>
            /// 数据库地址
            /// </summary>
            public string DbServer { get; set; }

            /// <summary>
            /// 数据库名称
            /// </summary>
            public string DbName { get; set; }

            /// <summary>
            /// 用户名
            /// </summary>
            public string UserID { get; set; }

            /// <summary>
            /// 密码
            /// </summary>
            public string Password { get; set; }
        }

        static readonly string _settingFilePath;
        static readonly string _localSettingFilePath;

        #region ctor.

        /// <summary>
        /// YEF Settings
        /// </summary>
        public YEFSettings()
        {
            SystemTitle = "VV车管理系统";
            ServiceRole = EServiceRole.PrivateMaster;
            CompanyCode = string.Empty;
        }

        static YEFSettings()
        {
            if (!Directory.Exists(AppContext.PathInfo.ConfigPath))
                Directory.CreateDirectory(AppContext.PathInfo.ConfigPath);
            _settingFilePath = Path.Combine(AppContext.PathInfo.ConfigPath, "YEF.sets");
            _localSettingFilePath = Path.Combine(AppContext.PathInfo.ConfigPath, "YEF-local.sets");
        }

        #endregion ctor.

        /// <summary>
        /// 系统服务标题
        /// </summary>
        public string SystemTitle { get; set; }

        EServiceRole _serviceRole;

        /// <summary>
        /// 服务类型
        /// </summary>
        public EServiceRole ServiceRole
        {
            get { return _serviceRole; }
            set
            {
                if (_serviceRole == value)
                    return;
                _serviceRole = value;
                switch (_serviceRole)
                {
                    case EServiceRole.OfflineStore:
                        RecordDataUpdateType = Data.DataDirection.None;
                        break;

                    case EServiceRole.OnlineStore:
                        RecordDataUpdateType = Data.DataDirection.Upload;
                        break;

                    default:
                        RecordDataUpdateType = Data.DataDirection.Push;
                        break;
                }
            }
        }

        /// <summary>
        /// 商户号
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 门店编号，ServiceRole为OnlineStore时需要。
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 数据库设置
        /// </summary>
        public DbSettings DbSetting { get; set; }

        /// <summary>
        /// 主服务地址，ServiceRole为OnlineStore时需要。
        /// </summary>
        public string MasterApiDomain { get; set; }

        /// <summary>
        /// 微信集成服务地址
        /// </summary>
        public string WeChatIntegrationService { get; set; }

        /// <summary>
        ///支付宝集成服务地址
        /// </summary>
        public string AlipayIntegrationService { get; set; }

        /// <summary>
        /// 会员服务地址
        /// </summary>
        public string MemberService { get; set; }

        /// <summary>
        /// 在线支付服务地址
        /// </summary>
        public string OnlinePayService { get; set; }

        /// <summary>
        /// 网页支付服务地址
        /// </summary>
        public string WebPayService { get; set; }

        /// <summary>
        /// 云数据中心服务地址
        /// </summary>
        public string YunDataCenterService { get; set; }

        /// <summary>
        /// 上传数据到云数据中心
        /// </summary>
        public bool UploadToDataCenter { get; set; }

        /// <summary>
        /// 从云数据中心获取数据
        /// </summary>
        public bool ReadDataFromDataCenter { get; set; }

        /// <summary>
        /// 站点域名信息
        /// </summary>
        public string SiteDomain { get; set; }

        /// <summary>
        /// T9 集成
        /// </summary>
        public bool T9Integration { get; set; }

        /// <summary>
        /// 记录数据更新类型
        /// </summary>
        [IgnoreDataMember]
        public Data.DataDirection RecordDataUpdateType { get; private set; }

        /// <summary>
        /// 是否是动态商户环境
        /// </summary>
        [IgnoreDataMember]
        public bool IsDynamicCompany
        {
            get
            {
                return ServiceRole == EServiceRole.PublicMaster;
            }
        }

        /// <summary>
        /// 是否是门店服务环境
        /// </summary>
        [IgnoreDataMember]
        public bool IsStoreService
        {
            get
            {
                return ServiceRole == EServiceRole.OfflineStore || ServiceRole == EServiceRole.OnlineStore;
            }
        }

        #region methods

        /// <summary>
        /// 加载 YEFSettings
        /// </summary>
        /// <returns></returns>
        public static YEFSettings LoadSettings()
        {
            var settings = LoadSettings(_settingFilePath);
            if (settings == null)
                settings = new YEFSettings();

            var localSettings = LoadSettings(_localSettingFilePath);
            if (localSettings != null)
            {
                if (localSettings.CompanyCode != null)
                    settings.CompanyCode = localSettings.CompanyCode;
                if (localSettings.DepartmentCode != null)
                    settings.DepartmentCode = localSettings.DepartmentCode;
                if (localSettings.DbSetting != null)
                    settings.DbSetting = localSettings.DbSetting;
                if (localSettings.SiteDomain != null)
                    settings.SiteDomain = localSettings.SiteDomain;
            }
            if (settings.CompanyCode == null)
                settings.CompanyCode = string.Empty;
            return settings;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static YEFSettings LoadSettings(string fileName)
        {
            if (!File.Exists(fileName))
                return null;
            try
            {
                string json = File.ReadAllText(fileName);
                if (string.IsNullOrEmpty(json))
                    return null;
                return JsonHelper.FromJson<YEFSettings>(json);
            }
            catch (Exception ex)
            {
                AppContext.Logger.Error("加载{0}失败", ex, fileName);
                return null;
            }
        }

        /// <summary>
        /// 保存 YEFConfig
        /// </summary>
        /// <returns></returns>
        public void SaveSettings()
        {
            var json = JsonHelper.Serialize(new { CompanyCode, DepartmentCode, DbSetting, SiteDomain }, true, true);
            try
            {
                File.WriteAllText(_localSettingFilePath, json, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                AppContext.Logger.Error("保存{0}失败", ex, _localSettingFilePath);
            }
        }

        #endregion methods
    }

    /// <summary>
    /// 服务类型
    /// </summary>
    public enum EServiceRole
    {
        /// <summary>
        /// 单店服务
        /// </summary>
        OfflineStore,

        /// <summary>
        /// 连锁店服务
        /// </summary>
        OnlineStore,

        /// <summary>
        /// 专属总部服务
        /// </summary>
        PrivateMaster,

        /// <summary>
        /// 共享总部服务
        /// </summary>
        PublicMaster,
    }
}