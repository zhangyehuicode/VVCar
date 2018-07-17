using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain;
using VVCar.BaseData.Domain.Services;
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
    /// 游戏推送服务领域
    /// </summary>
    public class GamePushService : DomainServiceBase<IRepository<GamePush>, GamePush, Guid>, IGamePushService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public GamePushService()
        {
        }

        #region properties

        IRepository<GamePushItem> GamePushItemRepo { get => UnitOfWork.GetRepository<IRepository<GamePushItem>>(); }

        IRepository<GamePushMember> GamePushMemberRepo { get => UnitOfWork.GetRepository<IRepository<GamePushMember>>(); }

        IRepository<GameSetting> GameSettingRepo { get => UnitOfWork.GetRepository<IRepository<GameSetting>>(); }

        IRepository<Member> MemberRepo { get => UnitOfWork.GetRepository<IRepository<Member>>(); }

        IRepository<Merchant> MerchantRepo { get => UnitOfWork.GetRepository<IRepository<Merchant>>(); }

        IWeChatService WeChatService { get => ServiceLocator.Instance.GetService<IWeChatService>(); }

        ISystemSettingService SystemSettingService { get => ServiceLocator.Instance.GetService<ISystemSettingService>(); }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override GamePush Add(GamePush entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        /// <summary>
        /// 批量删除游戏推送任务
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteGamePushs(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");
            var gamePushList = Repository.GetIncludes(false, "GamePushItems", "GamePushMembers").Where(t => ids.Contains(t.ID) && t.Status == EGamePushStatus.NotPush).ToList();
            if (gamePushList == null || gamePushList.Count() < 1)
                throw new DomainException("请选择未推送的数据");
            UnitOfWork.BeginTransaction();
            try
            {
                foreach(var gamePush in gamePushList)
                {
                    if (gamePush.GamePushItems.Count() > 0)
                    {
                        GamePushItemRepo.DeleteRange(gamePush.GamePushItems);
                        gamePush.GamePushItems = null;
                    }
                    if(gamePush.GamePushMembers.Count()>0)
                    {
                        GamePushMemberRepo.DeleteRange(gamePush.GamePushMembers);
                        gamePush.GamePushMembers = null;
                    }
                }
                Repository.Delete(gamePushList);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch(Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(GamePush entity)
        {
            if (entity == null)
                return false;
            var gamePush = Repository.GetByKey(entity.ID);
            if (gamePush == null)
                return false;
            if (gamePush.Status == EGamePushStatus.Pushed)
                throw new DomainException("已推送的不能修改");
            gamePush.Title = entity.Title;
            gamePush.PushDate = entity.PushDate;
            gamePush.PushAllMembers = entity.PushAllMembers;
            gamePush.Status = entity.Status;
            gamePush.LastUpdateUserID = AppContext.CurrentSession.UserID;
            gamePush.LastUpdateUser = AppContext.CurrentSession.UserName;
            gamePush.LastUpdateDate = DateTime.Now;
            return Repository.Update(gamePush) > 0;
        }

        /// <summary>
        /// 手动批量推送游戏
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchHandGamePush(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数不正确");
            var notPushData = Repository.GetIncludes(false, "GamePushItems", "GamePushMembers").Where(t => ids.Contains(t.ID) && EGamePushStatus.NotPush == t.Status).ToList();
            if (notPushData.Count < 1)
                throw new DomainException("请选择未推送的数据");
            var notExistItem = false;
            notPushData.ForEach(t =>
            {
                if (t.GamePushItems.Count() < 1)
                    notExistItem = true;
            });
            if (notExistItem)
                throw new DomainException("请先添加游戏");
            var memberCount = MemberRepo.GetQueryable(false).Where(t => t.Card.Status == ECardStatus.Activated && t.MerchantID == AppContext.CurrentSession.MerchantID).Count();
            if (memberCount < 1)
                throw new DomainException("还没有会员");
            return GamePushAction(notPushData);
        }

        /// <summary>
        /// 查询游戏推送任务
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<GamePushDto> Search(GamePushFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.Title))
                queryable = queryable.Where(t => t.Title.Contains(filter.Title));
            if (filter.Status.HasValue)
                queryable = queryable.Where(t => t.Status == filter.Status);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<GamePushDto>().ToArray();
        }

        /// <summary>
        /// 游戏推送任务
        /// </summary>
        /// <returns></returns>
        public bool GamePushTask()
        {
            var startTime = DateTime.Now.Date;
            var endTime = startTime.AddDays(1);
            var gamePushList = Repository.GetIncludes(false, "GamePushItems", "GamePushMembers").Where(t => t.PushDate >= startTime && t.PushDate < endTime && t.Status == EGamePushStatus.NotPush).ToList();
            return GamePushAction(gamePushList);
        }

        /// <summary>
        /// 游戏推送动作
        /// </summary>
        /// <param name="gamePushList"></param>
        /// <returns></returns>
        public bool GamePushAction(List<GamePush> gamePushList)
        {
            if (gamePushList != null || gamePushList.Count() > 0)
            {
                UnitOfWork.BeginTransaction();
                try
                {
                    var memberQueryable = MemberRepo.GetQueryable(false).Where(t => t.Card.Status == ECardStatus.Activated);
                    gamePushList.ForEach(t => 
                    {
                        if(t.GamePushItems != null && t.GamePushItems.Count>0)
                        {
                            if (!t.PushAllMembers)
                            {
                                var memberids = new List<Guid>();
                                if (t.GamePushItems != null)
                                    memberids = t.GamePushMembers.Select(m => m.MemberID).ToList();
                                memberQueryable = memberQueryable.Where(m => memberids.Contains(m.ID));
                            }
                            var members = memberQueryable.Where(m => m.MerchantID == t.MerchantID).ToList();
                            var merchant = MerchantRepo.GetQueryable(false).Where(m => m.ID == t.MerchantID).FirstOrDefault();
                            if (members != null && members.Count() > 0 && merchant != null) 
                            {
                                members.ForEach(m =>
                                {
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(m.WeChatOpenID))
                                        {
                                            var gameSettingIDs = t.GamePushItems.Select(item => item.GameSettingID).ToList();
                                            ReceiveGameAction(new ReceiveGameDto
                                            {
                                                ReceiveOpenID = m.WeChatOpenID,
                                                GameSettingIDs = t.GamePushItems.Select(item => item.GameSettingID).ToList(),
                                                CompanyCode = merchant.Code,
                                                NickName = m.Name,
                                                MerchantID = merchant.ID,
                                                MemberID = m.ID,
                                            });
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        AppContext.Logger.Error($"游戏推送会员出现异常,{e.Message}");
                                    }
                                });
                                t.Status = EGamePushStatus.Pushed;
                            }
                        }
                    });
                    Repository.Update(gamePushList);
                    UnitOfWork.CommitTransaction();
                }
                catch(Exception e)
                {
                    UnitOfWork.RollbackTransaction();
                    throw e;
                }
            }
            return true;
        }

        private void ReceiveGameAction(ReceiveGameDto receiveGameDto)
        {
            var companyCode = AppContext.CurrentSession.CompanyCode;
            var merchantId = AppContext.CurrentSession.MerchantID;
            if (string.IsNullOrEmpty(companyCode))
                companyCode = receiveGameDto.CompanyCode;
            if (receiveGameDto.MerchantID.HasValue)
                merchantId = receiveGameDto.MerchantID.Value;
            var gameSettingList = GameSettingRepo.GetQueryable(false).Where(t => receiveGameDto.GameSettingIDs.Contains(t.ID)).ToList();
            if (gameSettingList == null || gameSettingList.Count() < 1)
                throw new DomainException("游戏配置未配置");
            gameSettingList.ForEach(t =>
            {
                var message = new WeChatTemplateMessageDto
                {
                    touser = receiveGameDto.ReceiveOpenID,
                    template_id = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_GameRemind),
                    url = $"{AppContext.Settings.SiteDomain}/VVCarWebAPP/Mobile/Game/Wheels?mch={companyCode}&GameType={(int)t.GameType}",
                    //url = $"http://www.cheyinz.cn/VVCarWebAPP/Mobile/Game/Wheels?mch={companyCode}&GameType={(int)t.GameType}",
                    data = new System.Dynamic.ExpandoObject(),
                };
                message.data.first = new WeChatTemplateMessageDto.MessageData("您好，您获得了一次抽奖机会");
                message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(t.GameType.ToString());
                message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(receiveGameDto.NickName);
                message.data.keyword3 = new WeChatTemplateMessageDto.MessageData(t.ShareTitle);
                message.data.keyword4 = new WeChatTemplateMessageDto.MessageData(DateTime.Now.ToDateString());
                message.data.remark = new WeChatTemplateMessageDto.MessageData("点击链接完成抽奖");
                WeChatService.SendWeChatNotifyAsync(message, receiveGameDto.CompanyCode);
            });
        }
    }
}
