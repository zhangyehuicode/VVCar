﻿using System;
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
    /// 产品
    /// </summary>
    public class Product : EntityBase
    {
        /// <summary>
        /// ctor
        /// </summary>
        public Product()
        {
            ComboItemList = new List<ComboItem>();
        }

        /// <summary>
        /// 类别ID
        /// </summary>
        [Display(Name = "类别ID")]
        public Guid ProductCategoryID { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public virtual ProductCategory ProductCategory { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public int Index { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Display(Name = "编码")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        [Display(Name = "产品类型")]
        public EProductType ProductType { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [Display(Name = "图片")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 原单价
        /// </summary>
        [Display(Name = "原单价")]
        public decimal BasePrice { get; set; }

        /// <summary>
        /// 销售单价
        /// </summary>
        [Display(Name = "销售单价")]
        public decimal PriceSale { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        [Display(Name = "成本价")]
        public decimal CostPrice { get; set; }

        /// <summary>
        /// 批发价
        /// </summary>
        [Display(Name = "批发价")]
        public decimal WholesalePrice { get; set; }

        /// <summary>
        /// 施工是否按比例抽成(否为固定定额)
        /// </summary>
        [Display(Name = "施工是否按比例抽成(否为固定定额)")]
        public bool IsCommissionRate { get; set; }

        /// <summary>
        /// 施工抽成比例(0~100)
        /// </summary>
        [Display(Name = "施工抽成比例(0~100)")]
        public decimal CommissionRate { get; set; }

        /// <summary>
        /// 施工固定抽成金额
        /// </summary>
        [Display(Name = "固定抽成金额")]
        public decimal CommissionMoney { get; set; }

        /// <summary>
        /// 业务员是否按比例抽成(否为固定金额)
        /// </summary>
        [Display(Name = "业务员是否按比例抽成(否为固定金额)")]
        public bool IsSalesmanCommissionRate { get; set; }

        /// <summary>
        /// 业务员抽成比例(0~100)
        /// </summary>
        [Display(Name = "业务员抽成比例(0~100)")]
        public decimal SalesmanCommissionRate { get; set; }

        /// <summary>
        /// 业务员固定抽成金额
        /// </summary>
        [Display(Name = "业务员固定抽成金额")]
        public decimal SalesmanCommissionMoney { get; set; }

        /// <summary>
        /// 是否批发价抽成比例
        /// </summary>
        [Display(Name = "是否批发价抽成比例")]
        public bool IsWholesaleCommissionRate { get; set; }

        /// <summary>
        /// 批发价业务员抽成比例(0~100) --> 对于服务就是优惠价
        /// </summary>
        [Display(Name = "批发价业务员抽成比例(0~100)")]
        public decimal WholesaleCommissionRate { get; set; }

        /// <summary>
        /// 批发价固定抽成金额
        /// </summary>
        [Display(Name = "批发价固定抽成金额")]
        public decimal WholesaleCommissionMoney { get; set; }

        /// <summary>
        /// 是否优惠价施工抽成比例
        /// </summary>
        public bool IsWholesaleConstructionCommissionRate { get; set; }

        /// <summary>
        /// 优惠价施工抽成比例
        /// </summary>
        [Display(Name = "优惠价施工抽成比例")]
        public decimal WholesaleConstructionCommissionRate { get; set; }

        /// <summary>
        /// 优惠价施工固定抽成金额
        /// </summary>
        [Display(Name = "优惠价施工固定抽成金额")]
        public decimal WholesaleConstructionCommissionMoney { get; set; }

        /// <summary>
        /// 兑换积分
        /// </summary>
        [Display(Name = "兑换积分")]
        public int Points { get; set; }

        /// <summary>
        /// 兑换上限
        /// </summary>
        [Display(Name = "兑换上限")]
        public int UpperLimit { get; set; }

        /// <summary>
        /// 员工内部领取
        /// </summary>
        [Display(Name = "员工内部领取")]
        public bool IsInternaCollection { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        [Display(Name = "是否上架")]
        public bool IsPublish { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        [Display(Name = "是否推荐")]
        public bool IsRecommend { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        [Display(Name = "库存")]
        public int Stock { get; set; }

        /// <summary>
        /// 是否可以积分兑换
        /// </summary>
        [Display(Name = "是否可以积分兑换")]
        public bool IsCanPointExchange { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Display(Name = "单位")]
        public string Unit { get; set; }

        ///// <summary>
        ///// 生效时间
        ///// </summary>
        //[Display(Name = "生效时间")]
        //public DateTime? EffectiveDate { get; set; }

        ///// <summary>
        ///// 失效时间
        ///// </summary>
        //[Display(Name = "失效时间")]
        //public DateTime? ExpiredDate { get; set; }

        /// <summary>
        /// 产品介绍
        /// </summary>
        [Display(Name = "产品介绍")]
        public string Introduction { get; set; }

        /// <summary>
        /// 配送说明
        /// </summary>
        [Display(Name = "配送说明")]
        public string DeliveryNotes { get; set; }

        /// <summary>
        /// 是否套餐
        /// </summary>
        [Display(Name = "是否套餐")]
        public bool IsCombo { get; set; }

        /// <summary>
        /// 图文介绍
        /// </summary>
        [Display(Name = "图文介绍")]
        public string GraphicIntroduction { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid? CreatedUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [Display(Name = "最后修改人")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// 套餐子项
        /// </summary>
        public ICollection<ComboItem> ComboItemList { get; set; }
    }
}
