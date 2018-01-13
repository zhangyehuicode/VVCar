using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core.Context
{
    /// <summary>
    /// License信息
    /// </summary>
    public interface ILicenseInfo
    {
        /// <summary>
        /// 是否有效
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// 过期时间
        /// </summary>
        DateTime ExpiredDate { get; }
    }
}
