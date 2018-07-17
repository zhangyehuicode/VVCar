using System;
using System.Collections.Generic;
using System.Linq;
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
    /// 游戏推送会员领域服务
    /// </summary>
    public class GamePushMemberService : DomainServiceBase<IRepository<GamePushMember>, GamePushMember, Guid>, IGamePushMemberService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public GamePushMemberService()
        {
        }

        #region properties

        IRepository<GamePush> GamePushRepo { get => UnitOfWork.GetRepository<IRepository<GamePush>>(); }

        IRepository<MemberPlate> MemberPlateRepo { get => UnitOfWork.GetRepository<IRepository<MemberPlate>>(); }

        #endregion

        /// <summary>
        /// 批量新增游戏推送会员
        /// </summary>
        /// <param name="gamePushMembers"></param>
        /// <returns></returns>
        public bool BatchAdd(IEnumerable<GamePushMember> gamePushMembers)
        {
            if (gamePushMembers == null || gamePushMembers.Count() < 1)
                throw new DomainException("没有数据");
            var gamePushMemberList = gamePushMembers.ToList();
            var gamePushID = gamePushMemberList.FirstOrDefault().GamePushID;
            var gamePush = GamePushRepo.GetByKey(gamePushID);
            if(gamePush.Status != EGamePushStatus.NotPush)
                throw new DomainException("请选择未推送的数据!");
            var gamePushMemberIDs = gamePushMemberList.Select(t => t.MemberID).Distinct();
            var existData = Repository.GetQueryable(false)
                .Where(t => t.GamePushID == gamePushID && gamePushMemberIDs.Contains(t.MemberID))
                .Select(t => t.MemberID).ToList();
            if (existData.Count > 0)
                gamePushMemberList.RemoveAll(t => existData.Contains(t.MemberID));
            if (gamePushMemberList.Count < 1)
                return true;
            gamePushMemberList.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.MerchantID = AppContext.CurrentSession.MerchantID;
                t.CreatedUser = AppContext.CurrentSession.UserName;
                t.CreatedUserID = AppContext.CurrentSession.UserID;
                t.CreatedDate = DateTime.Now;
            });
            return Repository.AddRange(gamePushMemberList).Count() > 0;
        }

        /// <summary>
        /// 批量删除游戏推送会员
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchDelete(Guid[] ids)
        {
            if(ids == null || ids.Length<1)
                throw new DomainException("参数错误");
            var gamePushMemberList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (gamePushMemberList == null || gamePushMemberList.Count() < 1)
                throw new DomainException("数据不存在");
            var gamePushIDs = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).Select(t => t.GamePushID).Distinct();
            foreach(var gamePushID in gamePushIDs)
            {
                var gamePush = GamePushRepo.GetByKey(gamePushID);
                if (gamePush.Status != EGamePushStatus.NotPush)
                    throw new DomainException("请选择未推送的数据!");
            }
            return Repository.DeleteRange(gamePushMemberList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<GamePushMemberDto> Search(GamePushMemberFilter filter, out int totalCount)
        {
            if(filter.GamePushID == null)
                throw new DomainException("参数错误");
            var queryable = Repository.GetInclude(t => t.Member, false).Where(t => t.GamePushID == filter.GamePushID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            totalCount = queryable.Count();
            var result = queryable.MapTo<GamePushMemberDto>().ToArray();
            var memberPlateQueryable = MemberPlateRepo.GetQueryable(false);
            result.ForEach(t =>
            {
                var memberPlateList = memberPlateQueryable.Where(p => p.MemberID == t.MemberID);
                if (memberPlateList.Count() > 0)
                {
                    foreach (var memberPlate in memberPlateList)
                    {
                        t.PlateList += memberPlate.PlateNumber + '、';
                    }
                    t.PlateList = t.PlateList.Substring(0, t.PlateList.Length - 1);
                }
            });
            return result;
        }
    }
}
