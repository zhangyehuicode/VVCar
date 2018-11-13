using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    public class AdvisementSettingDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// 封面路径
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 图文内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 今日浏览人数
        /// </summary>
        public int FocusTodayCount { get; set; }

        /// <summary>
        /// 总浏览人数
        /// </summary>
        public int TotalFocusCount { get; set; }
    }
}
