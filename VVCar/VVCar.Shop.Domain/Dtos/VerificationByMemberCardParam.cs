using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 储值卡核销参数
    /// </summary>
    public class VerificationByMemberCardParam
    {
        /// <summary>
        /// 接车单单号ID
        /// </summary>
        public Guid PickUpOrderID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public Guid MemberID { get; set; }

        /// <summary>
        /// 接车单单号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal PayMoney { get; set; }

        /// <summary>
        /// 开单人员ID
        /// </summary>
        public Guid StaffID { get; set; }

        /// <summary>
        /// 开单人员名称
        /// </summary>
        public string StaffName { get; set; }
    }
}
