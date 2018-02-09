using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 卡券报表DTO
    /// </summary>
    public class CouponReportDto
    {
        public CouponReportDto()
        {
            BrowseCoupon = new ReportTemp();
            GetCoupon = new ReportTemp();
            Verification = new ReportTemp();
        }

        /// <summary>
        ///时间
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        ///浏览
        /// </summary>
        public ReportTemp BrowseCoupon { get; set; }

        /// <summary>
        ///领取
        /// </summary>
        public ReportTemp GetCoupon { get; set; }

        /// <summary>
        ///核销
        /// </summary>
        public ReportTemp Verification { get; set; }
    }

    public class ReportTemp
    {
        public ReportTemp()
        {
        }

        public ReportTemp(int number, int times)
        {
            Number = number;
            Times = times;
        }

        /// <summary>
        ///人数
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        ///次数
        /// </summary>
        public int Times { get; set; }
    }

    public class CouponReportExportDto
    {
        /// <summary>
        ///日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        ///浏览量
        /// </summary>
        public int BrowseTime { get; set; }

        /// <summary>
        ///领取人数
        /// </summary>
        public int GetNumber { get; set; }

        /// <summary>
        ///领取次数
        /// </summary>
        public int GetTime { get; set; }

        /// <summary>
        ///核销人数
        /// </summary>
        public int VerificationNumber { get; set; }

        /// <summary>
        ///核销次数
        /// </summary>
        public int VerificationTime { get; set; }
    }

    public class CouponTotalReportDto
    {
        /// <summary>
        ///  优惠券模板编号
        /// </summary>
        public string CouponTemplateCode { get; set; }

        /// <summary>
        ///优惠券名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///浏览量
        /// </summary>
        public int BrowseTimes { get; set; }

        /// <summary>
        ///领取人数
        /// </summary>
        public int GetNumber { get; set; }

        /// <summary>
        ///领取次数
        /// </summary>
        public int GetTimes { get; set; }

        /// <summary>
        ///核销人数
        /// </summary>
        public int VerificationNumber { get; set; }

        /// <summary>
        ///核销次数
        /// </summary>
        public int VerificationTimes { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public string Validity { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 投放时间
        /// </summary>
        public string PutInDate { get; set; }

        /// <summary>
        ///核销率
        /// </summary>
        public decimal VerificationRate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 使用条件
        /// </summary>
        public string UseCondition { get; set; }
    }
}
