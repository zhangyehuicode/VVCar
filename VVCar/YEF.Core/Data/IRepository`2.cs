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
    /// 泛型主键实体仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体主键类型</typeparam>
    public interface IRepository<TEntity, in TKey> : IDependency where TEntity : NormalEntityBase<TKey>, new()
    {
        #region properties

        /// <summary>
        /// 获取 当前单元操作对象
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// 获取 当前实体类型的查询数据集
        /// </summary>
        IQueryable<TEntity> Entities { get; }

        /// <summary>
        /// 是否禁用记录数据更新
        /// </summary>
        bool IsDisableRecordUpdate { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// 获取IQueryable对象
        /// </summary>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <returns></returns>
        IQueryable<TEntity> GetQueryable(bool trackEnabled = true);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>提交的实体</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>提交的实体集合</returns>
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entities);

        /// <summary>
        /// 新增或更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        int AddOrUpdate(object entity);

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>提交的实体集合</returns>
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        int Delete(TEntity entity);

        /// <summary>
        /// 批量删除删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        int Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        int DeleteByKey(TKey key);

        /// <summary>
        /// 删除所有符合特定条件的实体(此方法不会产品DataUpdateRecord记录)
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        int Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 批量删除删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        int DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        int Update(TEntity entity);

        /// <summary>
        /// 批量更新实体对象
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        int UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 批量更新(此方法不会产品DataUpdateRecord记录)
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>操作影响的行数</returns>
        int Update(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression);

        /// <summary>
        /// 获取指定主键的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <returns>符合主键的实体，不存在时返回null</returns>
        TEntity GetByKey(TKey key, bool trackEnabled = true);

        /// <summary>
        /// 获取符合条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>符合条件的实体，不存在时返回null</returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 获取符合条件的记录数
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 是否存在符合条件的记录
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取贪婪加载导航属性的查询数据集
        /// </summary>
        /// <param name="path">属性表达式，表示要贪婪加载的导航属性</param>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <returns>查询数据集</returns>
        IQueryable<TEntity> GetInclude<TProperty>(Expression<Func<TEntity, TProperty>> path, bool trackEnabled = true);

        /// <summary>
        /// 获取贪婪加载多个导航属性的查询数据集
        /// </summary>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <param name="paths">要贪婪加载的导航属性名称数组</param>
        /// <returns>查询数据集</returns>
        /// 
        IQueryable<TEntity> GetIncludes(bool trackEnabled, params string[] paths);

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回此集中的实体。 
        /// 默认情况下，上下文会跟踪返回的实体；可通过对返回的 DbRawSqlQuery 调用 AsNoTracking 来更改此设置。 请注意返回实体的类型始终是此集的类型，而不会是派生的类型。 如果查询的一个或多个表可能包含其他实体类型的数据，则必须编写适当的 SQL 查询以确保只返回适当类型的实体。 与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。 您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。 您提供的任何参数值都将自动转换为 DbParameter。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @p0", userSuppliedAuthor); 或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        /// </summary>
        /// <param name="sql">SQL 查询字符串。</param>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数。 如果使用输出参数，则它们的值在完全读取结果之前不可用。 这是由于 DbDataReader 的基础行为而导致的，有关详细信息，请参见 http://go.microsoft.com/fwlink/?LinkID=398589。</param>
        /// <returns></returns>
        IEnumerable<TEntity> SqlQuery(string sql, bool trackEnabled = true, params object[] parameters);

        #endregion
    }
}
