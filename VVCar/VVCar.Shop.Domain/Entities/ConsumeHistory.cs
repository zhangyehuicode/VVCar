using System;
using System.ComponentModel.DataAnnotations;
using YEF.Core.Data;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 历史消费记录
    /// </summary>
    public class ConsumeHistory : EntityBase
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        [Display(Name = "客户名称")]
        public string Name { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [Display(Name = "车牌号")]
        public string PlateNumber { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary>
        [Display(Name = "单据编号")]
        public string TradeNo { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Display(Name ="电话号码")]
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 消费项目
        /// </summary>
        [Display(Name = "消费项目")]
        public string Consumption { get; set; }

        /// <summary>
        /// 消费数量
        /// </summary>
        [Display(Name = "消费数量")]
        public decimal TradeCount { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Display(Name = "单位")]
        public string Unit { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [Display(Name = "单价")]
        public decimal Price { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [Display(Name = "金额")]
        public decimal TradeMoney { get; set; }

        /// <summary>
        /// 商品成本
        /// </summary>
        [Display(Name = "商品成本")]
        public decimal BasePrice { get; set; }

        /// <summary>
        /// 毛利
        /// </summary>
        [Display(Name = "毛利")]
        public decimal GrossProfit { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 门店
        /// </summary>
        [Display(Name = "门店")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 消费时间
        /// </summary>
        [Display(Name = "消费时间")]
        public DateTime? CreatedDate { get; set; }

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


    }
}
