using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.Shop.Domain.Enums;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 车比特记录
    /// </summary>
    public class CarBitCoinRecord : EntityBase
    {
        /// <summary>
        /// 车比特会员ID
        /// </summary>
        [Display(Name = "车比特会员ID")]
        public Guid CarBitCoinMemberID { get; set; }

        /// <summary>
        /// 车比特会员
        /// </summary>
        public CarBitCoinMember CarBitCoinMember { get; set; }

        /// <summary>
        /// 车比特记录类型
        /// </summary>
        [Display(Name = "车比特记录类型")]
        public ECarBitCoinRecordType CarBitCoinRecordType { get; set; }

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
        /// 交易单号（商城订单、接车单、币交易、购买引擎）
        /// </summary>
        [Display(Name = "交易单号（商城订单、接车单、币交易、购买引擎）")]
        public string TradeNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
