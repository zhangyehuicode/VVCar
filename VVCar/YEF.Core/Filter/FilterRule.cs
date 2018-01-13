using System;

namespace YEF.Core.Filter
{
    /// <summary>
    /// 过滤条件
    /// </summary>
    public class FilterRule
    {
        #region ctor.

        /// <summary>
        /// 初始化一个<see cref="FilterRule"/>的新实例
        /// </summary>
        public FilterRule()
        {
            Operate = FilterOperate.Equal;
        }

        /// <summary>
        /// 使用指定数据名称，数据值初始化一个<see cref="FilterRule"/>的新实例
        /// </summary>
        /// <param name="field">数据名称</param>
        /// <param name="value">数据值</param>
        public FilterRule(string field, object value)
            : this()
        {
            Field = field;
            Value = value;
        }

        /// <summary>
        /// 使用指定数据名称，数据值及操作方式初始化一个<see cref="FilterRule"/>的新实例
        /// </summary>
        /// <param name="field">数据名称</param>
        /// <param name="value">数据值</param>
        /// <param name="operate">操作方式</param>
        public FilterRule(string field, object value, FilterOperate operate)
            : this(field, value)
        {
            Operate = operate;
        }

        #endregion

        #region properties

        /// <summary>
        /// 获取或设置 字段名称
        /// </summary>
        public String Field { get; set; }

        /// <summary>
        /// 获取或设置 过滤值
        /// </summary>
        public Object Value { get; set; }

        /// <summary>
        /// 获取或设置 操作类型
        /// </summary>
        public FilterOperate Operate { get; set; }

        #endregion
    }
}
