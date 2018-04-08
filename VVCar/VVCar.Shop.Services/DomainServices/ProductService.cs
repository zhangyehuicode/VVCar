using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// 产品领域服务
    /// </summary>
    public class ProductServic : DomainServiceBase<IRepository<Product>, Product, Guid>, IProductService
    {
        public ProductServic()
        {
        }

        private int GenerateIndex()
        {
            var index = 1;
            var indexList = Repository.GetQueryable(false).Select(t => t.Index);
            if (indexList.Count() > 0)
                index = indexList.Max() + 1;
            return index;
        }

        public override Product Add(Product entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            if (!entity.EffectiveDate.HasValue)
                entity.EffectiveDate = DateTime.Now;
            entity.Index = GenerateIndex();
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            return base.Add(entity);
        }

        public override bool Update(Product entity)
        {
            if (entity == null)
                return false;
            var product = Repository.GetByKey(entity.ID);
            if (product == null)
                throw new DomainException("更新的产品不存在");

            //pointGoods.ProductType = entity.ProductType;
            product.ProductCategoryID = entity.ProductCategoryID;
            product.Name = entity.Name;
            product.ImgUrl = entity.ImgUrl;
            product.BasePrice = entity.BasePrice;
            product.PriceSale = entity.PriceSale;
            product.Points = entity.Points;
            product.UpperLimit = entity.UpperLimit;
            product.IsPublish = entity.IsPublish;
            product.IsRecommend = entity.IsRecommend;
            product.Stock = entity.Stock;
            product.EffectiveDate = entity.EffectiveDate;
            product.ExpiredDate = entity.ExpiredDate;
            product.LastUpdateDate = DateTime.Now;
            product.LastUpdateUser = AppContext.CurrentSession.UserName;
            product.LastUpdateUserID = AppContext.CurrentSession.UserID;

            return base.Update(product);
        }

        public IEnumerable<Product> Search(ProductFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false);
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            if (filter.ProductCategoryID.HasValue && filter.ProductCategoryID.Value != Guid.Parse("00000000-0000-0000-0000-000000000001"))
                queryable = queryable.Where(t => t.ProductCategoryID == filter.ProductCategoryID.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.Index).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }

        public bool AdjustIndex(AdjustIndexParam param)
        {
            if (param == null || param.ID == null)
                return false;
            var pointGoods = Repository.GetByKey(param.ID);
            if (pointGoods == null)
                return false;
            Product exchangeEntity = null;
            if (param.Direction == EAdjustDirection.Up)
            {
                var pre = Repository.GetQueryable().Where(t => t.Index == (pointGoods.Index - 1)).FirstOrDefault();
                if (pre == null)
                    return true;
                else
                    exchangeEntity = pre;
            }
            else if (param.Direction == EAdjustDirection.Down)
            {
                var next = Repository.GetQueryable().Where(t => t.Index == (pointGoods.Index + 1)).FirstOrDefault();
                if (next == null)
                    return true;
                else
                    exchangeEntity = next;
            }
            if (exchangeEntity != null)
            {
                var exchangeIndex = exchangeEntity.Index;
                exchangeEntity.Index = pointGoods.Index;
                pointGoods.Index = exchangeIndex;
                return Repository.Update(new List<Product> { exchangeEntity, pointGoods }) == 2;
            }
            return false;
        }

        public bool ChangePublishStatus(Guid id)
        {
            var entity = Repository.GetByKey(id);
            if (entity == null)
                return false;
            entity.IsPublish = !entity.IsPublish;
            return Repository.Update(entity) > 0;
        }
    }
}
