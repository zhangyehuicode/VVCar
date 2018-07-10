﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 超能课堂过滤条件
    /// </summary>
    public class SuperClassFilter : BasePageFilter
    {
        /// <summary>
        /// 视频名称
        /// </summary>
        [Display(Name = "视频名称")]
        public string Name { get; set; }
    }
}