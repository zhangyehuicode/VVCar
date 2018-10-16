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
    /// 产品分类服务类
    /// </summary>
    public class ProductCategoryService : DomainServiceBase<IRepository<ProductCategory>, ProductCategory, Guid>, IProductCategoryService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ProductCategoryService()
        {
        }

        #region properties

        private IRepository<Product> _productRepo;

        /// <summary>
        /// 产品档案Repo
        /// </summary>
        public IRepository<Product> ProductRepo
        {
            get
            {
                if (_productRepo == null)
                {
                    _productRepo = this.UnitOfWork.GetRepository<IRepository<Product>>();
                }
                return _productRepo;
            }
        }

        #endregion properties

        #region methods

        protected override bool DoValidate(ProductCategory entity)
        {
            var exists = Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (exists)
                throw new DomainException(string.Format("代码 {0} 已使用", entity.Code));
            if (entity.ID == entity.ParentId)
            {
                throw new DomainException("不能选择本类为自己的上级分类");
            }
            return true;
        }

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="entity">产品实体对象</param>
        /// <returns></returns>
        public override ProductCategory Add(ProductCategory entity)
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
        /// <param name="key">产品主键</param>
        /// <returns>是否删除成功</returns>
        public override bool Delete(Guid id)
        {
            if (ProductRepo.Exists(t => t.ProductCategoryID == id))
                throw new DomainException("该类别存在明细，无法删除！");
            if (Repository.Exists(t => t.ParentId == id))
                throw new DomainException("该类别存在下级分类，无法删除！");
            var productCategory = Repository.GetByKey(id);
            if (productCategory == null)
                throw new DomainException("数据不存在！");
            productCategory.IsDeleted = true;
            Repository.Update(productCategory);
            return true;
        }

        /// <summary>
        /// 更新产品分类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(ProductCategory entity)
        {
            if (entity == null)
                return false;
            var productCategory = Repository.GetByKey(entity.ID);
            if (productCategory == null)
                return false;
            productCategory.ParentId = entity.ParentId;
            productCategory.Code = entity.Code;
            productCategory.Name = entity.Name;

            productCategory.Index = entity.Index;
            productCategory.LastUpdateUserID = AppContext.CurrentSession.UserID;
            productCategory.LastUpdateUser = AppContext.CurrentSession.UserName;
            productCategory.LastUpdateDate = DateTime.Now;
            return base.Update(productCategory);
        }

        /// <summary>
        /// 获取树型数据
        /// </summary>
        /// <param name="parentID">上级ID</param>
        /// <returns></returns>
        public IEnumerable<ProductCategoryTreeDto> GetTreeData(Guid? parentID)
        {
            var deptRegions = this.Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
                .MapTo<ProductCategory, ProductCategoryTreeDto>()
                .OrderBy(t => t.ParentId).ThenBy(t => t.Index)
                .ToList();

            return BuildTree(deptRegions, null);
        }

        IEnumerable<ProductCategoryTreeDto> BuildTree(IList<ProductCategoryTreeDto> sources, Guid? parentID)
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
        /// <param name="filter">keywords(分类名称，)</param>
        /// <param name="totalCount"></param>
        /// <returns>产品分类数据集</returns>
        public IEnumerable<ProductCategory> Search(ProductCategoryFilter filter, out int totalCount)
        {
            var result = new List<ProductCategory>();
            var queryable = Repository.GetInclude(t => t.SubProducts, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Code))
                {
                    queryable = queryable.Where(p => p.Code.Contains(filter.Code));
                }
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    queryable = queryable.Where(p => p.Name.Contains(filter.Name));
                }
                if (!string.IsNullOrEmpty(filter.NameOrCode))
                {
                    queryable = queryable.Where(t => t.Code.Contains(filter.NameOrCode) || t.Name.Contains(filter.NameOrCode));
                }
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
                    if (t.SubProducts != null && t.SubProducts.Count > 0)
                    {
                        t.SubProducts = t.SubProducts.Where(item => item.IsPublish).ToList();//item.ProductType == EProductType.Service && item.IsPublish && !item.IsCombo
                    }
                });
            }
            if (filter.IsFromStockManager)
            {
                result.ForEach(t =>
                {
                    if (t.SubProducts != null && t.SubProducts.Count > 0)
                    {
                        t.SubProducts = t.SubProducts.Where(item => item.ProductType == EProductType.Goods).ToList();
                    }
                });
            }
            if (filter.IsFromPickUpOrder || filter.IsFromStockManager)
            {
                var removeItem = result.Where(t => t.SubProducts == null || t.SubProducts.Count < 1).ToList();
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
                    Name = t.Name
                }).ToList();
            return categories;
        }

        #endregion methods
    }
}
