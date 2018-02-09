using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 数据字典类型
    /// </summary>
    public class DataDictType : NormalEntityBase
    {
        /// <summary>
        /// 字典类型代码
        /// </summary>
        [Display(Name = "代码")]
        public String Code { get; set; }

        /// <summary>
        /// 字典类型名称
        /// </summary>
        [Display(Name = "名称")]
        public String Name { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [Display(Name = "序号")]
        public int Index { get; set; }
    }
}
