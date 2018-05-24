﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 历史数据分析Dto
    /// </summary>
    public class HistoryDataAnalysisDto
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 挖掘空间
        /// </summary>
        public int MiningSpace { get; set; }

        /// <summary>
        /// 历史服务次数
        /// </summary>
        public int ServiceTime { get; set; }
    }
}
