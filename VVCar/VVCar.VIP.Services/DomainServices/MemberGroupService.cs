using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Services;
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
    /// 会员分组 领域服务
    /// </summary>
    public class MemberGroupService : DomainServiceBase<IRepository<MemberGroup>, MemberGroup, Guid>, IMemberGroupService
    {
        #region fields        

        /// <summary>
        /// 默认分组
        /// </summary>
        readonly static Guid _defaultGroupID = Guid.Parse("00000000-0000-0000-0000-000000000001");

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberGroupService"/> class.
        /// </summary>
        public MemberGroupService()
        {
        }

        #region properties

        IRepository<Member> MemberRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<Member>>(); }
        }

        #endregion

        protected override bool DoValidate(MemberGroup entity)
        {
            var exists = Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (exists)
            {
                throw new DomainException($"代码 {entity.Code} 已使用，不能重复添加。");
            }
            exists = Repository.Exists(t => t.Name == entity.Name && t.ID != entity.ID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (exists)
            {
                throw new DomainException($"名称 {entity.Name} 已使用，不能重复添加。");
            }
            return true;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override MemberGroup Add(MemberGroup entity)
        {
            entity.ID = Util.NewID();
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(MemberGroup entity)
        {
            if (entity == null)
            {
                return true;
            }
            if (entity.ID == _defaultGroupID)
                throw new DomainException("不允许修改默认分组");
            var membergroup = Repository.GetByKey(entity.ID);
            if (membergroup == null)
            {
                return true;
            }
            membergroup.Name = entity.Name;
            membergroup.Code = entity.Code;
            membergroup.Index = entity.Index;
            membergroup.ParentId = entity.ParentId;
            membergroup.IsWholesalePrice = entity.IsWholesalePrice;
            membergroup.LastUpdateDate = DateTime.Now;
            membergroup.LastUpdateUser = AppContext.CurrentSession.UserName;
            membergroup.LastUpdateUserID = AppContext.CurrentSession.UserID;
            return base.Update(membergroup);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool Delete(Guid key)
        {
            if (key == _defaultGroupID)
                throw new DomainException("不允许删除默认分组");
            var hasMember = MemberRepo.Exists(t => t.MemberGroupID == key);
            if (hasMember)
                throw new DomainException("分组下有关联会员，请先移除关联会员");
            return base.Delete(key);
        }

        /// <summary>
        /// 获取默认会员分组ID
        /// </summary>
        /// <returns></returns>
        public Guid GetDefaultMemberGroupID()
        {
            return _defaultGroupID;
        }

        public IEnumerable<MemberGroup> Search(MemberGroupFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false);
            var result = new List<MemberGroup>();
            if (filter != null)
            {
                if (filter.All)
                    queryable = queryable.Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID || t.ID == _defaultGroupID);
                else
                    queryable = queryable.Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.ID != _defaultGroupID);
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    queryable = queryable.Where(t => t.Name.Contains(filter.Name));
                }
                if (!string.IsNullOrEmpty(filter.Code))
                {
                    queryable = queryable.Where(t => t.Code.Contains(filter.Code));
                }
            }
            totalCount = queryable.Count();
            if (filter != null && filter.Start.HasValue && filter.Limit.HasValue)
            {
                queryable = queryable.OrderBy(t => t.Index).Skip(filter.Start.Value).Take(filter.Limit.Value);
            }
            result = queryable.ToList();
            return result;
        }

        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public IEnumerable<MemberGroupTreeDto> GetTreeData(Guid? parentID)
        {
            var commenMemberID = Guid.Parse("00000000-0000-0000-0000-000000000000");
            var deptRegions = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID || t.MerchantID == commenMemberID)
                .MapTo<MemberGroup, MemberGroupTreeDto>()
                .OrderBy(t => t.ParentId).ThenBy(t => t.Index)
                .ToList();

            //var groups = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID || t.ID == _defaultGroupID)
            //    .OrderBy(t => t.Index)
            //    .Select(t => new IDCodeNameDto
            //    {
            //        ID = t.ID,
            //        Code = t.Code,
            //        Name = t.Name,
            //    }).ToList();
            //var groupMember = MemberRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
            //    .GroupBy(t => t.MemberGroupID)
            //    .Select(group => new
            //    {
            //        GroupID = group.Key,
            //        Count = group.Count()
            //    }).ToDictionary(t => t.GroupID.GetValueOrDefault());
            //foreach (var group in groups)
            //{
            //    if (groupMember.ContainsKey(group.ID))
            //    {
            //        group.Name = $"{group.Name}({groupMember[group.ID].Count})";
            //    }
            //    else
            //    {
            //        group.Name = $"{group.Name}(0)";
            //    }
            //}
            //groups.Insert(0, new IDCodeNameDto
            //{
            //    ID = Guid.Empty,
            //    Code = "ALL",
            //    Name = $"全部会员({groupMember.Sum(g => g.Value.Count)})",
            //});
            return BuildTree(deptRegions, null);
        }

        IEnumerable<MemberGroupTreeDto> BuildTree(IList<MemberGroupTreeDto> sources, Guid? parentID)
        {
            var children = sources.Where(t => t.ParentId == parentID).ToList();
            foreach (var child in children)
            {
                child.Children = BuildTree(sources, child.ID);
                child.MemberNumbers = MemberRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.MemberGroupID == child.ID).Count();
                child.Children.ForEach(t => child.MemberNumbers += t.MemberNumbers);
                child.leaf = child.Children.Count() == 0;
                child.expanded = false;
            }
            return children;
        }

        public IEnumerable<MemberGroupTreeDto> GetTreeDataContainsMember(MemberGroupFilter filter)
        {
            var result = new List<MemberGroupTreeDto>();
            var memberQueryable = MemberRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
                .Where(t => t.Card.Status != Domain.Enums.ECardStatus.Lost && t.Card.ExpiredDate != null ? t.Card.ExpiredDate >= DateTime.Now : true);
            if (filter != null && !string.IsNullOrEmpty(filter.CardNumberOrName))
            {
                memberQueryable = memberQueryable.Where(t => t.CardNumber.Contains(filter.CardNumberOrName) || t.Name.Contains(filter.CardNumberOrName));
            }
            var memberData = memberQueryable.Select(t => new
            {
                t.ID,
                t.MemberGroupID,
                t.Name,
                t.CardNumber
            }).ToList();
            var memberGroups = Repository.GetQueryable(false).OrderBy(t => t.Index);
            memberGroups.ForEach(t =>
            {
                var members = memberData.Where(m => m.MemberGroupID == t.ID).ToList();
                var count = members.Count;
                result.Add(new MemberGroupTreeDto()
                {
                    ID = t.ID,
                    Text = t.Name + $"({count})",
                    ParentId = null,
                    expanded = true,
                    Index = t.Index,
                    Children = members.Select(c => new MemberGroupTreeDto
                    {
                        ID = c.ID,
                        Text = string.IsNullOrEmpty(c.Name) ? c.CardNumber : c.Name + $"({c.CardNumber})",
                        ParentId = t.ID,
                        expanded = true,
                    }).ToArray()
                });
            });
            return result;
        }

        /// <summary>
        /// 获取精简结构数据
        /// </summary>
        /// <returns></returns>
        public IList<IDCodeNameDto> GetLiteData()
        {
            var categories = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
                .OrderBy(t => t.Index)
                .Select(t => new IDCodeNameDto
                {
                    ID = t.ID,
                    Code = t.Code,
                    Name = t.Name
                }).ToList();
            return categories;
        }
    }
}
