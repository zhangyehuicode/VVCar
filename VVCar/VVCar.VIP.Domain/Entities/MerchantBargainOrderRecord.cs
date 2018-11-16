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
    /// 门店发起砍价记录
    /// </summary>
    public class MerchantBargainOrderRecord : EntityBase
    {
        /// <summary>
        /// ctor
        /// </summary>
        public MerchantBargainOrderRecord()
        {
            MerchantBargainOrderRecordItemList = new List<MerchantBargainOrderRecordItem>();
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
        /// 砍价ID
        /// </summary>
        [Display(Name = "砍价ID")]
        public Guid MerchantBargainOrderID { get; set; }

        /// <summary>
        /// 砍价后应付金额
        /// </summary>
        public decimal FinalPrice { get; set; }

        /// <summary>
        /// 砍价
        /// </summary>
        public virtual MerchantBargainOrder MerchantBargainOrder { get; set; }

        /// <summary>
        /// 已加入砍价人数
        /// </summary>
        [Display(Name = "已加入砍价人数")]
        public int JoinPeople { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 发起拼单记录子项
        /// </summary>
        public ICollection<MerchantBargainOrderRecordItem> MerchantBargainOrderRecordItemList { get; set; }
    }
}
