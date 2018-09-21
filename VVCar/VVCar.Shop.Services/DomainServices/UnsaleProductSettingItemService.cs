using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Services;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 滞销产品参数设置子项领域服务
    /// </summary>
    public class UnsaleProductSettingItemService : DomainServiceBase<IRepository<UnsaleProductSettingItem>, UnsaleProductSettingItem, Guid>, IUnsaleProductSettingItemService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public UnsaleProductSettingItemService()
        {
        }

        IRepository<UnsaleProductSetting> UnsaleProductSettingRepo { get => UnitOfWork.GetRepository<IRepository<UnsaleProductSetting>>(); }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="unsaleProductSettingItems"></param>
        /// <returns></returns>
        public bool BatchAdd(IEnumerable<UnsaleProductSettingItem> unsaleProductSettingItems)
        {
            if (unsaleProductSettingItems == null || unsaleProductSettingItems.Count() < 1)
                throw new DomainException("没有数据");
            var unsaleProductSettingItemList = unsaleProductSettingItems.ToList();
            var unsaleProductSettingID = unsaleProductSettingItemList.FirstOrDefault().UnsaleProductSettingID;
            var unsaleProductSetting = UnsaleProductSettingRepo.GetByKey(unsaleProductSettingID);
            //if (unsaleProductSetting.IsAvailable)
            //    throw new DomainException("请选择未启用的数据!");
            var productIDs = unsaleProductSettingItemList.Select(t => t.ProductID).Distinct();
            var existData = Repository.GetQueryable(false)
                .Where(t => t.UnsaleProductSettingID == unsaleProductSettingID && productIDs.Contains(t.ProductID))
                .Select(t => t.ProductID).ToList();
            if (existData.Count > 0)
            {
                unsaleProductSettingItemList.RemoveAll(t => existData.Contains(t.ProductID));
            }
            if (unsaleProductSettingItemList.Count < 1)
                return true;
            unsaleProductSettingItemList.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.MerchantID = AppContext.CurrentSession.MerchantID;
                t.CreatedDate = DateTime.Now;
                t.CreatedUserID = AppContext.CurrentSession.UserID;
                t.CreatedUser = AppContext.CurrentSession.UserName;
            });
            return Repository.AddRange(unsaleProductSettingItemList).Count() > 0;
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
            var unsaleProductSettingItemList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (unsaleProductSettingItemList == null || unsaleProductSettingItemList.Count() < 1)
                throw new DomainException("数据不存在!");
            var unsaleProductSettingIDs = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).Select(t => t.UnsaleProductSettingID).Distinct().ToList();
            foreach(var unsaleProductSettingID in unsaleProductSettingIDs) {
                var unsaleProductSetting = UnsaleProductSettingRepo.GetByKey(unsaleProductSettingID);
                if (unsaleProductSetting.IsAvailable)
                    throw new DomainException("请选择未启用的数据");
            }
            unsaleProductSettingItemList.ForEach(t =>
            {
                t.IsDeleted = true;
            });
            return Repository.UpdateRange(unsaleProductSettingItemList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<UnsaleProductSettingItemDto> Search(UnsaleProductSettingItemFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.UnsaleProductSettingID.HasValue)
                queryable = queryable.Where(t => t.UnsaleProductSettingID == filter.UnsaleProductSettingID.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<UnsaleProductSettingItemDto>().ToArray();
        }
    }
}
