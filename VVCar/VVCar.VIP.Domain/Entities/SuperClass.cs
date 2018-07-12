using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 超能课堂
    /// </summary>
    public class SuperClass : EntityBase
    {
        /// <summary>
        /// ctor
        /// </summary>
        public SuperClass()
        {
        }

        /// <summary>
        /// 视频名称
        /// </summary>
        [Display(Name = "视频名称")]
        public string Name { get; set; }

        /// <summary>
        /// 视频
        /// </summary>
        [Display(Name = "视频")]
        public string VideoUrl { get; set; }

        /// <summary>
        /// 视频类型
        /// </summary>
        [Display(Name = "视频类型")]
        public EVideoType VideoType { get; set; }

        /// <summary>
        /// 视频简介
        /// </summary>
        [Display(Name = "视频简介")]
        public string Description { get; set; }

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
