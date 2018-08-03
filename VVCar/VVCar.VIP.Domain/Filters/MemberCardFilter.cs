using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 会员卡过滤器
    /// </summary>
    public class MemberCardFilter : BasePageFilter
    {
        /// <summary>
        /// 卡片编号
        /// </summary>
        [Display(Name = "卡片编号")]
        public string Code { get; set; }

        /// <summary>
        /// 起始卡号
        /// </summary>
        [Display(Name = "起始卡号")]
        public string StartCode { get; set; }

        /// <summary>
        /// 终止卡号
        /// </summary>
        [Display(Name = "终止卡号")]
        public string EndCode { get; set; }

        /// <summary>
        /// 批次代码
        /// </summary>
        [Display(Name = "批次代码")]
        public string BatchCode { get; set; }

        /// <summary>
        /// 是否是预生成
        /// </summary>
        public bool? IsGenerate { get; set; }

        /// <summary>
        /// 卡片类型
        /// </summary>
        public Guid? CardTypeID { get; set; }

        /// <summary>
        /// 卡片状态
        /// </summary>
        public ECardStatus? CardStatus { get; set; }

        /// <summary>
        /// 生成规则 （卡片前4位）+XXXX（4位数递增）
        /// </summary>
        public string GenerateRule { get; set; }

        /// <summary>
        /// 卡片张数
        /// </summary>
        public uint? Count { get; set; }

        /// <summary>
        /// 卡片余额
        /// </summary>
        public decimal? CardBalance { get; set; }

        /// <summary>
        ///验证码
        /// </summary>
        public string VerifyCode { get; set; }

        /// <summary>
        ///手机号
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 会员分组
        /// </summary>
        public Guid? MemberGroupID { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public Guid? MemberGradeID { get; set; }

        /// <summary>
        /// 卡片编号列表
        /// </summary>
        public List<string> Codes { get; set; }
    }
}
