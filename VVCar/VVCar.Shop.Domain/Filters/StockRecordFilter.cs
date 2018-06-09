using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 库存记录过滤器
    /// </summary>
    public class StockRecordFilter : BasePageFilter
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public Guid? ProductID { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public EStockRecordType? StockRecordType { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// 产品名称/编码/员工
        /// </summary>
        public string NameCodeStaff { get; set; }
    }
}
