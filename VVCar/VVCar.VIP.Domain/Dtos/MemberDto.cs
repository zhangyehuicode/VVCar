using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using YEF.Core;
using YEF.Core.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 会员信息DTO
    /// </summary>
    public class MemberDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 微信OpenID
        /// </summary>
        public string WeChatOpenID { get; set; }

        /// <summary>
        /// 会员卡ID
        /// </summary>
        public Guid CardID { get; set; }

        /// <summary>
        /// 会员卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 会员卡类型
        /// </summary>
        public MemberCardType CardType { get; set; }

        /// <summary>
        /// 卡片类型描述
        /// </summary>
        public string CardTypeDesc { get; set; }

        /// <summary>
        /// 会员卡状态
        /// </summary>
        public ECardStatus CardStatus { get; set; }

        /// <summary>
        /// 会员卡状态
        /// </summary>
        public string Status
        {
            get
            {
                return CardStatus.GetDescription();
            }
        }

        /// <summary>
        /// 卡余额
        /// </summary>
        public decimal CardBalance { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public ESex Sex { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 手机号码归属地
        /// </summary>
        public string PhoneLocation { get; set; }

        /// <summary>
        /// 归属门店(第一次消费所在门店)
        /// </summary>
        public string OwnerDepartment { get; set; }

        /// <summary>
        /// 会员分组ID
        /// </summary>
        public Guid? MemberGroupID { get; set; }

        /// <summary>
        /// 会员分组
        /// </summary>
        public string MemberGroup { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public string MemberGradeName { get; set; }

        /// <summary>
        /// 会员积分
        /// </summary>
        public double Point { get; set; }

        /// <summary>
        /// 获取或设置 生效日期, 激活时间
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 获取或设置 截止日期
        /// </summary>
        public DateTime? ExpiredDate { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 会员卡数量
        /// </summary>
        public int MemberCardCount { get; set; }

        /// <summary>
        /// 总消费次数
        /// </summary>
        public int TotalConsumeTime { get; set; }

        /// <summary>
        /// 总消费
        /// </summary>
        public decimal TotalConsume { get; set; }
    }
}
