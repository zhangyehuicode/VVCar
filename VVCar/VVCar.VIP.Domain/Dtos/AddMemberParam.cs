using System;
using YEF.Core.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 手动新增会员参数
    /// </summary>
    public class AddMemberParam
    {
        /// <summary>
        /// 会员分组ID
        /// </summary>
        public Guid? MemberGroupID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public ESex Sex { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 会员积分
        /// </summary>
        public double Point { get; set; }

        /// <summary>
        /// 保险到期时间
        /// </summary>
        public DateTime? InsuranceExpirationDate { get; set; }

        /// <summary>
        /// 储值卡金额
        /// </summary>
        public decimal CardBalance { get; set; }
    }
}
