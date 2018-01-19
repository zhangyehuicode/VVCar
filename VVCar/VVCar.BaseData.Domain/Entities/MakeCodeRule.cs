using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.BaseData.Domain.Enums;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 编码规则
    /// </summary>
    public class MakeCodeRule : NormalEntityBase
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "编号")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 是否手动填写
        /// </summary>
        [Display(Name = "是否手动填写")]
        public bool IsManualMake { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        [Display(Name = "长度")]
        public int Length { get; set; }

        /// <summary>
        /// 当前值
        /// </summary>
        public int CurrentValue { get; set; }

        /// <summary>
        /// 前缀1规则
        /// </summary>
        [Display(Name = "前缀1规则")]
        public ECodePrefixRule Prefix1Rule { get; set; }

        /// <summary>
        /// 前缀1长度
        /// </summary>
        [Display(Name = "前缀1长度")]
        public int Prefix1Length { get; set; }

        /// <summary>
        /// 前缀1
        /// </summary>
        [Display(Name = "前缀1")]
        public string Prefix1 { get; set; }

        /// <summary>
        /// 前缀2规则
        /// </summary>
        [Display(Name = "前缀2规则")]
        public ECodePrefixRule Prefix2Rule { get; set; }

        /// <summary>
        /// 前缀2长度
        /// </summary>
        [Display(Name = "前缀2长度")]
        public int Prefix2Length { get; set; }

        /// <summary>
        /// 前缀2
        /// </summary>
        [Display(Name = "前缀2")]
        public string Prefix2 { get; set; }

        /// <summary>
        /// 前缀3规则
        /// </summary>
        [Display(Name = "前缀3规则")]
        public ECodePrefixRule Prefix3Rule { get; set; }

        /// <summary>
        /// 前缀3长度
        /// </summary>
        [Display(Name = "前缀3长度")]
        public int Prefix3Length { get; set; }

        /// <summary>
        /// 前缀3
        /// </summary>
        [Display(Name = "前缀3")]
        public string Prefix3 { get; set; }
    }
}
