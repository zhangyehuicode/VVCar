using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 寻客侠广告浏览记录
    /// </summary>
    public class AdvisementBrowseHistoryDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 会员名称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 开始浏览时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束浏览时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 停留时间(秒)
        /// </summary>
        public decimal Period { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
