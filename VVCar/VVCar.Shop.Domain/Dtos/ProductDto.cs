using System;

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
        /// 是否推荐
        /// </summary>
        public bool IsRecommend { get; set; }

        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal PriceSale { get; set; }

        /// <summary>
        /// 是否批发价
        /// </summary>
        public bool IsWholesale { get; set; }

        /// <summary>
        /// 批发价
        /// </summary>
        public decimal WholesalePrice { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 产品介绍
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 图文介绍
        /// </summary>
        public string GraphicIntroduction { get; set; }

        /// <summary>
        /// 配送说明
        /// </summary>
        public string DeliveryNotes { get; set; }

        /// <summary>
        /// 是否会员卡
        /// </summary>
        public bool IsMemberCard { get; set; }

        /// <summary>
        /// 是否抽成比例
        /// </summary>
        public bool IsCommissionRate { get; set; }

        /// <summary>
        /// 抽成比例(0~100)
        /// </summary>
        public decimal CommissionRate { get; set; }

        /// <summary>
        /// 抽成金额
        /// </summary>
        public decimal CommissionMoney { get; set; }

        /// <summary>
        /// 是否业务员抽成比例
        /// </summary>
        public bool IsSalesmanCommissionRate { get; set; }

        /// <summary>
        /// 业务员抽成比例(0~100)
        /// </summary>
        public decimal SalesmanCommissionRate { get; set; }

        /// <summary>
        /// 业务员抽成金额
        /// </summary>
        public decimal SalesmanCommissionMoney { get; set; }

        /// <summary>
        /// /是否批发价抽成比例
        /// </summary>
        public bool IsWholesaleCommissionRate { get; set; }

        /// <summary>
        /// 批发价抽成比例(0~100) --> 对于服务就是优惠价
        /// </summary>
        public decimal WholesaleCommissionRate { get; set; }

        /// <summary>
        /// 批发价抽成金额
        /// </summary>
        public decimal WholesaleCommissionMoney { get; set; }

        /// <summary>
        /// 是否优惠价施工抽成比例
        /// </summary>
        public bool IsWholesaleConstructionCommissionRate { get; set; }

        /// <summary>
        /// 优惠价施工抽成比例
        /// </summary>
        public decimal WholesaleConstructionCommissionRate { get; set; }

        /// <summary>
        /// 优惠价施工抽成金额
        /// </summary>
        public decimal WholesaleConstructionCommissionMoney { get; set; }
    }
}
