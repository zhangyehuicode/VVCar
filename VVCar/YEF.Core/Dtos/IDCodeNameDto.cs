using System;

namespace YEF.Core
{
    /// <summary>
    /// 通用型ID,NameDTO
    /// </summary>
    public class IDCodeNameDto
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
        /// 折扣系数
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// 周次
        /// </summary>
        public string Weeks { get; set; }

        /// <summary>
        /// 周次类型
        /// </summary>
        public int WeekType { get; set; }
    }
}
