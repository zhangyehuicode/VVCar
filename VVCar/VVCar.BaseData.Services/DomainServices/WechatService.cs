using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
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
            string token = "cheyinzi";
            string[] arrTmp = { token, filter.timestamp, filter.nonce };
            Array.Sort(arrTmp);
            string tmpStr = string.Join("", arrTmp);
            tmpStr = Sha1(tmpStr);
            if(tmpStr == filter.signature)
            {
                if(!string.IsNullOrEmpty(filter.echostr))
                    return filter.signature;
            }
            return "";
        }

        private string Sha1(string str)
        {
            var buffer = Encoding.UTF8.GetBytes(str);
            var data = SHA1.Create().ComputeHash(buffer);

            var sb = new StringBuilder();
             foreach (var t in data)
             {
                 sb.Append(t.ToString("X2"));
             }
             return sb.ToString();
         }
    }
}
