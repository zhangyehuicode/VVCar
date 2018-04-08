﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 卡券适用门店
    /// </summary>
    public class CouponApplyStoreDto
    {
        /// <summary>
        /// 门店名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 公交站
        /// </summary>
        public string BusStation { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string TelPhone { get; set; }
    }
}