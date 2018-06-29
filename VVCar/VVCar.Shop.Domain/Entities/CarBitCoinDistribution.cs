using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 车比特分配
    /// </summary>
    public class CarBitCoinDistribution : NormalEntityBase
    {
        /// <summary>
        /// 车比特会员ID
        /// </summary>
        [Display(Name = "车比特会员ID")]
        public Guid CarBitCoinMemberID { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Display(Name = "电话号码")]
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 车比特会员
        /// </summary>
        public virtual CarBitCoinMember CarBitCoinMember { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public ECarBitCoinDistributionStatus Status { get; set; }

        /// <summary>
        /// 车比特
        /// </summary>
        [Display(Name = "车比特")]
        public decimal CarBitCoin { get; set; }

        /// <summary>
        /// 随机横向位置（0-1）
        /// </summary>
        [Display(Name = "随机横向位置（0-1）")]
        public double PositionX { get; set; }

        /// <summary>
        /// 随机垂直位置（0-1）
        /// </summary>
        [Display(Name = "随机垂直位置（0-1）")]
        public double PositionY { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
