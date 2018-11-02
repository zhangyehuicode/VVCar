using System;
using VVCar.Shop.Domain.Dtos;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 消费历史数据过滤器
    /// </summary>
    public class ConsumeHistoryFilter : BasePageFilter
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }
        
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 交易单号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public EHistorySource? Source { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
