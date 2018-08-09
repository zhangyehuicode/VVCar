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
        /// 节点文本
        /// </summary>
        public override string Text
        {
            get { return Name + "("+MemberNumbers.ToString()+")"; }
            set { Name = value; }
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid? ID { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 类别编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 是否批发价
        /// </summary>
        public bool IsWholesalePrice { get; set; }

        /// <summary>
        /// 会员数量
        /// </summary>
        public int MemberNumbers { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改日期
        /// </summary>
        public DateTime? LastUpdateDate { get; set; }
    }
}
