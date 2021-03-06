﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 会员注册信息
    /// </summary>
    public class MemberRegisterDto
    {
        /// <summary>
        /// 微信OpenID
        /// </summary>
        [Required]
        public string WeChatOpenID { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 核销密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 在线支付单流水号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 会员等级ID
        /// </summary>
        public Guid? MemberGradeID { get; set; }
    }
}
