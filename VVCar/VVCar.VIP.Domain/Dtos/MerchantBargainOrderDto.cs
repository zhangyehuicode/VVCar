﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 门店砍价DTO
    /// </summary>
    public class MerchantBargainOrderDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 砍价名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 砍价价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public Guid ProductID { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal PriceSale { get; set; }

        /// <summary>
        /// 拼单人数
        /// </summary>
        public int PeopleCount { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime PutawayTime { get; set; }

        /// <summary>
        /// 下架时间
        /// </summary>
        public DateTime SoleOutTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
