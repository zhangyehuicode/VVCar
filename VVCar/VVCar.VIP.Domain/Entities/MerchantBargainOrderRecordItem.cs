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
    /// 发起砍价记录子项
    /// </summary>
    public class MerchantBargainOrderRecordItem : EntityBase
    {
        /// <summary>
        /// 发起砍价记录ID
        /// </summary>
        [Display(Name = "发起砍价记录ID")]
        public Guid MerchantBargainOrderRecordID { get; set; }

        /// <summary>
        /// 发起砍价记录
        /// </summary>
        public virtual MerchantBargainOrderRecord MerchantBargainOrderRecord { get; set; }

        /// <summary>
        /// 帮砍金额
        /// </summary>
        [Display(Name = "帮砍金额")]
        public decimal Price { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        [Display(Name = "会员ID")]
        public Guid MemberID { get; set; }

        /// <summary>
        /// 会员
        /// </summary>
        public virtual Member Member { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
