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
    /// 游戏推送服务领域接口
    /// </summary>
    public interface IGamePushService : IDomainService<IRepository<GamePush>, GamePush, Guid>
    {
        /// <summary>
        /// 批量删除游戏推送任务
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DeleteGamePushs(Guid[] ids);

        /// <summary>
        /// 手动批量推送
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchHandGamePush(Guid[] ids);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<GamePushDto> Search(GamePushFilter filter, out int totalCount);

        /// <summary>
        /// 游戏推送任务
        /// </summary>
        /// <returns></returns>
        bool GamePushTask();
    }
}
