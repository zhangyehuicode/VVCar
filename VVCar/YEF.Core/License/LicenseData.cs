using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core.License
{
    /// <summary>
    /// 序列号
    /// </summary>
    internal class LicenseData
    {
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 硬件Id
        /// </summary>
        public string HardwareId { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiredDate { get; set; }
    }
}
