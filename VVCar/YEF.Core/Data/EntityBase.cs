using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core.Data
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class EntityBase<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public TKey ID { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        [Display(Name = "商户ID")]
        public TKey MerchantID { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        [Display(Name = "逻辑删除")]
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// 主键类型为Guid的实体基类
    /// </summary>
    public abstract class EntityBase : EntityBase<Guid>
    {
    }

    /// <summary>
    /// 包含部门ID的实体接口
    /// </summary>
    public interface IDepartmentEntity
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        Guid DepartmentID { get; set; }
    }
}
