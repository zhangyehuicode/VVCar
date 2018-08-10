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
    /// 公告推送会员服务
    /// </summary>
    public class AnnouncementPushMemberService : DomainServiceBase<IRepository<AnnouncementPushMember>, AnnouncementPushMember, Guid>, IAnnouncementPushMemberService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AnnouncementPushMemberService()
        {
        }

        #region properties

        IRepository<Announcement> AnnouncementRepo { get => UnitOfWork.GetRepository<IRepository<Announcement>>(); }

        IRepository<MemberPlate> MemberPlateRepo { get => UnitOfWork.GetRepository<IRepository<MemberPlate>>(); }

        #endregion

        public bool BatchAdd(IEnumerable<AnnouncementPushMember> announcementPushMembers)
        {
            if (announcementPushMembers == null || announcementPushMembers.Count() < 1)
                throw new DomainException("没有数据");
            var announcementPushMemberList = announcementPushMembers.ToList();
            var announcementID = announcementPushMemberList.FirstOrDefault().AnnouncementID;
            var announcement = AnnouncementRepo.GetByKey(announcementID);
            if (announcement.Status != Domain.Enums.EAnnouncementStatus.NotPush)
                throw new DomainException("请选择未推送的数据");
            var announcementPushMemberIDs = announcementPushMemberList.Select(t => t.MemberID).Distinct();
            var existData = Repository.GetQueryable(false)
                .Where(t => t.AnnouncementID == announcementID && announcementPushMemberIDs.Contains(t.MemberID))
                .Select(t => t.MemberID).ToList();
            if (existData.Count > 0)
                announcementPushMemberList.RemoveAll(t => existData.Contains(t.MemberID));
            if (announcementPushMemberList.Count < 1)
                return true;
            announcementPushMemberList.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.MerchantID = AppContext.CurrentSession.MerchantID;
                t.CreatedUserID = AppContext.CurrentSession.UserID;
                t.CreatedUser = AppContext.CurrentSession.UserName;
                t.CreatedDate = DateTime.Now;
            });
            return Repository.AddRange(announcementPushMemberList).Count() > 0;
        }

        /// <summary>
        /// 批量删除推送会员
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchDelete(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");
            var announcementPushMemberList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (announcementPushMemberList == null || announcementPushMemberList.Count() < 1)
                throw new DomainException("数据不存在");
            announcementPushMemberList.ForEach(t =>
            {
                var announcement = AnnouncementRepo.GetByKey(t.AnnouncementID);
                if (announcement.Status != Domain.Enums.EAnnouncementStatus.NotPush)
                    throw new DomainException("请选择未推送的数据");
            });
            return Repository.DeleteRange(announcementPushMemberList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<AnnouncementPushMemberDto> Search(AnnouncementPushMemberFilter filter, out int totalCount)
        {
            if (filter.AnnouncementID == null)
                throw new DomainException("参数错误");
            var queryable = this.Repository.GetInclude(t => t.Member, false).Where(t => t.AnnouncementID == filter.AnnouncementID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value);
            var result = queryable.MapTo<AnnouncementPushMemberDto>().ToArray();
            var memberPlateQueryable = MemberPlateRepo.GetQueryable(false);
            result.ForEach(t =>
            {
                var memberPlateList = memberPlateQueryable.Where(p => p.MemberID == t.MemberID).ToList();
                if (memberPlateList.Count() > 0)
                {
                    foreach (var memberPlate in memberPlateList)
                    {
                        t.PlateList += memberPlate.PlateNumber + "、";
                    }
                    t.PlateList = t.PlateList.Substring(0, t.PlateList.Length - 1);
                }
            });
            return result;
        }
    }
}
