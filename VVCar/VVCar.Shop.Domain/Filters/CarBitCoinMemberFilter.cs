using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;
using System.ComponentModel;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 车比特会员过滤条件
    /// </summary>
    public class CarBitCoinMemberFilter : BasePageFilter
    {
        /// <summary>
        /// OpenID
        /// </summary>
        public string OpenID { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 排序方向
        /// </summary>
        public ESortDirection SortDirection { get; set; }
    }

    public enum ESortDirection
    {
        /// <summary>
        /// 马力
        /// </summary>
        [Description("马力")]
        Horsepower = 0,

        /// <summary>
        /// 车比特
        /// </summary>
        [Description("车比特")]
        CarBitCoin = 1,
    }
}
