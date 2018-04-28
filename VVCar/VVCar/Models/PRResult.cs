using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VVCar.Models
{
    /// <summary>
    /// 车牌识别结果
    /// </summary>
    public class PRResult
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }
    }
}