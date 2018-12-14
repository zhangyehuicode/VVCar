using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    public class MaterialPublishItemDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 素材名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 素材路径
        /// </summary>
        public string Url { get; set; }
    }
}
