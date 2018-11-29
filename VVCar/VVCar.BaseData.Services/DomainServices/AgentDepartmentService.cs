using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Enums;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Services;
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

        IMemberService MemberService { get => ServiceLocator.Instance.GetService<IMemberService>(); }

        IWeChatService WeChatService { get => ServiceLocator.Instance.GetService<IWeChatService>(); }

        IMerchantService MerchantService { get => ServiceLocator.Instance.GetService<IMerchantService>(); }

        IRepository<Member> MemberRepo { get => ServiceLocator.Instance.GetService<IRepository<Member>>(); }

        IRepository<User> UserRepo { get => ServiceLocator.Instance.GetService<IRepository<User>>(); }

        ISystemSettingService SystemSettingService { get => ServiceLocator.Instance.GetService<ISystemSettingService>(); }

        #endregion

        protected override bool DoValidate(AgentDepartment entity)
        {
            if (entity == null)
                return false;
            var exist = Repository.Exists(t => t.MobilePhoneNo == entity.MobilePhoneNo && t.ID != entity.ID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (exist)
                throw new DomainException("相同手机号码的客户信息已存在");
            return true;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override AgentDepartment Add(AgentDepartment entity)
        {
            if (entity == null)
                return null;
            UnitOfWork.BeginTransaction();
            try
            {
                if (entity.AgentDepartmentCategoryID == Guid.Parse("00000000-0000-0000-0000-000000000001"))
                    entity.AgentDepartmentCategoryID = null;
                entity.ID = Util.NewID();
                entity.IsHQ = false;
                entity.CreatedDate = DateTime.Now;
                entity.CreatedUserID = AppContext.CurrentSession.UserID;
                entity.CreatedUser = AppContext.CurrentSession.UserName;
                entity.MerchantID = AppContext.CurrentSession.MerchantID;
                var result = base.Add(entity);
                if (entity.Type == EAgentDepartmentType.Development)
                {
                    MemberRegister(new MemberRegisterDto
                    {
                        MobilePhoneNo = entity.MobilePhoneNo,
                        Name = entity.LegalPerson,
                        Password = entity.MobilePhoneNo,
                        DepartmentName = entity.Name,
                        DepartmentAddress = entity.CompanyAddress,
                        AgentDepartmentID = entity.ID,
                    });
                }
                UnitOfWork.CommitTransaction();
                SendWeChatNotify(entity);
                SendOrderWeChatNotify(entity);
                return result;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
        }

        /// <summary>
        /// 发送开发门店成功给代理商
        /// </summary>
        /// <param name="agentDepartment"></param>
        private void SendWeChatNotify(AgentDepartment agentDepartment)
        {
            if (agentDepartment == null)
                return;
            var user = UserRepo.GetByKey(agentDepartment.CreatedUserID);
            if (user.OpenID == null || user.OpenID == "")
                return;
            var message = new WeChatTemplateMessageDto
            {
                touser = user.OpenID,
                template_id = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_Announcement),
                data = new System.Dynamic.ExpandoObject(),
            };      

            var remark = "已通知总部联系门店对接";

            message.data.first = new WeChatTemplateMessageDto.MessageData("您已成功提交门店信息给总部");
            message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(agentDepartment.Name);
            message.data.keyword2 = new WeChatTemplateMessageDto.MessageData("总部审核");
            message.data.remark = new WeChatTemplateMessageDto.MessageData(remark);
            WeChatService.SendWeChatNotifyAsync(message);
        }

        /// <summary>
        /// 发送新订单提醒给总部
        /// </summary>
        /// <param name="agentDepartment"></param>
        private void SendOrderWeChatNotify(AgentDepartment agentDepartment)
        {
            if (agentDepartment == null)
                return;
            var message = new WeChatTemplateMessageDto
            {
                touser = "oI4ee0pN20eepDVJHh_UlD_oH_Ew",
                template_id = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_Announcement),
                data = new System.Dynamic.ExpandoObject(),
            };

            message.data.first = new WeChatTemplateMessageDto.MessageData("您有新门店需要对接，请及时处理");
            message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(agentDepartment.Name);
            message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(agentDepartment.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"));
            message.data.remark = new WeChatTemplateMessageDto.MessageData($"在管理后台查看商户审核，请及时对接软件");
            
            WeChatService.SendWeChatNotify(message);
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

        private void MemberRegister(MemberRegisterDto registerDto)
        {
            MemberService.Register(registerDto);
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
            UnitOfWork.BeginTransaction();
            try
            {
                entity.IsDeleted = true;
                entity.LastUpdatedDate = DateTime.Now;
                entity.LastUpdatedUserID = AppContext.CurrentSession.UserID;
                entity.LastUpdatedUser = AppContext.CurrentSession.UserName;
                Repository.Update(entity);
                var member = MemberRepo.GetQueryable().Where(t => t.AgentDepartmentID == key).FirstOrDefault();
                if (member != null)
                {
                    member.IsDeleted = true;
                    MemberRepo.Update(member);
                }
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
            UnitOfWork.BeginTransaction();
            try
            {
                agentDepartment.Name = entity.Name;
                if (entity.AgentDepartmentCategoryID != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    agentDepartment.AgentDepartmentCategoryID = entity.AgentDepartmentCategoryID;
                }
                else
                {
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
                Repository.Update(agentDepartment);
                if (entity.Type == EAgentDepartmentType.Development)
                {
                    MemberRegister(new MemberRegisterDto
                    {
                        MobilePhoneNo = entity.MobilePhoneNo,
                        Name = entity.LegalPerson,
                        Password = entity.MobilePhoneNo,
                        DepartmentName = entity.Name,
                        DepartmentAddress = entity.CompanyAddress,
                        AgentDepartmentID = entity.ID,
                    });
                }
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

            UnitOfWork.BeginTransaction();
            try
            {
                agentDepartmentList.ForEach(t =>
                {
                    t.ApproveStatus = EAgentDepartmentApproveStatus.Imported;
                    t.LastUpdatedUserID = AppContext.CurrentSession.UserID;
                    t.LastUpdatedUser = AppContext.CurrentSession.UserName;
                    t.LastUpdatedDate = DateTime.Now;
                    var merchant = new Merchant();
                    merchant.Name = t.Name;
                    merchant.LegalPerson = t.LegalPerson;
                    merchant.IDNumber = t.IDNumber;
                    merchant.Email = t.Email;
                    merchant.WeChatOAPassword = t.WeChatOAPassword;
                    merchant.MobilePhoneNo = t.MobilePhoneNo;
                    merchant.BusinessLicenseImgUrl = t.BusinessLicenseImgUrl;
                    merchant.DepartmentImgUrl = t.DepartmentImgUrl;
                    merchant.LegalPersonIDCardFrontImgUrl = t.LegalPersonIDCardFrontImgUrl;
                    merchant.LegalPersonIDCardBehindImgUrl = t.LegalPersonIDCardBehindImgUrl;
                    merchant.CompanyAddress = t.CompanyAddress;
                    merchant.WeChatAppSecret = t.WeChatAppSecret;
                    merchant.WeChatMchID = t.WeChatMchID;
                    merchant.WeChatMchKey = t.WeChatMchKey;
                    merchant.MeChatMchPassword = t.MeChatMchPassword;
                    merchant.Bank = t.Bank;
                    merchant.BankCard = t.BankCard;
                    merchant.MerchantID = t.MerchantID;
                    merchant.Longitude = t.Longitude;
                    merchant.Latitude = t.Latitude;
                    merchant.LocationName = t.LocationName;
                    MerchantService.Add(merchant);
                    Repository.Update(t);
                });
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
            var queryable = Repository.GetIncludes(false, "Merchant", "User");
            if (!(AppContext.CurrentSession.MerchantID == Guid.Parse("00000000-0000-0000-0000-000000000001")))
            {
                queryable = queryable.Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.CreatedUserID == AppContext.CurrentSession.UserID);
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
            if (AppContext.CurrentSession.MerchantID == Guid.Parse("00000000-0000-0000-0000-000000000001") && AppContext.CurrentSession.UserID != Guid.Parse("00000000-0000-0000-0000-000000000001"))
            {
                queryable = queryable.Where(t => t.CreatedUserID == AppContext.CurrentSession.UserID || t.UserID == AppContext.CurrentSession.UserID);
            }
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderByDescending(t => t.CreatedDate).MapTo<AgentDepartmentDto>().ToArray();
        }
    }
}
