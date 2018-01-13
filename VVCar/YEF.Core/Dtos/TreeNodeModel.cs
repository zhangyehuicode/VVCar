using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YEF.Core.Dtos
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNodeModel<T>
    {
        /// <summary>
        /// 节点文本
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// 表示是否是终极节点
        /// </summary>
        public virtual bool leaf
        {
            get
            {
                return this.Children == null || this.Children.Count() == 0;
            }
            set { }
        }

        /// <summary>
        /// 是否默认展开
        /// </summary>
        public virtual bool expanded { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public virtual IEnumerable<T> Children { get; set; }
    }
}
