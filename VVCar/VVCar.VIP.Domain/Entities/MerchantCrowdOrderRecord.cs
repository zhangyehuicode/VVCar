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
    /// 门店发起拼单记录
    /// </summary>
    public class MerchantCrowdOrderRecord : EntityBase
    {
        /// <summary>
        /// ctor
        /// </summary>
        public MerchantCrowdOrderRecord()
        {
            MerchantCrowdOrderRecordItemList = new List<MerchantCrowdOrderRecordItem>();
        }

        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "编号")]
        public string Code { get; set; }

        /// <summary>
        /// 发起会员ID
        /// </summary>
        [Display(Name = "发起会员ID")]
        public Guid MemberID { get; set; }

        /// <summary>
        /// 会员
        /// </summary>
        public virtual Member Member { get; set; }

        /// <summary>
        /// 拼单ID
        /// </summary>
        [Display(Name = "拼单ID")]
        public Guid MerchantCrowdOrderID { get; set; }

        /// <summary>
        /// 拼单
        /// </summary>
        public virtual MerchantCrowdOrder MerchantCrowdOrder { get; set; }

        /// <summary>
        /// 已经加入拼单人数
        /// </summary>
        [Display(Name = "已经加入拼单人数")]
        public int JoinPeople { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 发起拼单记录子项
        /// </summary>
        public ICollection<MerchantCrowdOrderRecordItem> MerchantCrowdOrderRecordItemList { get; set; }
    }
}
