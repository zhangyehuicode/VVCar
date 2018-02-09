using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 系统信息
    /// </summary>
    public class SystemInfoDto
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 站点域名信息
        /// </summary>
        public string SiteDomain { get; set; }
    }
}
