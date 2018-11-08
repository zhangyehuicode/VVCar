using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 畅销/滞销产品历史记录
    /// </summary>
    public class UnsaleProductHistory : EntityBase
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Display(Name = "产品ID")]
        public Guid ProductID { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public EProductType ProductType { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        [Display(Name = "产品编码")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 滞销数量上限值,低于即视为滞销产品
        /// </summary>
        [Display(Name = "滞销数量上限值,低于即视为滞销产品")]
        public int UnsaleQuantity { get; set; }

        /// <summary>
        /// 畅销数量下限值,高于则视为畅销产品
        /// </summary>
        [Display(Name = "畅销数量下限值,高于则视为畅销产品")]
        public int SaleWellQuantity { get; set; }

        /// <summary>
        /// 实际销售数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 产品销售状态
        /// </summary>
        [Display(Name = "产品销售状态")]
        public EUnsaleProductStatus Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate{ get; set; }
    }
}
