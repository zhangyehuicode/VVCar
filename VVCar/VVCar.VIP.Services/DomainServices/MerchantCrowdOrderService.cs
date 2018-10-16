using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 门店拼单领域服务
    /// </summary>
    public class MerchantCrowdOrderService : DomainServiceBase<IRepository<MerchantCrowdOrder>, MerchantCrowdOrder, Guid>, IMerchantCrowdOrderService
    {
        public MerchantCrowdOrderService()
        {
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override MerchantCrowdOrder Add(MerchantCrowdOrder entity)
        {
            if (entity == null)
                throw new DomainException("参数错误");
            entity.ID = Util.NewID();
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
        public override bool Update(MerchantCrowdOrder entity)
        {
            if (entity == null)
                return false;
            var crowdOrder = Repository.GetByKey(entity.ID);
            if (crowdOrder == null)
                return false;
            crowdOrder.Name = entity.Name;
            crowdOrder.Price = entity.Price;
            crowdOrder.ProductID = entity.ProductID;
            crowdOrder.PeopleCount = entity.PeopleCount;
            crowdOrder.IsAvailable = entity.IsAvailable;
            crowdOrder.PutawayTime = entity.PutawayTime;
            crowdOrder.SoleOutTime = entity.SoleOutTime;
            crowdOrder.LastUpdateDate = DateTime.Now;
            crowdOrder.LastUpdateUserID = AppContext.CurrentSession.UserID;
            crowdOrder.LastUpdateUser = AppContext.CurrentSession.UserName;
            return Repository.Update(crowdOrder) > 0;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchDelete(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");
            var merchantCrowdOrderList = Repository.GetQueryable(true).Where(t => ids.Contains(t.ID)).ToList();
            if (merchantCrowdOrderList == null || merchantCrowdOrderList.Count < 1)
                throw new DomainException("数据不存在");
            merchantCrowdOrderList.ForEach(t =>
            {
                t.IsDeleted = true;
                t.LastUpdateDate = DateTime.Now;
                t.LastUpdateUserID = AppContext.CurrentSession.UserID;
                t.LastUpdateUser = AppContext.CurrentSession.UserName;
            });
            return Repository.UpdateRange(merchantCrowdOrderList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<MerchantCrowdOrderDto> Search(MerchantCrowdOrderFilter filter, out int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.Product, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            if (filter.IsAvailable.HasValue)
                queryable = queryable.Where(t => t.IsAvailable == filter.IsAvailable);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<MerchantCrowdOrderDto>().ToArray();
        }

        /// <summary>
        /// 获取拼单数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MerchantCrowdOrderDto> GetMerchantCrowdOrderListByProductID(Guid id)
        {
            if (id == null)
                throw new DomainException("参数错误");
            var now = DateTime.Now;
            var queryable = Repository.GetInclude(t => t.Product, false).Where(t =>t.ProductID == id && t.IsAvailable && t.PutawayTime <= now && t.SoleOutTime > now && t.Product.Stock > 0 && t.PeopleCount > 0);
            return queryable.MapTo<MerchantCrowdOrderDto>().ToArray();
        }
    }
}
