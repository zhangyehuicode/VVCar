using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using VVCar.BaseData.Domain;
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
    /// 优惠券领域服务
    /// </summary>
    public partial class CouponService : DomainServiceBase<IRepository<Coupon>, Coupon, Guid>, ICouponService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CouponService"/> class.
        /// </summary>
        public CouponService()
        {
        }

        #region properties

        IRepository<CouponTemplate> CouponTemplateRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<CouponTemplate>>(); }
        }

        IRepository<CouponTemplateUseTime> CouponTemplateUseTimeRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<CouponTemplateUseTime>>(); }
        }

        IRepository<CouponTemplateStock> CouponTemplateStockRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<CouponTemplateStock>>(); }
        }

        IRepository<VerificationCode> VerificationCodeRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<VerificationCode>>(); }
        }

        IRepository<VerificationRecord> VerificationRecordRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<VerificationRecord>>(); }
        }

        IRepository<VisitRecord> VisitRecordRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<VisitRecord>>(); }
        }

        IVerificationRecordService VerificationRecordService
        {
            get { return ServiceLocator.Instance.GetService<IVerificationRecordService>(); }
        }

        IWeChatService WeChatService
        {
            get { return ServiceLocator.Instance.GetService<IWeChatService>(); }
        }

        IRepository<Department> DepartmentRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<Department>>(); }
        }

        IRepository<PointExchangeCoupon> PointExchangeCouponRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<PointExchangeCoupon>>(); }
        }

        IRepository<Member> MemberRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<Member>>(); }
        }

        IRepository<Merchant> MerchantRepo
        {
            get => UnitOfWork.GetRepository<IRepository<Merchant>>();
        }

        #region SystemSettingService

        ISystemSettingService SystemSettingService
        {
            get { return ServiceLocator.Instance.GetService<ISystemSettingService>(); }
        }

        #endregion SystemSettingService

        #endregion properties

        #region services

        ICouponTemplateStockService CouponTemplateStockService
        {
            get { return ServiceLocator.Instance.GetService<ICouponTemplateStockService>(); }
        }

        IGivenCouponRecordService GivenCouponRecordService
        {
            get { return ServiceLocator.Instance.GetService<IGivenCouponRecordService>(); }
        }

        IMemberService MemberService
        {
            get { return ServiceLocator.Instance.GetService<IMemberService>(); }
        }

        #endregion services

        #region methods

        public override Coupon Add(Coupon entity)
        {
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.CouponCode = GenerateCouponCode(entity.ID);

            var template = CouponTemplateRepo.GetByKey(entity.TemplateID, false);
            if (template != null)
            {
                entity.CouponValue = template.CouponValue;
            }

            return base.Add(entity);
        }

        /// <summary>
        /// 赠送卡券
        /// </summary>
        /// <param name="couponGivenDto"></param>
        /// <returns></returns>
        public bool GiveAwayCoupon(CouponGivenDto couponGivenDto)
        {
            if (couponGivenDto == null)
            {
                return false;
            }
            if (couponGivenDto.CouponID == null)
            {
                return false;
            }
            var coupon = Repository.GetQueryable(true).Where(t => t.ID == couponGivenDto.CouponID).FirstOrDefault();
            if (coupon == null)
            {
                return false;
            }

            coupon.OwnerOpenID = couponGivenDto.OwnerOpenID;
            coupon.OwnerNickName = couponGivenDto.OwnerNickName;
            coupon.OwnerHeadImgUrl = couponGivenDto.OwnerHeadImgUrl;
            coupon.OwnerPhoneNo = couponGivenDto.OwnerPhoneNo;

            //coupon.CreatedDate = DateTime.Now;

            return Repository.Update(coupon) > 0;
        }

        public bool IsCouponOwner(Guid couponID, string givenOpenID)
        {
            return Repository.GetQueryable(false).Where(t => t.ID == couponID && t.OwnerOpenID == givenOpenID).FirstOrDefault() != null;
        }

        /// <summary>
        /// 领取卡券
        /// </summary>
        /// <param name="receiveCouponDto"></param>
        public IEnumerable<Guid> ReceiveCoupons(ReceiveCouponDto receiveCouponDto, bool sendNotify = false)
        {
            var result = new List<Guid>();
            if (receiveCouponDto.IsFromGiven)
            {
                var originalcoupon = Repository.GetInclude(t => t.Template, false).Where(t => t.ID == receiveCouponDto.CouponID).FirstOrDefault();
                if (originalcoupon == null)
                {
                    throw new DomainException("无法获取要赠送的卡券");
                }
                if (originalcoupon.Status != ECouponStatus.Default)
                {
                    throw new DomainException("卡券已被使用或已过期");
                }
                if (!IsCouponOwner(originalcoupon.ID, receiveCouponDto.GivenOpenID))
                {
                    throw new DomainException("赠送的卡券已经被领取啦！");
                }
                var receiverCoupons = Repository.GetQueryable(false).Where(t => t.OwnerOpenID == receiveCouponDto.ReceiveOpenID && t.TemplateID == originalcoupon.TemplateID).ToArray();
                var coupontemplate = CouponTemplateRepo.GetInclude(t => t.Stock, false).Where(t => t.ID == originalcoupon.TemplateID).FirstOrDefault();
                if (coupontemplate != null || coupontemplate.Stock != null)
                {
                    if (coupontemplate.GetExpiredDate() < DateTime.Now)
                    {
                        throw new DomainException($"券 {coupontemplate.Title} 已过期");
                    }
                    if (!coupontemplate.Stock.IsNoCollarQuantityLimit && coupontemplate.Stock.CollarQuantityLimit > 0 && coupontemplate.Stock.CollarQuantityLimit <= receiverCoupons.Count())
                    {
                        throw new DomainException($"券 {coupontemplate.Title} 超过最大领用上限");
                    }
                }

                UnitOfWork.BeginTransaction();
                try
                {
                    var giveres = GiveAwayCoupon(new CouponGivenDto
                    {
                        CouponID = (Guid)receiveCouponDto.CouponID,
                        OwnerOpenID = receiveCouponDto.ReceiveOpenID,
                        OwnerNickName = receiveCouponDto.NickName,
                        OwnerHeadImgUrl = receiveCouponDto.HeadImgUrl,
                        OwnerPhoneNo = receiveCouponDto.MobilePhoneNo
                    });
                    if (!giveres)
                    {
                        throw new Exception("GiveAwayCoupon失败");
                    }

                    var givenCouponRecord = new GivenCouponRecord
                    {
                        CouponID = (Guid)receiveCouponDto.CouponID,
                        OwnerOpenID = receiveCouponDto.ReceiveOpenID,
                        OwnerNickName = receiveCouponDto.NickName,
                        OwnerHeadImgUrl = receiveCouponDto.HeadImgUrl,
                        DonorOpenID = originalcoupon.OwnerOpenID,
                        DonorNickName = originalcoupon.OwnerNickName,
                        DonorHeadImgUrl = originalcoupon.OwnerHeadImgUrl,
                        DonorReceivedDate = originalcoupon.CreatedDate
                    };
                    var giveRecord = GivenCouponRecordService.Add(givenCouponRecord);
                    if (giveRecord == null)
                    {
                        throw new Exception("添加赠送记录失败");
                    }

                    result.Add((Guid)receiveCouponDto.CouponID);
                    UnitOfWork.CommitTransaction();
                }
                catch (Exception ex)
                {
                    UnitOfWork.RollbackTransaction();
                    AppContext.Logger.Debug("领取来自赠送的卡券出现异常:" + ex.ToString());
                    throw new DomainException("领取失败，" + ex.ToString());
                }
            }
            else
            {
                result = this.ReceiveCouponsAtcion(receiveCouponDto, sendNotify).Select(c => c.ID).ToList();
            }
            return result;
        }

        public IEnumerable<string> ReceiveCouponsWidthCode(ReceiveCouponDto receiveCouponDto)
        {
            return this.ReceiveCouponsAtcion(receiveCouponDto).Select(c => c.CouponCode).ToArray();
        }

        public bool CenterReceiveCoupon(ReceiveCouponDto receiveCouponDto, bool sendNotify = false)
        {
            if (receiveCouponDto == null || string.IsNullOrEmpty(receiveCouponDto.ReceiveOpenID) || receiveCouponDto.PointExchangeCouponID == null)
            {
                throw new DomainException("参数错误");
            }
            try
            {
                UnitOfWork.BeginTransaction();

                var pointExchangeCoupon = PointExchangeCouponRepo.GetQueryable().Where(t => t.ID == receiveCouponDto.PointExchangeCouponID).FirstOrDefault();
                if (pointExchangeCoupon == null)
                {
                    throw new DomainException("找不到对应的积分兑换规则");
                }
                if (receiveCouponDto.ExchangePoint > 0)
                {
                    var setPointRes = MemberService.AdjustMemberPoint(receiveCouponDto.ReceiveOpenID, EMemberPointType.ExchangeCouponUse, -receiveCouponDto.ExchangePoint);
                    if (!setPointRes)
                    {
                        throw new DomainException("设置会员积分失败");
                    }
                }
                ReceiveCouponsAtcion(receiveCouponDto, sendNotify);

                pointExchangeCoupon.ExchangeCount++;
                PointExchangeCouponRepo.Update(pointExchangeCoupon);

                UnitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                UnitOfWork.RollbackTransaction();
                throw ex;
            }
            return true;
        }

        public IEnumerable<Coupon> ReceiveCouponsAtcion(ReceiveCouponDto receiveCouponDto, bool sendNotify = false)
        {
            if (receiveCouponDto == null || string.IsNullOrEmpty(receiveCouponDto.ReceiveOpenID)
                || receiveCouponDto.CouponTemplateIDs == null || receiveCouponDto.CouponTemplateIDs.Count == 0)
            {
                if (receiveCouponDto == null)
                {
                    AppContext.Logger.Error("参数为null");
                }
                if (receiveCouponDto != null && string.IsNullOrEmpty(receiveCouponDto.ReceiveOpenID))
                {
                    AppContext.Logger.Error("ReceiveOpenID为null");
                }
                if (receiveCouponDto != null && receiveCouponDto.CouponTemplateIDs == null)
                {
                    AppContext.Logger.Error("CouponTemplateIDs为null");
                }
                if (receiveCouponDto != null && receiveCouponDto.CouponTemplateIDs.Count == 0)
                {
                    AppContext.Logger.Error("CouponTemplateIDs个数为0");
                }
                throw new DomainException("参数错误");
            }

            var templates = CouponTemplateRepo.GetIncludes(true, "Stock", "UseTimeList")
                    .Where(t => receiveCouponDto.CouponTemplateIDs.Contains(t.ID) && t.IsAvailable && t.ApproveStatus == EApproveStatus.Delivered)
                    .ToList();
            if (templates.Count < 1)
                throw new DomainException("没有可以领取的卡券");
            if (receiveCouponDto.ReceiveOpenID == "specialcoupon")
            {
                var receivedSpecialCouponIds = Repository.GetQueryable(false)
                .Where(t => receiveCouponDto.CouponTemplateIDs.Contains(t.TemplateID)).Select(t => t.ID).ToArray();
                if (receivedSpecialCouponIds.Count() > 0)
                {
                    receivedSpecialCouponIds.ForEach(id =>
                    {
                        var verificationRecordIds = VerificationRecordRepo.GetQueryable(false).Where(t => t.CouponID == id).Select(t => t.ID).ToArray();
                        if (verificationRecordIds.Count() > 0)
                        {
                            verificationRecordIds.ForEach(vid =>
                            {
                                VerificationRecordRepo.Delete(vid);
                            });
                        }
                        Delete(id);
                    });
                }
            }
            UnitOfWork.BeginTransaction();
            try
            {
                var receivedCoupons = Repository.GetQueryable(false)
                    .Where(t => t.OwnerOpenID == receiveCouponDto.ReceiveOpenID && receiveCouponDto.CouponTemplateIDs.Contains(t.TemplateID))
                    .GroupBy(t => t.TemplateID)
                    .ToDictionary(group => group.Key, group => group.Count());

                foreach (var template in templates)
                {
                    if (template.GetExpiredDate() < DateTime.Now)
                    {
                        throw new DomainException($"券 {template.Title} 已过期");
                    }
                    if ((template.PutInStartDate.HasValue && template.PutInStartDate.Value > DateTime.Today)
                        || (template.PutInEndDate.HasValue && template.PutInEndDate.Value < DateTime.Today))
                    {
                        throw new DomainException($"券 {template.Title} 当前时间不能领取");
                    }
                    if (!template.PutInIsUseAllTime)
                    {
                        var dayOfWeek = ((int)DateTime.Now.DayOfWeek).ToString();
                        if (template.PutInUseDaysOfWeek != null && !template.PutInUseDaysOfWeek.Contains(dayOfWeek))
                            throw new DomainException($"券 {template.Title} 当前时间不能领取");

                        if (template.UseTimeList != null && template.UseTimeList.Count > 0)
                        {
                            bool allow = true;
                            var now = DateTime.Now;
                            var today = DateTime.Today.ToString("yyyy-MM-dd");
                            DateTime beginTime;
                            DateTime endTime;
                            foreach (var putInTime in template.UseTimeList)
                            {
                                if (putInTime.Type != EUseTimeType.PutIn)
                                    continue;
                                if (!DateTime.TryParse($"{today} {putInTime.BeginTime}:00", out beginTime))
                                    continue;
                                if (!DateTime.TryParse($"{today} {putInTime.EndTime}:00", out endTime))
                                    continue;
                                allow = beginTime <= now && now < endTime;
                                if (allow) break;
                            }
                            if (!allow)
                                throw new DomainException($"券 {template.Title} 当前时间不能领取");
                        }
                    }
                    if (template.Stock == null || template.Stock.FreeStock < 1)
                        throw new DomainException(string.Format("券 {0} 已被领完", template.Title));
                    if (!template.Stock.IsNoCollarQuantityLimit && template.Stock.CollarQuantityLimit > 0
                       && receivedCoupons.ContainsKey(template.ID) && template.Stock.CollarQuantityLimit <= receivedCoupons[template.ID] && receiveCouponDto.ReceiveOpenID != "specialcoupon")
                    {
                        throw new DomainException(string.Format("券 {0} 超过最大领用上限", template.Title));
                    }
                    template.Stock.UsedStock++;
                    if (template.IsSpecialCoupon)
                    {
                        template.EffectiveDate = template.GetEffectiveDate();
                        template.ExpiredDate = template.GetExpiredDate();
                    }
                    CouponTemplateRepo.Update(template);
                }
                UnitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                UnitOfWork.RollbackTransaction();
                throw ex;
            }

            var newCoupons = new List<Coupon>();
            foreach (var template in templates)
            {
                var newCoupon = new Coupon
                {
                    ID = Util.NewID(),
                    TemplateID = template.ID,
                    CouponValue = template.CouponValue,
                    EffectiveDate = template.GetEffectiveDate(),
                    ExpiredDate = template.GetExpiredDate(),
                    OwnerOpenID = receiveCouponDto.ReceiveOpenID,
                    OwnerNickName = receiveCouponDto.NickName,
                    OwnerHeadImgUrl = receiveCouponDto.HeadImgUrl,
                    OwnerPhoneNo = receiveCouponDto.MobilePhoneNo,
                    CreatedDate = DateTime.Now,
                    IsCanReuse = template.IsSpecialCoupon,
                    ReceiveChannel = receiveCouponDto.ReceiveChannel,
                    //IsDeductionFirst = template.IsDeductionFirst,
                };
                newCoupon.CouponCode = GenerateCouponCode(newCoupon.ID);
                newCoupons.Add(newCoupon);
                if (sendNotify || receiveCouponDto.SendNotify)
                {
                    var message = new WeChatTemplateMessageDto
                    {
                        touser = receiveCouponDto.ReceiveOpenID,
                        template_id = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_CouponReceived),
                        url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/MemberCard?mch={AppContext.CurrentSession.CompanyCode}",
                        data = new System.Dynamic.ExpandoObject(),
                    };
                    message.data.first = new WeChatTemplateMessageDto.MessageData("恭喜您获得新的卡券");
                    message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(receiveCouponDto.NickName);
                    message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(template.Title);
                    message.data.keyword3 = new WeChatTemplateMessageDto.MessageData(newCoupon.CreatedDate.ToDateString());
                    message.data.remark = new WeChatTemplateMessageDto.MessageData("点击查看我的卡包");
                    WeChatService.SendWeChatNotifyAsync(message);
                }
            }
            Repository.Add(newCoupons);
            return newCoupons.ToArray();
        }

        public IEnumerable<CouponBaseInfoDto> GetAvailableCouponList(string userOpenID)
        {
            var now = DateTime.Now.Date;
            var coupons = Repository.GetInclude(t => t.Template, false)
                .Where(t => t.Status == ECouponStatus.Default && t.OwnerOpenID == userOpenID && t.ExpiredDate >= now)
                .OrderBy(t => t.ExpiredDate)
                .MapTo<CouponBaseInfoDto>()
                .ToList();
            var member = MemberRepo.GetInclude(t => t.Card, false).Where(t => t.WeChatOpenID == userOpenID).FirstOrDefault();
            if (member != null)
            {
                coupons.ForEach(t =>
                {
                    t.MemberPoint = member.Point;
                    t.TotalConsume = member.Card.TotalConsume;
                });
            }
            return coupons;
        }

        public IEnumerable<CouponBaseInfoDto> GetCenterAvailableCouponList(string userOpenId, ECouponType Cid)
        {
            var nowtime = System.DateTime.Now.Date;
            var pushtemplates =
                CouponTemplateRepo.GetInclude(t => t.Stock)
                    .Where(t => t.IsAvailable && t.ApproveStatus == EApproveStatus.Delivered && (!t.IsFiexedEffectPeriod | t.ExpiredDate > nowtime | t.ExpiredDate == null))
                    .Where(t => (t.PutInStartDate == null || t.PutInStartDate <= nowtime) && (t.PutInEndDate == null || t.PutInEndDate >= nowtime))
                    .MapTo<CouponBaseInfoDto>().ToList();

            if (Cid != ECouponType.CashCoupon & Cid != ECouponType.Discount & Cid != ECouponType.Exchange &
                Cid != ECouponType.Voucher)
            {
            }
            else
            {
                pushtemplates = pushtemplates.Where(t => t.CouponType == Cid).ToList();
            }

            //var mycoupons =
            //    Repository.GetInclude(t => t.Template, false)
            //        .Where(t => t.Status == ECouponStatus.Default && t.OwnerOpenID == userOpenId)
            //        .OrderBy(t => t.ExpiredDate).ToList();

            var pointexchangecoupon = PointExchangeCouponRepo.GetQueryable(false)
                .Where(t => t.BeginDate <= nowtime && t.FinishDate >= nowtime)
                .Where(t => t.CouponTemplate.IsAvailable && t.CouponTemplate.ApproveStatus == EApproveStatus.Delivered && (!t.CouponTemplate.IsFiexedEffectPeriod || t.CouponTemplate.ExpiredDate > nowtime || t.CouponTemplate.ExpiredDate == null))
                .ToArray();

            var member = MemberRepo.GetQueryable(false).Where(t => t.WeChatOpenID == userOpenId).FirstOrDefault();
            if (member != null)
            {
                pushtemplates.ForEach(t =>
                {
                    t.MemberPoint = member.Point;
                });
            }

            // var recevied = pushtemplates.Where(t => mycoupons.Contains(t.TemplateID)).ToList();
            //foreach (var re in mycoupons)
            //{
            //    foreach (var dto in pushtemplates)
            //    {
            //        if (dto.TemplateID == re.TemplateID)
            //        {
            //            dto.IsReceived = true; //已领取
            //            dto.ExpiredDate = re.ExpiredDate;
            //            dto.EffectiveDate = re.EffectiveDate;
            //        }
            //    }
            //}
            var resultTemp = new List<CouponBaseInfoDto>();
            foreach (var ec in pointexchangecoupon)
            {
                foreach (var dto in pushtemplates)
                {
                    if (dto.TemplateID == ec.CouponTemplateId)
                    {
                        dto.ExchangeType = ec.ExchangeType;
                        dto.ExchangeFinishDate = ec.FinishDate;
                        dto.ExchangePoint = ec.Point;
                        dto.PointExchangeCouponID = ec.ID;
                        resultTemp.Add(dto);
                    }
                }
            }
            var resfree = resultTemp.OrderByDescending(t => t.ExpiredDate).Where(t => t.ExchangeType == EExchangeType.Free && t.Stock != t.UsedStock).ToList();
            var respoint = resultTemp.OrderByDescending(t => t.ExpiredDate).Where(t => t.ExchangeType == EExchangeType.Point && t.Stock != t.UsedStock).ToList();
            var resfinish = resultTemp.OrderByDescending(t => t.ExpiredDate).Where(t => t.Stock == t.UsedStock).ToList();
            var result = new List<CouponBaseInfoDto>();
            result.AddRange(resfree);
            result.AddRange(respoint);
            result.AddRange(resfinish);
            return result;
        }

        string GenerateCouponCode(Guid guid)
        {
            var guidInt = guid.ToString().Split('-').Select(i => Convert.ToInt64(i, 16)).Sum();
            var str = guidInt.PadRight(16, '0');
            var codeBuilder = new StringBuilder();
            for (var i = 0; i < 16; i += 4)
            {
                //codeBuilder.Append("-");
                codeBuilder.Append(str.Substring(i, 4));
            }
            //codeBuilder.Remove(0, 1);
            return codeBuilder.ToString();
        }

        public CouponFullInfoDto GetCouponInfoByTemplateID(Guid templateID, string openId)
        {
            var couponTemplate = this.CouponTemplateRepo.GetInclude(c => c.Stock, false).FirstOrDefault(t => t.ID == templateID);
            var couponUseTime = this.CouponTemplateUseTimeRepo.GetQueryable(false).Where(u => u.TemplateID == templateID);
            if (couponTemplate == null)
            {
                throw new DomainException("优惠券不存在");
            }
            var couponInfo = couponTemplate.MapTo<CouponFullInfoDto>();
            couponInfo.EffectiveDate = couponTemplate.GetEffectiveDate();
            couponInfo.ExpiredDate = couponTemplate.GetExpiredDate();
            couponInfo.CouponTemplateUseTimes = couponUseTime.ToArray();
            if (!string.IsNullOrEmpty(couponInfo.CoverImage))
            {
                couponInfo.CoverImage = string.Concat(AppContext.Settings.SiteDomain, couponInfo.CoverImage);
            }
            var coupon = Repository.GetQueryable(false).FirstOrDefault(t => t.TemplateID == templateID && t.OwnerOpenID == openId && t.Status == ECouponStatus.Default);
            if (coupon != null)
            {
                //couponInfo.CouponID = coupon.ID;
                //couponInfo.CouponCode = coupon.CouponCode;
                couponInfo.TemplateID = coupon.TemplateID;
                //couponInfo.EffectiveDate = coupon.EffectiveDate;
                //couponInfo.ExpiredDate = coupon.ExpiredDate;
                couponInfo.Status = coupon.Status;
                couponInfo.OwnerOpenID = coupon.OwnerOpenID;
                couponInfo.OwnerNickName = coupon.OwnerNickName;
                couponInfo.OwnerPhoneNo = coupon.OwnerPhoneNo;
            }
            return couponInfo;
        }

        public CouponFullInfoDto GetCouponInfo(Guid couponID)
        {
            var coupon = Repository.GetInclude(t => t.Template, false)
                .FirstOrDefault(t => t.ID == couponID);
            if (coupon == null)
            {
                throw new DomainException("优惠券不存在");
            }
            var couponInfo = coupon.Template.MapTo<CouponFullInfoDto>();
            if (!string.IsNullOrEmpty(couponInfo.CoverImage))
            {
                couponInfo.CoverImage = string.Concat(AppContext.Settings.SiteDomain, couponInfo.CoverImage);
            }
            couponInfo.CouponID = coupon.ID;
            couponInfo.CouponCode = coupon.CouponCode;
            couponInfo.TemplateID = coupon.TemplateID;
            couponInfo.EffectiveDate = coupon.EffectiveDate;
            couponInfo.ExpiredDate = coupon.ExpiredDate;
            couponInfo.Status = coupon.Status;
            couponInfo.OwnerOpenID = coupon.OwnerOpenID;
            couponInfo.OwnerNickName = coupon.OwnerNickName;
            couponInfo.OwnerPhoneNo = coupon.OwnerPhoneNo;
            couponInfo.CanGiveToPeople = coupon.Template.CanGiveToPeople;
            couponInfo.CanShareByPeople = coupon.Template.CanShareByPeople;
            return couponInfo;
        }

        public CouponFullInfoDto GetCouponInfo(string couponCode)
        {
            var coupon = Repository.GetInclude(t => t.Template, false)
                .FirstOrDefault(t => t.CouponCode == couponCode && t.Status == ECouponStatus.Default);
            if (coupon == null)
            {
                throw new DomainException("优惠券不存在");
            }
            var couponInfo = coupon.Template.MapTo<CouponFullInfoDto>();
            if (!string.IsNullOrEmpty(couponInfo.CoverImage))
            {
                couponInfo.CoverImage = string.Concat(AppContext.Settings.SiteDomain, couponInfo.CoverImage);
            }
            couponInfo.CouponID = coupon.ID;
            couponInfo.CouponCode = coupon.CouponCode;
            couponInfo.TemplateID = coupon.TemplateID;
            couponInfo.EffectiveDate = coupon.EffectiveDate;
            couponInfo.ExpiredDate = coupon.ExpiredDate;
            couponInfo.Status = coupon.Status;
            couponInfo.OwnerOpenID = coupon.OwnerOpenID;
            couponInfo.OwnerNickName = coupon.OwnerNickName;
            couponInfo.OwnerPhoneNo = coupon.OwnerPhoneNo;
            couponInfo.CanGiveToPeople = coupon.Template.CanGiveToPeople;
            couponInfo.CanShareByPeople = coupon.Template.CanShareByPeople;
            return couponInfo;
        }

        /// <summary>
        /// 获取卡券适用门店信息
        /// </summary>
        /// <param name="templateID"></param>
        /// <returns></returns>
        /// <exception cref="DomainException">优惠券不存在</exception>
        public IEnumerable<CouponApplyStoreDto> GetCouponApplyStoreInfo(Guid templateID)
        {
            var couponTemplate = this.CouponTemplateRepo.GetQueryable(false).FirstOrDefault(t => t.ID == templateID);
            if (couponTemplate == null)
            {
                throw new DomainException("优惠券不存在");
            }
            var defaultDeptId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var deptQueryable = DepartmentRepo.GetQueryable(false);//.Where(d => d.ID != defaultDeptId);
            if (!couponTemplate.IsApplyAllStore && !string.IsNullOrEmpty(couponTemplate.ApplyStores))
            {
                var deptCodes = couponTemplate.ApplyStores.Split(',');
                deptQueryable = deptQueryable.Where(d => deptCodes.Contains(d.Code));
            }
            return deptQueryable.Select(d => new CouponApplyStoreDto
            {
                Name = d.Name,
                Address = d.Address,
                TelPhone = d.ContactPhoneNo,
            }).ToArray();
        }

        /// <summary>
        /// 检查优惠券是否有效
        /// </summary>
        /// <param name="checkDto"></param>
        /// <returns></returns>
        public CouponInfoDto CheckCoupon(CheckCouponDto checkDto)
        {
            var coupon = Repository.GetInclude(t => t.Template, false)
                .FirstOrDefault(t => t.CouponCode == checkDto.CouponCode);
            CheckCoupon(coupon, checkDto.DepartmentCode, EVerificationMode.ScanCode);
            var couponInfo = new CouponInfoDto();
            coupon.Template.MapTo(couponInfo);
            coupon.MapTo(couponInfo);
            if (!string.IsNullOrEmpty(coupon.OwnerOpenID))
            {
                var memberService = ServiceLocator.Instance.GetService<IMemberService>();
                var member = memberService.GetMemberLiteInfo(EMemberFindType.WeChatOpenID, coupon.OwnerOpenID);
                if (member != null)
                {
                    couponInfo.OwnerMemberName = member.Name;
                    couponInfo.OwnerMemberGroup = member.MemberGroup;
                }
            }
            return couponInfo;
        }

        /// <summary>
        /// 检查优惠券是否可以被使用
        /// </summary>
        /// <param name="coupon">优惠券</param>
        /// <param name="departmentCode">门店编号</param>
        /// <param name="verifyMode">核销方式</param>
        void CheckCoupon(Coupon coupon, string departmentCode, EVerificationMode verifyMode)
        {
            if (coupon == null)
                throw new DomainException("券不存在");
            var title = coupon.Template.Title;
            if (coupon.Status != ECouponStatus.Default)
                throw new DomainException($"{title}不可用，状态为: " + coupon.Status.GetDescription());
            if (coupon.Template.VerificationMode != verifyMode && !coupon.Template.IsSpecialCoupon)
                throw new DomainException($"不允许通过当前核销方式核销{title}");
            //if (!coupon.Template.IsApplyAllStore)
            //{
            //    if (string.IsNullOrEmpty(coupon.Template.ApplyStores))
            //        throw new DomainException($"{title}不允许在当前门店使用");
            //    var applyStores = coupon.Template.ApplyStores.Split(',');
            //    if (!applyStores.Contains(departmentCode))
            //        throw new DomainException($"{title}不允许在当前门店使用");
            //}
            if (coupon.EffectiveDate > DateTime.Now || coupon.ExpiredDate < DateTime.Now)
                throw new DomainException(string.Format($"{title}不可用，有效日期: {0} - {1}", coupon.EffectiveDate.ToDateString(), coupon.ExpiredDate.ToDateString()));
            if (!coupon.Template.IsUseAllTime)
            {
                var dayOfWeek = ((int)DateTime.Now.DayOfWeek).ToString();
                if (coupon.Template.UseDaysOfWeek != null && !coupon.Template.UseDaysOfWeek.Contains(dayOfWeek))
                    throw new DomainException($"{title}不可用，非允许使用日期");

                if (coupon.Template.UseTimeList != null && coupon.Template.UseTimeList.Count > 0)
                {
                    bool allow = true;
                    var now = DateTime.Now;
                    var today = DateTime.Today.ToString("yyyy-MM-dd");
                    DateTime beginTime;
                    DateTime endTime;
                    foreach (var useTime in coupon.Template.UseTimeList)
                    {
                        if (useTime.Type != EUseTimeType.Use)
                            continue;
                        if (!DateTime.TryParse($"{today} {useTime.BeginTime}:00", out beginTime))
                            continue;
                        if (!DateTime.TryParse($"{today} {useTime.EndTime}:00", out endTime))
                            continue;
                        allow = beginTime <= now && now < endTime;
                        if (allow) break;
                    }
                    if (!allow)
                        throw new DomainException($"{title}不可用，非允许使用时段");
                }
            }
        }

        /// <summary>
        /// 核销优惠券
        /// </summary>
        /// <param name="verifyDto">核销优惠券DTO</param>
        /// <returns></returns>
        public bool VerifyCoupon(VerifyCouponDto verifyDto)
        {
            var verifyCode = verifyDto.VerifyCode;
            var departmentCode = verifyDto.DepartmentCode;
            if (verifyDto.VerificationMode == EVerificationMode.VerifyCode)
            {
                var verificationCode = VerificationCodeRepo.Get(t => t.Code == verifyCode);
                if (verificationCode == null)
                    throw new DomainException("核销码不正确");
                departmentCode = verificationCode.DepartmentCode;
            }
            var couponCodes = verifyDto.CouponCodes;
            var coupons = Repository.GetInclude(t => t.Template).Where(t => couponCodes.Contains(t.CouponCode)).ToList();
            foreach (var couponCode in couponCodes)
            {
                var coupon = coupons.FirstOrDefault(c => c.CouponCode == couponCode);
                CheckCoupon(coupon, departmentCode, verifyDto.VerificationMode);
            }
            UnitOfWork.BeginTransaction();
            try
            {
                foreach (var coupon in coupons)
                {
                    decimal voucherAmount = 0;
                    var record = new VerificationRecord
                    {
                        CouponCode = coupon.CouponCode,
                        CouponID = coupon.ID,
                        VerificationMode = verifyDto.VerificationMode,
                        VerificationCode = verifyCode,
                        DepartmentCode = departmentCode
                    };
                    if (coupon.Template.Nature == ENature.Coupon)
                    {
                        coupon.Status = ECouponStatus.Used;
                        if (coupon.IsCanReuse)
                        {
                            coupon.Status = ECouponStatus.Default;
                            var couponTempStock = CouponTemplateStockRepo.GetQueryable(false).Where(s => s.ID == coupon.TemplateID).FirstOrDefault();
                            if (couponTempStock != null)
                            {
                                couponTempStock.UsedStock += 1;
                                CouponTemplateStockService.Update(couponTempStock);
                            }
                        }
                    }
                    else
                    {
                        record.Nature = ENature.Card;
                        if (coupon.Template.CouponType != ECouponType.Discount)
                        {
                            if (verifyDto.MemberCardVoucherInfoList != null && verifyDto.MemberCardVoucherInfoList.Count > 0)
                            {
                                var info = verifyDto.MemberCardVoucherInfoList.Where(t => t.Code == coupon.CouponCode).FirstOrDefault();
                                if (info != null)
                                {
                                    if (coupon.CouponValue < info.VoucherAmount)
                                        throw new DomainException($"{coupon.Template.Title}抵用金额不足");
                                    coupon.CouponValue -= info.VoucherAmount;
                                    voucherAmount = info.VoucherAmount;
                                    record.VoucherAmount = info.VoucherAmount;
                                }
                                else
                                {
                                    throw new DomainException($"{coupon.Template.Title}信息缺失");
                                }
                            }
                            else
                            {
                                throw new DomainException($"{coupon.Template.Title}信息缺失");
                            }
                        }
                    }
                    Repository.Update(coupon);
                    VerificationRecordService.Add(record);
                    SendCouponUsedNotify(coupon, voucherAmount);
                }
                UnitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException("核销失败，" + ex.Message);
            }
            return true;
        }

        /// <summary>
        /// 发送卡券使用通知
        /// </summary>
        /// <param name="coupon"></param>
        void SendCouponUsedNotify(Coupon coupon, decimal voucherAmount = 0)
        {
            if (string.IsNullOrEmpty(coupon.OwnerOpenID) || "specialcoupon".Equals(coupon.OwnerOpenID))
                return;
            var templateId = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_VerificationSuccess);
            var message = new WeChatTemplateMessageDto
            {
                touser = coupon.OwnerOpenID,
                template_id = templateId,
                url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/MemberCard?mch={AppContext.CurrentSession.CompanyCode}",
                data = new System.Dynamic.ExpandoObject(),
            };
            message.data.first = new WeChatTemplateMessageDto.MessageData(string.Format("您好，您已成功使用{0}！", coupon.Template.Title));
            message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//coupon.CouponCode
            var couponValueUnit = coupon.Template.CouponType == ECouponType.Discount ? "折" : "元";
            message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(coupon.Template.Title);
            if (coupon.Template.CouponType == ECouponType.Discount)
                message.data.keyword3 = new WeChatTemplateMessageDto.MessageData(coupon.Template.CouponValue.ToString("0.##") + couponValueUnit);
            else if (voucherAmount != 0)
                message.data.keyword3 = new WeChatTemplateMessageDto.MessageData($"{voucherAmount.ToString("0.##")}{couponValueUnit}");
            message.data.remark = new WeChatTemplateMessageDto.MessageData("感谢您的使用，欢迎下次光临");
            WeChatService.SendWeChatNotifyAsync(message);
        }

        /// <summary>
        /// 发送即将过期的卡券通知
        /// </summary>
        public void SendCouponExpiredNotify()
        {
            var expiredDateBegin = DateTime.Today.AddDays(7);
            var expiredDateEnd = expiredDateBegin.AddDays(1);
            var couponlist = Repository.GetQueryable(false)
                .Where(t => t.Status == ECouponStatus.Default && expiredDateBegin <= t.ExpiredDate && t.ExpiredDate < expiredDateEnd)
                .Select(t => new
                {
                    t.OwnerOpenID,
                    t.CouponCode,
                    t.ExpiredDate,
                    CouponTitle = t.Template.Title,
                    t.Template.MerchantID,
                }).ToList();
            if (couponlist == null || couponlist.Count < 1)
                return;
            var templateId = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_CouponWillExpire);
            foreach (var coupon in couponlist)
            {
                if (string.IsNullOrEmpty(coupon.OwnerOpenID))
                    continue;
                var merchant = MerchantRepo.GetByKey(coupon.MerchantID, false);
                if (merchant == null)
                {
                    AppContext.Logger.Error($"商户ID为:{coupon.MerchantID}的商户不存在");
                    continue;
                }
                var message = new WeChatTemplateMessageDto
                {
                    touser = coupon.OwnerOpenID,
                    template_id = templateId,
                    url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/MemberCard?mch={merchant.Code}",//{AppContext.CurrentSession.CompanyCode}
                    data = new System.Dynamic.ExpandoObject(),
                };
                message.data.first = new WeChatTemplateMessageDto.MessageData(string.Format("您有一张卡券{0}即将过期，赶紧去使用吧。", ""));//, coupon.CouponTitle
                message.data.keyword1 = new WeChatTemplateMessageDto.MessageData("0");
                message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(coupon.CouponTitle);//"系统发送（请勿回复）";
                message.data.keyword3 = new WeChatTemplateMessageDto.MessageData("1");
                //message.data.keyword4 = "到店消费出示优惠券即可";
                message.data.remark = new WeChatTemplateMessageDto.MessageData($"过期时间：{coupon.ExpiredDate.ToDateTimeString()}");
                WeChatService.SendWeChatNotifyAsync(message, merchant.Code);
            }
        }

        /// <summary>
        /// 是否拥有优惠券
        /// </summary>
        /// <param name="userOpenID"></param>
        /// <param name="couponTemplateID"></param>
        /// <returns></returns>
        public bool IsReceivedCoupon(string userOpenID, Guid couponTemplateID)
        {
            return Repository.Exists(t => t.TemplateID == couponTemplateID && t.OwnerOpenID == userOpenID);
        }

        /// <summary>
        ///优惠券报表数据
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public IEnumerable<CouponReportDto> CouponReportData(CouponReportFilter filter)
        {
            filter.EndTime = filter.EndTime.AddDays(1);
            var couponqueryable = this.Repository.GetQueryable(false);
            var verificationqueryable = VerificationRecordRepo.GetInclude(v => v.Coupon);
            var visitdata = this.VisitRecordRepo.GetQueryable(false);

            if (filter.CouponType != -1)
            {
                couponqueryable = couponqueryable.Where(c => c.Template.CouponType == (ECouponType)filter.CouponType);
                verificationqueryable = verificationqueryable.Where(v => v.Coupon.Template.CouponType == (ECouponType)filter.CouponType);
                visitdata = visitdata.Where(v => v.CouponTemplate.CouponType == (ECouponType)filter.CouponType);
            }
            if (filter.IsLimited)
            {
                couponqueryable = couponqueryable.Where(c => c.CreatedDate >= filter.StartTime).Where(c => c.CreatedDate < filter.EndTime);
                verificationqueryable = verificationqueryable.Where(v => v.VerificationDate >= filter.StartTime).Where(v => v.VerificationDate < filter.EndTime);
                visitdata = visitdata.Where(v => v.VisitDate >= filter.StartTime).Where(v => v.VisitDate < filter.EndTime);
            }
            if (!string.IsNullOrEmpty(filter.TemplateCode))
            {
                couponqueryable = couponqueryable.Where(c => c.Template.TemplateCode == filter.TemplateCode);
                verificationqueryable = verificationqueryable.Where(v => v.Coupon.Template.TemplateCode == filter.TemplateCode);
                visitdata = visitdata.Where(v => v.CouponTemplate.TemplateCode == filter.TemplateCode);
            }

            var couponReportTemp = couponqueryable.ToArray().GroupBy(c => c.CreatedDate.ToShortDateString()).Select(c => new
            {
                Date = c.Key,
                Number = c.GroupBy(g => g.OwnerOpenID).Count(),
                Time = c.Count()
            });
            var verificationTemp = verificationqueryable.ToArray().GroupBy(v => v.VerificationDate.ToShortDateString()).Select(v => new
            {
                Date = v.Key,
                Number = v.GroupBy(g => g.Coupon.OwnerOpenID).Count(),
                Time = v.Count()
            });
            var visitData = visitdata.ToList().GroupBy(v => v.VisitDate.ToShortDateString()).Select(v => new
            {
                VisitDate = v.Key,
                PV = v.Sum(s => s.PV)
            });

            var result = new List<CouponReportDto>();
            if (filter.IsLimited)
            {
                CouponReportDto tempReportObj;
                for (var i = filter.StartTime; i < filter.EndTime; i = i.AddDays(1))
                {
                    tempReportObj = new CouponReportDto
                    {
                        Date = i.ToShortDateString()
                    };
                    var couponReport = couponReportTemp.FirstOrDefault(c => c.Date == tempReportObj.Date);
                    if (couponReport != null)
                        tempReportObj.GetCoupon = new ReportTemp(couponReport.Number, couponReport.Time);

                    var verification = verificationTemp.FirstOrDefault(c => c.Date == tempReportObj.Date);
                    if (verification != null)
                        tempReportObj.Verification = new ReportTemp(verification.Number, verification.Time);

                    var visit = visitData.FirstOrDefault(c => c.VisitDate == tempReportObj.Date);
                    if (visit != null)
                        tempReportObj.BrowseCoupon = new ReportTemp(visit.PV, visit.PV);
                    result.Add(tempReportObj);
                }
            }
            else
            {
                var datesexceptTemp = couponReportTemp.Select(c => c.Date).Except(verificationTemp.Select(v => v.Date));
                var datesequal = couponReportTemp.Select(c => c.Date).Except(datesexceptTemp);

                if (datesequal.Count() > 0)
                {
                    couponReportTemp.ForEach(c =>
                    {
                        verificationTemp.ForEach(v =>
                        {
                            if (datesequal.Contains(v.Date))
                            {
                                result.Add(new CouponReportDto
                                {
                                    Date = v.Date,
                                    GetCoupon = new ReportTemp
                                    {
                                        Number = c.Number,
                                        Times = c.Time
                                    },
                                    Verification = new ReportTemp
                                    {
                                        Number = v.Number,
                                        Times = v.Time
                                    }
                                });
                            }
                            else
                            {
                                result.Add(new CouponReportDto
                                {
                                    Date = c.Date,
                                    GetCoupon = new ReportTemp
                                    {
                                        Number = c.Number,
                                        Times = c.Time
                                    }
                                });
                                result.Add(new CouponReportDto
                                {
                                    Date = v.Date,
                                    Verification = new ReportTemp
                                    {
                                        Number = v.Number,
                                        Times = v.Time
                                    }
                                });
                            }
                        });
                    });
                }
                else
                {
                    couponReportTemp.ForEach(c =>
                    {
                        result.Add(new CouponReportDto
                        {
                            Date = c.Date,
                            GetCoupon = new ReportTemp
                            {
                                Number = c.Number,
                                Times = c.Time
                            }
                        });
                    });
                    verificationTemp.ForEach(v =>
                    {
                        result.Add(new CouponReportDto
                        {
                            Date = v.Date,
                            Verification = new ReportTemp
                            {
                                Number = v.Number,
                                Times = v.Time
                            }
                        });
                    });
                }

                visitData.ToArray().ForEach(v =>
                {
                    var flag = true;
                    result.ForEach(r =>
                    {
                        if (v.VisitDate == r.Date)
                        {
                            r.BrowseCoupon.Times = v.PV;
                            r.BrowseCoupon.Number = v.PV;
                            flag = false;
                        }
                    });
                    if (flag)
                    {
                        result.Add(new CouponReportDto
                        {
                            Date = v.VisitDate,
                            BrowseCoupon = new ReportTemp
                            {
                                Times = v.PV,
                                Number = v.PV
                            }
                        });
                    }
                });
            }
            return result.OrderBy(r => DateTime.Parse(r.Date));
        }

        /// <summary>
        ///导出卡券报表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<CouponReportExportDto> ExportCouponReport(CouponReportFilter filter)
        {
            var reportdata = this.CouponReportData(filter);
            return reportdata.Select(r => new CouponReportExportDto
            {
                Date = r.Date,
                BrowseTime = r.BrowseCoupon != null ? r.BrowseCoupon.Times : 0,
                GetNumber = r.GetCoupon != null ? r.GetCoupon.Number : 0,
                GetTime = r.GetCoupon != null ? r.GetCoupon.Times : 0,
                VerificationNumber = r.Verification != null ? r.Verification.Number : 0,
                VerificationTime = r.Verification != null ? r.Verification.Times : 0
            }).ToArray();
        }

        /// <summary>
        ///整体报表数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<CouponTotalReportDto> CouponTotalReportData(CouponReportFilter filter, ref int totalCount)
        {
            filter.EndTime = filter.EndTime.AddDays(1);
            var couponQueryable = this.Repository.GetQueryable(false);
            var verificationQueryable = VerificationRecordRepo.GetQueryable(false);
            var visitQueryable = this.VisitRecordRepo.GetQueryable(false);
            var templateQueryable = this.CouponTemplateRepo.GetQueryable(false);

            #region 处理过滤条件
            if (filter.CouponType != -1)
            {
                couponQueryable = couponQueryable.Where(c => c.Template.CouponType == (ECouponType)filter.CouponType);
                verificationQueryable = verificationQueryable.Where(v => v.Coupon.Template.CouponType == (ECouponType)filter.CouponType);
                visitQueryable = visitQueryable.Where(v => v.CouponTemplate.CouponType == (ECouponType)filter.CouponType);
                templateQueryable = templateQueryable.Where(c => c.CouponType == (ECouponType)filter.CouponType);
            }
            if (filter.IsLimited)
            {
                couponQueryable = couponQueryable.Where(c => c.CreatedDate >= filter.StartTime && c.CreatedDate < filter.EndTime);
                verificationQueryable = verificationQueryable.Where(v => v.VerificationDate >= filter.StartTime && v.VerificationDate < filter.EndTime);
                visitQueryable = visitQueryable.Where(v => v.VisitDate >= filter.StartTime && v.VisitDate < filter.EndTime);
                templateQueryable = templateQueryable.Where(c => c.CreatedDate >= filter.StartTime && c.CreatedDate < filter.EndTime);
            }
            #endregion

            #region 获取报表数据
            var couponReportData = couponQueryable.GroupBy(c => c.TemplateID)
                .Select(c => new
                {
                    TemplateID = c.Key,
                    Number = c.GroupBy(g => g.OwnerOpenID).Count(),//领取人数
                    Times = c.Count()//领取数量
                }).ToDictionary(c => c.TemplateID);
            var verificationData = verificationQueryable.GroupBy(v => v.Coupon.TemplateID)
                .Select(v => new
                {
                    TemplateID = v.Key,
                    Number = v.GroupBy(g => g.Coupon.OwnerOpenID).Count(),//核销人数
                    Times = v.Count()//核销次数
                }).ToDictionary(v => v.TemplateID);
            var visitData = visitQueryable.ToList().GroupBy(v => v.IdentifyID)
                .Select(v => new
                {
                    TemplateID = v.Key,
                    PV = v.Sum(s => s.PV)//访问量
                }).ToDictionary(v => v.TemplateID);
            var deliveredData = templateQueryable
                .Where(c => c.ApproveStatus == EApproveStatus.Delivered)
                .Select(c => c.ID).ToList();
            #endregion

            //获取有数据的CouponTemplate ID
            var reportTemplateIds = couponReportData.Keys.ToList();
            reportTemplateIds.AddRange(verificationData.Keys);
            reportTemplateIds.AddRange(visitData.Keys);
            reportTemplateIds.AddRange(deliveredData);
            reportTemplateIds = reportTemplateIds.Distinct().ToList();

            var reportQueryable = CouponTemplateRepo.GetQueryable(false)
                .Where(c => reportTemplateIds.Contains(c.ID));
            totalCount = reportQueryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
            {
                reportQueryable = reportQueryable.OrderByDescending(t => t.CreatedDate)
                    .Skip(filter.Start.Value)
                    .Take(filter.Limit.Value);
            }
            var reportTemplate = reportQueryable
                .Select(t => new
                {
                    t.ID,
                    t.Title,
                    t.TemplateCode,
                    t.IsFiexedEffectPeriod,
                    t.EffectiveDate,
                    t.ExpiredDate,
                    t.EffectiveDaysAfterReceived,
                    t.EffectiveDays,
                    t.CreatedDate,
                    t.Remark,
                    t.PutInStartDate,
                    t.PutInEndDate,
                    t.IsMinConsumeLimit,
                    t.MinConsume,
                    t.IsExclusive
                });
            var result = new List<CouponTotalReportDto>();
            foreach (var template in reportTemplate)
            {
                var reportItem = new CouponTotalReportDto()
                {
                    CouponTemplateCode = template.TemplateCode,
                    Name = template.Title,
                    Validity = template.IsFiexedEffectPeriod ? $"{template.EffectiveDate.Value.ToDateString()}~{template.ExpiredDate.Value.ToDateString()}" : $"领取后{template.EffectiveDaysAfterReceived.GetValueOrDefault()}天生效,{template.EffectiveDays.GetValueOrDefault()}天有效",
                    CreateDate = template.CreatedDate,
                    PutInDate = $"{template.PutInStartDate.Value.ToDateString()}~{template.PutInEndDate.Value.ToDateString()}",
                    Remark = template.Remark,
                };
                reportItem.UseCondition = template.IsMinConsumeLimit ? $"满 {template.MinConsume.ToString("0.##")} 元可用 " : string.Empty;
                if (template.IsExclusive)
                {
                    reportItem.UseCondition += " 不与其他优惠共享";
                }
                if (couponReportData.ContainsKey(template.ID))
                {
                    reportItem.GetNumber = couponReportData[template.ID].Number;
                    reportItem.GetTimes = couponReportData[template.ID].Times;
                }
                if (verificationData.ContainsKey(template.ID))
                {
                    reportItem.VerificationNumber = verificationData[template.ID].Number;
                    reportItem.VerificationTimes = verificationData[template.ID].Times;
                    if (reportItem.GetTimes > 0)
                    {
                        reportItem.VerificationRate = Math.Round(reportItem.VerificationTimes / (decimal)reportItem.GetTimes, 4) * 100;
                    }
                }
                if (visitData.ContainsKey(template.ID))
                {
                    reportItem.BrowseTimes = visitData[template.ID].PV;
                }
                result.Add(reportItem);
            }
            return result;
        }

        public void AddTitleToExcel(CouponReportFilter filter, string filePath, bool isTotalReport)
        {
            string extension = System.IO.Path.GetExtension(filePath);
            if (!extension.Equals(".xls") && !extension.Equals(".xlsx"))
            {
                return;
            }
            var title = this.GenerateReportTitle(filter, isTotalReport);
            this.WriteToExcel(filePath, title);
        }

        public IEnumerable<CouponInfoDto> GetSpecialCoupon(string departmentCode, ref int totalCount)
        {
            var specialCoupons = new List<CouponInfoDto>();
            var now = DateTime.Now;
            var today = DateTime.Today;
            var coupons = Repository.GetIncludes(false, "Template", "Template.UseTimeList")
                .Where(t => t.Template.ApproveStatus == EApproveStatus.Delivered && t.Template.IsSpecialCoupon && t.Template.PutInStartDate <= now && t.Template.PutInEndDate >= today
                    && t.EffectiveDate <= now && t.ExpiredDate >= now && t.Status == ECouponStatus.Default)
                .ToList();
            if (coupons.Count < 1)
                return specialCoupons;
            foreach (var coupon in coupons)
            {
                try
                {
                    CheckCoupon(coupon, departmentCode, EVerificationMode.ScanCode);
                    var couponInfo = new CouponInfoDto();
                    coupon.Template.MapTo(couponInfo);
                    coupon.MapTo(couponInfo);
                    var stock = CouponTemplateStockRepo.GetQueryable(false).Where(t => t.ID == coupon.Template.ID).FirstOrDefault();
                    couponInfo.FreeStock = stock != null ? stock.FreeStock : 0;
                    specialCoupons.Add(couponInfo);
                }
                catch { continue; }
            }
            return specialCoupons;
        }

        private void WriteToExcel(string filePath, string title)
        {
            IWorkbook wk = null;
            string extension = System.IO.Path.GetExtension(filePath);
            var fileName = Path.GetFileName(filePath);
            var path = AppContext.PathInfo.RootPath;
            var finalfilePath = Path.Combine(path, "export", fileName);
            try
            {
                FileStream fs = File.OpenRead(finalfilePath);
                if (extension.Equals(".xls"))
                {
                    wk = new HSSFWorkbook(fs);
                }
                else
                {
                    wk = new XSSFWorkbook(fs);
                }
                fs.Close();

                ISheet sheet = wk.GetSheetAt(0);
                sheet.ShiftRows(0, sheet.LastRowNum, 1, true, true);
                CellRangeAddress cellRangeAddress = new CellRangeAddress(0, 0, 0, sheet.GetRow(1).LastCellNum - 1);
                sheet.AddMergedRegion(cellRangeAddress);

                //字体
                IFont font = wk.CreateFont();
                font.Boldweight = (short)FontBoldWeight.Normal;
                //设置样式
                ICellStyle style = wk.CreateCellStyle();
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                style.SetFont(font);

                sheet.GetRow(0).CreateCell(0);
                sheet.GetRow(0).GetCell(0).SetCellValue(title);
                sheet.GetRow(0).GetCell(0).CellStyle = style;

                FileStream ws = File.OpenWrite(finalfilePath);
                wk.Write(ws);
                ws.Close();
            }
            catch
            {
            }
        }

        private string GetEnumDescription(Enum enumValue)
        {
            string str = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
            {
                return str;
            }
            System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];
            return da.Description;
        }

        private string GenerateReportTitle(CouponReportFilter filter, bool isTotalReport)
        {
            var title = string.Empty;
            if (isTotalReport)
            {
                var coupontype = filter.CouponType != -1 ? this.GetEnumDescription((ECouponType)filter.CouponType) : "优惠券";
                if (filter.IsLimited)
                {
                    var starttime = string.Empty;
                    var endtime = string.Empty;
                    if (filter.TimeAvailable)
                    {
                        starttime = filter.StartTime.ToString("yyyy-MM-dd");
                        endtime = filter.EndTime.ToString("yyyy-MM-dd");
                    }
                    title = string.Concat(coupontype, starttime, filter.TimeAvailable ? "~" : "", endtime, "报表");
                }
                else
                {
                    title = string.Concat(coupontype, "全部时段报表");
                }
            }
            else
            {
                var datedes = string.Empty;
                var coupontype = string.Empty;
                if (filter.IsLimited)
                {
                    if (filter.TimeAvailable)
                    {
                        datedes = string.Concat(filter.StartTime.ToString("yyyy-MM-dd"), "~", filter.EndTime.ToString("yyyy-MM-dd"), "投放详情");
                    }
                    else
                    {
                        datedes = "投放详情";
                    }
                }
                else
                {
                    datedes = "全部时段投放详情";
                }
                if (!string.IsNullOrEmpty(filter.TemplateTitle) && !string.IsNullOrEmpty(filter.TemplateCode))
                {
                    coupontype = filter.TemplateTitle;
                }
                else if (filter.CouponType != -1)
                {
                    coupontype = this.GetEnumDescription((ECouponType)filter.CouponType);
                }
                else
                {
                    coupontype = "优惠券";
                }
                title = string.Concat(coupontype, datedes);
            }
            return title;
        }

        public IEnumerable<CouponDto> GetCoupon(CouponFilter filter, ref int totalCount)
        {
            var nextDdate = filter.EndTime.AddDays(1);
            var result = Repository.GetInclude(t => t.Template)
                .Where(t => t.CreatedDate >= filter.StartTime)
               .Where(t => t.CreatedDate < nextDdate);
            if (!string.IsNullOrEmpty(filter.TemplateCode))
            {
                result = result.Where(t => t.Template.TemplateCode.Contains(filter.TemplateCode));
            }
            if (!string.IsNullOrEmpty(filter.DepartmentCode) && !filter.DepartmentCode.Equals("allstore"))
            {
                result = result.Where(t => t.Template.IsApplyAllStore || t.Template.ApplyStores.Contains(filter.DepartmentCode));
            }
            if (filter.CouponType != -1)
            {
                result = result.Where(t => t.Template.CouponType == (ECouponType)filter.CouponType);
            }
            if (!string.IsNullOrEmpty(filter.CouponCode))
            {
                result = result.Where(t => t.CouponCode.Contains(filter.CouponCode));
            }
            if (!string.IsNullOrEmpty(filter.TemplateTitle))
            {
                result = result.Where(t => t.Template.Title.Contains(filter.TemplateTitle));
            }
            totalCount = result.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
            {
                result = result.OrderBy(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            }
            return result.Select(item => new CouponDto
            {
                CouponID = item.ID,
                CouponCode = item.CouponCode,
                TemplateCode = item.Template.TemplateCode,
                Title = item.Template.Title,
                OwnerOpenID = item.OwnerOpenID,
                OwnerNickName = item.OwnerNickName,
                CreatedDate = item.CreatedDate,
                CanGiveToPeople = item.Template.CanGiveToPeople
            }).OrderByDescending(t => t.CreatedDate).ToArray();
        }

        public CouponStatusInfoDto GetCouponStatus(string couponCode)
        {
            var coupon = Repository.GetQueryable(false).Where(t => t.CouponCode == couponCode)
                .Select(t => new
                {
                    t.CouponCode,
                    t.Template.Title,
                    t.OwnerOpenID,
                    t.Status,
                    t.CreatedDate,
                    t.EffectiveDate,
                    t.ExpiredDate
                }).FirstOrDefault();
            if (coupon == null)
                throw new DomainException("券号不存在");
            return new CouponStatusInfoDto
            {
                CouponCode = coupon.CouponCode,
                CouponTitle = coupon.Title,
                Receiver = coupon.OwnerOpenID,
                Status = coupon.Status.GetDescription(),
                ReceiveDate = coupon.CreatedDate,
                EffectiveDate = coupon.EffectiveDate,
                ExpiredDate = coupon.ExpiredDate,
            };
        }

        #endregion methods
    }
}
