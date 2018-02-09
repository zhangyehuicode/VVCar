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
    /// 储值方案过滤条件
    /// </summary>
    public class RechargePlanFilter : BasePageFilter
    {
        /// <summary>
        /// 方案编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 方案编号列表
        /// </summary>
        public List<string> Codes { get; set; }

        /// <summary>
        /// 储值方案信息
        /// </summary>
        public List<RechargePlans> RechargePlans { get; set; }

        /// <summary>
        /// 方案名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 方案状态
        /// </summary>
        public bool? Status { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime? ExpiredDate { get; set; }

        /// <summary>
        /// 卡片类型
        /// </summary>
        public EPlanType? PlanType { get; set; }
    }

    /// <summary>
    /// 储值方案信息
    /// </summary>
    public class RechargePlans
    {
        /// <summary>
        /// 储值方案编号
        /// </summary>
        public string RechargePlanCode { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
    }
}
