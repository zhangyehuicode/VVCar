using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 接车单子项
    /// </summary>
    public class PickUpOrderItem : EntityBase
    {
        public PickUpOrderItem()
        {
            PickUpOrderTaskDistributionList = new List<PickUpOrderTaskDistribution>();
        }

        /// <summary>
        /// 接车单ID
        /// </summary>
        [Display(Name = "接车单ID")]
        public Guid PickUpOrderID { get; set; }

        /// <summary>
        /// 接车单
        /// </summary>
        public virtual PickUpOrder PickUpOrder { get; set; }

        /// <summary>
        /// 服务ID
        /// </summary>
        [Display(Name = "服务ID")]
        public Guid ProductID { get; set; }

        /// <summary>
        /// 服务
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        [Display(Name = "服务名称")]
        public string ProductName { get; set; }

        /// <summary>
        /// 服务编码
        /// </summary>
        [Display(Name = "服务编码")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 服务总额
        /// </summary>
        [Display(Name = "服务总额")]
        public decimal Money { get; set; }

        /// <summary>
        /// 服务单价
        /// </summary>
        [Display(Name = "服务单价")]
        public decimal PriceSale { get; set; }

        /// <summary>
        /// 是否优惠
        /// </summary>
        [Display(Name = "是否优惠")]
        public bool IsReduce { get; set; }

        /// <summary>
        /// 优惠价
        /// </summary>
        [Display(Name = "优惠价")]
        public decimal ReducedPrice { get; set; }

        /// <summary>
        /// 折扣系数
        /// </summary>
        [Display(Name = "折扣系数")]
        public decimal Discount { get; set; }

        /// <summary>
        /// 服务次数
        /// </summary>
        [Display(Name = "服务次数")]
        public int Quantity { get; set; }

        /// <summary>
        /// 施工人数
        /// </summary>
        [Display(Name = "施工人数")]
        public int ConstructionCount { get; set; }

        /// <summary>
        /// 业务员人数
        /// </summary>
        [Display(Name = "业务员人数")]
        public int SalesmanCount { get; set; }

        /// <summary>
        /// 抽成比例
        /// </summary>
        [Display(Name = "抽成比例")]
        public decimal CommissionRate { get; set; }

        /// <summary>
        /// 业务员抽成比例(0~100)
        /// </summary>
        [Display(Name = "业务员抽成比例(0~100)")]
        public decimal SalesmanCommissionRate { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [Display(Name = "图片")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 接车单任务分配
        /// </summary>
        public ICollection<PickUpOrderTaskDistribution> PickUpOrderTaskDistributionList { get; set; }
    }
}
