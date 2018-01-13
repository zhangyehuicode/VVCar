using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core
{
    /// <summary>
    /// 领域异常，主要用在业务逻辑出错，需要终止当前业务时
    /// </summary>
    [Serializable]
    public class DomainException: Exception
    {
        /// <summary>
        /// 领域异常，主要用在业务逻辑出错，需要终止当前业务时
        /// </summary>
        public DomainException()
        { }

        /// <summary>
        /// 领域异常，主要用在业务逻辑出错，需要终止当前业务时
        /// </summary>
        /// <param name="message">错误信息</param>
        public DomainException(string message)
            : base(message)
        { }

        /// <summary>
        /// 领域异常，主要用在业务逻辑出错，需要终止当前业务时
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="inner">内部异常</param>
        public DomainException(string message, Exception inner)
            : base(message, inner)
        { }

        protected DomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
