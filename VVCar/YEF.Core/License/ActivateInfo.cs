using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core.License
{
    /// <summary>
    /// 激活信息
    /// </summary>
    public class ActivateInfo
    {
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 硬件ID
        /// </summary>
        public string HardwareId { get; set; }

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
