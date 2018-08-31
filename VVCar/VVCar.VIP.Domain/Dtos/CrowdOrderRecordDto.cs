using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 发起拼单记录Dto
    /// </summary>
    public class CrowdOrderRecordDto
    {
        /// <summary>
        /// ctor.
        /// </summary>
        public CrowdOrderRecordDto()
        {
            CrowdOrderRecordItemList = new List<CrowdOrderRecordItem>();
        }

        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 拼单ID
        /// </summary>
        public Guid CrowdOrderID { get; set; }

        /// <summary>
        /// 拼单
        /// </summary>
        public CrowdOrder CrowdOrder { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public Guid ProductID { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public CarBitCoinProduct CarBitCoinProduct { get; set; }

        /// <summary>
        /// 已加入拼单人数
        /// </summary>
        public int JoinPeople { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 拼单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 拼单价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 拼单人数
        /// </summary>
        public int PeopleCount { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal PriceSale { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 是否可以购买
        /// </summary>
        public bool IsCanBuy { get; set; }

        /// <summary>
        /// 发起拼单记录子项
        /// </summary>
        public List<CrowdOrderRecordItem> CrowdOrderRecordItemList { get; set; }
    }
}
