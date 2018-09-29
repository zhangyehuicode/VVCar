using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 数据分析条件过滤器
    /// </summary>
    public class DataAnalyseFilter : BasePageFilter
    {
        /// <summary>
        /// 时间条件
        /// </summary>
        public ETimeSelect? TimeSelect { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public EMemberType? MemberType { get; set; }

        /// <summary>
        /// 大客户
        /// </summary>
        public EBigMember? BigMember { get; set; }

        /// <summary>
        /// 会员忠诚度
        /// </summary>
        public ELoyalMember? LoyalMember { get; set; }

        /// <summary>
        /// 会员流失等级
        /// </summary>
        public ELoseMember? LoseMember { get; set; }
    }

    /// <summary>
    /// 时间过滤
    /// </summary>
    public enum ETimeSelect
    {
        /// <summary>
        /// 当日
        /// </summary>
        [Description("当日")]
        ByDay = 1,
        
        /// <summary>
        /// 当月
        /// </summary>
        [Description("当月")]
        ByMonth = 2,
    }

    /// <summary>
    /// 会员类型
    /// </summary>
    public enum EMemberType
    {
        /// <summary>
        /// 小于2千（普通会员）
        /// </summary>
        NomalMember = 1,

        /// <summary>
        /// 2千~5千（白银会员）
        /// </summary>
       SilverMember = 2,

        /// <summary>
        /// 5千~1万（黄金会员）
        /// </summary>
        GoldMember = 3,

        /// <summary>
        /// 大于1万（铂金会员）
        /// </summary>
        PlatinumMember = 4,
    }

    /// <summary>
    /// 大客户
    /// </summary>
    public enum EBigMember
    {
        /// <summary>
        /// 普通大客户(年消费大于1万)
        /// </summary>
        Nomal = 1,

        /// <summary>
        /// 土豪大客户(年消费大于1万)
        /// </summary>
        Rich = 2,
    }

    /// <summary>
    /// 忠诚度
    /// </summary>
    public enum ELoyalMember
    {
        /// <summary>
        /// 每月消费1次（一般）
        /// </summary>
        NomalLoyal = 1,

        /// <summary>
        /// 每月消费2次（良好）
        /// </summary>
        GoodLoyal = 2,

        /// <summary>
        /// 每月消费3次以上（绝对忠诚）
        /// </summary>
        AbsoluteLoyalty = 3,
    }

    /// <summary>
    /// 会员流失等级
    /// </summary>
    public enum ELoseMember
    {
        /// <summary>
        /// 三个月消费未消费
        /// </summary>
        NomalLose = 1,

        /// <summary>
        /// 六个月消费未消费
        /// </summary>
        SevereLose = 2,

        /// <summary>
        /// 十二个月消费未消费
        /// </summary>
        AbsoluteLose = 3,
    }
}
