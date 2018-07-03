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

namespace VVCar.BaseData.Services.DomainServices
{
    /// <summary>
    /// 代理商门店领域服务
    /// </summary>
    public class AgentDepartmentService : DomainServiceBase<IRepository<AgentDpartment>, AgentDpartment, Guid>, IAgentDepartmentService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AgentDepartmentService()
        {
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override AgentDpartment Add(AgentDpartment entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.IsHQ = false;
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.MerchantID = Guid.Parse("00000000-0000-0000-0000-000000000001");
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
            entity.LastUpdatedDate = DateTime.Now;
            entity.LastUpdatedUserID = AppContext.CurrentSession.UserID;
            entity.LastUpdatedUser = AppContext.CurrentSession.UserName;
            return Repository.Update(entity) > 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(AgentDpartment entity)
        {
            if (entity == null)
                return false;
            var agentDpartment = Repository.GetByKey(entity.ID);
            if (agentDpartment == null)
                return false;
            agentDpartment.Name = entity.Name;
            agentDpartment.LegalPerson = entity.LegalPerson;
            agentDpartment.IDNumber = entity.IDNumber;
            agentDpartment.Email = entity.Email;
            agentDpartment.WeChatOAPassword = entity.WeChatOAPassword;
            agentDpartment.MobilePhoneNo = entity.MobilePhoneNo;
            agentDpartment.BusinessLicenseImgUrl = entity.BusinessLicenseImgUrl;
            agentDpartment.LegalPersonIDCardFrontImgUrl = entity.LegalPersonIDCardFrontImgUrl;
            agentDpartment.LegalPersonIDCardBehindImgUrl = entity.LegalPersonIDCardBehindImgUrl;
            agentDpartment.CompanyAddress = entity.CompanyAddress;
            agentDpartment.WeChatAppID = entity.WeChatAppID;
            agentDpartment.WeChatAppSecret = entity.WeChatAppSecret;
            agentDpartment.MeChatMchPassword = entity.MeChatMchPassword;
            agentDpartment.WeChatMchID = entity.WeChatMchID;
            agentDpartment.WeChatMchKey = entity.WeChatMchKey;
            agentDpartment.Bank = entity.Bank;
            agentDpartment.ApproveStatus = entity.ApproveStatus;
            agentDpartment.UserID = entity.UserID;
            agentDpartment.BankCard = entity.BankCard;
            agentDpartment.LastUpdatedDate = DateTime.Now;
            agentDpartment.LastUpdatedUserID = AppContext.CurrentSession.UserID;
            agentDpartment.LastUpdatedUser = AppContext.CurrentSession.UserName;
            return Repository.Update(agentDpartment) > 0;
        }

        /// <summary>
        /// 审核代理商门店
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool ApproveAgentDepartment(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数不正确");
            var agentDepartmentList = this.Repository.GetQueryable(true).Where(t => ids.Contains(t.ID)).ToList();
            if (agentDepartmentList == null || agentDepartmentList.Count() < 1)
                throw new DomainException("数据不存在");
            UnitOfWork.BeginTransaction();
            try
            {
                agentDepartmentList.ForEach(t =>
                {
                    t.ApproveStatus = Domain.Enums.EAgentDepartmentApproveStatus.Approved;
                });
                this.Repository.UpdateRange(agentDepartmentList);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<AgentDepartmentDto> Search(AgentDepartmentFilter filter, out int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.User, false);
            if (filter.ApproveStatus.HasValue)
                queryable = queryable.Where(t => t.ApproveStatus == filter.ApproveStatus.Value);
            if (filter.ID.HasValue)
                queryable = queryable.Where(t => t.ID == filter.ID.Value);
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<AgentDepartmentDto>().ToArray();
        }
    }
}
