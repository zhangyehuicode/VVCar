using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Logging;

namespace YEF.Data
{
    /// <summary>
    /// 泛型主键实体的仓储实现
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体主键类型</typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>, IRepositoryAsync<TEntity, TKey>
        where TEntity : EntityBase<TKey>, new()
    {
        #region fields
        const string EFDynamicProxyNamesapce = "System.Data.Entity.DynamicProxies";

        private readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 数据记录Set
        /// </summary>
        private readonly DbSet<DataUpdateRecord> _dataUpdateRecordSet;

        /// <summary>
        /// 实体类型数据方向缓存
        /// </summary>
        private static Dictionary<Type, DataDirection> _entityDataDirectionCache;
        #endregion

        #region ctor.

        public Repository(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._dbSet = ((DbContext)unitOfWork).Set<TEntity>();
            this._dataUpdateRecordSet = ((DbContext)unitOfWork).Set<DataUpdateRecord>();
        }

        static Repository()
        {
            _entityDataDirectionCache = new Dictionary<Type, DataDirection>();
            Logger = YEF.Core.Logging.LoggerManager.GetLogger("System");
        }

        #endregion

        #region properties

        public IUnitOfWork UnitOfWork { get { return this._unitOfWork; } }

        public IQueryable<TEntity> Entities
        {
            get
            {
                return _dbSet.Where(t => t.IsDeleted == false);
            }
        }

        public bool IsDisableRecordUpdate { get; set; }

        static ILogger Logger { get; set; }

        #endregion

        #region IRepository<TEntity,TKey> 成员

        public IQueryable<TEntity> GetQueryable(bool trackEnabled = true)
        {
            return trackEnabled ? Entities : Entities.AsNoTracking();
        }

        public TEntity Add(TEntity entity)
        {
            if (entity == null)
                return entity;
            DoAdd(entity);
            return this.UnitOfWork.SaveChanges() > 0 ? entity : null;
        }

        public IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            if (entities == null || entities.Count() < 1)
                return entities;
            DoAddRange(entities);
            return this.UnitOfWork.SaveChanges() > 0 ? entities : null;
        }

        /// <summary>
        /// 新增或更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        public int AddOrUpdate(object entity)
        {
            //因为dynamic无法支持泛型参数，故此处使用object类型
            if (entity == null)
                return 0;
            var realEntity = entity as TEntity;
            if (realEntity == null)
                return 0;
            _dbSet.AddOrUpdate(realEntity);
            return this.UnitOfWork.SaveChanges();
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null || entities.Count() < 1)
                return entities;
            DoAddRange(entities);
            return this.UnitOfWork.SaveChanges() > 0 ? entities : null;
        }

        public int Delete(TEntity entity)
        {
            if (entity == null)
                return 0;
            DoDelete(entity);
            return this.UnitOfWork.SaveChanges();
        }

        public int Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return 0;
            DoDeleteRange(entities);
            return this.UnitOfWork.SaveChanges();
        }

        public int DeleteByKey(TKey key)
        {
            var entity = _dbSet.Find(key);
            if (entity == null)
                return 0;
            DoDelete(entity);
            return this.UnitOfWork.SaveChanges();
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                return 0;
            return _dbSet.Where(predicate).Delete();
        }

        public int DeleteRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return 0;
            DoDeleteRange(entities);
            return this.UnitOfWork.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            if (entity == null)
                return 0;
            DoUpdate(entity);
            return this.UnitOfWork.SaveChanges();
        }

        public int UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return 0;
            DoUpdate(entities);
            return this.UnitOfWork.SaveChanges();
        }

        public int Update(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            if (predicate == null)
                return 0;
            return _dbSet.Where(predicate).Update(updateExpression);
        }

        public TEntity GetByKey(TKey key, bool trackEnabled = true)
        {
            return trackEnabled ? _dbSet.Find(key) : _dbSet.AsNoTracking().FirstOrDefault(t => t.ID.ToString() == key.ToString());
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate).FirstOrDefault<TEntity>();
        }

        public int Count()
        {
            return Entities.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return Entities.Count(predicate);
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = Entities.Where(predicate).AsNoTracking().Select(m => 1).FirstOrDefault();
            return entity == 1;
        }

        public IQueryable<TEntity> GetInclude<TProperty>(Expression<Func<TEntity, TProperty>> path, bool trackEnabled = true)
        {
            IQueryable<TEntity> source = null;
            source = path == null ? Entities : Entities.Include(path);

            return trackEnabled ? source : source.AsNoTracking();
        }

        public IQueryable<TEntity> GetIncludes(bool trackEnabled = true, params string[] paths)
        {
            IQueryable<TEntity> source = Entities;
            if (paths != null)
            {
                foreach (string path in paths)
                {
                    source = source.Include(path);
                }
            }

            if (trackEnabled == false)
                source = source.AsNoTracking();
            return source;
        }

        public IEnumerable<TEntity> SqlQuery(string sql, bool trackEnabled = true, params object[] parameters)
        {
            return trackEnabled
                ? _dbSet.SqlQuery(sql, parameters)
                : _dbSet.SqlQuery(sql, parameters).AsNoTracking();
        }

        #endregion

        #region IRepositoryAsync<TEntity,TKey> 成员

        public async Task<int> AddAsync(TEntity entity)
        {
            if (entity == null)
                return 0;
            DoAdd(entity);
            return await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null || entities.Count() < 1)
                return 0;
            DoAddRange(entities);
            return await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            if (entity == null)
                return 0;
            DoDelete(entity);
            return await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TKey key)
        {
            var entity = new TEntity();
            entity.ID = key;
            DoDelete(entity);
            return await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                return 0;
            return await _dbSet.Where(predicate).DeleteAsync();
        }

        public async Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return 0;
            DoDeleteRange(entities);
            return await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                return 0;
            DoUpdate(entity);
            return await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return 0;
            DoUpdate(entities);
            return await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            if (predicate == null)
                return 0;
            return await _dbSet.Where(predicate).UpdateAsync(updateExpression);
        }

        public async Task<TEntity> GetByKeyAsync(TKey key)
        {
            return await _dbSet.FindAsync(key);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Entities.Where(predicate).FirstOrDefaultAsync<TEntity>();
        }

        public async Task<int> CountAsync()
        {
            return await Entities.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Entities.CountAsync(predicate);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await Entities.Where(predicate).AsNoTracking().Select(m => 1).FirstOrDefaultAsync();
            return entity == 1;
        }

        #endregion

        #region methods

        void DoAdd(TEntity entity)
        {
            _dbSet.Add(entity);
            RecordUpdate(entity, UpdateType.Insert);
        }

        void DoAddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            RecordUpdate(entities, UpdateType.Insert);
        }

        void DoDelete(TEntity entity)
        {
            if (((DbContext)this.UnitOfWork).Entry(entity).State == EntityState.Detached)
            {
                try
                {
                    _dbSet.Attach(entity);
                }
                catch (InvalidOperationException)
                {
                    entity = _dbSet.Find(entity.ID);
                }
            }
            _dbSet.Remove(entity);
            RecordUpdate(entity, UpdateType.Delete);
        }

        void DoDeleteRange(IEnumerable<TEntity> entities)
        {
            var dbContext = (DbContext)this.UnitOfWork;
            entities.ForEach(entity =>
            {
                if (dbContext.Entry(entity).State == EntityState.Detached)
                    _dbSet.Attach(entity);
            });
            _dbSet.RemoveRange(entities);
            RecordUpdate(entities, UpdateType.Delete);
        }

        void DoUpdate(TEntity entity)
        {
            var dbContext = (DbContext)this.UnitOfWork;
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
                    TEntity oldEntity = _dbSet.Find(entity.ID);
                    dbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
            }
            RecordUpdate(entity, UpdateType.Update);
        }

        void DoUpdate(IEnumerable<TEntity> entities)
        {
            var dbContext = (DbContext)this.UnitOfWork;
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
                    catch (InvalidOperationException e)
                    {
                        TEntity oldEntity = _dbSet.Find(entity.ID);
                        dbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
                    }
                }
            }
            RecordUpdate(entities, UpdateType.Update);
        }

        void RecordUpdate(TEntity entity, UpdateType updateType)
        {
            if (IsDisableRecordUpdate)
                return;
            var entityType = GetEntityType(entity);
            if (!_entityDataDirectionCache.ContainsKey(entityType))
            {
                var attrs = entityType.GetCustomAttributes(typeof(DataRecordAttribute), false);
                if (attrs.Length > 0)
                {
                    var dataRecordAttr = attrs[0] as DataRecordAttribute;
                    _entityDataDirectionCache[entityType] = dataRecordAttr.Direction;
                }
                else
                {
                    _entityDataDirectionCache[entityType] = DataDirection.None;
                    return;
                }
            }
            var dataDirection = _entityDataDirectionCache[entityType];
            if (dataDirection == DataDirection.None)
                return;
            if (dataDirection != AppContext.Settings.RecordDataUpdateType && dataDirection != DataDirection.Both)
            {
                //YEF.Core.Logging.LoggerManager.GetLogger("System").Debug("{0} has Changed but this runtime is not master server, skip log update record.", entityType.Name);
                return;
            }
            Guid entityID = Guid.Parse(entity.ID.ToString());
            var newDataRecord = new DataUpdateRecord()
            {
                EntityName = entityType.Name,
                EntityFullName = entityType.FullName,
                DataID = entityID,
                UpdateType = updateType,
                Direction = AppContext.Settings.RecordDataUpdateType,
                Status = RecordStatus.Default,
                CreatedUserID = AppContext.CurrentSession.UserID,
                CreatedUser = AppContext.CurrentSession.UserName,
                CreatedDate = DateTime.Now,
            };
            var deptEntity = entity as IDepartmentEntity;
            if (deptEntity != null)
            {
                newDataRecord.TargetDepartmentID = deptEntity.DepartmentID;
            }
            else if ("Department".Equals(entityType.Name))
            {
                newDataRecord.TargetDepartmentID = entityID;
            }
            _dataUpdateRecordSet.Add(newDataRecord);
        }

        void RecordUpdate(IEnumerable<TEntity> entities, UpdateType updateType)
        {
            if (IsDisableRecordUpdate)
                return;
            foreach (var entity in entities)
            {
                RecordUpdate(entity, updateType);
            }
        }

        Type GetEntityType(TEntity entity)
        {
            Type entityType = entity.GetType();
            if (EFDynamicProxyNamesapce.Equals(entityType.Namespace))
                return entityType.BaseType;
            return entityType;
        }

        #endregion
    }
}