using System;

namespace YEF.Core.Filter
{
    public class OperateCodeAttribute : Attribute
    {
        /// <summary>
        /// 初始化一个<see cref="OperateCodeAttribute"/>类型的新实例
        /// </summary>
        public OperateCodeAttribute(String code)
        {
            Code = code;
        }

        /// <summary>
        /// 获取 操作符
        /// </summary>
        public String Code { get; private set; }
    }
}
