using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 管理后台导航菜单
    /// </summary>
    public class SysNavMenuDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid? ID { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public Guid? ParentID { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string iconCls { get; set; }

        /// <summary>
        /// 表示是否是终极节点
        /// </summary>
        public bool leaf { get; set; }

        /// <summary>
        /// 是否默认展开
        /// </summary>
        public bool expanded { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string SysMenuUrl { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public ESysMenuType Type { get; set; }

        /// <summary>
        /// 组件名称
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 菜单顺序
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 下级菜单
        /// </summary>
        public virtual IEnumerable<SysNavMenuDto> Children { get; set; }
    }
}
