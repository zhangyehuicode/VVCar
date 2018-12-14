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
    /// 信息发布
    /// </summary>
    public class MaterialPublish : EntityBase
    {
        public MaterialPublish()
        {
            MaterialPublishItems = new List<MaterialPublishItem>();
        }

        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "编号")]
        public string Code { get; set; }

        /// <summary>
        /// 信息名称
        /// </summary>
        [Display(Name = "信息名称")]
        public string Name { get; set; }

        /// <summary>
        /// 信息发布状态
        /// </summary>
        [Display(Name = "信息发布状态")]
        public EMaterialPublishStatus Status { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [Display(Name = "发布时间")]
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人名称
        /// </summary>
        [Display(Name = "最后修改人名称")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改日期
        /// </summary>
        [Display(Name = "最后修改日期")]
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// 素材
        /// </summary>
        public virtual ICollection<MaterialPublishItem> MaterialPublishItems { get; set; }
    }
}
