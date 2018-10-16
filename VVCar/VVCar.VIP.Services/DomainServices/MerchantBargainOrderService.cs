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
    /// 门店砍价领域服务
    /// </summary>
    public class MerchantBargainOrderService : DomainServiceBase<IRepository<MerchantBargainOrder>, MerchantBargainOrder, Guid>, IMerchantBargainOrderService
    {
        /// <summary>
        /// ctor.
        /// </summary>
        public MerchantBargainOrderService()
        {
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override MerchantBargainOrder Add(MerchantBargainOrder entity)
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
        public override bool Update(MerchantBargainOrder entity)
        {
            if (entity == null)
                return false;
            var merchantBargainOrder = Repository.GetByKey(entity.ID);
            if (merchantBargainOrder == null)
                return false;
            merchantBargainOrder.Name = entity.Name;
            merchantBargainOrder.Price = entity.Price;
            merchantBargainOrder.ProductID = entity.ProductID;
            merchantBargainOrder.PeopleCount = entity.PeopleCount;
            merchantBargainOrder.IsAvailable = entity.IsAvailable;
            merchantBargainOrder.PutawayTime = entity.PutawayTime;
            merchantBargainOrder.SoleOutTime = entity.SoleOutTime;
            merchantBargainOrder.LastUpdateDate = DateTime.Now;
            merchantBargainOrder.LastUpdateUserID = AppContext.CurrentSession.UserID;
            merchantBargainOrder.LastUpdateUser = AppContext.CurrentSession.UserName;
            return Repository.Update(merchantBargainOrder) > 0;
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
            var merchantBargainOrderList = Repository.GetQueryable(true).Where(t => ids.Contains(t.ID)).ToList();
            if (merchantBargainOrderList == null || merchantBargainOrderList.Count < 1)
                throw new DomainException("数据不存在");
            merchantBargainOrderList.ForEach(t =>
            {
                t.IsDeleted = true;
                t.LastUpdateDate = DateTime.Now;
                t.LastUpdateUserID = AppContext.CurrentSession.UserID;
                t.LastUpdateUser = AppContext.CurrentSession.UserName;
            });
            return Repository.UpdateRange(merchantBargainOrderList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<MerchantBargainOrderDto> Search(MerchantBargainOrderFilter filter, out int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.Product, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            if (filter.IsAvailable.HasValue)
                queryable = queryable.Where(t => t.IsAvailable == filter.IsAvailable);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<MerchantBargainOrderDto>().ToArray();
        }

        /// <summary>
        /// 获取拼单数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MerchantBargainOrderDto> GetMerchantBargainOrderListByProductID(Guid id)
        {
            if (id == null)
                throw new DomainException("参数错误");
            var now = DateTime.Now;
            var queryable = Repository.GetInclude(t => t.Product, false).Where(t => t.ProductID == id && t.IsAvailable && t.PutawayTime <= now && t.SoleOutTime > now && t.Product.Stock > 0 && t.PeopleCount > 0);
            return queryable.MapTo<MerchantBargainOrderDto>().ToArray();
        }
    }
}
