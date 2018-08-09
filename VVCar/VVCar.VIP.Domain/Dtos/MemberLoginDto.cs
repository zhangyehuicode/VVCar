using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 会员登录Dto
    /// </summary>
    public class MemberLoginDto
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// openid
        /// </summary>
        public string WeChatOpenID { get; set; }

        /// <summary>
        /// 是否代理商门店登录
        /// </summary>
        public bool IsAgentDeptLogin { get; set; }
    }
}
