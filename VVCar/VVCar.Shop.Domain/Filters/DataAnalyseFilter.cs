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
        /// 会员昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 时间条件
        /// </summary>
        public ETimeSelect? TimeSelect { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public EMemberType? MemberType { get; set; }

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
    /// 忠诚度
    /// </summary>
    public enum ELoyalMember
    {
        /// <summary>
        /// 月消费小于1次（不忠诚）
        /// </summary>
        NoLoyal = 1,

        /// <summary>
        /// 月消费1~3次（缺乏忠诚）
        /// </summary>
        LessLoyal = 2,

        /// <summary>
        /// 月消费3~5次（一般忠诚）
        /// </summary>
        NomalLoyal = 3,


        /// <summary>
        /// 月消费5~8次（良好忠诚）
        /// </summary>
        GoodLoyal = 4,

        /// <summary>
        /// 月消费大于8次（绝对忠诚）
        /// </summary>
        AbsoluteLoyalty = 4,
    }

    /// <summary>
    /// 会员流失等级
    /// </summary>
    public enum ELoseMember
    {
        /// <summary>
        /// 三个月消费8~10次（没有流失）
        /// </summary>
        NoLose = 1,

        /// <summary>
        /// 三个月消费5~8次（轻微流失）
        /// </summary>
        LightLose = 2,

        /// <summary>
        /// 三个月消费3~5次（大量流失）
        /// </summary>
        HeavyLose = 3,

        /// <summary>
        /// 三个月消费1~3次（严重流失）
        /// </summary>
        SeverLose = 3,

        /// <summary>
        /// 三个月消费小于1次（完全流失）
        /// </summary>
        AbsoluteLose = 4,
    }
}
