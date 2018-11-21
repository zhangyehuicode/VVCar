using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using YEF.Core;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 车检详情Dto
    /// </summary>
    public class CarInspectionDetailsDto
    {
        public CarInspectionDetailsDto()
        {
            ImgList = new List<CarInspectionDetailsImg>();
        }

        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 检查部位
        /// </summary>
        public ECarInspectionPart Part { get; set; }

        /// <summary>
        /// 检查部位文本
        /// </summary>
        public string PartText { get { return Part.GetDescription(); } }

        /// <summary>
        /// 状态
        /// </summary>
        public ECarInspectionStatus Status { get; set; }

        /// <summary>
        /// 状态文本
        /// </summary>
        public string StatusText { get { return Status.GetDescription(); } }

        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public ICollection<CarInspectionDetailsImg> ImgList { get; set; }
    }
}
