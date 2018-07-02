using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 车比特会员Dto
    /// </summary>
    public class CarBitCoinRecordDto
    {
        /// <summary>
        /// 车比特会员ID
        /// </summary>
        [Display(Name = "车比特会员ID")]
        public Guid CarBitCoinMemberID { get; set; }

        /// <summary>
        /// 车比特会员名称
        /// </summary>
        [Display(Name = "车比特会员名称")]
        public string CarBitCoinMemberName { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Display(Name = "电话号码")]
        public string MobilePhoneNo { get; set; }

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
