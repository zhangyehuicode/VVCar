using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace YEF.Data
{
    ///// <summary>
    ///// 上下文扩展辅助操作类
    ///// </summary>
    //public static class DbContextExtensions
    //{
    //    /// <summary>
    //    /// 更新上下文中指定的实体的状态
    //    /// </summary>
    //    /// <typeparam name="TEntity">实体类型</typeparam>
    //    /// <typeparam name="TKey">主键类型</typeparam>
    //    /// <param name="dbContext">上下文对象</param>
    //    /// <param name="entities">要更新的实体类型</param>
    //    public static void Update<TEntity, TKey>(this DbContext dbContext, params TEntity[] entities) where TEntity : EntityBase<TKey>
    //    {
    //        Contract.Requires(dbContext != null && entities != null);
    //        foreach (TEntity entity in entities)
    //        {
    //            DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
    //            try
    //            {
    //                DbEntityEntry<TEntity> entry = dbContext.Entry(entity);
    //                if (entry.State == EntityState.Detached)
    //                {
    //                    dbSet.Attach(entity);
    //                    entry.State = EntityState.Modified;
    //                }
    //            }
    //            catch (InvalidOperationException)
    //            {
    //                TEntity oldEntity = dbSet.Find(entity.ID);
    //                dbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 按实体属性更新上下文中指定实体的状态
    //    /// </summary>
    //    /// <typeparam name="TKey">实体主键类型</typeparam>
    //    /// <param name="dbContext">上下文对象</param>
    //    /// <param name="propertyExpression">实体属性表达式，提供要更新的实体属性</param>
    //    /// <param name="entities">附带新值的实体对象，必须包含主键</param>
    //    public static void Update<TEntity, TKey>(this DbContext dbContext,
    //        Expression<Func<TEntity, object>> propertyExpression,
    //        params TEntity[] entities)
    //        where TEntity : EntityBase<TKey>
    //    {
    //        Contract.Requires(dbContext != null && propertyExpression != null && entities != null);
    //        ReadOnlyCollection<MemberInfo> memberInfos = ((dynamic)propertyExpression.Body).Members;
    //        foreach (TEntity entity in entities)
    //        {
    //            DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
    //            try
    //            {
    //                DbEntityEntry<TEntity> entry = dbContext.Entry(entity);
    //                entry.State = EntityState.Unchanged;
    //                foreach (MemberInfo memberInfo in memberInfos)
    //                {
    //                    entry.Property(memberInfo.Name).IsModified = true;
    //                }
    //            }
    //            catch (InvalidOperationException)
    //            {
    //                TEntity originalEntity = dbSet.Local.Single(m => Equals(m.ID, entity.ID));
    //                ObjectContext objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
    //                ObjectStateEntry objectEntry = objectContext.ObjectStateManager.GetObjectStateEntry(originalEntity);
    //                objectEntry.ApplyCurrentValues(entity);
    //                objectEntry.ChangeState(EntityState.Unchanged);
    //                foreach (MemberInfo memberInfo in memberInfos)
    //                {
    //                    objectEntry.SetModifiedProperty(memberInfo.Name);
    //                }
    //            }
    //        }
    //    }
    //}
}
