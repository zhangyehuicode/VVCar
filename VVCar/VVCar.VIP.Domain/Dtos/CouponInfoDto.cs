using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 优惠券信息
    /// </summary>
    public class CouponInfoDto : CouponBaseInfoDto
    {
        /// <summary>
        ///  适用商品编码，以,分隔
        /// </summary>
        public string IncludeProducts { get; set; }

        /// <summary>
        ///  不适用商品编码，以,分隔
        /// </summary>
        public string ExcludeProducts { get; set; }

        /// <summary>
        ///  是否全时段
        /// </summary>
        public bool IsUseAllTime { get; set; }

        /// <summary>
        ///  可用的日期
        /// </summary>
        public string UseDaysOfWeek { get; set; }

        /// <summary>
        ///  核销方式
        /// </summary>
        public EVerificationMode VerificationMode { get; set; }

        /// <summary>
        /// 全部门店适用
        /// </summary>
        public bool IsApplyAllStore { get; set; }

        /// <summary>
        ///  适用门店编码，以,分隔
        /// </summary>
        public string ApplyStores { get; set; }

        /// <summary>
        /// 剩余库存
        /// </summary>
        public int FreeStock { get; set; }

        /// <summary>
        /// 会员姓名
        /// </summary>
        public string OwnerMemberName { get; set; }

        /// <summary>
        /// 会员分组
        /// </summary>
        public string OwnerMemberGroup { get; set; }

        /// <summary>
        /// 是否优先抵扣
        /// </summary>
        public bool IsDeductionFirst { get; set; }
    }
}
