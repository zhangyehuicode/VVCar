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
    /// 滞销产品参数设置领域服务
    /// </summary>
    public class UnsaleProductSettingService : DomainServiceBase<IRepository<UnsaleProductSetting>, UnsaleProductSetting, Guid>, IUnsaleProductSettingService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public UnsaleProductSettingService()
        {
        }

        IRepository<UnsaleProductSettingItem> UnsaleProductSettingItemRepo { get => UnitOfWork.GetRepository<IRepository<UnsaleProductSettingItem>>(); }

        protected override bool DoValidate(UnsaleProductSetting entity)
        {
            bool exists = this.Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (exists)
                throw new DomainException(string.Format("代码{0}已经使用,不能重复添加.", entity.Code));
            return true;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override UnsaleProductSetting Add(UnsaleProductSetting entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
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
            var unsaleProductSettingList = this.Repository.GetQueryable(true)
                .Where(t => ids.Contains(t.ID)).ToList();
            if (unsaleProductSettingList == null || unsaleProductSettingList.Count() < 1)
                throw new DomainException("数据不存在!");
            unsaleProductSettingList.ForEach(t =>
            {
                if (t.IsAvailable)
                    throw new DomainException("请选择未启用的数据进行删除");
                var unsaleProductSettingItemList = UnsaleProductSettingItemRepo.GetQueryable(true).Where(m => m.UnsaleProductSettingID == t.ID).ToList();
                unsaleProductSettingItemList.ForEach(m =>
                {
                    m.IsDeleted = true;
                });
                UnsaleProductSettingItemRepo.UpdateRange(unsaleProductSettingItemList);
                t.IsDeleted = true;
                t.LastUpdateDate = DateTime.Now;
                t.LastUpdateUserID = AppContext.CurrentSession.UserID;
                t.LastUpdateUser = AppContext.CurrentSession.UserName;
            });
            return Repository.UpdateRange(unsaleProductSettingList) > 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(UnsaleProductSetting entity)
        {
            if (entity == null)
                return false;
            var unsaleProductSetting = Repository.GetByKey(entity.ID);
            if (unsaleProductSetting == null)
                return false;
            unsaleProductSetting.Code = entity.Code;
            unsaleProductSetting.Name = entity.Name;
            unsaleProductSetting.PeriodDays = entity.PeriodDays;
            unsaleProductSetting.Quantities = entity.Quantities;
            unsaleProductSetting.Performence = entity.Performence;
            unsaleProductSetting.IsAvailable = entity.IsAvailable;

            unsaleProductSetting.LastUpdateDate = DateTime.Now;
            unsaleProductSetting.LastUpdateUserID = AppContext.CurrentSession.UserID;
            unsaleProductSetting.LastUpdateUser = AppContext.CurrentSession.UserName;
            return base.Update(unsaleProductSetting);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<UnsaleProductSettingDto> Search(UnsaleProductSettingFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetQueryable(false).Where(t=> t.MerchantID == AppContext.CurrentSession.MerchantID);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<UnsaleProductSettingDto>().ToArray();
        }
    }
}
