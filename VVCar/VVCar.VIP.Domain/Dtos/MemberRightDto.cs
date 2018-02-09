using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 会员权益
    /// </summary>
    public class MemberRightDto
    {
        /// <summary>
        /// 权益ID
        /// </summary>
        public Guid RightID { get; set; }

        /// <summary>
        /// 权益编码
        /// </summary>
        public string PosRightCode { get; set; }

        /// <summary>
        /// 权益名称
        /// </summary>
        public string PosRightName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
    }
}
