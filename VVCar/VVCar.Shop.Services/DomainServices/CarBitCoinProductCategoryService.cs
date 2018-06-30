using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Services;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Enums;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 车比特产品分类服务
    /// </summary>
    public class CarBitCoinProductCategoryService : DomainServiceBase<IRepository<CarBitCoinProductCategory>, CarBitCoinProductCategory, Guid>, ICarBitCoinProductCategoryService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public CarBitCoinProductCategoryService()
        {
        }

        #region properties

        private IRepository<CarBitCoinProduct> _carBitCoinProductRepo;

        /// <summary>
        /// 产品档案Repo
        /// </summary>
        public IRepository<CarBitCoinProduct> CarBitCoinProductRepo
        {
            get
            {
                if (_carBitCoinProductRepo == null)
                {
                    _carBitCoinProductRepo = this.UnitOfWork.GetRepository<IRepository<CarBitCoinProduct>>();
                }
                return _carBitCoinProductRepo;
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool DoValidate(CarBitCoinProductCategory entity)
        {
            var exists = Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (exists)
                throw new DomainException(string.Format("代码{0}已使用", entity.Code));
            if (entity.ID == entity.ParentId)
                throw new DomainException("不能选择本类作为自己的上级分类");
            return true;
        }

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override CarBitCoinProductCategory Add(CarBitCoinProductCategory entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        /// <summary>
        /// 删除产品分类
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool Delete(Guid key)
        {
            if (CarBitCoinProductRepo.Exists(t => t.CarBitCoinProductCategoryID == key))
                throw new DomainException("该类别存在明细,无法删除!");
            if (Repository.Exists(t => t.ParentId == key))
                throw new DomainException("该类别存在下级分类,无法删除!");
            var carBitCoinProductCategory = Repository.GetByKey(key);
            if (carBitCoinProductCategory == null)
                throw new DomainException("数据不存在!");
            carBitCoinProductCategory.IsDeleted = true;
            Repository.Update(carBitCoinProductCategory);
            return true;
        }

        /// <summary>
        /// 更新产品分类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(CarBitCoinProductCategory entity)
        {
            if (entity == null)
                return false;
            var carBitCoinProductCategory = Repository.GetByKey(entity.ID);
            if (carBitCoinProductCategory == null)
                return false;
            carBitCoinProductCategory.ParentId = entity.ParentId;
            carBitCoinProductCategory.Code = entity.Code;
            carBitCoinProductCategory.Name = entity.Name;

            carBitCoinProductCategory.Index = entity.Index;
            carBitCoinProductCategory.LastUpdateUserID = AppContext.CurrentSession.UserID;
            carBitCoinProductCategory.LastUpdateUser = AppContext.CurrentSession.UserName;
            carBitCoinProductCategory.LastUpdateDate = DateTime.Now;
            return base.Update(carBitCoinProductCategory);
        }

        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public IEnumerable<CarBitCoinProductCategoryTreeDto> GetTreeData(Guid? parentID)
        {
            var deptRegions = this.Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
                .MapTo<CarBitCoinProductCategory, CarBitCoinProductCategoryTreeDto>()
                .OrderBy(t => t.ParentId).ThenBy(t => t.Index)
                .ToList();

            return BuildTree(deptRegions, null);
        }

        IEnumerable<CarBitCoinProductCategoryTreeDto> BuildTree(IList<CarBitCoinProductCategoryTreeDto> sources, Guid? parentID)
        {
            var children = sources.Where(t => t.ParentId == parentID).ToList();
            foreach (var child in children)
            {
                child.Children = BuildTree(sources, child.ID);
                child.leaf = child.Children.Count() == 0;
                child.expanded = false;
            }
            return children;
        }

        /// <summary>
        /// 根据条件查询产品分类
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<CarBitCoinProductCategory> Search(CarBitCoinProductCategoryFilter filter, out int totalCount)
        {
            var result = new List<CarBitCoinProductCategory>();
            var queryable = Repository.GetInclude(t => t.SubCarBitCoinProducts, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Code))
                    queryable = queryable.Where(p => p.Code.Contains(filter.Code));
                if (!string.IsNullOrEmpty(filter.Name))
                    queryable = queryable.Where(p => p.Name.Contains(filter.Name));
                if (!string.IsNullOrEmpty(filter.NameOrCode))
                    queryable = queryable.Where(t => t.Code.Contains(filter.NameOrCode) || t.Name.Contains(filter.NameOrCode));
            }
            queryable = queryable.OrderBy(t => t.ParentId).ThenBy(t => t.Index);
            totalCount = queryable.Count();
            if (filter != null && filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.Skip(filter.Start.Value).Take(filter.Limit.Value);
            result = queryable.ToList();
            if (filter.IsFromPickUpOrder)
            {
                result.ForEach(t =>
                {
                    if (t.SubCarBitCoinProducts != null && t.SubCarBitCoinProducts.Count > 0)
                        t.SubCarBitCoinProducts = t.SubCarBitCoinProducts.Where(item => item.CarBitCoinProductType == ECarBitCoinProductType.Engine).ToList();
                });
            }
            if (filter.IsFromStockManager)
            {
                result.ForEach(t =>
                {
                    if (t.SubCarBitCoinProducts != null && t.SubCarBitCoinProducts.Count > 0)
                        t.SubCarBitCoinProducts = t.SubCarBitCoinProducts.Where(item => item.CarBitCoinProductType == ECarBitCoinProductType.Goods).ToList();
                });
            }
            if (filter.IsFromPickUpOrder || filter.IsFromStockManager)
            {
                var removeItem = result.Where(t => t.SubCarBitCoinProducts == null || t.SubCarBitCoinProducts.Count < 1).ToList();
                if (removeItem != null && removeItem.Count > 0)
                {
                    removeItem.ForEach(t =>
                    {
                        result.Remove(t);
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// 获取精简结构数据
        /// </summary>
        /// <returns></returns>
        public IList<IDCodeNameDto> GetLiteData()
        {
            var categories = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
                .OrderBy(t => t.Index)
                .Select(t => new IDCodeNameDto
                {
                    ID = t.ID,
                    Code = t.Code,
                    Name = t.Name,
                }).ToList();
            return categories;
        }
        #endregion 
    }
}
