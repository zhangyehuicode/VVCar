using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 产品Dto
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 原单价
        /// </summary>
        public decimal BasePrice { get; set; }

        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal PriceSale { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 产品介绍
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 配送说明
        /// </summary>
        public string DeliveryNotes { get; set; }

        /// <summary>
        /// 是否会员卡
        /// </summary>
        public bool IsMemberCard { get; set; }
    }
}
