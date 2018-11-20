using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// <summary>
    /// 业务报销服务
    /// </summary>
    public partial class ReimbursementService : DomainServiceBase<IRepository<Reimbursement>, Reimbursement, Guid>, IReimbursementService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ReimbursementService()
        {
        }

        IRepository<MakeCodeRule> MakeCodeRuleRepo { get => UnitOfWork.GetRepository<IRepository<MakeCodeRule>>(); }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override Reimbursement Add(Reimbursement entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            if (string.IsNullOrEmpty(entity.Code))
                entity.Code = GetCode();
            if (entity.UserID == null)
                entity.UserID = AppContext.CurrentSession.UserID;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        string GetCode()
        {
            var newCode = string.Empty;
            var existCode = false;
            var makeCodeRuleService = ServiceLocator.Instance.GetService<IMakeCodeRuleService>();
            var entity = Repository.GetQueryable(false).OrderByDescending(t => t.CreatedDate).FirstOrDefault();
            if (entity != null && entity.CreatedDate.Date != DateTime.Now.Date)
            {
                var rule = MakeCodeRuleRepo.GetQueryable().Where(t => t.Code == "Reimbursement" && t.IsAvailable).FirstOrDefault();
                if (rule != null)
                {
                    rule.CurrentValue = 0;
                    MakeCodeRuleRepo.Update(rule);
                }
            }
            do
            {
                newCode = makeCodeRuleService.GenerateCode("Reimbursement", DateTime.Now);
                existCode = Repository.Exists(t => t.Code == newCode);
            } while (existCode);
            return newCode;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(Reimbursement entity)
        {
            if (entity == null)
                return false;
            var reimbursement = Repository.GetByKey(entity.ID);
            if (reimbursement == null)
                return false;
            reimbursement.UserID = entity.UserID;
            reimbursement.Project = entity.Project;
            reimbursement.Remark = entity.Remark;
            reimbursement.ImgUrl = entity.ImgUrl;
            reimbursement.MemberSource = entity.MemberSource;
            reimbursement.LastUpdateUserID = AppContext.CurrentSession.UserID;
            reimbursement.LastUpdateUser = AppContext.CurrentSession.UserName;
            reimbursement.LastUpdateDate = DateTime.Now;
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
            var reimbursementList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (reimbursementList == null || reimbursementList.Count() < 1)
                throw new DomainException("数据不存在");
            reimbursementList.ForEach(t =>
            {
                if (t.CreatedUserID != AppContext.CurrentSession.UserID)
                    throw new DomainException("不允许删除其他人的数据!");
                if (t.Status == EReimbursementApproveStatus.Approved)
                    throw new DomainException("已审核的数据不允许删除!");
                t.IsDeleted = true;
            });
            return Repository.UpdateRange(reimbursementList) > 0;
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool ApproveReimbursement(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数不正确");
            var reimbursementList = Repository.GetQueryable(true).Where(t => ids.Contains(t.ID)).ToList();
            if (reimbursementList == null || reimbursementList.Count() < 1)
                throw new DomainException("数据不存在");
            reimbursementList.ForEach(t =>
            {
                t.Status = EReimbursementApproveStatus.Approved;
                t.LastUpdateUserID = AppContext.CurrentSession.UserID;
                t.LastUpdateUser = AppContext.CurrentSession.UserName;
                t.LastUpdateDate = DateTime.Now;
            });
            return Repository.UpdateRange(reimbursementList) > 0;
        }

        /// <summary>
        /// 批量反审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool AntiApproveReimbursement(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数不正确");
            var reimbursementList = Repository.GetQueryable(true).Where(t => ids.Contains(t.ID)).ToList();
            if (reimbursementList == null || reimbursementList.Count() < 1)
                throw new DomainException("数据不存在");
            reimbursementList.ForEach(t =>
            {
                t.Status = EReimbursementApproveStatus.Pedding;
                t.LastUpdateUserID = AppContext.CurrentSession.UserID;
                t.LastUpdateUser = AppContext.CurrentSession.UserName;
                t.LastUpdateDate = DateTime.Now;
            });
            return Repository.UpdateRange(reimbursementList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<ReimbursementDto> Search(ReimbursementFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.UserID.HasValue)
                queryable = queryable.Where(t => t.UserID == filter.UserID);
            if (!string.IsNullOrEmpty(filter.Project))
                queryable = queryable.Where(t => t.Project.Contains(filter.Project));
            if (filter.Status.HasValue)
                queryable = queryable.Where(t => t.Status == filter.Status);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<ReimbursementDto>().OrderByDescending(t=>t.CreatedDate).ToArray();
        }
    }
}
