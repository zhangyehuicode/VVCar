using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace YEF.Core.License
{
    /// <summary>
    /// 授权异常，当系统出现授权异常时。
    /// </summary>
    [Serializable]
    public class LicenseException : Exception    
    {
        /// <summary>
        /// 授权异常，当系统出现授权异常时。
        /// </summary>
        public LicenseException()
        { }

        /// <summary>
        /// 授权异常，当系统出现授权异常时。
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="errorCode">错误代码</param>
        public LicenseException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// 授权异常，当系统出现授权异常时。
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="inner">内部异常</param>
        /// <param name="errorCode">错误代码</param>
        public LicenseException(string message, Exception inner, int errorCode)
            : base(message, inner)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// 授权异常，当系统出现授权异常时。
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected LicenseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode { get; set; }
    }
}
