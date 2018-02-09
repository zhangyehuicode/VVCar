using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 领券中心
    /// </summary>
    public class PointExchangeCoupon : NormalEntityBase
    {
        /// <summary>
        /// 积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 有效期开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// 有效期结束时间
        /// </summary>
        public DateTime FinishDate { get; set; }

        /// <summary>
        /// 兑换次数
        /// </summary>
        public int ExchangeCount { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 卡券Id
        /// </summary>
        public Guid CouponTemplateId { get; set; }

        /// <summary>
        /// 卡券
        /// </summary>
        public virtual CouponTemplate CouponTemplate { get; set; }

        /// <summary>
        /// 兑换方式
        /// </summary>
        public EExchangeType ExchangeType { get; set; }
    }
}
