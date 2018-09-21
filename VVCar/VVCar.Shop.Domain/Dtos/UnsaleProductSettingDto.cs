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
        /// 滞销产品提醒周期参数(天)
        /// </summary>
        public int PeriodDays { get; set; }

        /// <summary>
        /// 滞销产品提醒数量参数
        /// </summary>
        public int Quantities { get; set; }

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
