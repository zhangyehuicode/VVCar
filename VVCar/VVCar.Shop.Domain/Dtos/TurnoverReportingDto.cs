using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 营业报表Dto
    /// </summary>
    public class TurnoverReportingDto
    {
        /// <summary>
        /// constructor
        /// </summary>
        public TurnoverReportingDto()
        {
            TotalTurnover = new List<TurnoverDto>();
            PickUpOrderTurnover = new List<TurnoverDto>();
            MemberCardTurnover = new List<TurnoverDto>();
            ShopTurnover = new List<TurnoverDto>();
        }

        /// <summary>
        /// 总营业额
        /// </summary>
        public List<TurnoverDto> TotalTurnover { get; set; }

        /// <summary>
        /// 接车单营业额
        /// </summary>
        public List<TurnoverDto> PickUpOrderTurnover { get; set; }

        /// <summary>
        /// 会员卡营业额
        /// </summary>
        public List<TurnoverDto> MemberCardTurnover { get; set; }

        /// <summary>
        /// 商城营业额
        /// </summary>
        public List<TurnoverDto> ShopTurnover { get; set; }
    }

    /// <summary>
    /// 营业额Dto
    /// </summary>
    public class TurnoverDto
    {
        /// <summary>
        /// 月/号
        /// </summary>
        public int Unit { get; set; }

        /// <summary>
        /// 营业额
        /// </summary>
        public decimal Turnover { get; set; }
    }

    /// <summary>
    /// 月营业额
    /// </summary>
    public class MonthTurnoverDto
    {
        /// <summary>
        /// 总营业额
        /// </summary>
        public decimal TotalTurnover { get; set; }

        /// <summary>
        /// 接车单营业额
        /// </summary>
        public decimal PickUpOrderTurnover { get; set; }

        /// <summary>
        /// 商城营业额
        /// </summary>
        public decimal ShopTurnover { get; set; }
    }

    /// <summary>
    /// 员工绩效
    /// </summary>
    public class StaffPerformance
    {
        /// <summary>
        /// 总业绩
        /// </summary>
        public decimal TotalPerformance { get; set; }

        /// <summary>
        /// 总抽成
        /// </summary>
        public decimal TotalCommission { get; set; }

        /// <summary>
        /// 当月业绩
        /// </summary>
        public decimal MonthPerformance { get; set; }

        /// <summary>
        /// 当月抽成
        /// </summary>
        public decimal MonthCommission { get; set; }

        /// <summary>
        /// 底薪
        /// </summary>
        public decimal BasicSalary { get; set; }

        /// <summary>
        /// 奖励/补贴
        /// </summary>
        public decimal Subsidy { get; set; }

        /// <summary>
        /// 客户服务量
        /// </summary>
        public int CustomerServiceCount { get; set; }

        /// <summary>
        /// 月客户服务量
        /// </summary>
        public int MonthCustomerServiceCount { get; set; }
    }
}
