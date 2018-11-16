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
    /// 发起砍价记录领域服务
    /// </summary>
    public class MerchantBargainOrderRecordService : DomainServiceBase<IRepository<MerchantBargainOrderRecord>, MerchantBargainOrderRecord, Guid>, IMerchantBargainOrderRecordService
    {
        public MerchantBargainOrderRecordService()
        {
        }

        #region properties
        IRepository<MerchantBargainOrderRecordItem> MerchantBargainOrderRecordItemRepo { get => UnitOfWork.GetRepository<IRepository<MerchantBargainOrderRecordItem>>(); }

        ISystemSettingService SystemSettingService { get => ServiceLocator.Instance.GetService<ISystemSettingService>(); }

        IWeChatService WeChatService { get => ServiceLocator.Instance.GetService<IWeChatService>(); }

        IRepository<Order> OrderRepo { get => UnitOfWork.GetRepository<IRepository<Order>>(); }

        IRepository<Member> MemberRepo { get => UnitOfWork.GetRepository<IRepository<Member>>(); }

        IRepository<Product> ProductRepo { get => UnitOfWork.GetRepository<IRepository<Product>>(); }

        IRepository<MakeCodeRule> MakeCodeRuleRepo { get => UnitOfWork.GetRepository<IRepository<MakeCodeRule>>(); }

        IRepository<MerchantBargainOrder> MerchantBargainOrderRepo { get => UnitOfWork.GetRepository<IRepository<MerchantBargainOrder>>(); }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override MerchantBargainOrderRecord Add(MerchantBargainOrderRecord entity)
        {
            if (entity == null || entity.MerchantBargainOrderRecordItemList == null || entity.MerchantBargainOrderRecordItemList.Count < 1)
                return null;
            entity.ID = Util.NewID();
            if (string.IsNullOrEmpty(entity.Code))
                entity.Code = GetCode();
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            entity.JoinPeople = entity.MerchantBargainOrderRecordItemList.Count() - 1; 
            entity.MerchantBargainOrderRecordItemList.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.MerchantBargainOrderRecordID = entity.ID;
                t.CreatedDate = DateTime.Now;
                t.MerchantID = entity.MerchantID;
            });
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
                var rule = MakeCodeRuleRepo.GetQueryable().Where(t => t.Code == "MerchantBargainOrder" && t.IsAvailable).FirstOrDefault();
                if (rule != null)
                {
                    rule.CurrentValue = 0;
                    MakeCodeRuleRepo.Update(rule);
                }
            }
            do
            {
                newCode = makeCodeRuleService.GenerateCode("MerchantBargainOrder", DateTime.Now);
                existCode = Repository.Exists(t => t.Code == newCode);
            } while (existCode);
            return newCode;
        }

        /// <summary>
        /// 新增砍价子项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public MerchantBargainOrderRecordDto AddMerchantBargainOrderRecordItem(MerchantBargainOrderRecordItem entity)
        {
            if (entity == null)
                return null;
            var merchantBargainOrderRecord = Repository.GetByKey(entity.MerchantBargainOrderRecordID);
            var merchantBargainOrder = MerchantBargainOrderRepo.GetByKey(merchantBargainOrderRecord.MerchantBargainOrderID);
            UnitOfWork.BeginTransaction();
            try
            {
                entity.Price = (merchantBargainOrderRecord.FinalPrice - merchantBargainOrder.Price) / (merchantBargainOrder.PeopleCount - merchantBargainOrderRecord.JoinPeople);
                merchantBargainOrderRecord.JoinPeople += 1;
                if (merchantBargainOrderRecord.JoinPeople > merchantBargainOrder.PeopleCount)
                    throw new DomainException("砍价人数已满");
                merchantBargainOrderRecord.FinalPrice -= entity.Price;
                entity.ID = Util.NewID();
                entity.CreatedDate = DateTime.Now;
                entity.MerchantID = AppContext.CurrentSession.MerchantID;
                MerchantBargainOrderRecordItemRepo.Add(entity);
                UnitOfWork.CommitTransaction();
                JoinBargainOrderSuccessNotify(merchantBargainOrder, merchantBargainOrderRecord, entity);
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException("加入砍价出现异常：" + e.Message);
            }
            return merchantBargainOrderRecord.MapTo<MerchantBargainOrderRecordDto>();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<MerchantBargainOrderRecordDto> Search(MerchantBargainOrderRecordFilter filter, out int totalCount)
        {
            var queryable = Repository.GetIncludes(false, "Member", "MerchantBargainOrder", "MerchantBargainOrder.Product");
            if (filter.ID.HasValue)
            {
                queryable = queryable.Where(t => t.ID == filter.ID.Value);
            }
            if (filter.MemberID.HasValue)
            {
                queryable = queryable.Where(t => t.MemberID == filter.MemberID.Value);
            }
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
            {
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            }
            var result = queryable.OrderByDescending(t => t.CreatedDate).MapTo<MerchantBargainOrderRecordDto>().ToArray();
            result.ForEach(t =>
            {
                t.IsCanBuy = t.JoinPeople >= t.PeopleCount;
                if (filter.MemberID.HasValue)
                    t.IsOrdered = OrderRepo.Exists(o => o.MerchantBargainOrderRecordID == t.ID && o.MemberID == filter.MemberID);
            });
            return result;
        }

        /// <summary>
        /// 加入砍价
        /// </summary>
        /// <param name="merchantBargainOrderRecordID"></param>
        /// <param name="MemberID"></param>
        /// <returns></returns>
        public bool JoinMerchantBargainOrderRecord(Guid merchantBargainOrderRecordID, Guid MemberID)
        {
            if (merchantBargainOrderRecordID == null || MemberID == null)
            {
                AppContext.Logger.Error("JoinMerchantCrowdOrderRecord:加入砍价，参数错误");
                return false;
            }
            var merchantBargainOrderRecord = Repository.GetIncludes(false, "Member", "MerchantBargainOrder", "MerchantBargainOrder.Product").FirstOrDefault(t => t.ID == merchantBargainOrderRecordID);
            if (merchantBargainOrderRecord == null)
            {
                AppContext.Logger.Error("JoinMerchantBargainOrderRecord:加入砍价，砍价记录不存在");
                return false;
            }
            UnitOfWork.BeginTransaction();
            try
            {
                MerchantBargainOrderRecordItemRepo.Add(new MerchantBargainOrderRecordItem
                {
                    ID = Util.NewID(),
                    MerchantBargainOrderRecordID = merchantBargainOrderRecordID,
                    MemberID = MemberID,
                    CreatedDate = DateTime.Now,
                    MerchantID = AppContext.CurrentSession.MerchantID,
                });
                merchantBargainOrderRecord.JoinPeople += 1;
                Repository.Update(merchantBargainOrderRecord);
                UnitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                AppContext.Logger.Error($"JoinBargainOrderRecord:加入砍价出现异常，{e.Message}");
                return false;
            }
            if (merchantBargainOrderRecord.JoinPeople >= merchantBargainOrderRecord.MerchantBargainOrder.PeopleCount)
            {
                MerchantBargainOrderSuccessNotify(merchantBargainOrderRecord);
            }
            return true;
        }

        /// <summary>
        /// 发送砍价成功提醒
        /// </summary>
        /// <param name="merchantBargainOrderRecord"></param>
        void MerchantBargainOrderSuccessNotify(MerchantBargainOrderRecord merchantBargainOrderRecord)
        {
            try
            {
                if (merchantBargainOrderRecord == null || merchantBargainOrderRecord.Member == null || string.IsNullOrEmpty(merchantBargainOrderRecord.Member.WeChatOpenID))
                    return;
                var templateId = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_CrowdOrderSuccess);
                if (string.IsNullOrEmpty(templateId))
                    return;
                var message = new WeChatTemplateMessageDto
                {
                    touser = merchantBargainOrderRecord.Member.WeChatOpenID,
                    template_id = templateId,
                    url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/MyMerchantBargainOrderDetails?mch={AppContext.CurrentSession.MerchantCode}&bargainid={merchantBargainOrderRecord.ID}",
                    data = new System.Dynamic.ExpandoObject(),
                };
                message.data.first = new WeChatTemplateMessageDto.MessageData("砍价成功");
                message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(merchantBargainOrderRecord.MerchantBargainOrder.Product.Name);
                message.data.keyword2 = new WeChatTemplateMessageDto.MessageData("");
                message.data.keyword3 = new WeChatTemplateMessageDto.MessageData(merchantBargainOrderRecord.MerchantBargainOrder.CreatedDate.ToString("yyyy-MM-dd"));
                message.data.remark = new WeChatTemplateMessageDto.MessageData("立即下单");
                WeChatService.SendWeChatNotifyAsync(message);
            }
            catch (Exception e)
            {
                AppContext.Logger.Error($"发送砍价成功提醒异常，{e.Message}");
            }
        }

        void JoinBargainOrderSuccessNotify(MerchantBargainOrder merchantBargainOrder, MerchantBargainOrderRecord merchantBargainOrderRecord, MerchantBargainOrderRecordItem merchantBargainOrderRecordItem)
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
                    url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/MyMerchantBargainOrderDetails?mch={AppContext.CurrentSession.MerchantCode}&isadd=false&bargainid={merchantBargainOrderRecord.ID}",
                    data = new System.Dynamic.ExpandoObject(),
                };
                var product = ProductRepo.GetByKey(merchantBargainOrder.ProductID);
                var helper = MemberRepo.GetByKey(merchantBargainOrderRecordItem.MemberID);
                message.data.first = new WeChatTemplateMessageDto.MessageData(helper!=null ? helper.Name+"帮您砍价成功": "有人帮您砍价成功");
                message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(product.Name);
                message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(merchantBargainOrderRecord.Code);
                message.data.keyword3 = new WeChatTemplateMessageDto.MessageData(DateTime.Now.ToString("yyyy-MM-dd"));
                message.data.remark = new WeChatTemplateMessageDto.MessageData("立即下单");
                var member = MemberRepo.GetByKey(merchantBargainOrderRecord.MemberID);
                if (member!= null)
                {
                    message.touser = member.WeChatOpenID;
                    WeChatService.SendWeChatNotify(message);
                }
            }
            catch (Exception e)
            {
                AppContext.Logger.Error($"加入砍价提醒异常，{e.Message}");
            }
        }
    }
}
