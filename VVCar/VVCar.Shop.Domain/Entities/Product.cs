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
    /// 产品
    /// </summary>
    public class Product : EntityBase
    {
        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public int Index { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        //[Display(Name = "产品类型")]
        //public EProductType ProductType { get; set; }

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
        /// 是否发布
        /// </summary>
        [Display(Name = "是否发布")]
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
        /// 生效时间
        /// </summary>
        [Display(Name = "生效时间")]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        [Display(Name = "失效时间")]
        public DateTime? ExpiredDate { get; set; }

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
    }
}
