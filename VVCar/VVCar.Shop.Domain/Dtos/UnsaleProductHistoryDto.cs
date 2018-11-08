using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 畅销/滞销产品历史记录Dto
    /// </summary>
    public class UnsaleProductHistoryDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public EProductType ProductType { get; set; }

        /// <summary>
        /// 产品类型文本
        /// </summary>
        public string ProductTypeText { get { return ProductType.GetDescription(); } }

        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 滞销数量上限值,低于即视为滞销产品
        /// </summary>
        public int UnsaleQuantity { get; set; }

        /// <summary>
        /// 畅销数量下限值,高于则视为畅销产品
        /// </summary>
        public int SaleWellQuantity { get; set; }

        /// <summary>
        /// 实际销售数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 产品销售状态
        /// </summary>
        public EUnsaleProductStatus Status { get; set; }

        /// <summary>
        /// 产品销售状态文本
        /// </summary>
        public string StatusText { get { return Status.GetDescription(); } }
    }
}
