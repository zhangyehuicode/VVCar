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
    /// 车比特产品领域服务
    /// </summary>
    public class CarBitCoinProductService : DomainServiceBase<IRepository<CarBitCoinProduct>, CarBitCoinProduct, Guid>, ICarBitCoinProductService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public CarBitCoinProductService()
        {
        }

        #region properties

        IRepository<CarBitCoinProductCategory> _carBitCoinProductCategoryRepo;

        IRepository<CarBitCoinProductCategory> CarBitCoinProductCategoryRepo
        {
            get
            {
                if (_carBitCoinProductCategoryRepo == null)
                    _carBitCoinProductCategoryRepo = UnitOfWork.GetRepository<IRepository<CarBitCoinProductCategory>>();
                return _carBitCoinProductCategoryRepo;
            }
        }

        IRepository<Member> MemberRepo { get => UnitOfWork.GetRepository<IRepository<Member>>(); }

        #endregion

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool DoValidate(CarBitCoinProduct entity)
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

        public override CarBitCoinProduct Add(CarBitCoinProduct entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.Index = GenerateIndex();
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreateUser = AppContext.CurrentSession.UserName;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        public override bool Update(CarBitCoinProduct entity)
        {
            if (entity == null)
                return false;
            var carBitCoinProduct = Repository.GetByKey(entity.ID);
            if (carBitCoinProduct == null)
                throw new DomainException("更新的产品不存在");
            carBitCoinProduct.CarBitCoinProductType = entity.CarBitCoinProductType;
            carBitCoinProduct.Horsepower = entity.Horsepower;
            carBitCoinProduct.CarBitCoinProductCategoryID = entity.CarBitCoinProductCategoryID;
            carBitCoinProduct.Name = entity.Name;
            carBitCoinProduct.Code = entity.Code;
            carBitCoinProduct.ImgUrl = entity.ImgUrl;
            carBitCoinProduct.BasePrice = entity.BasePrice;
            carBitCoinProduct.PriceSale = entity.PriceSale;
            carBitCoinProduct.CarBitCoinPoints = entity.CarBitCoinPoints;
            carBitCoinProduct.UpperLimit = entity.UpperLimit;
            carBitCoinProduct.IsPublish = entity.IsPublish;
            carBitCoinProduct.IsRecommend = entity.IsRecommend;
            carBitCoinProduct.Stock = entity.Stock;
            carBitCoinProduct.Introduction = entity.Introduction;
            carBitCoinProduct.DeliveryNotes = entity.DeliveryNotes;
            carBitCoinProduct.CommissionRate = entity.CommissionRate;
            carBitCoinProduct.IsCanPointExchange = entity.IsCanPointExchange;
            carBitCoinProduct.Unit = entity.Unit;
            carBitCoinProduct.LastUpdateDate = DateTime.Now;
            carBitCoinProduct.LastUpdateUser = AppContext.CurrentSession.UserName;
            carBitCoinProduct.LastUpdateUserID = AppContext.CurrentSession.UserID;

            return base.Update(entity);
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
        public IEnumerable<CarBitCoinProduct> Search(CarBitCoinProductFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            if (!string.IsNullOrEmpty(filter.Code))
                queryable = queryable.Where(t => t.Code.Contains(filter.Code));
            if (!string.IsNullOrEmpty(filter.NameCode))
                queryable = queryable.Where(t => t.Name.Contains(filter.NameCode) || t.Code.Contains(filter.NameCode));
            if (filter.CarBitCoinProductCategoryID.HasValue && filter.CarBitCoinProductCategoryID.Value != Guid.Parse("00000000-0000-0000-0000-000000000001"))
                queryable = queryable.Where(t => t.CarBitCoinProductCategoryID == filter.CarBitCoinProductCategoryID.Value);
            if (filter.IsFromStockManager)
                queryable = queryable.Where(t => t.CarBitCoinProductType == ECarBitCoinProductType.Goods);
            if (filter.CarBitCoinProductType.HasValue)
                queryable = queryable.Where(t => t.CarBitCoinProductType == filter.CarBitCoinProductType.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.Index).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }

        /// <summary>
        /// 获取可上架产品
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CarBitCoinProductDto> GetCarBitCoinProduct()
        {
            var result = Repository.GetQueryable(false).Where(t => t.CarBitCoinProductType == ECarBitCoinProductType.Goods && t.IsPublish && t.Stock > 0).ToList();
            return result.MapTo<List<CarBitCoinProductDto>>();
        }

        /// <summary>
        /// 调整索引
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool AdjustIndex(AdjustIndexParam param)
        {
            if (param == null || param.ID == null)
                return false;
            var pointGoods = Repository.GetByKey(param.ID);
            if (pointGoods == null)
                return false;
            var carBitCoinProductQueryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            CarBitCoinProduct exchangeEntity = null;
            if (param.Direction == EAdjustDirection.Up)
            {
                var pre = carBitCoinProductQueryable.Where(t => t.Index == (pointGoods.Index - 1)).FirstOrDefault();
                if (pre == null)
                    return true;
                else
                    exchangeEntity = pre;
            }
            else if (param.Direction == EAdjustDirection.Down)
            {
                var next = carBitCoinProductQueryable.Where(t => t.Index == (pointGoods.Index + 1)).FirstOrDefault();
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
                return Repository.Update(new List<CarBitCoinProduct> { exchangeEntity, pointGoods }) == 2;
            }
            return false;
        }

        /// <summary>
        /// 更改发布状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        public IList<CarBitCoinProductCategoryLiteDto> GetCarBitCoinProductLiteData()
        {
            var categories = CarBitCoinProductCategoryRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
                .OrderBy(t => t.ParentId).ThenBy(t => t.Index)
                .MapTo<CarBitCoinProductCategory, CarBitCoinProductCategoryLiteDto>()
                .ToList();
            var carBitCoinProducts = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
                .Where(t => t.IsPublish)
                .MapTo<CarBitCoinProduct, CarBitCoinProductLiteDto>()
                .ToList();
            foreach (var category in categories)
            {
                category.CarBitCoinProducts = carBitCoinProducts.Where(t => t.CarBitCoinProductCategoryID == category.ID).ToList();
            }
            return categories;
        }

        /// <summary>
        /// 获取推荐产品
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CarBitCoinProduct> GetRecommendCarBitCoinProduct()
        {
            var result = new List<CarBitCoinProduct>();
            var queryable = Repository.GetQueryable(false).Where(t => t.IsPublish && t.Stock > 0);
            var recommend = queryable.Where(t => t.IsRecommend).ToList();
            result = recommend;
            if (result.Count < 4)
            {
                var additional = queryable.Where(t => !t.IsRecommend).OrderBy(t => t.Index).ToList();
                foreach (var item in additional)
                {
                    result.Add(item);
                    if (result.Count >= 4)
                        break;
                }
            }
            else if (result.Count > 4)
            {
                result = new List<CarBitCoinProduct>();
                foreach (var item in recommend)
                {
                    result.Add(item);
                    if (result.Count >= 4)
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取引擎商品
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CarBitCoinProduct> GetEngineProduct()
        {
            return Repository.GetQueryable(false).Where(t => t.CarBitCoinProductType == ECarBitCoinProductType.Engine && t.IsPublish).ToList();
        }
    }
}
