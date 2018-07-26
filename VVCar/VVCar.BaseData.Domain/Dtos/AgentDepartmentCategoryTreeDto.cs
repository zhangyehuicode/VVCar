using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 门店分类树形Dto
    /// </summary>
    public class AgentDepartmentCategoryTreeDto : TreeNodeModel<AgentDepartmentCategoryTreeDto>
    {
        /// <summary>
        /// 节点文本
        /// </summary>
        public override string Text
        {
            get { return Name; }
            set { Name = value; }
        }

        /// <summary>
        /// 主键
        /// </summary>
        public Guid? ID { get; set; }

        /// <summary>
        /// 父级ID
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
        /// 备注
        /// </summary>
        public string Remark { get; set; }

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

        /// <summary>
        /// 排序
        /// </summary>
        public int Index { get; set; }
    }
}
