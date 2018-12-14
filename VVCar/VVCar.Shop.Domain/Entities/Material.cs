 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 素材
    /// </summary>
    public class Material : EntityBase
    {
        /// <summary>
        /// 素材名称
        /// </summary>
        [Display(Name = "素材名称")]
        public string Name { get; set; }

        /// <summary>
        /// 素材路径
        /// </summary>
        [Display(Name = "素材路径")]
        public string Url { get; set; }

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
