using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain;
using VVCar.BaseData.Domain.Services;
using VVCar.BaseData.Services.DomainServices;
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
    /// 发起拼单记录
    /// </summary>
    public class CrowdOrderRecordService : DomainServiceBase<IRepository<CrowdOrderRecord>, CrowdOrderRecord, Guid>, ICrowdOrderRecordService
    {
        /// <summary>
        /// ctor.
        /// </summary>
        public CrowdOrderRecordService()
        {
        }

        #region properties

        IRepository<CrowdOrderRecordItem> CrowdOrderRecordItemRepo { get => UnitOfWork.GetRepository<IRepository<CrowdOrderRecordItem>>(); }

        ISystemSettingService SystemSettingService { get => ServiceLocator.Instance.GetService<ISystemSettingService>(); }

        IWeChatService WeChatService { get => ServiceLocator.Instance.GetService<IWeChatService>(); }

        IRepository<CarBitCoinOrder> CarBitCoinOrderRepo { get => UnitOfWork.GetRepository<IRepository<CarBitCoinOrder>>(); }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override CrowdOrderRecord Add(CrowdOrderRecord entity)
        {
            if (entity == null || entity.CrowdOrderRecordItemList == null || entity.CrowdOrderRecordItemList.Count < 1)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            entity.JoinPeople = entity.CrowdOrderRecordItemList.Count();
            entity.CrowdOrderRecordItemList.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.CrowdOrderRecordID = entity.ID;
                t.CreatedDate = DateTime.Now;
                t.MerchantID = entity.MerchantID;
            });
            return base.Add(entity);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<CrowdOrderRecordDto> Search(CrowdOrderRecordFilter filter, out int totalCount)
        {
            var queryable = Repository.GetIncludes(false, "CrowdOrder", "CrowdOrder.CarBitCoinProduct");
            if (filter.ID.HasValue)
            {
                queryable = queryable.Where(t => t.ID == filter.ID.Value);
            }
            if (filter.CarBitCoinMemberID.HasValue)
            {
                queryable = queryable.Where(t => t.CarBitCoinMemberID == filter.CarBitCoinMemberID.Value);
            }
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
            {
                queryable = queryable.OrderBy(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            }
            var result = queryable.MapTo<CrowdOrderRecordDto>().ToArray();
            result.ForEach(t =>
            {
                t.IsCanBuy = t.JoinPeople >= t.PeopleCount;
                if (filter.CarBitCoinMemberID.HasValue)
                    t.IsOrdered = CarBitCoinOrderRepo.Exists(o => o.CrowdOrderRecordID == t.ID && o.MemberID == filter.CarBitCoinMemberID);
            });
            return result;
        }

        /// <summary>
        /// 加入拼单
        /// </summary>
        /// <param name="crowdOrderRecordID"></param>
        /// <param name="carBitCoinMemberID"></param>
        /// <returns></returns>
        public bool JoinCrowdOrderRecord(Guid crowdOrderRecordID, Guid carBitCoinMemberID)
        {
            if (crowdOrderRecordID == null || carBitCoinMemberID == null)
            {
                AppContext.Logger.Error("JoinCrowdOrderRecord:加入拼单，参数错误");
                return false;
            }
            var crowdOrderRecord = Repository.GetIncludes(false, "CarBitCoinMember", "CrowdOrder", "CrowdOrder.CarBitCoinProduct").FirstOrDefault(t => t.ID == crowdOrderRecordID);
            if (crowdOrderRecord == null)
            {
                AppContext.Logger.Error("JoinCrowdOrderRecord:加入拼单，拼单记录不存在");
                return false;
            }
            UnitOfWork.BeginTransaction();
            try
            {
                CrowdOrderRecordItemRepo.Add(new CrowdOrderRecordItem
                {
                    ID = Util.NewID(),
                    CrowdOrderRecordID = crowdOrderRecordID,
                    CarBitCoinMemberID = carBitCoinMemberID,
                    CreatedDate = DateTime.Now,
                    MerchantID = AppContext.CurrentSession.MerchantID,
                });
                crowdOrderRecord.JoinPeople += 1;
                Repository.Update(crowdOrderRecord);
                UnitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                AppContext.Logger.Error($"JoinCrowdOrderRecord:加入拼单出现异常，{e.Message}");
                return false;
            }
            if (crowdOrderRecord.JoinPeople >= crowdOrderRecord.CrowdOrder.PeopleCount)
            {
                CrowdOrderSuccessNotify(crowdOrderRecord);
            }
            return true;
        }

        /// <summary>
        /// 发送拼单成功提醒
        /// </summary>
        /// <param name="crowdOrderRecord"></param>
        void CrowdOrderSuccessNotify(CrowdOrderRecord crowdOrderRecord)
        {
            try
            {
                if (crowdOrderRecord == null || crowdOrderRecord.CarBitCoinMember == null || string.IsNullOrEmpty(crowdOrderRecord.CarBitCoinMember.OpenID))
                    return;
                var templateId = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_CrowdOrderSuccess);
                if (string.IsNullOrEmpty(templateId))
                    return;
                var message = new WeChatTemplateMessageDto
                {
                    touser = crowdOrderRecord.CarBitCoinMember.OpenID,
                    template_id = templateId,
                    url = $"{AppContext.Settings.SiteDomain}/Mobile/CarBitcoin/MyCrowdOrderDetails?mch={AppContext.CurrentSession.MerchantCode}&coid={crowdOrderRecord.ID}",
                    data = new System.Dynamic.ExpandoObject(),
                };
                message.data.first = new WeChatTemplateMessageDto.MessageData("拼单成功");
                message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(crowdOrderRecord.CrowdOrder.CarBitCoinProduct.Name);
                message.data.keyword2 = new WeChatTemplateMessageDto.MessageData("");
                message.data.keyword3 = new WeChatTemplateMessageDto.MessageData(crowdOrderRecord.CrowdOrder.CreatedDate.ToString("yyyy-MM-dd"));
                message.data.remark = new WeChatTemplateMessageDto.MessageData("立即下单");
                WeChatService.SendWeChatNotifyAsync(message);
            }
            catch (Exception e)
            {
                AppContext.Logger.Error($"发送拼单成功提醒异常，{e.Message}");
            }
        }
    }
}