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
    /// 车比特产品
    /// </summary>
    public class CarBitCoinProduct : EntityBase
    {
        /// <summary>
        /// 类别ID
        /// </summary>
        [Display(Name = "类别ID")]
        public Guid CarBitCoinProductCategoryID { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public virtual CarBitCoinProductCategory CarBitCoinProductCategory { get; set; }

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
        /// 车比特产品类型
        /// </summary>
        [Display(Name = "车比特产品类型")]
        public ECarBitCoinProductType CarBitCoinProductType { get; set; }

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
        /// 抽成比例(0~100)
        /// </summary>
        [Display(Name = "抽成比例(0~100)")]
        public decimal CommissionRate { get; set; }

        /// <summary>
        /// 车比特兑换
        /// </summary>
        [Display(Name = "车比特兑换")]
        public int CarBitCoinPoints { get; set; }

        /// <summary>
        /// 兑换上限
        /// </summary>
        [Display(Name = "兑换上限")]
        public int UpperLimit { get; set; }

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

        /// <summary>
        /// 产品介绍
        /// </summary>
        [Display(Name = "产品介绍")]
        public string Introduction { get; set; }

        /// <summary>
        /// 配送说明
        /// </summary>
        [Display(Name = "配置说明")]
        public string DeliveryNotes { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreateUser { get; set; }

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
    }
}
