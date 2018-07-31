using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    public class OrderFilter : BasePageFilter
    {
        /// <summary>
        /// OpenID
        /// </summary>
        public string OpenID { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [Display(Name = "订单号")]
        public string TradeNo { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = "联系人")]
        public string LinkMan { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        public string Phone { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        [Display(Name = "收货地址")]
        public string Address { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        [Display(Name = "快递单号")]
        public string ExpressNumber { get; set; }

        /// <summary>
        /// 订单号/联系人/联系电话/收货地址/快递单号
        /// </summary>
        [Display(Name = "订单号/联系人/联系电话/收货地址/快递单号")]
        public string TNoLMPAddEN { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public EOrderStatus? Status { get; set; }

        /// <summary>
        /// 是否只获取物流数据
        /// </summary>
        [Display(Name = "是否只获取物流数据")]
        public bool IsLogistics { get; set; }

    }
}
