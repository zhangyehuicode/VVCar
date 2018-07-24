using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.BaseData.Domain.Entities;
using VVCar.Shop.Domain.Enums;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 接车单任务分配
    /// </summary>
    public class PickUpOrderTaskDistribution : EntityBase
    {
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
        /// 接车单子项ID
        /// </summary>
        [Display(Name = "接车单子项ID")]
        public Guid PickUpOrderItemID { get; set; }

        /// <summary>
        /// 接车单子项
        /// </summary>
        public virtual PickUpOrderItem PickUpOrderItem { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        [Display(Name = "员工ID")]
        public Guid UserID { get; set; }

        /// <summary>
        /// 员工
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 人员类型
        /// </summary>
        [Display(Name = "人员类型")]
        public ETaskDistributionPeopleType PeopleType { get; set; }

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
        /// 接车单订单子项总额
        /// </summary>
        [Display(Name = "接车单订单子项总额")]
        public decimal TotalMoney { get; set; }

        /// <summary>
        /// 抽成比例
        /// </summary>
        [Display(Name = "抽成比例")]
        public decimal CommissionRate { get; set; }

        /// <summary>
        /// 抽成
        /// </summary>
        [Display(Name = "抽成")]
        public decimal Commission { get; set; }

        /// <summary>
        /// 业务员抽成比例(0~100)
        /// </summary>
        [Display(Name = "业务员抽成比例(0~100)")]
        public decimal SalesmanCommissionRate { get; set; }

        /// <summary>
        /// 业务员抽成
        /// </summary>
        [Display(Name = "业务员抽成")]
        public decimal SalesmanCommission { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Display(Name = "创建人名称")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人名称
        /// </summary>
        [Display(Name = "最后修改人名称")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改日期
        /// </summary>
        [Display(Name = "最后修改日期")]
        public DateTime? LastUpdateDate { get; set; }
    }
}
