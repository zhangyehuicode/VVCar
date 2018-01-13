using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using YEF.Core.Dtos;
using YEF.Core.Filter;

namespace YEF.Core.Domain
{
    /// <summary>
    /// 领域服务基类
    /// </summary>
    /// <typeparam name="TRepository">领域实体仓储对象</typeparam>
    /// <typeparam name="TEntity">领域实体</typeparam>
    /// <typeparam name="TKey">领域实体主键类型</typeparam>
    public abstract class DomainServiceBase<TRepository, TEntity, TKey> : IDomainService<TRepository, TEntity, TKey>
        where TRepository : IRepository<TEntity, TKey>
        where TEntity : EntityBase<TKey>, new()
    {

        /// <summary>
        /// 领域服务基类
        /// </summary>
        public DomainServiceBase()
        {
        }

        #region properties

        IUnitOfWork _unitOfWork;
        /// <summary>
        /// 获取IUnitOfWork对象
        /// </summary>
        protected IUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                    _unitOfWork = ServiceLocator.Instance.GetService<IUnitOfWork>();
                return _unitOfWork;
            }
        }

        TRepository _repository;
        /// <summary>
        /// 获取Repository对象
        /// </summary>
        protected TRepository Repository
        {
            get
            {
                if (_repository == null)
                    _repository = this.UnitOfWork.GetRepository<TRepository>();
                return _repository;
            }
        }

        /// <summary>
        /// 是否禁止校验
        /// </summary>
        protected bool DisableValidate { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// 校验
        /// </summary>
        /// <returns>通过则为true，否则为false</returns>
        private bool Validate(TEntity entity)
        {
            if (this.DisableValidate == true)
                return true;
            return this.DoValidate(entity);
        }

        /// <summary>
        /// 执行校验
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool DoValidate(TEntity entity)
        {
            return true;
        }

        #endregion

        #region IDomainService<TEntity,TRepository,TKey> 成员

        /// <summary>
        /// 添加Entity到Repository
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Add(TEntity entity)
        {
            var validateResult = this.Validate(entity);
            if (false == validateResult)
                return null;
            return this.Repository.Add(entity);
        }
        
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool Delete(TKey key)
        {
            this.Repository.DeleteByKey(key);
            return true;
        }

        /// <summary>
        /// 更新Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Update(TEntity entity)
        {
            var validateResult = this.Validate(entity);
            if (false == validateResult)
                return validateResult;
            this.Repository.Update(entity);
            return true;
        }

        /// <summary>
        /// 根据key获取Entity
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TEntity Get(TKey key)
        {
            return this.Repository.GetByKey(key);
        }

        /// <summary>
        /// 获得符合条件的数据
        /// </summary>
        /// <param name="predicate">where条件</param>
        /// <returns></returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Repository.Get(predicate);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            var querybale = this.Repository.GetQueryable(false);
            if (predicate != null)
            {
                querybale = querybale.Where(predicate);
            }
            return querybale.ToArray();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual PagedResultDto<TEntity> GetPagerList(Expression<Func<TEntity, bool>> predicate, int startIndex, int pageSize)
        {
            if (startIndex < 0 || pageSize < 1)
                throw new ArgumentOutOfRangeException("分页参数不正确");
            var querybale = this.Repository.GetQueryable(false);
            if (predicate != null)
            {
                querybale = querybale.Where(predicate);
            }
            var result = new PagedResultDto<TEntity>();
            result.TotalCount = querybale.Count();
            result.Items = querybale.OrderBy(t => t.ID).Skip(startIndex).Take(pageSize).ToArray();
            return result;
        }

        /// <summary>
        /// 获取符合条件的记录数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Count(predicate);
        }

        /// <summary>
        /// 是否存在符合条件的记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Exists(predicate);
        }

        #endregion
    }
}
