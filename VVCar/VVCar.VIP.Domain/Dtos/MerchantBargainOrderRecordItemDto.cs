using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 发起砍价记录子项DTO
    /// </summary>
    public class MerchantBargainOrderRecordItemDto
    {
        /// <summary>
        /// 发起砍价记录ID
        /// </summary>
        public Guid MerchantBargainOrderRecordID { get; set; }

        /// <summary>
        /// 会员名称
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 帮砍金额
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public Guid MemberID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
