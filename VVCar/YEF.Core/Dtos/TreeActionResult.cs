using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YEF.Core.Dtos
{
    /// <summary>
    /// 适用于WebApi的统一格式的树型数据结构
    /// </summary>
    /// <typeparam name="T">树节点的数据类型</typeparam>
    public class TreeActionResult<T>
    {
        private bool _isSuccessful = true;
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool IsSuccessful
        {
            get { return this._isSuccessful; }
            set { this._isSuccessful = value; }
        }

        /// <summary>
        /// 树节点
        /// </summary>
        public IEnumerable<T> Children { get; set; }

        /// <summary>
        /// 错误信息，如果<see cref="IsSuccessful"/>值为<see cref="false"/>时有值
        /// </summary>
        public String ErrorMessage { get; set; }
    }
}
