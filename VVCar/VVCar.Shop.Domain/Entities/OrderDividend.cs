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
    /// 订单分红表
    /// </summary>
    public class OrderDividend : EntityBase
    {
        /// <summary>
        /// 交易单号
        /// </summary>
        [Display(Name = "交易单号")]
        public string TradeNo { get; set; }

        /// <summary>
        /// 交易订单ID(包含 PickUpOrderID OrderID)
        /// </summary>
        [Display(Name = "交易订单ID")]
        public Guid TradeOrderID { get; set; }

        /// <summary>
        /// 人员分配ID
        /// </summary>
        [Display(Name = "人员分配ID")]
        public Guid? PickUpOrderTaskDistributionID { get; set; }

        /// <summary>
        /// 编码(包含 ProductCode TemplateCode)
        /// </summary>
        [Display(Name = "编码")]
        public string Code { get; set; }

        /// <summary>
        /// 名称(包含 ProductName Title)
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 产品ID(包含 ProductID CouponTemplateID)
        /// </summary>
        [Display(Name = "产品ID")]
        public Guid GoodsID { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [Display(Name = "车牌号")]
        public string PlateNumber { get; set; }

        /// <summary>
        /// 用户编码
        /// </summary>
        [Display(Name = "用户编码")]
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Display(Name = "用户名称")]
        public string UserName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        public Guid? UserID { get; set; }

        /// <summary>
        /// 业绩
        /// </summary>
        [Display(Name = "业绩")]
        public decimal Money { get; set; }

        /// <summary>
        /// 成本
        /// </summary>
        [Display(Name = "成本")]
        public decimal CostMoney { get; set; }

        /// <summary>
        /// 是否按比例抽成
        /// </summary>
        [Display(Name = "是否按比例抽成")]
        public bool IsCommissionRate { get; set; }

        /// <summary>
        /// 抽成比例
        /// </summary>
        [Display(Name = "抽成比例")]
        public decimal CommissionRate { get; set; }

        /// <summary>
        /// 抽成金额
        /// </summary>
        [Display(Name = "抽成金额")]
        public decimal CommissionMoney { get; set; }

        /// <summary>
        /// 抽成
        /// </summary>
        [Display(Name = "抽成")]
        public decimal Commission { get; set; }

        /// <summary>
        /// 交易订单类型
        /// </summary>
        [Display(Name = "交易订单类型")]
        public EShopTradeOrderType OrderType { get; set; }

        /// <summary>
        /// 人员类型
        /// </summary>
        [Display(Name = "人员类型")]
        public ETaskDistributionPeopleType PeopleType { get; set; }

        /// <summary>
        /// 是否已结算
        /// </summary>
        [Display(Name = "是否已结算")]
        public bool IsBalance { get; set; }

        /// <summary>
        /// 结算时间
        /// </summary>
        [Display(Name = "结算时间")]
        public DateTime? BalanceDate { get; set; }

        /// <summary>
        /// 结算者ID
        /// </summary>
        [Display(Name = "结算者ID")]
        public Guid? BalanceUserID { get; set; }

        /// <summary>
        /// 结算者姓名
        /// </summary>
        [Display(Name = "结算者姓名")]
        public string BalanceUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
