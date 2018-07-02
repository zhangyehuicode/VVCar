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
    /// <summary>
    /// 车比特记录类型过滤条件
    /// </summary>
    public class CarBitCoinRecordFilter : BasePageFilter
    {
        /// <summary>
        /// 车比特记录类型
        /// </summary>
        [Display(Name = "车比特记录类型")]
        public ECarBitCoinRecordType? CarBitCoinRecordType { get; set; }

        /// <summary>
        /// 会员名称/手机号
        /// </summary>
        public string NamePhone { get; set; }
    }
}
