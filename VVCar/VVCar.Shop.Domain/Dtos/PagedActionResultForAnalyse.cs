using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 适用于WebApi的统一格式的分页数据结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedActionResultForAnalyse<T>
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
        /// 返回结果，如果<see cref="IsSuccessful"/>值为<see cref="true"/>时有值
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 会员总数
        /// </summary>
        public int TotalMemberCount { get; set; }

        /// <summary>
        /// 错误信息，如果<see cref="IsSuccessful"/>值为<see cref="false"/>时有值
        /// </summary>
        public String ErrorMessage { get; set; }
    }
}
