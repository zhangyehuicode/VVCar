using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Enums;
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
            if (agentDpartment.ApproveStatus != EAgentDepartmentApproveStatus.Pedding)
                throw new DomainException("已通过审核或导入的不能修改");
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
            agentDpartment.DataSource = entity.DataSource;
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
            var approvedAndIportedData = Repository.GetQueryable(false)
                .Where(t => ids.Contains(t.ID) && (t.ApproveStatus == EAgentDepartmentApproveStatus.Approved || t.ApproveStatus == EAgentDepartmentApproveStatus.Imported)).Select(t => t.ID).ToList();
            if (approvedAndIportedData.Count > 0)
            {
                agentDepartmentList.RemoveAll(t => approvedAndIportedData.Contains(t.ID));
            }
            if (agentDepartmentList.Count < 1)
                return true;
            agentDepartmentList.ForEach(t =>
            {
                t.ApproveStatus = EAgentDepartmentApproveStatus.Approved;
            });
            return Repository.UpdateRange(agentDepartmentList) > 0;
        }

        /// <summary>
        /// 反审核代理商门店
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool RejectAgentDepartment(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数不正确");
            var agentDepartmentList = this.Repository.GetQueryable(true).Where(t => ids.Contains(t.ID)).ToList();
            if (agentDepartmentList == null || agentDepartmentList.Count() < 1)
                throw new DomainException("数据不存在");
            var peddingAndImportedData = Repository.GetQueryable(false)
                .Where(t => ids.Contains(t.ID) && (t.ApproveStatus == EAgentDepartmentApproveStatus.Pedding || t.ApproveStatus == EAgentDepartmentApproveStatus.Imported)).Select(t => t.ID).ToList();
            if (peddingAndImportedData.Count > 0)
            {
                agentDepartmentList.RemoveAll(t => peddingAndImportedData.Contains(t.ID));
            }
            if (agentDepartmentList.Count < 1)
                return true;
            agentDepartmentList.ForEach(t =>
            {
                t.ApproveStatus = EAgentDepartmentApproveStatus.Pedding;
            });
            return Repository.UpdateRange(agentDepartmentList) > 0;
        }


        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool ImportAgentDepartment(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数不正确");
            var agentDepartmentList = this.Repository.GetQueryable(true).Where(t => ids.Contains(t.ID)).ToList();
            if (agentDepartmentList == null || agentDepartmentList.Count() < 1)
                throw new DomainException("数据不存在");
            var peddingAndImportedData = Repository.GetQueryable(false)
                .Where(t => ids.Contains(t.ID) && (t.ApproveStatus == EAgentDepartmentApproveStatus.Pedding || t.ApproveStatus == EAgentDepartmentApproveStatus.Imported)).Select(t => t.ID).ToList();
            if (peddingAndImportedData.Count > 0)
            {
                agentDepartmentList.RemoveAll(t => peddingAndImportedData.Contains(t.ID));
            }
            if (agentDepartmentList.Count < 1)
                return true;
            agentDepartmentList.ForEach(t =>
            {
                t.ApproveStatus = EAgentDepartmentApproveStatus.Imported;
            });
            return Repository.UpdateRange(agentDepartmentList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<AgentDepartmentDto> Search(AgentDepartmentFilter filter, out int totalCount)
        {
            var queryable = Repository.GetIncludes(false, "Merchant", "User");
            if (!(AppContext.CurrentSession.MerchantID == Guid.Parse("00000000-0000-0000-0000-000000000001")))
            {
                queryable = queryable.Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            }
            if (filter.ApproveStatus.HasValue)
                queryable = queryable.Where(t => t.ApproveStatus == filter.ApproveStatus.Value);
            if (!string.IsNullOrEmpty(filter.MerchantName))
                queryable = queryable.Where(t => t.Merchant.Name.Contains(filter.MerchantName));
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<AgentDepartmentDto>().ToArray();
        }
    }
}
