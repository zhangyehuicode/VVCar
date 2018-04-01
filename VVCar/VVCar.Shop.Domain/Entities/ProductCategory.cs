using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 产品类别
    /// </summary>
    public class ProductCategory : EntityBase
    {
        /// <summary>
        /// 产品类别ctor
        /// </summary>
        public ProductCategory()
        {
            SubProducts = new List<Product>();
        }

        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 类别编号
        /// </summary>
        [Display(Name = "类别编号")]
        public string Code { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [Display(Name = "类别名称")]
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public int Index { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [Display(Name = "最后修改人")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改日期
        /// </summary>
        [Display(Name = "最后修改日期")]
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// 下级子产品
        /// </summary>
        public virtual ICollection<Product> SubProducts { get; set; }
    }
}
