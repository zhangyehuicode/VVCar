using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YEF.Core;

namespace YEF.Core.Data
{
    /// <summary>
    /// 异步泛型主键实体仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体主键类型</typeparam>
    public interface IRepositoryAsync<TEntity, in TKey> : IDependency where TEntity : NormalEntityBase<TKey>
    {
        /// <summary>
        /// 异步插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        Task<int> AddAsync(TEntity entity);

        /// <summary>
        /// 异步批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// 异步删除指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteAsync(TKey key);

        /// <summary>
        /// 异步删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 异步批量删除删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 异步更新实体对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// 异步批量更新实体对象
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>操作影响的行数</returns>
        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression);

        /// <summary>
        /// 异步获取指定主键的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>符合主键的实体，不存在时返回null</returns>
        Task<TEntity> GetByKeyAsync(TKey key);

        /// <summary>
        /// 异步获取符合条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>符合条件的实体，不存在时返回null</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 异步获取记录数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// 异步获取符合条件的记录数
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 异步是否存在符合条件的记录
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }

    public interface IRepositoryAsync<TEntity> : IRepositoryAsync<TEntity, Guid> where TEntity : NormalEntityBase<Guid>
    {
    }
}
