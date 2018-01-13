using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using YEF.Core.Dtos;

namespace YEF.Core.Domain
{
    /// <summary>
    /// 领域服务接口
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IDomainService<TRepository, TEntity, in TKey> : IDependency
        where TRepository : IRepository<TEntity, TKey>
        where TEntity : EntityBase<TKey>, new()
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Delete(TKey key);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(TEntity entity);

        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity Get(TKey key);

        /// <summary>
        /// 获得符合条件的数据
        /// </summary>
        /// <param name="predicate">where条件</param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagedResultDto<TEntity> GetPagerList(Expression<Func<TEntity, bool>> predicate, int startIndex, int pageSize);

        /// <summary>
        /// 获取符合条件的记录数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 是否存在符合条件的记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> predicate);
    }
}