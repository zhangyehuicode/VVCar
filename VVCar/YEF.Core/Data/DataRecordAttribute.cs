using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YEF.Core.Data
{
    /// <summary>
    /// 数据记录
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class DataRecordAttribute : Attribute
    {
        /// <summary>
        /// 数据记录
        /// </summary>
        /// <param name="direction">数据方向</param>
        public DataRecordAttribute(DataDirection direction)
        {
            Direction = direction;
        }

        /// <summary>
        /// 数据方向
        /// </summary>
        public DataDirection Direction { get; set; }
    }

    /// <summary>
    /// 数据方向
    /// </summary>
    public enum DataDirection
    {
        /// <summary>
        /// 不进行数据数据交换
        /// </summary>
        None = 0,

        /// <summary>
        /// 推送
        /// </summary>
        Push = 1,

        /// <summary>
        /// 上传
        /// </summary>
        Upload = 2,

        /// <summary>
        /// 推送和上传
        /// </summary>
        Both = 3,
    }
}
