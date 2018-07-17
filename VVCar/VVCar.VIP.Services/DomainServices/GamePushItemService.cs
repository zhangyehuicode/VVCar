using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 游戏推送子项服务
    /// </summary>
    public class GamePushItemService : DomainServiceBase<IRepository<GamePushItem>, GamePushItem, Guid>, IGamePushItemService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public GamePushItemService()
        {
        }

        #region properties

        IRepository<GamePush> GamePushRepo { get => UnitOfWork.GetRepository<IRepository<GamePush>>(); }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override GamePushItem Add(GamePushItem entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool Delete(Guid key)
        {
            var gamePushItem = Repository.GetByKey(key);
            if (gamePushItem == null)
                throw new DomainException("删除失败, 数据不存在");
            gamePushItem.IsDeleted = true;
            return Repository.Update(gamePushItem) > 0;
        }
        /// <summary>
        /// 批量新增游戏推送子项
        /// </summary>
        /// <param name="gamePushItems"></param>
        /// <returns></returns>
        public bool BatchAdd(IEnumerable<GamePushItem> gamePushItems)
        {
            if (gamePushItems == null || gamePushItems.Count() < 1)
                throw new DomainException("没有数据");
            var gamePushItemList = gamePushItems.ToList();
            var gamePushID = gamePushItemList.FirstOrDefault().GamePushID;
            var gamePush = GamePushRepo.GetByKey(gamePushID);
            if (gamePush.Status != EGamePushStatus.NotPush)
                throw new DomainException("请选择未推送的数据!");
            var gameSettingIDs = gamePushItemList.Select(t => t.GameSettingID).Distinct();
            var existData = Repository.GetQueryable(false)
                .Where(t => t.GamePushID == gamePushID && gameSettingIDs.Contains(t.GameSettingID))
                .Select(t => t.GameSettingID).ToList();
            if(existData.Count >0 )
            {
                gamePushItemList.RemoveAll(t => existData.Contains(t.GameSettingID));
            }
            if (gamePushItemList.Count < 1)
                return true;
            gamePushItemList.ForEach(t=>
            {
                t.ID = Util.NewID();
                t.MerchantID = AppContext.CurrentSession.MerchantID;
                t.CreatedUserID = AppContext.CurrentSession.UserID;
                t.CreatedUser = AppContext.CurrentSession.UserName;
                t.CreatedDate = DateTime.Now;
            });
            return Repository.AddRange(gamePushItemList).Count() > 0;
        }

        /// <summary>
        /// 批量删除游戏推送子项
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchDelete(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");
            var gamePushItemList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (gamePushItemList == null || gamePushItemList.Count() < 1)
                throw new DomainException("数据不存在");
            var gamePushIDs = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).Select(t => t.GamePushID).Distinct();
            foreach (var gamePushID in gamePushIDs)
            {
                var gamePush = GamePushRepo.GetByKey(gamePushID);
                if (gamePush.Status != EGamePushStatus.NotPush)
                    throw new DomainException("请选择未推送的数据");
            }
            return Repository.DeleteRange(gamePushItemList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<GamePushItemDto> Search(GamePushItemFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.GamePushID == filter.GamePushID);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<GamePushItemDto>().ToArray();
            
        }
    }
}
