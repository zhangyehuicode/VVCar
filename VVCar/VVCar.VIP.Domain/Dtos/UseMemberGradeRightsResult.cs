using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    public class UseMemberGradeRightsResult
    {
        /// <summary>
        /// 赠送积分
        /// </summary>
        public int GiftPoint { get; set; }

        /// <summary>
        /// 升级等级名称
        /// </summary>
        public string UpGradeName { get; set; }

        /// <summary>
        /// 是否升级
        /// </summary>
        public bool IsUpGrade { get; set; }

        /// <summary>
        /// 会员权益描述
        /// </summary>
        public string GradeRightsDesc { get; set; }
    }
}
