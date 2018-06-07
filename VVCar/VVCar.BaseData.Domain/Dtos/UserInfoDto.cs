using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfoDto
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public Guid MerchantID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 到岗时间
        /// </summary>
        public DateTime? DutyTime { get; set; }

        /// <summary>
        /// 本月业绩
        /// </summary>
        public decimal MonthPerformance { get; set; }

        /// <summary>
        /// 总业绩
        /// </summary>
        public decimal TotalPerformance { get; set; }

        /// <summary>
        /// 业绩排名
        /// </summary>
        public int PerformanceRanking { get; set; }

        /// <summary>
        /// 客户服务量
        /// </summary>
        public int CustomerServiceCount { get; set; }

        /// <summary>
        /// 本月收入
        /// </summary>
        public decimal MonthIncome { get; set; }

        /// <summary>
        /// 所属门店ID
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// 所属门店编号
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 所属门店名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 是否可以登录管理后台
        /// </summary>
        public bool CanLoginAdminPortal { get; set; }
    }
}
