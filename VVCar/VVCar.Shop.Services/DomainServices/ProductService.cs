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
using VVCar.VIP.Domain.Entities;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 产品领域服务
    /// </summary>
    public class ProductService : DomainServiceBase<IRepository<Product>, Product, Guid>, IProductService
    {
        public ProductService()
        {
        }

        #region properties

        IRepository<ProductCategory> _productCategoryRepo;

        /// <summary>
        /// 产品类别 Repository
        /// </summary>
        IRepository<ProductCategory> ProductCategoryRepo
        {
            get
            {
                if (_productCategoryRepo == null)
                    _productCategoryRepo = UnitOfWork.GetRepository<IRepository<ProductCategory>>();
                return _productCategoryRepo;
            }
        }

        IStockRecordService StockRecordService { get => ServiceLocator.Instance.GetService<IStockRecordService>(); }

        IRepository<PickUpOrder> PickUpOrderRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrder>>(); }

        IRepository<Member> MemberRepo { get => UnitOfWork.GetRepository<IRepository<Member>>(); }

        IRepository<UnsaleProductSettingItem> UnsaleProductSettingItemRepo { get => UnitOfWork.GetRepository<IRepository<UnsaleProductSettingItem>>(); }

        IRepository<MerchantCrowdOrder> MerchantCrowdOrderRepo { get => UnitOfWork.GetRepository<IRepository<MerchantCrowdOrder>>(); }

        IRepository<Product> ProductRepo { get => UnitOfWork.GetRepository<IRepository<Product>>(); }
        #endregion

        protected override bool DoValidate(Product entity)
        {
            bool exists = this.Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (exists)
                throw new DomainException(String.Format("代码 {0} 已使用，不能重复添加。", entity.Code));
            return true;
        }

        private int GenerateIndex()
        {
            var index = 1;
            var indexList = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID).Select(t => t.Index);
            if (indexList.Count() > 0)
                index = indexList.Max() + 1;
            return index;
        }

        public override Product Add(Product entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            //if (!entity.EffectiveDate.HasValue)
            //    entity.EffectiveDate = DateTime.Now;
            entity.Index = GenerateIndex();
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(Product entity)
        {
            if (entity == null)
                return false;
            var product = Repository.GetByKey(entity.ID);
            if (product == null)
                throw new DomainException("更新的产品不存在");

            product.ProductType = entity.ProductType;
            product.ProductCategoryID = entity.ProductCategoryID;
            product.Name = entity.Name;
            product.Code = entity.Code;
            product.ImgUrl = entity.ImgUrl;
            product.BasePrice = entity.BasePrice;
            product.WholesalePrice = entity.WholesalePrice;
            product.PriceSale = entity.PriceSale;
            product.CostPrice = entity.CostPrice;
            product.Points = entity.Points;
            product.UpperLimit = entity.UpperLimit;
            product.IsInternaCollection = entity.IsInternaCollection;
            if (entity.IsInternaCollection)
            {
                product.IsPublish = false;
                product.IsRecommend = false;
            }
            else
            {
                product.IsPublish = entity.IsPublish;
                product.IsRecommend = entity.IsRecommend;
            }
            product.Stock = entity.Stock;
            product.Introduction = entity.Introduction;
            product.DeliveryNotes = entity.DeliveryNotes;
            //product.EffectiveDate = entity.EffectiveDate;
            //product.ExpiredDate = entity.ExpiredDate;
            product.IsCombo = entity.IsCombo;
            product.CommissionRate = entity.CommissionRate;
            product.SalesmanCommissionRate = entity.SalesmanCommissionRate;
            product.WholesaleCommissionRate = entity.WholesaleCommissionRate;
            product.WholesaleConstructionCommissionRate = entity.WholesaleConstructionCommissionRate;
            product.IsCanPointExchange = entity.IsCanPointExchange;
            product.Unit = entity.Unit;
            product.GraphicIntroduction = entity.GraphicIntroduction;
            product.LastUpdateDate = DateTime.Now;
            product.LastUpdateUser = AppContext.CurrentSession.UserName;
            product.LastUpdateUserID = AppContext.CurrentSession.UserID;

            return base.Update(product);
        }

        public override bool Delete(Guid key)
        {
            var entity = Repository.GetByKey(key);
            if (entity == null)
                throw new DomainException("数据不存在");
            entity.IsDeleted = true;
            return Repository.Update(entity) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<Product> Search(ProductFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            if (!string.IsNullOrEmpty(filter.Code))
                queryable = queryable.Where(t => t.Code.Contains(filter.Code));
            if (!string.IsNullOrEmpty(filter.NameCode))
                queryable = queryable.Where(t => t.Name.Contains(filter.NameCode) || t.Code.Contains(filter.NameCode));
            if (filter.ProductCategoryID.HasValue && filter.ProductCategoryID.Value != Guid.Parse("00000000-0000-0000-0000-000000000001"))
                queryable = queryable.Where(t => t.ProductCategoryID == filter.ProductCategoryID.Value);
            if (filter.IsFromStockManager)
                queryable = queryable.Where(t => t.ProductType == EProductType.Goods);
            if (filter.ProductType.HasValue)
                queryable = queryable.Where(t => t.ProductType == filter.ProductType.Value);
            if (filter.IsCombo.HasValue)
                queryable = queryable.Where(t => t.IsCombo == filter.IsCombo.Value);
            if (filter.IsInternaCollection.HasValue)
                queryable = queryable.Where(t => t.IsInternaCollection == filter.IsInternaCollection.Value);
            if (filter.IsUnsaleProduct.HasValue)
            {
                var productIDs = UnsaleProductSettingItemRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID).Select(t=>t.ProductID).Distinct().ToList();
                if (productIDs != null && productIDs.Count() > 0)
                    queryable = queryable.Where(t => !productIDs.Contains(t.ID));
            }
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.Index).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }

        public IEnumerable<ProductDto> GetProduct()
        {
            var result = new List<ProductDto>();
            var resulttmp = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.ProductType == EProductType.Goods && t.IsPublish && t.Stock > 0 && !t.IsCombo).ToList();
            result = resulttmp.MapTo<List<ProductDto>>();
            if (AppContext.CurrentSession.MemberID != null && AppContext.CurrentSession.MemberID != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                var member = MemberRepo.GetInclude(t => t.MemberGroup, false).FirstOrDefault(t => t.ID == AppContext.CurrentSession.MemberID);
                if (member != null && member.MemberGroup != null && member.MemberGroup.IsWholesalePrice)
                {
                    result.ForEach(t =>
                    {
                        t.PriceSale = t.WholesalePrice;
                        t.IsWholesale = true;
                    });
                }
            }
            return result;
        }

        public bool AdjustIndex(AdjustIndexParam param)
        {
            if (param == null || param.ID == null)
                return false;
            var pointGoods = Repository.GetByKey(param.ID);
            if (pointGoods == null)
                return false;
            var productQueryable = Repository.GetQueryable().Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            Product exchangeEntity = null;
            if (param.Direction == EAdjustDirection.Up)
            {
                var pre = productQueryable.Where(t => t.Index == (pointGoods.Index - 1)).FirstOrDefault();
                if (pre == null)
                    return true;
                else
                    exchangeEntity = pre;
            }
            else if (param.Direction == EAdjustDirection.Down)
            {
                var next = productQueryable.Where(t => t.Index == (pointGoods.Index + 1)).FirstOrDefault();
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

        /// <summary>
        /// 获取轻量型产品档案数据
        /// </summary>
        /// <returns></returns>
        public IList<ProductCategoryLiteDto> GetProductLiteData()
        {
            var categories = ProductCategoryRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
                .OrderBy(t => t.ParentId).ThenBy(t => t.Index)
                .MapTo<ProductCategory, ProductCategoryLiteDto>()
                .ToList();
            var products = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
                .Where(t => t.IsPublish)
                .MapTo<Product, ProductLiteDto>()
                .ToList();
            foreach (var category in categories)
            {
                category.Products = products.Where(t => t.ProductCategoryID == category.ID).ToList();
            }
            return categories;
        }

        /// <summary>
        /// 获取推荐产品
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductDto> GetRecommendProduct()
        {
            var result = new List<ProductDto>();
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.ProductType == EProductType.Goods && t.IsPublish && t.Stock > 0);
            var recommend = queryable.Where(t => t.IsRecommend).ToList().MapTo<List<ProductDto>>();
            result = recommend;
            if (result.Count < 4)
            {
                var additional = queryable.Where(t => !t.IsRecommend).OrderBy(t => t.Index).ToList().MapTo<List<ProductDto>>();
                foreach (var item in additional)
                {
                    result.Add(item);
                    if (result.Count >= 4)
                        break;
                }
            }
            else if (result.Count > 4)
            {
                result = new List<ProductDto>();
                foreach (var item in recommend)
                {
                    result.Add(item);
                    if (result.Count >= 4)
                        break;
                }
            }
            if (AppContext.CurrentSession.MemberID != null && AppContext.CurrentSession.MemberID != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                var member = MemberRepo.GetInclude(t => t.MemberGroup, false).FirstOrDefault(t => t.ID == AppContext.CurrentSession.MemberID);
                if (member != null && member.MemberGroup != null && member.MemberGroup.IsWholesalePrice)
                {
                    result.ForEach(t =>
                    {
                        t.PriceSale = t.WholesalePrice;
                        t.IsWholesale = true;
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetAppointmentProduct()
        {
            return Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.ProductType == EProductType.Service && t.IsPublish && !t.IsCombo).ToList();
        }

        /// <summary>
        /// 接车单历史数据分析
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HistoryDataAnalysisDto> GetHistoryAnalysisData(HistoryDataAnalysisParam param)
        {
            var result = new List<HistoryDataAnalysisDto>();

            var servicelist = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.ProductType == EProductType.Service && t.IsPublish && !t.IsCombo).ToList();
            if (servicelist != null && servicelist.Count() > 0)
            {
                var pickuporderQueryable = PickUpOrderRepo.GetInclude(t => t.PickUpOrderItemList, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);

                if (param != null)
                {
                    Member member = null;
                    if (!string.IsNullOrEmpty(param.OpenID))
                    {
                        member = MemberRepo.GetInclude(t => t.MemberPlateList, false).FirstOrDefault(t => t.WeChatOpenID == param.OpenID);
                    }
                    else if (param.MemberID.HasValue)
                    {
                        member = MemberRepo.GetInclude(t => t.MemberPlateList, false).FirstOrDefault(t => t.ID == param.MemberID.Value);
                    }
                    if (member != null)
                    {
                        var memberPlateNumberList = member.MemberPlateList.Select(t => t.PlateNumber).ToList();
                        pickuporderQueryable = pickuporderQueryable.Where(t => memberPlateNumberList.Contains(t.PlateNumber));
                    }
                    if (param.PlateNumberList != null && param.PlateNumberList.Count() > 0)
                    {
                        pickuporderQueryable = pickuporderQueryable.Where(t => param.PlateNumberList.Contains(t.PlateNumber));
                    }
                }

                var pickuporderList = pickuporderQueryable.ToList();

                servicelist.ForEach(t =>
                {
                    var serviceTime = pickuporderList.GroupBy(g => 1).Select(p => p.Sum(s => s.PickUpOrderItemList.Count(item => item.ProductID == t.ID))).FirstOrDefault();
                    result.Add(new HistoryDataAnalysisDto
                    {
                        ID = t.ID,
                        Name = t.Name,
                        Code = t.Code,
                        PriceSale = t.PriceSale,
                        MiningSpace = 5,
                        ServiceTime = serviceTime,
                    });
                });

                result.ForEach(t =>
                {
                    if (t.ServiceTime > 100)
                        t.MiningSpace = 1;
                    else if (t.ServiceTime > 80)
                        t.MiningSpace = 2;
                    else if (t.ServiceTime > 60)
                        t.MiningSpace = 3;
                    else if (t.ServiceTime > 10)
                        t.MiningSpace = 4;
                    else
                        t.MiningSpace = 5;
                });

            }

            return result.OrderBy(t => t.ServiceTime).ToList();
        }

        /// <summary>
        /// 出/入库
        /// </summary>
        /// <returns></returns>
        public bool StockOutIn(StockRecord stockRecord)
        {
            if (stockRecord == null || stockRecord.ProductID == null || stockRecord.Quantity == 0)
                throw new DomainException("参数错误");
            var product = Repository.GetByKey(stockRecord.ProductID);
            if (product == null)
                throw new DomainException("产品不存在");
            product.Stock += stockRecord.Quantity;
            if (product.Stock < 0)
                throw new DomainException("库存不足");
            UnitOfWork.BeginTransaction();
            try
            {
                Repository.Update(product);
                StockRecordService.Add(stockRecord);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
        }
    }
}
