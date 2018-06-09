using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 库存记录Dto
    /// </summary>
    public class StockRecordDto
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品类别名称
        /// </summary>
        public string ProductCategoryName { get; set; }

        /// <summary>
        /// 出/入库数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public EStockRecordType StockRecordType { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        public EStockRecordSource Source { get; set; }
    }
}
