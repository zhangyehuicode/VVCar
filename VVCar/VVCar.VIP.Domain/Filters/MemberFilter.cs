using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 会员过滤器
    /// </summary>
    public class MemberFilter : BasePageFilter
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 会员状态
        /// </summary>
        public ECardStatus? Status { get; set; }

        /// <summary>
        /// 卡片类型
        /// </summary>
        public Guid? CardTypeID { get; set; }

        /// <summary>
        /// 会员分组
        /// </summary>
        public Guid? MemberGroupID { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public Guid? MemberGradeID { get; set; }

        /// <summary>
        /// 是否是股东
        /// </summary>
        public bool? IsStockholder { get; set; }

        /// <summary>
        /// 是否来自后台客户分配请求
        /// </summary>
        public bool IsFromUserMember { get; set; }

        /// <summary>
        /// 当日数据
        /// </summary>
        public bool? ForDay { get; set; }

        /// <summary>
        /// 当月数据
        /// </summary>
        public bool? ForMonth { get; set; }
    }
}
