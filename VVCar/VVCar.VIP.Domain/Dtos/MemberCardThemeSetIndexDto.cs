using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 卡片主题调整顺序Dto
    /// </summary>
    public class MemberCardThemeSetIndexDto
    {
        /// <summary>
        /// 主题ID
        /// </summary>
        public Guid CardThemeID { get; set; }

        /// <summary>
        /// 调整方向 0：下调，1：上调
        /// </summary>
        public int Direction { get; set; }
    }
}
