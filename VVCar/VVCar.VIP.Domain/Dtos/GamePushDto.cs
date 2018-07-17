using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 游戏推送任务Dto
    /// </summary>
    public class GamePushDto
    {
        /// <summary>
        /// 游戏推送ID
        /// </summary>
        public Guid ID { get; set; }
        
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 推送时间
        /// </summary>
        public DateTime PushDate { get; set; }

        /// <summary>
        /// 推送状态
        /// </summary>
        public EGamePushStatus Status { get; set; }

        /// <summary>
        /// 是否推荐所有会员
        /// </summary>
        public bool PushAllMembers { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
