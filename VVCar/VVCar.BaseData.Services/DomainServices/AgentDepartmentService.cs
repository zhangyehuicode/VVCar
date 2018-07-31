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
    public class AgentDepartmentService : DomainServiceBase<IRepository<AgentDepartment>, AgentDepartment, Guid>, IAgentDepartmentService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AgentDepartmentService()
        {
        }

        #region properties

        IAgentDepartmentTagService AgentDepartmentTagService { get => ServiceLocator.Instance.GetService<IAgentDepartmentTagService>(); }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override AgentDepartment Add(AgentDepartment entity)
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
        /// 新增带标签
        /// </summary>
        /// <param name="agentDepartmentDto"></param>
        /// <returns></returns>
        public AgentDepartment AddWidthTag(AgentDepartmentDto agentDepartmentDto)
        {
            if (agentDepartmentDto == null)
                return null;
            var agentDepartment = agentDepartmentDto.MapTo<AgentDepartment>();
            if (agentDepartment == null)
                return null;
            UnitOfWork.BeginTransaction();
            try
            {
                var result = Add(agentDepartment);
                if (agentDepartmentDto.TagList != null && agentDepartmentDto.TagList.Count > 0)
                {
                    var agentDepartmentTagList = new List<AgentDepartmentTag>();
                    agentDepartmentDto.TagList.ForEach(t =>
                    {
                        agentDepartmentTagList.Add(new AgentDepartmentTag
                        {
                            AgentDepartmentID = result.ID,
                            TagID = t.ID,
                        });
                    });
                    AgentDepartmentTagService.BatchAdd(agentDepartmentTagList);
                }
                UnitOfWork.CommitTransaction();
                return result;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
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
        public override bool Update(AgentDepartment entity)
        {
            if (entity == null)
                return false;
            var agentDepartment = Repository.GetByKey(entity.ID);
            if (agentDepartment == null)
                return false;
            if (agentDepartment.ApproveStatus != EAgentDepartmentApproveStatus.Pedding)
                throw new DomainException("已通过审核或导入的不能修改");
            agentDepartment.Name = entity.Name;
            if (entity.AgentDepartmentCategoryID != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                agentDepartment.AgentDepartmentCategoryID = entity.AgentDepartmentCategoryID;
            }
            else {
                agentDepartment.AgentDepartmentCategoryID = null;
            }
            agentDepartment.LegalPerson = entity.LegalPerson;
            agentDepartment.IDNumber = entity.IDNumber;
            agentDepartment.Email = entity.Email;
            agentDepartment.WeChatOAPassword = entity.WeChatOAPassword;
            agentDepartment.MobilePhoneNo = entity.MobilePhoneNo;
            agentDepartment.BusinessLicenseImgUrl = entity.BusinessLicenseImgUrl;
            agentDepartment.LegalPersonIDCardFrontImgUrl = entity.LegalPersonIDCardFrontImgUrl;
            agentDepartment.LegalPersonIDCardBehindImgUrl = entity.LegalPersonIDCardBehindImgUrl;
            agentDepartment.DepartmentImgUrl = entity.DepartmentImgUrl;
            agentDepartment.CompanyAddress = entity.CompanyAddress;
            agentDepartment.WeChatAppID = entity.WeChatAppID;
            agentDepartment.WeChatAppSecret = entity.WeChatAppSecret;
            agentDepartment.MeChatMchPassword = entity.MeChatMchPassword;
            agentDepartment.WeChatMchID = entity.WeChatMchID;
            agentDepartment.WeChatMchKey = entity.WeChatMchKey;
            agentDepartment.Bank = entity.Bank;
            agentDepartment.ApproveStatus = entity.ApproveStatus;
            agentDepartment.Type = entity.Type;
            agentDepartment.UserID = entity.UserID;
            agentDepartment.BankCard = entity.BankCard;
            agentDepartment.DataSource = entity.DataSource;
            agentDepartment.LastUpdatedDate = DateTime.Now;
            agentDepartment.LastUpdatedUserID = AppContext.CurrentSession.UserID;
            agentDepartment.LastUpdatedUser = AppContext.CurrentSession.UserName;
            return Repository.Update(agentDepartment) > 0;
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
                t.LastUpdatedUserID = AppContext.CurrentSession.UserID;
                t.LastUpdatedUser = AppContext.CurrentSession.UserName;
                t.LastUpdatedDate = DateTime.Now;
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
                t.LastUpdatedUserID = AppContext.CurrentSession.UserID;
                t.LastUpdatedUser = AppContext.CurrentSession.UserName;
                t.LastUpdatedDate = DateTime.Now;
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
                t.LastUpdatedUserID = AppContext.CurrentSession.UserID;
                t.LastUpdatedUser = AppContext.CurrentSession.UserName;
                t.LastUpdatedDate = DateTime.Now;
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
            if (filter.AgentDepartmentCategoryID.HasValue && filter.AgentDepartmentCategoryID.Value != Guid.Parse("00000000-0000-0000-0000-000000000001"))
                queryable = queryable.Where(t => t.AgentDepartmentCategoryID == filter.AgentDepartmentCategoryID.Value);
            if (filter.Type.HasValue)
                queryable = queryable.Where(t => t.Type == filter.Type.Value);
            if (filter.CreatedDate.HasValue)
            {
                var startday = filter.CreatedDate.Value.Date;
                var nextday = startday.AddDays(1);
                queryable = queryable.Where(t => t.CreatedDate >= startday && t.CreatedDate < nextday);
            }
            if (filter.AgentDepartmentID.HasValue)
                queryable = queryable.Where(t => t.ID == filter.AgentDepartmentID.Value);
            if (filter.UserID.HasValue)
                queryable = queryable.Where(t => t.UserID == filter.UserID.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderByDescending(t => t.CreatedDate).MapTo<AgentDepartmentDto>().ToArray();
        }
    }
}
