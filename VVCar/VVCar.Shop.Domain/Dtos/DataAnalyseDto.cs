using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 数据分析Dto
    /// </summary>
    public class DataAnalyseDto
    {
        public DataAnalyseDto()
        {
            dataAnalyseItemDtos = new List<DataAnalyseItemDto>();
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        public Guid MemberID { get; set; }

        /// <summary>
        /// 会员名称
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 会员手机号
        /// </summary>
        public string MemberMobilePhone { get; set; }

        /// <summary>
        /// 消费项目数目
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// 消费总额
        /// </summary>
        public decimal TotalMoney { get; set; }

        /// <summary>
        /// 集合
        /// </summary>
        public ICollection<DataAnalyseItemDto> dataAnalyseItemDtos { get; set; }

    }

    /// <summary>
    /// 数据分析子项Dto
    /// </summary>
    public class DataAnalyseItemDto
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public Guid ProductID { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 消费数量
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal TotalMoney { get; set; }
    }
}
