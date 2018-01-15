using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    public class SysMenu : NormalEntityBase
    {
        public SysMenu()
        {
            Children = new List<SysMenu>();
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Display(Name = "菜单名称")]
        public string Name { get; set; }

        /// <summary>
        /// 组件名称
        /// </summary>
        [Display(Name = "组件名称")]
        public string Component { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        [Display(Name = "链接地址")]
        public string SysMenuUrl { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        [Display(Name = "父菜单ID")]
        public Guid? ParentID { get; set; }

        /// <summary>
        /// 是否叶子节点
        /// </summary>
        [Display(Name = "是否叶子节点")]
        public bool IsLeaf { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        [Display(Name = "菜单类型")]
        public ESysMenuType Type { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 菜单顺序
        /// </summary>
        [Display(Name = "菜单顺序")]
        public int Index { get; set; }

        /// <summary>
        /// 下级节点
        /// </summary>
        [Display(Name = "下级节点")]
        public virtual ICollection<SysMenu> Children { get; set; }
    }
}
