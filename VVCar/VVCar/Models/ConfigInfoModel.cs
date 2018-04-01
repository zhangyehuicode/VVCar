using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VVCar.Models
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public class ConfigInfoModel
    {
        /// <summary>
        /// 站点域名
        /// </summary>
        public string SiteDomain { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 门店编号
        /// </summary>
        public string DepartmentCode { get; set; }
    }
}