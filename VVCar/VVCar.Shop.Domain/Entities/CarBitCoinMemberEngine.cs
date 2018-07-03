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
    /// 车比特会员引擎
    /// </summary>
    public class CarBitCoinMemberEngine : EntityBase
    {
        /// <summary>
        /// 车比特会员ID
        /// </summary>
        [Display(Name = "车比特会员ID")]
        public Guid CarBitCoinMemberID { get; set; }

        /// <summary>
        /// 车比特会员
        /// </summary>
        public virtual CarBitCoinMember CarBitCoinMember { get; set; }

        /// <summary>
        /// 产品（引擎）ID
        /// </summary>
        [Display(Name = "产品（引擎）ID")]
        public Guid CarBitCoinProductID { get; set; }

        /// <summary>
        /// 产品（引擎）
        /// </summary>
        public virtual CarBitCoinProduct CarBitCoinProduct { get; set; }

        /// <summary>
        /// 产品（引擎）名称
        /// </summary>
        [Display(Name = "产品（引擎）名称")]
        public string CarBitCoinProductName { get; set; }

        /// <summary>
        /// 马力
        /// </summary>
        [Display(Name = "马力")]
        public int Horsepower { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        [Display(Name = "来源")]
        public ECarBitCoinMemberEngineSource Source { get; set; }

        /// <summary>
        /// 交易单号
        /// </summary>
        [Display(Name = "交易单号")]
        public string TradeNo { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Display(Name = "数量")]
        public int Quantity { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [Display(Name = "图片")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
