using System;
using System.ComponentModel.DataAnnotations;
using VVCar.Shop.Domain.Entities;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 拼单
    /// </summary>
    public class CrowdOrder : EntityBase
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
        /// 拼单名称
        /// </summary>
        [Display(Name = "拼单名称")]
        public string Name { get; set; }

        /// <summary>
        /// 拼单价格
        /// </summary>
        [Display(Name = "拼单价格")]
        public decimal Price { get; set; }

        /// <summary>
        /// 拼单人数
        /// </summary>
        [Display(Name = "拼单人数")]
        public int PeopleCount { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 上架时间
        /// </summary>
        [Display(Name = "上架时间")]
        public DateTime PutawayTime { get; set; }

        /// <summary>
        /// 下架时间
        /// </summary>
        [Display(Name = "下架时间")]
        public DateTime SoleOutTime { get; set; }

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

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人姓名
        /// </summary>
        [Display(Name = "最后修改人")]
        public String LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastUpdateDate { get; set; }
    }
}
