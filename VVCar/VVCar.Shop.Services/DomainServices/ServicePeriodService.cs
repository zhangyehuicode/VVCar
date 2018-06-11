using System;
using System.Collections.Generic;
using System.Linq;
using VVCar.BaseData.Services;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 服务周期配置领域服务
    /// </summary>
    public class ServicePeriodService : DomainServiceBase<IRepository<ServicePeriodSetting>, ServicePeriodSetting, Guid>, IServicePeriodService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServicePeriodService()
        {
        }

        #region properties

        IRepository<ServicePeriodCoupon> ServicePeriodCouponRepo { get => UnitOfWork.GetRepository<IRepository<ServicePeriodCoupon>>(); }

        IRepository<PickUpOrderItem> PickUpOrderItemRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrderItem>>(); }

        IWeChatService WeChatService { get => ServiceLocator.Instance.GetService<IWeChatService>(); }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ServicePeriodSetting Add(ServicePeriodSetting entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteServicePeriods(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");
            var servicePeriodList = this.Repository.GetInclude(t => t.ServicePeriodCouponList, false)
                .Where(t => ids.Contains(t.ID)).ToList();
            if (servicePeriodList == null || servicePeriodList.Count() < 1)
                throw new DomainException("未选择数据");
            UnitOfWork.BeginTransaction();
            try
            {
                foreach (var servicePeriod in servicePeriodList)
                {
                    if (servicePeriod.ServicePeriodCouponList.Count() > 0)
                    {
                        ServicePeriodCouponRepo.DeleteRange(servicePeriod.ServicePeriodCouponList);
                        servicePeriod.ServicePeriodCouponList = null;
                    }
                }
                this.Repository.DeleteRange(servicePeriodList);
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
        public IEnumerable<ServicePeriodSettingDto> Search(ServicePeriodFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetInclude(t => t.Product, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.ProductName))
                queryable = queryable.Where(t => filter.ProductName.Contains(t.Product.Name));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<ServicePeriodSettingDto>().ToArray();
        }

        public bool ServicePeriodReminder()
        {
            var now = DateTime.Now.Date;
            var pickUpOrderItemQueryable = PickUpOrderItemRepo.GetInclude(t => t.PickUpOrder, false);
            var servicePeriodQueryable = Repository.GetInclude(t => t.ServicePeriodCouponList, false).ToList();
            servicePeriodQueryable.ForEach(t =>
            {
                var dueservices = new List<PickUpOrderItem>();
                var pickuporderservices = pickUpOrderItemQueryable.Where(p => p.ProductID == t.ProductID).ToList();
                pickuporderservices.ForEach(p =>
                {
                    var perioddays = (now - p.PickUpOrder.CreatedDate.Date).Days;
                    if (perioddays == t.PeriodDays)
                        dueservices.Add(p);
                });
            });
            return true;
        }

        public bool SendServiceDueNotice(List<PickUpOrderItem> pickUpOrderItemList, ServicePeriodSetting servicePeriodSetting)
        {
            //var message = new WeChatTemplateMessageDto
            //{
            //    touser = receiveCouponDto.ReceiveOpenID,
            //    template_id = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_ReceivedSuccess),//WXMsg_CouponReceived
            //    url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/MemberCard?mch={companyCode}",
            //    data = new System.Dynamic.ExpandoObject(),
            //};
            //message.data.first = new WeChatTemplateMessageDto.MessageData($"尊敬的会员，您的服务即将到期");
            //message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(receiveCouponDto.NickName);
            //message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(template.Title);
            //message.data.keyword3 = new WeChatTemplateMessageDto.MessageData(newCoupon.CreatedDate.ToDateString());
            //message.data.remark = new WeChatTemplateMessageDto.MessageData("点击查看我的卡包");
            //WeChatService.SendWeChatNotifyAsync(message, receiveCouponDto.CompanyCode);
            return true;
        }
    }
}
