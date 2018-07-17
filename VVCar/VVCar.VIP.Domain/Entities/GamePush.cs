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
    /// 游戏推送
    /// </summary>
    public class GamePush : EntityBase
    {
        /// <summary>
        /// 游戏推送
        /// </summary>
        public GamePush()
        {
            GamePushItems = new List<GamePushItem>();
            GamePushMembers = new List<GamePushMember>();
        }

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 推送时间
        /// </summary>
        [Display(Name = "推送时间")]
        public DateTime PushDate { get; set; }

        /// <summary>
        /// 推送状态
        /// </summary>
        [Display(Name = "推送状态")]
        public EGamePushStatus Status { get; set; }

        /// <summary>
        /// 是否推送所有会员
        /// </summary>
        [Display(Name = "是否推送所有会员")]
        public bool PushAllMembers { get; set; }

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
        /// 游戏推送子项
        /// </summary>
        public virtual ICollection<GamePushItem> GamePushItems { get; set; }

        /// <summary>
        /// 游戏推送会员
        /// </summary>
        public virtual ICollection<GamePushMember> GamePushMembers { get; set; }
    }
}
