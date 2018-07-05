using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 人才招聘领域服务
    /// </summary>
    public class RecruitmemtService : DomainServiceBase<IRepository<Recruitment>, Recruitment, Guid>, IRecruitmentService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public RecruitmemtService()
        {
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override Recruitment Add(Recruitment entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
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
            var entity = Repository.GetByKey(key);
            if (entity == null)
                return false;
            entity.IsDeleted = true;
            entity.LastUpdateDate = DateTime.Now;
            entity.LastUpdateUserID = AppContext.CurrentSession.UserID;
            entity.LastUpdateUser = AppContext.CurrentSession.UserName;
            return base.Delete(key);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(Recruitment entity)
        {
            if (entity == null)
                return false;
            var recruitment = Repository.GetByKey(entity.ID);
            if (recruitment == null)
                return false;
            recruitment.Recruiter = entity.Recruiter;
            recruitment.Position = entity.Position;
            recruitment.StartDate = entity.StartDate;
            recruitment.EndDate = entity.EndDate;
            recruitment.HRName = entity.HRName;
            recruitment.HRPhoneNo = entity.HRPhoneNo;
            recruitment.DegreeType = entity.DegreeType;
            recruitment.Sex = entity.Sex;
            recruitment.WorkTime = entity.WorkTime;
            recruitment.Address = entity.Address;
            recruitment.Requirement = entity.Requirement;
            return base.Update(entity);
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
            var recruitmentList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (recruitmentList == null || recruitmentList.Count < 1)
                throw new DomainException("数据不存在");
            return Repository.DeleteRange(recruitmentList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<Recruitment> Search(RecruitmentFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetInclude(t => t.Merchant, false);
            if (!(AppContext.CurrentSession.MerchantID == Guid.Parse("00000000-0000-0000-0000-000000000001")))
            {
                queryable = queryable.Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            }
            if (!string.IsNullOrEmpty(filter.Recruiter))
                queryable = queryable.Where(t => t.Recruiter.Contains(filter.Recruiter));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}
