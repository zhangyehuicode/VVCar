using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Enums;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 车比特会员注册参数
    /// </summary>
    public class CarBitCoinMemberRegister
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Display(Name = "电话号码")]
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// OpenID
        /// </summary>
        [Display(Name = "OpenID")]
        public string OpenID { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public ESex Sex { get; set; }

        /// <summary>
        /// 拼单记录ID
        /// </summary>
        [Display(Name = "拼单记录ID")]
        public Guid? CrowdOrderRecordID { get; set; }
    }
}
