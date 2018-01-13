using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core.Dtos
{
    /// <summary>
    /// 统一格式的分页结果集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResultDto<T> where T : class
    {
        #region ctor.

        /// <summary>
        /// 统一格式的分页结果集
        /// </summary>
        public PagedResultDto()
        {
        }

        /// <summary>
        /// 统一格式的分页结果集
        /// </summary>
        /// <param name="totalCount">总数</param>
        /// <param name="items">结果集</param>
        public PagedResultDto(int totalCount, IEnumerable<T> items)
        {
            this.TotalCount = totalCount;
            this.Items = items;
        }

        #endregion

        #region properties
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 数据集合
        /// </summary>
        public IEnumerable<T> Items { get; set; }
        #endregion
    }
}
