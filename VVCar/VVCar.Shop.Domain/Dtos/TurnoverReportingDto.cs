using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            MemberCount = new List<TurnoverDto>();
        }

        /// <summary>
        /// 市场营业总额
        /// </summary>
        public decimal TotalMarketTurnover { get; set; }

        /// <summary>
        /// 今日营业额
        /// </summary>
        public decimal TodayTurnover { get; set; }

        /// <summary>
        /// 总会员数
        /// </summary>
        public int TotalMember { get; set; }

        /// <summary>
        /// 今日会员
        /// </summary>
        public int TodayMember { get; set; }

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

        /// <summary>
        /// 会员数量
        /// </summary>
        public List<TurnoverDto> MemberCount { get; set; }
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
        /// 营业额/数量
        /// </summary>
        public decimal Turnover { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
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
        /// 员工ID
        /// </summary>
        public Guid StaffID { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 员工编码
        /// </summary>
        public string StaffCode { get; set; }

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

        /// <summary>
        /// 当前业绩
        /// </summary>
        public decimal CurrentPerformance { get; set; }

        /// <summary>
        /// 当前抽成
        /// </summary>
        public decimal CurrentCommission { get; set; }

        /// <summary>
        /// 当前客户服务量
        /// </summary>
        public int CurrentCustomerServiceCount { get; set; }

        /// <summary>
        /// 月总收入
        /// </summary>
        public decimal MonthIncome
        {
            get
            {
                return BasicSalary + Subsidy + MonthCommission;
            }
        }

        /// <summary>
        /// 总开发门店数
        /// </summary>
        public int TotalOpenAccountCount { get; set; }

        /// <summary>
        /// 当月开发门店数
        /// </summary>
        public int MonthOpenAccountCount { get; set; }
    }

    /// <summary>
    /// 门店开发业绩
    /// </summary>
    public class DepartmentPerformance
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        [Display(Name = "员工ID")]
        public Guid StaffID { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        [Display(Name = "员工姓名")]
        public string StaffName { get; set; }

        /// <summary>
        /// 员工编码
        /// </summary>
        [Display(Name = "员工编码")]
        public string StaffCode { get; set; }

        /// <summary>
        /// 总开发门店数量
        /// </summary>
        [Display(Name = "总开发门店数量")]
        public decimal TotalDepartmentNumber { get; set; }

        /// <summary>
        /// 当前开发门店数量
        /// </summary>
        [Display(Name = "当前开发门店数量")]
        public decimal CurrentDepartmentNumber { get; set; }

        /// <summary>
        /// 当月开发门店数量
        /// </summary>
        [Display(Name = "当月开发门店数量")]
        public decimal MonthDepartmentNumber { get; set; }
    }

    /// <summary>
    /// 代理商门店开发报表Dto
    /// </summary>
    public class OpenAccountReportingDto
    {
        public OpenAccountReportingDto()
        {
            TotalOpenAccount = new List<TurnoverDto>();
        }

        /// <summary>
        /// 门店开发总数
        /// </summary>
        [Display(Name = "门店开发总数")]
        public List<TurnoverDto> TotalOpenAccount { get; set; }
    }

    /// <summary>
    /// 月开发门店业绩
    /// </summary>
    public class MonthOpenAccountPerformanceDto
    {
        /// <summary>
        /// 月总开发门店数
        /// </summary>
        public decimal TotalOpenAccountCount { get; set; }
    }
}
