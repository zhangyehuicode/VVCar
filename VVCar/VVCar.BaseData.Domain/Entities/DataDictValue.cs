using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using YEF.Core.Data;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 数据字典值
    /// </summary>
    public class DataDictValue : NormalEntityBase
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        [Display(Name = "字典类型")]
        public string DictType { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        [Display(Name = "字典值")]
        public string DictValue { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        [Display(Name = "字典名称")]
        public string DictName { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [Display(Name = "序号")]
        public int Index { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}
