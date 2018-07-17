using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 游戏推送会员服务
    /// </summary>
    public interface IGamePushMemberService : IDomainService<IRepository<GamePushMember>, GamePushMember, Guid>
    {
        /// <summary>
        /// 批量新增游戏推送会员
        /// </summary>
        /// <param name="gamePushMembers"></param>
        /// <returns></returns>
        bool BatchAdd(IEnumerable<GamePushMember> gamePushMembers);

        /// <summary>
        /// 批量删除游戏推送会员
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDelete(Guid[] ids);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<GamePushMemberDto> Search(GamePushMemberFilter filter, out int totalCount);
    }
}
