using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.Shop.Domain.Entities
{
    public class MaterialPublishItem : EntityBase
    {
        /// <summary>
        /// 信息发布ID
        /// </summary>
        [Display(Name = "信息发布ID")]
        public Guid MaterialPublishID { get; set; }

        /// <summary>
        /// 素材
        /// </summary>
        public virtual Material Material { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public int Index { get; set; }

        /// <summary>
        /// 素材ID
        /// </summary>
        [Display(Name = "素材ID")]
        public Guid MaterialID { get; set; }

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
    }
}
