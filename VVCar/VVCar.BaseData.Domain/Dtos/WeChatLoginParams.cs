using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 微信登录参数
    /// </summary>
    public class WeChatLoginParams
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenID { get; set; }

        /// <summary>
        /// 是否是店长
        /// </summary>
        public bool IsManager { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MerchantCode { get; set; }

        /// <summary>
        /// 是否代理商总经理
        /// </summary>
        public bool IsGeneralManager { get; set; }

        /// <summary>
        /// 是否代理商销售经理
        /// </summary>
        public bool IsSalesManger { get; set; }
    }
}
