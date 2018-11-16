using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using VVCar.Shop.Domain.Entities;
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
    /// 发起拼单记录领域服务
    /// </summary>
    public class MerchantCrowdOrderRecordService : DomainServiceBase<IRepository<MerchantCrowdOrderRecord>, MerchantCrowdOrderRecord, Guid>, IMerchantCrowdOrderRecordService
    {
        public MerchantCrowdOrderRecordService()
        {
        }

        #region properties
        IRepository<MerchantCrowdOrderRecordItem> MerchantCrowdOrderRecordItemRepo { get => UnitOfWork.GetRepository<IRepository<MerchantCrowdOrderRecordItem>>(); }

        ISystemSettingService SystemSettingService { get => ServiceLocator.Instance.GetService<ISystemSettingService>(); }

        IWeChatService WeChatService { get => ServiceLocator.Instance.GetService<IWeChatService>(); }

        IRepository<Order> OrderRepo { get => UnitOfWork.GetRepository<IRepository<Order>>(); }

        IRepository<Member> MemberRepo { get => UnitOfWork.GetRepository<IRepository<Member>>(); }

        IRepository<Product> ProductRepo { get => UnitOfWork.GetRepository<IRepository<Product>>(); }

        IRepository<MakeCodeRule> MakeCodeRuleRepo { get => UnitOfWork.GetRepository<IRepository<MakeCodeRule>>(); }

        IRepository<MerchantCrowdOrder> MerchantCrowdOrderRepo { get => UnitOfWork.GetRepository<IRepository<MerchantCrowdOrder>>(); }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override MerchantCrowdOrderRecord Add(MerchantCrowdOrderRecord entity)
        {
            if (entity == null || entity.MerchantCrowdOrderRecordItemList == null || entity.MerchantCrowdOrderRecordItemList.Count < 1)
                return null;
            entity.ID = Util.NewID();
            if (string.IsNullOrEmpty(entity.Code))
                entity.Code = GetCode();
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            entity.JoinPeople = entity.MerchantCrowdOrderRecordItemList.Count();
            var merchantCrowdOrder = MerchantCrowdOrderRepo.GetByKey(entity.MerchantCrowdOrderID);
            entity.MerchantCrowdOrderRecordItemList.ForEach(t =>
            {
                t.ID = Util.NewID();
                var member = MemberRepo.GetByKey(entity.MemberID);
                if (member != null)
                    t.MemberName = member.Name;
                t.MerchantCrowdOrderRecordID = entity.ID;
                t.CreatedDate = DateTime.Now;
                t.MerchantID = entity.MerchantID;
            });
            var result = base.Add(entity);

            if (entity.JoinPeople == merchantCrowdOrder.PeopleCount)
            {
                var memberIDs = MerchantCrowdOrderRecordItemRepo.GetQueryable(false).Where(t => t.MerchantCrowdOrderRecordID == result.ID).Select(t => t.MemberID).Distinct().ToArray();
                JoinCrowdOrderSuccessNotify(merchantCrowdOrder, result, memberIDs);
            }

            return result;
        }

        string GetCode()
        {
            var newCode = string.Empty;
            var existCode = false;
            var makeCodeRuleService = ServiceLocator.Instance.GetService<IMakeCodeRuleService>();
            var entity = Repository.GetQueryable(false).OrderByDescending(t => t.CreatedDate).FirstOrDefault();
            if (entity != null && entity.CreatedDate.Date != DateTime.Now.Date)
            {
                var rule = MakeCodeRuleRepo.GetQueryable().Where(t => t.Code == "MerchantCrowdOrder" && t.IsAvailable).FirstOrDefault();
                if (rule != null)
                {
                    rule.CurrentValue = 0;
                    MakeCodeRuleRepo.Update(rule);
                }
            }
            do
            {
                newCode = makeCodeRuleService.GenerateCode("MerchantCrowdOrder", DateTime.Now);
                existCode = Repository.Exists(t => t.Code == newCode);
            } while (existCode);
            return newCode;
        }

        /// <summary>
        /// 新增拼单子项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public MerchantCrowdOrderRecordDto AddMerchantCrowdOrderRecordItem(MerchantCrowdOrderRecordItem entity)
        {
            if (entity == null)
                return null;
            var merchantCrowdOrderRecord = Repository.GetByKey(entity.MerchantCrowdOrderRecordID);
            var merchantCrowdOrder = MerchantCrowdOrderRepo.GetByKey(merchantCrowdOrderRecord.MerchantCrowdOrderID);
            UnitOfWork.BeginTransaction();
            try {
                merchantCrowdOrderRecord.JoinPeople += 1;
                if (merchantCrowdOrderRecord.JoinPeople > merchantCrowdOrder.PeopleCount)
                    throw new DomainException("拼单人数已满");
                entity.ID = Util.NewID();
                var member = MemberRepo.GetByKey(entity.MemberID);
                if (member != null)
                    entity.MemberName = member.Name;
                entity.CreatedDate = DateTime.Now;
                entity.MerchantID = AppContext.CurrentSession.MerchantID;
                MerchantCrowdOrderRecordItemRepo.Add(entity);
                UnitOfWork.CommitTransaction();
                //通知
                var result = merchantCrowdOrderRecord.MapTo<MerchantCrowdOrderRecordDto>();
                if (merchantCrowdOrderRecord.JoinPeople == result.PeopleCount)
                {
                    var memberIDs = MerchantCrowdOrderRecordItemRepo.GetQueryable(false).Where(t => t.MerchantCrowdOrderRecordID == merchantCrowdOrderRecord.ID).Select(t => t.MemberID).Distinct().ToArray();
                    JoinCrowdOrderSuccessNotify(merchantCrowdOrder, merchantCrowdOrderRecord, memberIDs);
                }
                return result;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException("加入拼单出现异常："+ e.Message);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<MerchantCrowdOrderRecordDto> Search(MerchantCrowdOrderRecordFilter filter, out int totalCount)
        {
            var queryable = Repository.GetIncludes(false, "MerchantCrowdOrder", "MerchantCrowdOrder.Product", "MerchantCrowdOrderRecordItemList");
            if (filter.ID.HasValue)
            {
                queryable = queryable.Where(t => t.ID == filter.ID.Value);
            }
            if (filter.MemberID.HasValue)
            {
                queryable = queryable.Where(t => t.MerchantCrowdOrderRecordItemList.Select(item => item.MemberID).Contains(filter.MemberID.Value));
            }
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
            {
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            }
            var result = queryable.MapTo<MerchantCrowdOrderRecordDto>().ToArray();
            result.ForEach(t =>
            {
                t.IsCanBuy = t.JoinPeople >= t.PeopleCount;
                if (filter.MemberID.HasValue)
                    t.IsOrdered = OrderRepo.Exists(o => o.MerchantCrowdOrderRecordID == t.ID && o.MemberID == filter.MemberID);
            });
            return result.OrderByDescending(t=>t.CreatedDate);
        }

        /// <summary>
        /// 加入拼单
        /// </summary>
        /// <param name="merchantCrowdOrderRecordID"></param>
        /// <param name="MemberID"></param>
        /// <returns></returns>
        public bool JoinMerchantCrowdOrderRecord(Guid merchantCrowdOrderRecordID, Guid MemberID)
        {
            if (merchantCrowdOrderRecordID == null || MemberID == null)
            {
                AppContext.Logger.Error("JoinMerchantCrowdOrderRecord:加入拼单，参数错误");
                return false;
            }
            var merchantCrowdOrderRecord = Repository.GetIncludes(false, "Member", "MerchantCrowdOrder", "MerchantCrowdOrder.Product").FirstOrDefault(t => t.ID == merchantCrowdOrderRecordID);
            if (merchantCrowdOrderRecord == null)
            {
                AppContext.Logger.Error("JoinMerchantCrowdOrderRecord:加入拼单，拼单记录不存在");
                return false;
            }
            UnitOfWork.BeginTransaction();
            try
            {
                MerchantCrowdOrderRecordItemRepo.Add(new MerchantCrowdOrderRecordItem
                {
                    ID = Util.NewID(),
                    MerchantCrowdOrderRecordID = merchantCrowdOrderRecordID,
                    MemberID = MemberID,
                    CreatedDate = DateTime.Now,
                    MerchantID = AppContext.CurrentSession.MerchantID,
                });
                merchantCrowdOrderRecord.JoinPeople += 1;
                Repository.Update(merchantCrowdOrderRecord);
                UnitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                AppContext.Logger.Error($"JoinCrowdOrderRecord:加入拼单出现异常，{e.Message}");
                return false;
            }
            if (merchantCrowdOrderRecord.JoinPeople >= merchantCrowdOrderRecord.MerchantCrowdOrder.PeopleCount)
            {
                MerchantCrowdOrderSuccessNotify(merchantCrowdOrderRecord);
            }
            return true;
        }

        /// <summary>
        /// 发送拼单成功提醒
        /// </summary>
        /// <param name="merchantCrowdOrderRecord"></param>
        void MerchantCrowdOrderSuccessNotify(MerchantCrowdOrderRecord merchantCrowdOrderRecord)
        {
            try
            {
                if (merchantCrowdOrderRecord == null || merchantCrowdOrderRecord.Member == null || string.IsNullOrEmpty(merchantCrowdOrderRecord.Member.WeChatOpenID))
                    return;
                var templateId = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_CrowdOrderSuccess);
                if (string.IsNullOrEmpty(templateId))
                    return;
                var message = new WeChatTemplateMessageDto
                {
                    touser = merchantCrowdOrderRecord.Member.WeChatOpenID,
                    template_id = templateId,
                    url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/MyMerchantCrowdOrderDetails?mch={AppContext.CurrentSession.MerchantCode}&crowdid={merchantCrowdOrderRecord.ID}",
                    data = new System.Dynamic.ExpandoObject(),
                };
                message.data.first = new WeChatTemplateMessageDto.MessageData("拼单成功");
                message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(merchantCrowdOrderRecord.MerchantCrowdOrder.Product.Name);
                message.data.keyword2 = new WeChatTemplateMessageDto.MessageData("");
                message.data.keyword3 = new WeChatTemplateMessageDto.MessageData(merchantCrowdOrderRecord.MerchantCrowdOrder.CreatedDate.ToString("yyyy-MM-dd"));
                message.data.remark = new WeChatTemplateMessageDto.MessageData("立即下单");
                WeChatService.SendWeChatNotifyAsync(message);
            }
            catch (Exception e)
            {
                AppContext.Logger.Error($"发送拼单成功提醒异常，{e.Message}");
            }
        }

        void JoinCrowdOrderSuccessNotify(MerchantCrowdOrder merchantCrowdOrder, MerchantCrowdOrderRecord merchantCrowdOrderRecord, Guid[] memberIDs)
        {
            try
            {
                var templateId = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_CrowdOrderSuccess);
                if (string.IsNullOrEmpty(templateId))
                    return;
                var message = new WeChatTemplateMessageDto
                {
                    touser = "",
                    template_id = templateId,
                    url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/MyMerchantCrowdOrderDetails?mch={AppContext.CurrentSession.MerchantCode}&crowdid={merchantCrowdOrderRecord.ID}",
                    data = new System.Dynamic.ExpandoObject(),
                };
                var product = ProductRepo.GetByKey(merchantCrowdOrder.ProductID);
                message.data.first = new WeChatTemplateMessageDto.MessageData("拼单成功");
                message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(product.Name);
                message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(merchantCrowdOrderRecord.Code);
                message.data.keyword3 = new WeChatTemplateMessageDto.MessageData(DateTime.Now.ToString("yyyy-MM-dd"));
                message.data.remark = new WeChatTemplateMessageDto.MessageData("立即下单");
                var memberList = MemberRepo.GetQueryable(false).Where(t => memberIDs.Contains(t.ID)).ToList();
                memberList.ForEach(t =>
                {
                    message.touser = t.WeChatOpenID;
                    WeChatService.SendWeChatNotify(message);
                });
            }
            catch(Exception e)
            {
                AppContext.Logger.Error($"加入拼单提醒异常，{e.Message}");
            }
        }
    }
}
