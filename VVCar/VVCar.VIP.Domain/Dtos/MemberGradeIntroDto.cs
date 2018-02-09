using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 会员等级介绍
    /// </summary>
    public class MemberGradeIntroDto
    {
        /// <summary>
        /// 等级ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 等级名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 购买金额/差价
        /// </summary>
        public decimal PurchaseAmount { get; set; }

        /// <summary>
        /// 原购买金额
        /// </summary>
        public decimal OrignalPurchaseAmount { get; set; }

        /// <summary>
        /// 折扣系数
        /// </summary>
        public decimal? DiscountRate { get; set; }

        /// <summary>
        /// 会员折扣
        /// </summary>
        public string DiscountDesc
        {
            //get
            //{
            //if (!DiscountRate.HasValue)
            //    return string.Empty;
            //return (DiscountRate.Value * 10).ToString("0.#") + "折";
            //}
            get; set;
        }

        /// <summary>
        /// 等级初始积分
        /// </summary>
        public int GradePoint { get; set; }

        /// <summary>
        /// 专享菜品
        /// </summary>
        public string ProductRight { get; set; }

        /// <summary>
        /// 消费送积分
        /// </summary>
        public bool IsConsumeGiftPoint { get; set; }

        /// <summary>
        /// 储值送积分
        /// </summary>
        public bool IsRechargeGiftPoint { get; set; }

        /// <summary>
        /// 是否为默认等级
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
