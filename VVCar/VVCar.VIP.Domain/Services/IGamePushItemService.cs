using System;
using System.Collections.Generic;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 游戏推送子项服务
    /// </summary>
    public interface IGamePushItemService : IDomainService<IRepository<GamePushItem>, GamePushItem, Guid>
    {
        /// <summary>
        /// 批量新增游戏推送子项
        /// </summary>
        /// <param name="gamePushItems"></param>
        /// <returns></returns>
        bool BatchAdd(IEnumerable<GamePushItem> gamePushItems);

        /// <summary>
        /// 批量删除游戏推送子项
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDelete(Guid[] ids);

        /// <summary>
        /// 查询游戏推送子项
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<GamePushItemDto> Search(GamePushItemFilter filter, out int totalCount);
    }
}
