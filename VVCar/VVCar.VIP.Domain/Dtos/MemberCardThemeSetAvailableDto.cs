using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    public class MemberCardThemeSetAvailableDto
    {
        /// <summary>
        /// 主题ID
        /// </summary>
        public Guid ThemeID { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
