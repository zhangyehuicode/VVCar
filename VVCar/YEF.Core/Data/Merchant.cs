﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace YEF.Core.Data
{
    /// <summary>
    /// 商户
    /// </summary>
    public class Merchant : NormalEntityBase
    {
        /// <summary>
        /// 商户号
        /// </summary>
        [Display(Name = "商户号")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 是否总部
        /// </summary>
        [Display(Name = "是否总部")]
        public bool IsHQ { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
