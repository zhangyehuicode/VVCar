using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 寻客侠广告浏览记录表
    /// </summary>
    public class AdvisementBrowseHistory : EntityBase
    {
        /// <summary>
        /// 寻客侠广告配置ID
        /// </summary>
        [Display(Name = "寻客侠广告配置ID")]
        public Guid AdvisementSettingID { get; set; }

        /// <summary>
        /// 广告配置
        /// </summary>
        public virtual AdvisementSetting AdvisementSetting { get; set; }

        /// <summary>
        /// 会员OpenID
        /// </summary>
        [Display(Name = "会员OpenID")]
        public string OpenID { get; set; }

        /// <summary>
        /// 微信昵称
        /// </summary>
        [Display(Name = "微信昵称")]
        public string NickName { get; set; } 

        /// <summary>
        /// 开始浏览时间
        /// </summary>
        [Display(Name = "开始浏览时间")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束浏览时间
        /// </summary>
        [Display(Name = "结束浏览时间")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 间隔时间(秒)
        /// </summary>
        [Display(Name = "间隔时间(秒)")]
        public decimal Period { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
