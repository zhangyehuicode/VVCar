using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.BaseData.Services.DomainServices
{
    /// <summary>
    /// 微信服务
    /// </summary>
    public partial class WechatService : DomainServiceBase<IRepository<Wechat>, Wechat, Guid>, IWechatService
    {
        public WechatService()
        {
        }

        /// <summary>
        /// 微信接入
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string Index(WechatFilter filter)
        {
            string token = "YBCYZ2018";
            string[] arrTmp = { token, filter.timestamp, filter.nonce };
            Array.Sort(arrTmp);
            string tmpStr = string.Join("", arrTmp);
            tmpStr = SHA1(tmpStr);
            if (tmpStr.ToUpper() == filter.signature.ToUpper())
            {
                if (!string.IsNullOrEmpty(filter.echostr))
                {
                    return filter.echostr;
                }
                else {
                    return "";
                }
            }
            return "";
        }

        private string SHA1(string content)
        {
            return SHA1(content, Encoding.UTF8);
        }
         
        private string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错:" + ex.Message);
            }
        }   
    }
}
