using System;
using System.ComponentModel.DataAnnotations;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 图文消息子项
    /// </summary>
    public class ArticleItem : EntityBase
    {
        /// <summary>
        /// 图文消息ID
        /// </summary>
        [Display(Name = "图文消息ID")]
        public Guid ArticleID { get; set; }

        /// <summary>
        /// 图文标题
        /// </summary>
        [Display(Name = "文章标题")]
        public string Title { get; set; }

        /// <summary>
        /// 图文消息的封面图片素材ID
        /// </summary>
        [Display(Name = "图文消息的封面图片素材ID")]
        public string ThumbMediaID{ get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [Display(Name = "作者")]
        public string Author { get; set; }

        /// <summary>
        /// 图文消息的摘要(仅有单图文消息才有摘要,多图文此处为空)
        /// </summary>
        [Display(Name = "图文消息的摘要(仅有单图文消息才有摘要,多图文此处为空)")]
        public string Digest { get; set; }

        /// <summary>
        /// 是否显示封面
        /// </summary>
        [Display(Name = "是否显示封面")]
        public bool IsShowCoverPic { get; set; } 

        /// <summary>
        /// 封面图片路径
        /// </summary>
        [Display(Name = "封面图片路径")]
        public string CoverPicUrl { get; set; }

        /// <summary>
        /// 图文消息具体内容
        /// </summary>
        [Display(Name = "图文消息具体内容")]
        public string Content { get; set; }

        /// <summary>
        /// 消息图文的原文地址
        /// </summary>
        [Display(Name = "消息图文的原文地址")]
        public string ContentSourceUrl { get; set; }

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
