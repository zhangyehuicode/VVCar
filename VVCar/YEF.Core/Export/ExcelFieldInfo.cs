using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core.Export
{
    /// <summary>
    /// Excel字段属性
    /// </summary>
    public class ExcelFieldInfo
    {
        #region ctor.
        /// <summary>
        /// Excel字段属性
        /// </summary>
        public ExcelFieldInfo()
        {
        }

        /// <summary>
        /// Excel字段属性
        /// </summary>
        /// <param name="index">列序号</param>
        /// <param name="propertyName">属性名</param>
        public ExcelFieldInfo(int index, string propertyName) :
            this(index, propertyName, propertyName)
        {
        }

        /// <summary>
        /// Excel字段属性
        /// </summary>
        /// <param name="index">列序号</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="displayName">列头显示名称</param>
        public ExcelFieldInfo(int index, string propertyName, string displayName) :
            this(index, propertyName, displayName, false)
        {
        }

        /// <summary>
        /// Excel字段属性
        /// </summary>
        /// <param name="index">列序号</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="displayName">列头显示名称</param>
        /// <param name="allowNullValue">允许空值</param>
        public ExcelFieldInfo(int index, string propertyName, string displayName, bool allowNullValue)
        {
            Index = index;
            PropertyName = propertyName;
            DisplayName = displayName;
            AllowNullValue = allowNullValue;
        }
        #endregion

        /// <summary>
        /// 列序号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 列头显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 允许空值
        /// </summary>
        public bool AllowNullValue { get; set; }
    }
}