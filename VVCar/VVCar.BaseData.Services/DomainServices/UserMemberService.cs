using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using VVCar.VIP.Domain.Entities;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Services.DomainServices
{
    /// <summary>
    /// 用户会员关联服务
    /// </summary>
    public partial class UserMemberService : DomainServiceBase<IRepository<UserMember>, UserMember, Guid>, IUserMemberService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public UserMemberService()
        {
        }

        #region properties

        IRepository<Member> MemberRepo { get => UnitOfWork.GetRepository<IRepository<Member>>(); }
        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override UserMember Add(UserMember entity)
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
        /// 批量新增
        /// </summary>
        /// <param name="userMembers"></param>
        /// <returns></returns>
        public bool BatchAdd(IEnumerable<UserMember> userMembers)
        {
            if (userMembers == null || userMembers.Count() < 1)
                throw new DomainException("新增失败, 没有数据");
            var userMemberList = userMembers.ToList();
            var userId = userMemberList.FirstOrDefault().UserID;
            var memberIds = userMemberList.Select(t => t.MemberID).Distinct();
            var existData = Repository.GetQueryable(false)
                .Where(t => memberIds.Contains(t.MemberID))
                .Select(t => t.MemberID).ToList();
            if (existData.Count > 0)
                userMemberList.RemoveAll(t => existData.Contains(t.MemberID));
            if (userMemberList.Count < 1)
                return true;
            foreach(var userMember in userMemberList)
            {
                userMember.ID = Util.NewID();
                userMember.CreatedUserID = AppContext.CurrentSession.UserID;
                userMember.CreatedUser = AppContext.CurrentSession.UserName;
                userMember.MerchantID = AppContext.CurrentSession.MerchantID;
                userMember.CreatedDate = DateTime.Now;
            }
            return Repository.AddRange(userMemberList).Count() > 0;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchDelete(IEnumerable<Guid> ids)
        {
            var userMemberList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (userMemberList == null || userMemberList.Count() < 1)
                throw new DomainException("数据不存在");
            return Repository.DeleteRange(userMemberList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<UserMemberDto> Search(UserMemberFilter filter, out int totalCount)
        {
            if (filter == null || !filter.UserID.HasValue)
                throw new DomainException("参数错误");
            var queryable = Repository.GetQueryable().Where(t => t.UserID == filter.UserID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            if(AppContext.CurrentSession.UserID != Guid.Parse("00000000-0000-0000-0000-000000000001"))
            {
                var adminId = Guid.Parse("00000000-0000-0000-0000-000000000001");
                queryable = queryable.Where(t => t.ID != adminId);
            }
            totalCount = queryable.Count();
            if(filter.Start.HasValue && filter.Limit.HasValue)
            {
                queryable = queryable.OrderByDescending(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value);
            }
            var userMemberList = queryable.ToList();
            IList<UserMemberDto> userMemberDtoList = new List<UserMemberDto>();
            userMemberList.ForEach(t => 
            {
                var userMemberDto = new UserMemberDto();
                var member = MemberRepo.GetByKey(t.MemberID);
                userMemberDto.ID = t.ID;
                userMemberDto.MemberGroup = member.MemberGroupID == Guid.Parse("00000000-0000-0000-0000-000000000001")?"普通会员":"批发价会员";
                userMemberDto.MemberName = member.Name;
                userMemberDto.Sex = member.Sex;
                userMemberDto.CreatedDate = t.CreatedDate;
                userMemberDtoList.Add(userMemberDto);
            });
            return userMemberDtoList;
        }
    }
}
