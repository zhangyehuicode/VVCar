using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using YEF.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 车比特会员
    /// </summary>
    public class CarBitCoinMember : EntityBase
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
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public ESex Sex { get; set; }

        /// <summary>
        /// OpenID
        /// </summary>
        [Display(Name = "OpenID")]
        public string OpenID { get; set; }

        /// <summary>
        /// 马力
        /// </summary>
        [Display(Name = "马力")]
        public int Horsepower { get; set; }

        /// <summary>
        /// 车比特
        /// </summary>
        [Display(Name = "车比特")]
        public decimal CarBitCoin { get; set; }

        /// <summary>
        /// 冻结的车比特（赠送）
        /// </summary>
        [Display(Name = "冻结的车比特（赠送）")]
        public decimal FrozenCoin { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
