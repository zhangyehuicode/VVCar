using System;
using System.IO;
using System.Net.Http;
using System.Text;
using YEF.Core.Dtos;
using YEF.Core.Extensions;
using YEF.Utility;

namespace YEF.Core.License
{
    /// <summary>
    /// License
    /// </summary>
    public static class LicenseManager
    {
        /// <summary>
        /// 激活服务器地址
        /// </summary>
        static readonly string _activateServerUrl;

        /// <summary>
        /// 公钥
        /// </summary>
        const string _publicKey = "<RSAKeyValue><Modulus>10T7NCK48T2Ff7VOCbAnwB8lyaLehmd06AealwwyMnWad0+LPb/TM1BLQVCvSTt45Dtp7aY1fT+oKWpG2uPTl1vDRPrjRaIvwAhHux6x8iOr14q5eHc3rhxLmhd5VgMIPInvEb6NYpttGPYOko72zcCkGdwWB4pVePXYoeD+IJ0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        static LicenseManager()
        {
#if DEBUG
            _activateServerUrl = "http://192.168.1.223:10086";
#else
            _activateServerUrl = "http://activate.yunmainetwork.com";
#endif
        }

        /// <summary>
        /// 获取License文件路径
        /// </summary>
        /// <returns></returns>
        static string GetLicenseFilePath()
        {
            if (!Directory.Exists(AppContext.PathInfo.AppDataPath))
                Directory.CreateDirectory(AppContext.PathInfo.AppDataPath);
            return Path.Combine(AppContext.PathInfo.AppDataPath, "license.dat");
        }

        /// <summary>
        /// 获取激活服务地址
        /// </summary>
        /// <returns></returns>
        public static string Activate(ActivateInfo activateInfo)
        {
            activateInfo.HardwareId = HardwareInfo.GetHardwareId();
            var httpClient = new HttpClient();
            var response = httpClient.PostJsonAsync(string.Concat(_activateServerUrl, "/api/activate"), activateInfo).Result;
            var responseData = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var activateResult = JsonHelper.DeserializeObject<JsonActionResult<string>>(responseData);
                if (activateResult == null)
                    throw new Exception("激活服务器返回了错误的结果");
                if (!activateResult.IsSuccessful)
                    throw new Exception(activateResult.ErrorMessage);

                var expiredDate = ImportLicense(activateResult.Data);
                AppContext.Settings.CompanyCode = activateInfo.CompanyCode;
                AppContext.Settings.DepartmentCode = activateInfo.DepartmentCode;
                AppContext.Settings.SaveSettings();
                return expiredDate.ToDateString();
            }
            else
            {
                throw new Exception("激活服务器返回了错误的结果" + response.StatusCode.ToString());
            }
        }

        /// <summary>
        /// 获取许可证信息
        /// </summary>
        /// <returns></returns>
        internal static LicenseInfo GetLicense()
        {
            bool isValid = false;
            DateTime expireDate = DateTime.MinValue;
            var licenseFilePath = GetLicenseFilePath();
            if (!File.Exists(licenseFilePath))
                return new LicenseInfo(isValid, expireDate);
            var licenseString = File.ReadAllText(licenseFilePath, Encoding.UTF8);
            try
            {
                var license = ValidateLicense(licenseString);
                isValid = license.ExpiredDate >= DateTime.Today;
                expireDate = license.ExpiredDate;
                CheckSerialNumber(license.SerialNumber);
            }
            catch (Exception ex)
            {
                AppContext.Logger.Error("GetLicense 发生异常。", ex);
            }
            return new LicenseInfo(isValid, expireDate);
        }

        /// <summary>
        /// 导入License
        /// </summary>
        /// <param name="license">The license.</param>
        /// <returns></returns>
        static DateTime ImportLicense(string license)
        {
            var licenseInfo = ValidateLicense(license);
            SaveLicense(license);
            AppContext.ReloadLicense();
            return licenseInfo.ExpiredDate;
        }

        /// <summary>
        /// 保存License
        /// </summary>
        /// <param name="license"></param>
        static void SaveLicense(string license)
        {
            var licenseFilePath = GetLicenseFilePath();
            if (File.Exists(licenseFilePath))
            {
                var licenseString = File.ReadAllText(licenseFilePath, Encoding.UTF8);
                if (licenseString.Equals(license))
                    return;
            }
            File.WriteAllText(licenseFilePath, license, Encoding.UTF8);
        }

        /// <summary>
        /// 校验License
        /// </summary>
        /// <param name="license"></param>
        static LicenseData ValidateLicense(string license)
        {
            if (string.IsNullOrEmpty(license))
                throw new LicenseException("License不可为空", 0);
            var rsaProvider = new System.Security.Cryptography.RSACryptoServiceProvider();
            rsaProvider.FromXmlString(_publicKey);
            var licenseDataStr = rsaProvider.DecryptUsePublic(license);//sample: hwId={string},ed={yyyy-MM-dd};
            if (string.IsNullOrEmpty(licenseDataStr))
                throw new LicenseException("无效License, errorCode=1", 1);
            var licenseData = licenseDataStr.Split(',');
            if (licenseData.Length != 3)
                throw new LicenseException("无效License, errorCode=2", 2);
            if (licenseData[0].Length < 4)//第1组数据SN，前3位为sn=,如果小于4位数据不正确，sn格式为XXXX-XXXX-XXXX-XXXX。
                throw new LicenseException("无效License, errorCode=3", 3);
            if (licenseData[1].Length < 6)//第2组数据为硬件ID，前5位为hwId=，如果小于6位表示数据不正确。
                throw new LicenseException("无效License, errorCode=4", 4);
            if (licenseData[2].Length < 4)//第3组数据为过期时间,前3位为ed=，如果小于4位表示数据不正确。
                throw new LicenseException("无效License, errorCode=5", 5);

            var sn = licenseData[0].Substring(3);
            var hwId = licenseData[1].Substring(5);
            var localHardwareId = HardwareInfo.GetHardwareId();
            if (!localHardwareId.Equals(hwId, StringComparison.OrdinalIgnoreCase))
                throw new LicenseException("无效License, errorCode=4,1", 41);
            var ed = licenseData[2].Substring(3);
            var expiredDate = DateTime.Parse(ed);
            if (expiredDate < DateTime.Today)
                throw new LicenseException("无效License, errorCode=5,1", 51);
            return new LicenseData { SerialNumber = sn, HardwareId = hwId, ExpiredDate = expiredDate };
        }

        /// <summary>
        /// 检查序列号
        /// </summary>
        /// <param name="serialNumber"></param>
        static void CheckSerialNumber(string serialNumber)
        {
            var checkAction = new Action(() =>
            {
                try
                {
                    var httpClient = new HttpClient();
                    var response = httpClient.PostAsync(string.Concat(_activateServerUrl, "/api/Activate/Check/", serialNumber), null).Result;
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var checkResult = JsonHelper.DeserializeObject<JsonActionResult<string>>(responseData);
                        if (checkResult == null)
                        {
                            AppContext.Logger.Error("CheckSerialNumber失败，激活服务器返回了错误的结果[{0}]。", responseData);
                            return;
                        }
                        if (!checkResult.IsSuccessful)
                        {
                            AppContext.Logger.Error("CheckSerialNumber失败，[{0}]。", responseData);
                            return;
                        }
                        if (!string.IsNullOrEmpty(checkResult.Data))
                        {
                            SaveLicense(checkResult.Data);
                        }
                    }
                    else
                    {
                        AppContext.Logger.Error("激活服务器返回了错误的结果" + response.StatusCode.ToString());
                    }
                }
                catch (Exception ex)
                {
                    AppContext.Logger.Error("CheckSerialNumber失败。", ex);
                }
            });
            checkAction.BeginInvoke(null, null);
        }
    }
}