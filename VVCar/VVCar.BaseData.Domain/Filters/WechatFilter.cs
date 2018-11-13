using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Filters
{
    public class WechatFilter : BaseFilter
    {
        /// <summary>
        /// 微信加密签名
        /// </summary>
        public string signature { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public string nonce { get; set; }

        /// <summary>
        /// 随机数字符串
        /// </summary>
        public string echostr { get; set; }
    }
}
