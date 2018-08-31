using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 发起拼单记录子项
    /// </summary>
    public class CrowdOrderRecordItem : EntityBase
    {
        /// <summary>
        /// 发起拼单记录ID
        /// </summary>
        [Display(Name = "发起拼单记录ID")]
        public Guid CrowdOrderRecordID { get; set; }

        /// <summary>
        /// 发起拼单记录
        /// </summary>
        public virtual CrowdOrderRecord CrowdOrderRecord { get; set; }

        /// <summary>
        /// 车比特会员ID
        /// </summary>
        [Display(Name = "车比特会员ID")]
        public Guid CarBitCoinMemberID { get; set; }

        /// <summary>
        /// 车比特会员
        /// </summary>
        public virtual CarBitCoinMember CarBitCoinMember { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
