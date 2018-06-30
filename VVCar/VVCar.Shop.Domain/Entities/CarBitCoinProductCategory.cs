using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 车比特产品类别
    /// </summary>
    public class CarBitCoinProductCategory : EntityBase
    {
        /// <summary>
        /// 车比特产品类别ctor
        /// </summary>
        public CarBitCoinProductCategory()
        {
            SubCarBitCoinProducts = new List<CarBitCoinProduct>();
        }

        /// <summary>
        /// 父级ID
        /// </summary>
        [Display(Name = "父级ID")]
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
        public Guid? CreatedUserID { get; set; }

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
        public virtual ICollection<CarBitCoinProduct> SubCarBitCoinProducts { get; set; }
    }
}
