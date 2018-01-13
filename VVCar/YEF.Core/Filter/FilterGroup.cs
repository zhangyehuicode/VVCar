using System;
using System.Collections.Generic;

namespace YEF.Core.Filter
{
    /// <summary>
    /// 过滤条件组
    /// </summary>
    public class FilterGroup
    {
        #region ctor.

        /// <summary>
        /// 初始化一个<see cref="FilterGroup"/>的新实例
        /// </summary>
        public FilterGroup()
            : this(true)
        {
        }

        public FilterGroup(bool isAnd)
        {
            IsAnd = isAnd;
            Rules = new List<FilterRule>();
            Groups = new List<FilterGroup>();
        }
        #endregion

        #region properties

        /// <summary>
        /// 获取或设置 是否是And关系
        /// </summary>
        public Boolean IsAnd { get; set; }

        /// <summary>
        /// 获取或设置 条件集合
        /// </summary>
        public ICollection<FilterRule> Rules { get; set; }

        /// <summary>
        /// 获取或设置 条件组集合
        /// </summary>
        public ICollection<FilterGroup> Groups { get; set; }
        #endregion

    }
}
