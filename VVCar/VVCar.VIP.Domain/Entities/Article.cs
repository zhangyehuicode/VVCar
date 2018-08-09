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
    /// 图文消息
    /// </summary>
    public class Article : EntityBase
    {
        /// <summary>
        /// ctor
        /// </summary>
        public Article()
        {
            ArtitleItems = new List<ArticleItem>();
        }

        /// <summary>
        /// 图文编码
        /// </summary>
        [Display(Name = "图文编码")]
        public string Code { get; set; }

        /// <summary>
        /// 图文标题
        /// </summary>
        [Display(Name = "图文标题")]
        public string Name { get; set; }

        /// <summary>
        /// 推送时间
        /// </summary>
        [Display(Name = "推送时间")]
        public DateTime PushDate { get; set; } 

        /// <summary>
        /// 推送状态
        /// </summary>
        [Display(Name = "推送状态")]
        public EArticlePushStatus Status { get; set; }

        /// <summary>
        /// 是否推送所有会员
        /// </summary>
        [Display(Name = "是否推送所有会员")]
        public bool IsPushAllMembers { get; set; }

        /// <summary>
        /// 图文子项
        /// </summary>
        public virtual ICollection<ArticleItem> ArtitleItems { get; set; }

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
