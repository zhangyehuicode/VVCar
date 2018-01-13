using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YEF.Core.Data
{
    /// <summary>
    /// 非泛型版本的实体仓储接口
    /// </summary>
    public interface IRepository : IDependency
    {
        #region methods

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>提交的实体</returns>
        object Add(object entity);

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>提交的实体集合</returns>
        IEnumerable AddRange(IEnumerable entities);

        /// <summary>
        /// 更具主键删除实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>操作影响的行数</returns>
        int DeleteByKey(object key);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        int Delete(object entity);

        /// <summary>
        /// 批量删除删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        int DeleteRange(IEnumerable entities);

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        int Update(object entity);

        /// <summary>
        /// 批量更新实体对象
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        int UpdateRange(IEnumerable entities);

        /// <summary>
        /// 获取指定主键的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>符合主键的实体，不存在时返回null</returns>
        object GetByKey(object key);

        #endregion
    }
}
