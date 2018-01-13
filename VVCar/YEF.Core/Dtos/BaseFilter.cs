using System;
using System.ComponentModel.DataAnnotations;

namespace YEF.Core.Dtos
{
    /// <summary>
    /// 基础过滤器
    /// </summary>
    public class BaseFilter
    {
        /// <summary>
        /// 是否加载全部数据
        /// </summary>
        [Required]
        public bool All { get; set; }
    }
}
