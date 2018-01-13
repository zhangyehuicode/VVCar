using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YEF.Core.Export
{
    /// <summary>
    /// 导出html table时的样式 class设置接口
    /// </summary>
    public interface IExportStyle
    {
        /// <summary>
        /// 获取table的tr标签的class属性
        /// </summary>
        /// <returns></returns>
        string GetRowCssClass();

        /// <summary>
        /// 获取table的td标签 class
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        string GetCellCssClass(string propertyName, object value);

        /// <summary>
        /// 获取table的th标签 class
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        string GetHeadCellCssClass(string propertyName);
    }
}
