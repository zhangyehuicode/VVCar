using System;
using System.Collections.Generic;
using System.Linq;
using VVCar.BaseData.Domain;
using VVCar.BaseData.Domain.Services;
using VVCar.BaseData.Services;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
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

        IRepository<PickUpOrder> PickUpOrderRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrder>>(); }

        IRepository<PickUpOrderItem> PickUpOrderItemRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrderItem>>(); }

        IRepository<Member> MemberRepo { get => UnitOfWork.GetRepository<IRepository<Member>>(); }

        IWeChatService WeChatService { get => ServiceLocator.Instance.GetService<IWeChatService>(); }

        ISystemSettingService SystemSettingService { get => ServiceLocator.Instance.GetService<ISystemSettingService>(); }

        IRepository<Merchant> MerchantRepo { get => UnitOfWork.GetRepository<IRepository<Merchant>>(); }

        ICouponService CouponService { get => ServiceLocator.Instance.GetService<ICouponService>(); }

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
            var count = this.Repository.Count(t => t.ProductID == entity.ProductID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (count > 0)
                throw new DomainException("该服务已经配置过");
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
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(ServicePeriodSetting entity)
        {
            if (entity == null)
                return false;
            var servicePeriodSetting = Repository.GetByKey(entity.ID);
            if (servicePeriodSetting == null)
                return false;
            servicePeriodSetting.ProductID = entity.ProductID;
            servicePeriodSetting.PeriodDays = entity.PeriodDays;
            servicePeriodSetting.ExpirationNotice = entity.ExpirationNotice;
            servicePeriodSetting.IsAvailable = entity.IsAvailable;
            return Repository.Update(servicePeriodSetting) > 0;
        }

        /// <summary>
        /// 启用服务
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool EnableServicePeriod(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");
            var servicePeriodList = this.Repository.GetQueryable(true).Where(t => ids.Contains(t.ID)).ToList();
            if (servicePeriodList == null || servicePeriodList.Count() < 1)
                throw new DomainException("数据不存在");
            UnitOfWork.BeginTransaction();
            try
            {
                servicePeriodList.ForEach(t =>
                {
                    t.IsAvailable = true;
                });
                this.Repository.UpdateRange(servicePeriodList);
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
        /// 禁用服务
        /// </summary>
        /// <param name="ids"></param>
        public bool DisableServicePeriod(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");
            var servicePeriodList = this.Repository.GetQueryable(true).Where(t => ids.Contains(t.ID)).ToList();
            if (servicePeriodList == null || servicePeriodList.Count() < 1)
                throw new DomainException("数据不存在");
            UnitOfWork.BeginTransaction();
            try
            {
                servicePeriodList.ForEach(t =>
                {
                    t.IsAvailable = false;
                });
                this.Repository.UpdateRange(servicePeriodList);
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
            var servicePeriodQueryable = Repository.GetInclude(t => t.ServicePeriodCouponList, false).Where(t => t.IsAvailable).ToList();
            if (servicePeriodQueryable != null && servicePeriodQueryable.Count() > 0)
            {
                servicePeriodQueryable.ForEach(t =>
                {
                    var dueservices = new List<PickUpOrderItem>();
                    var pickuporderservices = pickUpOrderItemQueryable.Where(p => p.ProductID == t.ProductID).ToList();
                    if (pickuporderservices != null && pickuporderservices.Count() > 0)
                    {
                        pickuporderservices.ForEach(p =>
                        {
                            var perioddays = (now - p.PickUpOrder.CreatedDate.Date).Days;
                            if (perioddays == t.PeriodDays)
                                dueservices.Add(p);
                        });
                        SendServiceDueNotice(dueservices, t);
                    }
                });
            }
            return true;
        }

        public bool SendServiceDueNotice(List<PickUpOrderItem> pickUpOrderItemList, ServicePeriodSetting servicePeriodSetting)
        {
            if (pickUpOrderItemList == null || pickUpOrderItemList.Count < 1 || servicePeriodSetting == null)
                return false;

            var memberQueryable = MemberRepo.GetInclude(t => t.MemberPlateList, false);
            pickUpOrderItemList.ForEach(t =>
            {
                var memberlist = memberQueryable.Where(m => m.MemberPlateList.Count(p => p.PlateNumber == t.PickUpOrder.PlateNumber) > 0 && m.MerchantID == t.PickUpOrder.MerchantID).ToList();
                if (memberlist != null && memberlist.Count > 0)
                {
                    memberlist.ForEach(m =>
                    {
                        if (!string.IsNullOrEmpty(m.WeChatOpenID))
                        {
                            try
                            {
                                var merchant = MerchantRepo.GetByKey(m.MerchantID, false);
                                if (merchant == null)
                                    throw new DomainException("商户不存在");

                                CouponPushAction(servicePeriodSetting, m, merchant.Code);

                                var message = new WeChatTemplateMessageDto
                                {
                                    touser = m.WeChatOpenID,
                                    template_id = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_ServiceExpiredRemind, merchant.ID),
                                    url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/Shop?mch={merchant.Code}",
                                    data = new System.Dynamic.ExpandoObject(),
                                };
                                var tips = $"尊敬的会员，您的{t.ProductName}服务周期已到";
                                if (!string.IsNullOrEmpty(servicePeriodSetting.ExpirationNotice))
                                    tips = servicePeriodSetting.ExpirationNotice;
                                var remark = "点击进入会员商城预约服务";
                                if (servicePeriodSetting.ServicePeriodCouponList != null && servicePeriodSetting.ServicePeriodCouponList.Count() > 0)
                                    remark = $"已赠送{servicePeriodSetting.ServicePeriodCouponList.Count()}张优惠券，可到我的卡包查看。" + remark;
                                message.data.first = new WeChatTemplateMessageDto.MessageData(tips);
                                message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(t.ProductCode);
                                message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(t.ProductName);
                                message.data.keyword3 = new WeChatTemplateMessageDto.MessageData(DateTime.Now.ToDateString());
                                message.data.remark = new WeChatTemplateMessageDto.MessageData(remark);
                                WeChatService.SendWeChatNotifyAsync(message, merchant.Code);
                            }
                            catch (Exception e)
                            {
                                AppContext.Logger.Error($"服务到期提醒发送微信消息出现异常，{e.Message}");
                            }
                        }
                    });
                }

            });
            return true;
        }

        /// <summary>
        /// 卡券推送动作
        /// </summary>
        /// <param name="couponPushList"></param>
        /// <returns></returns>
        public bool CouponPushAction(ServicePeriodSetting servicePeriodSetting, Member member, string companyCode)
        {
            if (servicePeriodSetting != null && servicePeriodSetting.ServicePeriodCouponList != null && servicePeriodSetting.ServicePeriodCouponList.Count() > 0 && member != null && !string.IsNullOrEmpty(companyCode))
            {
                if (!string.IsNullOrEmpty(member.WeChatOpenID))
                {
                    try
                    {
                        CouponService.ReceiveCouponsAtcion(new ReceiveCouponDto
                        {
                            ReceiveOpenID = member.WeChatOpenID,
                            CouponTemplateIDs = servicePeriodSetting.ServicePeriodCouponList.Select(item => item.CouponTemplateID).ToList(),
                            ReceiveChannel = "服务到期推送卡券",
                            CompanyCode = companyCode,
                            NickName = member.Name,
                            MerchantID = member.MerchantID,
                        }, true);
                    }
                    catch (Exception e)
                    {
                        AppContext.Logger.Error($"服务周期到期提醒推送卡券出现异常，{e.Message}");
                    }
                }
            }
            return true;
        }
    }
}
