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
    /// 会员积分设置
    /// </summary>
    public class MemberPoint : NormalEntityBase
    {
        /// <summary>
        /// 会员积分设置 初始化
        /// </summary>
        public MemberPoint()
        {
            AdditionalRules = new List<MemberPointAdditionalRule>();
        }

        /// <summary>
        /// 积分
        /// </summary>
        [Display(Name = "积分")]
        public int Point { get; set; }

        /// <summary>
        /// 积分类型
        /// </summary>
        [Display(Name = "积分类型")]
        public EMemberPointType Type { get; set; }

        /// <summary>
        /// 限制(每人每天最多可获得分享/评价积分次数) Type为分享或评价时有效
        /// </summary>
        [Display(Name = "限制(每人每天最多可获得分享/评价积分次数) Type为分享或评价时有效")]
        public int Limit { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 分享图标
        /// </summary>
        [Display(Name = "分享图标")]
        public string Icon { get; set; }

        /// <summary>
        /// 分享标题
        /// </summary>
        [Display(Name = "分享标题")]
        public string Title { get; set; }

        /// <summary>
        /// 分享副标题
        /// </summary>
        [Display(Name = "分享副标题")]
        public string SubTitle { get; set; }

        /// <summary>
        /// 附加规则
        /// </summary>
        [Display(Name = "附加规则")]
        public virtual ICollection<MemberPointAdditionalRule> AdditionalRules { get; set; }
    }
}
