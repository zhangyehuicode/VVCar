using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 领取游戏Dto
    /// </summary>
    public class ReceiveGameDto
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public Guid? MemberID { get; set; }

        /// <summary>
        /// 游戏设置ID
        /// </summary>
        public IList<Guid> GameSettingIDs { get; set; }

        /// <summary>
        /// 领取者昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 领取者OpenID
        /// </summary>
        public string ReceiveOpenID { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public Guid? MerchantID { get; set; }
    }
}
