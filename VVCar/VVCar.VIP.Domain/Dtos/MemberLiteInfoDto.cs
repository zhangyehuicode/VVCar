using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core;
using YEF.Core.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 会员基本信息
    /// </summary>
    public class MemberLiteInfoDto
    {
        /// <summary>
        /// 卡片类型ID
        /// </summary>
        public Guid? CardTypeID { get; set; }

        /// <summary>
        /// 会员卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public ESex Sex { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string MemberSex { get { return Sex.GetDescription(); } }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 会员卡状态
        /// </summary>
        public ECardStatus CardStatus { get; set; }

        /// <summary>
        /// 会员卡状态
        /// </summary>
        public string Status { get { return CardStatus.GetDescription(); } }

        /// <summary>
        /// 卡余额
        /// </summary>
        public decimal CardBalance { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

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
        public int Point { get; set; }
    }
}
