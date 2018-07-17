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
    /// 游戏推送子项
    /// </summary>
    public class GamePushItem : EntityBase
    {
        /// <summary>
        /// 游戏推送ID
        /// </summary>
        [Display(Name = "游戏推送ID")]
        public Guid GamePushID { get; set; }

        /// <summary>
        /// 游戏ID
        /// </summary>
        [Display(Name = "游戏ID")]
        public Guid GameSettingID { get; set; }

        /// <summary>
        /// 游戏推送
        /// </summary>
        public virtual GameSetting GameSetting { get; set; }

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
