using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Context;

namespace YEF.Core.License
{
    /// <summary>
    /// 许可证信息
    /// </summary>
    internal class LicenseInfo : ILicenseInfo
    {
        public LicenseInfo(bool isValid, DateTime expiredDate)
        {
#if DEBUG
            IsValid = true;
#else
            IsValid = isValid;
#endif
            ExpiredDate = expiredDate;
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiredDate { get; private set; }
    }
}
