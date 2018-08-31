using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.Shop.Domain.Entities;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 发起拼单记录
    /// </summary>
    public class CrowdOrderRecord : EntityBase
    {
        /// <summary>
        /// ctor.
        /// </summary>
        public CrowdOrderRecord()
        {
            CrowdOrderRecordItemList = new List<CrowdOrderRecordItem>();
        }

        /// <summary>
        /// 发起人ID（车比特会员ID）
        /// </summary>
        [Display(Name = "发起人ID（车比特会员ID）")]
        public Guid CarBitCoinMemberID { get; set; }

        /// <summary>
        /// 车比特会员
        /// </summary>
        public virtual CarBitCoinMember CarBitCoinMember { get; set; }

        /// <summary>
        /// 拼单ID
        /// </summary>
        [Display(Name = "拼单ID")]
        public Guid CrowdOrderID { get; set; }

        /// <summary>
        /// 拼单
        /// </summary>
        public virtual CrowdOrder CrowdOrder { get; set; }

        /// <summary>
        /// 已加入拼单人数
        /// </summary>
        [Display(Name = "已加入拼单人数")]
        public int JoinPeople { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 发起拼单记录子项
        /// </summary>
        public ICollection<CrowdOrderRecordItem> CrowdOrderRecordItemList { get; set; }
    }
}
