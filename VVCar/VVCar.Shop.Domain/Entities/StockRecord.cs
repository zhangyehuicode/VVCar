using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.Shop.Domain.Enums;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 库存记录(出/入库)
    /// </summary>
    public class StockRecord : EntityBase
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        [Display(Name = "产品ID")]
        public Guid ProductID { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// 出/入库数量
        /// </summary>
        [Display(Name = "出/入库数量")]
        public int Quantity { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        public EStockRecordType StockRecordType { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        [Display(Name = "原因")]
        public string Reason { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        [Display(Name = "员工ID")]
        public Guid StaffID { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        [Display(Name = "员工姓名")]
        public string StaffName { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        [Display(Name = "订单ID")]
        public Guid? OrderID { get; set; }

        /// <summary>
        /// 订单
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        [Display(Name = "数据来源")]
        public EStockRecordSource Source { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
