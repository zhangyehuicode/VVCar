using System;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 滞销产品参数设置Dto
    /// </summary>
    public class UnsaleProductSettingDto
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
        /// 滞销数量上限值,低于即视为滞销产品
        /// </summary>
        public int UnsaleQuantity { get; set; }

        /// <summary>
        /// 畅销数量下限值,高于则视为畅销产品
        /// </summary>
        public int SaleWellQuantity { get; set; }

        /// <summary>
        /// 滞销产品提醒营业额参数
        /// </summary>
        public decimal Performence { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
