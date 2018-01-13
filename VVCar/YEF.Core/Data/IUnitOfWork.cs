using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core;

namespace YEF.Core.Data
{
    
    public interface IUnitOfWork : IDependency
    {
        #region methods

        /// <summary>
        /// 获取实体仓库
        /// </summary>
        /// <typeparam name="TRepository">实体仓库类型</typeparam>
        /// <returns></returns>
        TRepository GetRepository<TRepository>();

        /// <summary>
        /// 对数据库执行给定的 DDL/DML 命令。
        /// </summary>
        /// <param name="transactionalBehavior">事务行为</param>
        /// <param name="sql">命令字符串</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的行数</returns>
        int ExecuteSqlCommand(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters);

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回给定泛型类型的元素。
        /// </summary>
        /// <typeparam name="TElement">查询所返回对象的类型</typeparam>
        /// <param name="sql">SQL 查询字符串</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数</param>
        /// <returns></returns>
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回给定类型的元素。
        /// </summary>
        /// <param name="elementType">查询所返回对象的类型</param>
        /// <param name="sql">SQL 查询字符串</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数</param>
        /// <returns></returns>
        IEnumerable<object> SqlQuery(Type elementType, string sql, params Object[] parameters);

        /// <summary>
        /// 提交当前单元操作的更改。
        /// </summary>
        /// <returns>操作影响的行数</returns>
        int SaveChanges();
        /// <summary>
        /// 执行数据sql语句
        /// </summary>
        /// <returns>执行数据sql语句</returns>
        Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters);

        /// <summary>
        /// 异步提交当前单元操作的更改。
        /// </summary>
        /// <returns>操作影响的行数</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTransaction();

        #endregion
    }
}
