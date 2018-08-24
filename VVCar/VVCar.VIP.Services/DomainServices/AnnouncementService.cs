using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain;
using VVCar.BaseData.Domain.Entities;
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
    public class AnnouncementService : DomainServiceBase<IRepository<Announcement>, Announcement, Guid>, IAnnouncementService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AnnouncementService()
        {
        }

        #region properties

        IRepository<AnnouncementPushMember> AnnouncementPushMemberRepo { get => UnitOfWork.GetRepository<IRepository<AnnouncementPushMember>>(); }

        IRepository<Merchant> MerchantRepo { get => UnitOfWork.GetRepository<IRepository<Merchant>>(); }

        IRepository<Member> MemberRepo { get => UnitOfWork.GetRepository<IRepository<Member>>(); }

        IWeChatService WeChatService { get => ServiceLocator.Instance.GetService<IWeChatService>(); }

        ISystemSettingService SystemSettingService { get => ServiceLocator.Instance.GetService<ISystemSettingService>(); }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override Announcement Add(Announcement entity)
        {
            if (entity == null)
                throw new DomainException("参数错误");
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(Announcement entity)
        {
            if (entity == null)
                return false;
            var announcement = Repository.GetByKey(entity.ID);
            if (announcement == null)
                throw new DomainException("数据不存在");
            announcement.Title = entity.Title;
            announcement.PushAllMembers = entity.PushAllMembers;
            announcement.Process = entity.Process;
            announcement.Content = entity.Content;
            announcement.LastUpdateDate = DateTime.Now;
            announcement.LastUpdateUser = AppContext.CurrentSession.UserName;
            announcement.LastUpdateUserID = AppContext.CurrentSession.UserID;
            return Repository.Update(entity) > 0;
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
            var announcementList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (announcementList == null || announcementList.Count() < 1)
                throw new DomainException("数据不存在");
            announcementList.ForEach(t =>
            {
                t.IsDeleted = true;
                t.LastUpdateDate = DateTime.Now;
                t.LastUpdateUserID = AppContext.CurrentSession.UserID;
                t.LastUpdateUser = AppContext.CurrentSession.UserName;
            });
            return Repository.UpdateRange(announcementList) > 0;
        }

        /// <summary>
        /// 批量推送
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchHandPush(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数不正确");
            var notPushData = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID) && t.Status == EAnnouncementStatus.NotPush).ToList();
            if (notPushData.Count() < 1)
                throw new DomainException("请选择未推送的数据");
            notPushData.ForEach(t =>
            {
                t.Status = EAnnouncementStatus.Pushed;
                t.PushDate = DateTime.Now;
                t.LastUpdateDate = DateTime.Now;
                t.LastUpdateUser = AppContext.CurrentSession.UserName;
                t.LastUpdateUserID = AppContext.CurrentSession.UserID;
            });
            if (Repository.UpdateRange(notPushData) > 0)
            {
                notPushData.ForEach(t => SendNotifyToSalesman(t.ID, t.PushAllMembers));
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<Announcement> Search(AnnouncementFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.Title))
                queryable = queryable.Where(t => t.Title.Contains(filter.Title));
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            if (filter.Status.HasValue)
                queryable = queryable.Where(t => t.Status == filter.Status);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }

        void SendNotifyToSalesman(Guid id, bool PushAllMembers)
        {
            var announcement = Repository.GetByKey(id);
            var memberList = AnnouncementPushMemberRepo.GetInclude(t=>t.Member).Where(t=> t.AnnouncementID == announcement.ID).ToList();
            var allMemberList = MemberRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID).ToList();
            var merchant = MerchantRepo.GetByKey(announcement.MerchantID);
            if (PushAllMembers)
            {
                allMemberList.ForEach(t =>
                {
                    var message = new WeChatTemplateMessageDto
                    {
                        touser = t.WeChatOpenID,
                        template_id = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_Announcement),
                        url = "http://cheyinz.cn",
                        data = new System.Dynamic.ExpandoObject()
                    };
                    message.data.first = new WeChatTemplateMessageDto.MessageData(announcement.Title);
                    message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(announcement.Name);
                    message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(announcement.Process);
                    message.data.remark = new WeChatTemplateMessageDto.MessageData(announcement.Remark);
                    WeChatService.SendWeChatNotifyAsync(message, merchant.Code);
                });
            }
            else {
                memberList.ForEach(t =>
                {
                    var message = new WeChatTemplateMessageDto
                    {
                        touser = t.Member.WeChatOpenID,
                        template_id = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_Announcement),
                        url = "http://cheyinz.cn",
                        data = new System.Dynamic.ExpandoObject()
                    };
                    message.data.first = new WeChatTemplateMessageDto.MessageData(announcement.Title);
                    message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(announcement.Name);
                    message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(announcement.Process);
                    message.data.remark = new WeChatTemplateMessageDto.MessageData(announcement.Remark);
                    WeChatService.SendWeChatNotifyAsync(message, merchant.Code);
                });
            }
        }
    }
}
