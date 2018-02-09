using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 卡片主题类别菜单
    /// </summary>
    public class CardThemeCategoryMenu
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
        /// 表示是否是终极节点
        /// </summary>
        public bool leaf { get; set; }

        /// <summary>
        /// 是否默认展开
        /// </summary>
        public bool expanded { get; set; }

        /// <summary>
        /// 下级菜单
        /// </summary>
        public virtual IEnumerable<CardThemeCategoryMenu> Children { get; set; }
    }
}
