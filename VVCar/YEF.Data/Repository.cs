using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using YEF.Core;
using YEF.Core.Data;

namespace YEF.Data
{
    /// <summary>
    /// 非泛型版本的实体的仓储实现
    /// </summary>
    public class Repository : IRepository
    {
        #region fields
        const string EFDynamicProxyNamesapce = "System.Data.Entity.DynamicProxies";

        private readonly DbSet _dbSet;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region ctor.

        public Repository(IUnitOfWork unitOfWork, Type entityType)
        {
            this._unitOfWork = unitOfWork;
            this._dbSet = ((DbContext)unitOfWork).Set(entityType);
        }

        #endregion

        #region IRepository 成员

        public object Add(object entity)
        {
            if (entity == null)
                return entity;
            DoAdd(entity);
            return _unitOfWork.SaveChanges() > 0 ? entity : null;

        }

        public IEnumerable AddRange(IEnumerable entities)
        {
            if (entities == null)
                return entities;
            DoAddRange(entities);
            return _unitOfWork.SaveChanges() > 0 ? entities : null;
        }

        public int DeleteByKey(object key)
        {
            var entity = _dbSet.Find(key);
            if (entity == null)
                return 0;
            _dbSet.Remove(entity);
            return _unitOfWork.SaveChanges();
        }

        public int Delete(object entity)
        {
            if (entity == null)
                return 0;
            var result = DoDelete(entity);
            return result ? _unitOfWork.SaveChanges() : 0;
        }

        public int DeleteRange(IEnumerable entities)
        {
            if (entities == null)
                return 0;
            var result = DoDeleteRange(entities);
            return result ? _unitOfWork.SaveChanges() : 0;
        }

        public int Update(object entity)
        {
            if (entity == null)
                return 0;
            var result = DoUpdate(entity);
            return result ? _unitOfWork.SaveChanges() : 0;
        }

        public int UpdateRange(IEnumerable entities)
        {
            if (entities == null)
                return 0;
            var result = DoUpdateRange(entities);
            return result ? _unitOfWork.SaveChanges() : 0;
        }

        public object GetByKey(object key)
        {
            return _dbSet.Find(key);
        }

        #endregion

        #region methods

        void DoAdd(object entity)
        {
            _dbSet.Add(entity);
        }

        void DoAddRange(IEnumerable entities)
        {
            _dbSet.AddRange(entities);
        }

        bool DoDelete(object entity)
        {
            if (((DbContext)_unitOfWork).Entry(entity).State == EntityState.Detached)
            {
                try
                {
                    _dbSet.Attach(entity);
                }
                catch (InvalidOperationException)
                {
                    var baseEntity = entity as EntityBase;
                    if (baseEntity == null)
                        return false;
                    entity = _dbSet.Find(baseEntity.ID);
                }
            }
            _dbSet.Remove(entity);
            return true;
        }

        bool DoDeleteRange(IEnumerable entities)
        {
            var dbContext = (DbContext)_unitOfWork;
            foreach (var entity in entities)
            {
                if (dbContext.Entry(entity).State == EntityState.Detached)
                    _dbSet.Attach(entity);
            }
            _dbSet.RemoveRange(entities);
            return true;
        }

        bool DoUpdate(object entity)
        {
            var dbContext = (DbContext)_unitOfWork;
            var entry = dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                try
                {
                    _dbSet.Attach(entity);
                    entry.State = EntityState.Modified;
                }
                catch (InvalidOperationException)
                {
                    var baseEntity = entity as EntityBase;
                    if (baseEntity == null)
                        return false;
                    var oldEntity = _dbSet.Find(baseEntity.ID);
                    dbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
            }
            return true;
        }

        bool DoUpdateRange(IEnumerable entities)
        {
            var dbContext = (DbContext)_unitOfWork;
            foreach (var entity in entities)
            {
                var entry = dbContext.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    try
                    {
                        _dbSet.Attach(entity);
                        entry.State = EntityState.Modified;
                    }
                    catch (InvalidOperationException)
                    {
                        var baseEntity = entity as EntityBase;
                        if (baseEntity == null)
                            return false;
                        var oldEntity = _dbSet.Find(baseEntity.ID);
                        dbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
                    }
                }
            }
            return true;
        }
        #endregion
    }
}
