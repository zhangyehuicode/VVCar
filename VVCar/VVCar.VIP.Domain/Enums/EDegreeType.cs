using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 学历类型
    /// </summary>
    public enum EDegreeType
    {
        /// <summary>
        /// 不限
        /// </summary>
        [Display(Name = "不限")]
        NoLimit = -1,

        /// <summary>
        /// 大专
        /// </summary>
        [Display(Name = "大专")]
        College = 0,

        /// <summary>
        /// 本科
        /// </summary>
        [Display(Name = "本科")]
        Bachelor = 1,

        /// <summary>
        /// 硕士
        /// </summary>
        [Display(Name = "硕士")]
        Master = 2,

        /// <summary>
        /// 博士
        /// </summary>
        [Display(Name = "博士")]
        Doctor = 3,
    }
}
