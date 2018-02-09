using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 会员分组树型DTO
    /// </summary>
    public class MemberGroupTreeDto : TreeNodeModel<MemberGroupTreeDto>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid? ID { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        ///索引
        /// </summary>
        public int Index { get; set; }
    }
}
