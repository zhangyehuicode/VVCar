using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    public class GetMemberInfoByWeChatDto
    {
        /// <summary>
        /// openID
        /// </summary>
        public string openID { get; set; }

        /// <summary>
        /// 是否代理商门店
        /// </summary>
        public bool isagentdept { get; set; }
    }
}
