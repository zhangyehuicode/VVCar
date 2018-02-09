using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 消费/充值 历史纪录过滤器
    /// </summary>
    public class HistoryFilter : BasePageFilter
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 交易门店
        /// </summary>
        public Guid? TradeDepartmentID { get; set; }

        /// <summary>
        /// 会员卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 卡片类型ID
        /// </summary>
        public Guid? CardTypeID { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 微信ID
        /// </summary>
        public string WeChatOpenID { get; set; }

        /// <summary>
        /// 会员状态
        /// </summary>
        public ECardStatus? Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? FinishDate { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public EBusinessType? BusinessType { get; set; }

        /// <summary>
        /// 消费类型
        /// </summary>
        public EConsumeType? ConsumeType { get; set; }

        /// <summary>
        /// 批次代码
        /// </summary>
        public string BatchCode { get; set; }
    }
}
