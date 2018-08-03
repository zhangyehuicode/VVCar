using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using VVCar.VIP.Domain;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;
using YEF.Core.Dtos;

namespace VVCar.VIP.Services.DomainServices
{
    public class MemberCardService : DomainServiceBase<IRepository<MemberCard>, MemberCard, Guid>, IMemberCardService
    {
        public MemberCardService()
        {
        }

        IMemberService _memberService;

        IMemberService MemberService
        {
            get
            {
                if (_memberService == null)
                    _memberService = ServiceLocator.Instance.GetService<IMemberService>();
                return _memberService;
            }
        }

        IMakeCodeRuleService _makeCodeRuleService;

        /// <summary>
        /// 编码规则
        /// </summary>
        IMakeCodeRuleService MakeCodeRuleService
        {
            get
            {
                if (_makeCodeRuleService == null)
                    _makeCodeRuleService = ServiceLocator.Instance.GetService<IMakeCodeRuleService>();
                return _makeCodeRuleService;
            }
        }

        IRepository<RechargeHistory> RechargeHistoryRepo { get => UnitOfWork.GetRepository<IRepository<RechargeHistory>>(); }

        IRepository<Member> MemberRepo { get => UnitOfWork.GetRepository<IRepository<Member>>(); }

        IRechargeHistoryService _rechargeHistoryService;

        /// <summary>
        /// 储值记录
        /// </summary>
        IRechargeHistoryService RechargeHistoryService
        {
            get
            {
                if (_rechargeHistoryService == null)
                {
                    _rechargeHistoryService = ServiceLocator.Instance.GetService<IRechargeHistoryService>();
                }
                return _rechargeHistoryService;
            }
        }

        IRechargePlanService _rechargePlanService;

        /// <summary>
        /// 储值方案
        /// </summary>
        IRechargePlanService RechargePlanService
        {
            get
            {
                if (_rechargePlanService == null)
                {
                    _rechargePlanService = ServiceLocator.Instance.GetService<IRechargePlanService>();
                }
                return _rechargePlanService;
            }
        }

        ISystemSettingService SystemSettingService
        {
            get { return ServiceLocator.Instance.GetService<ISystemSettingService>(); }
        }

        /// <summary>
        /// 微信服务
        /// </summary>
        IWeChatService WeChatService
        {
            get { return ServiceLocator.Instance.GetService<IWeChatService>(); }
        }

        ITradeHistoryService _tradeHistoryService;

        /// <summary>
        /// 消费记录
        /// </summary>
        ITradeHistoryService TradeHistoryService
        {
            get
            {
                if (_tradeHistoryService == null)
                {
                    _tradeHistoryService = ServiceLocator.Instance.GetService<ITradeHistoryService>();
                }
                return _tradeHistoryService;
            }
        }

        IMemberCardTypeService _memberCardTypeService;

        /// <summary>
        /// 卡片类型
        /// </summary>
        IMemberCardTypeService MemberCardTypeService
        {
            get
            {
                if (_memberCardTypeService == null)
                {
                    _memberCardTypeService = ServiceLocator.Instance.GetService<IMemberCardTypeService>();
                }
                return _memberCardTypeService;
            }
        }

        #region properties

        ///// <summary>
        ///// 卡片关联表
        ///// </summary>
        //IMemberGiftCardService MemberGiftCarService
        //{
        //    get { return ServiceLocator.Instance.GetService<IMemberGiftCardService>(); }
        //}
        //IRepository<MemberGiftCard> MemberGiftCarRepo
        //{
        //    get { return UnitOfWork.GetRepository<IRepository<MemberGiftCard>>(); }
        //}

        ///// <summary>
        ///// 会员卡主题表
        ///// </summary>
        //IMemberCardThemeService MemberCardThemeService
        //{
        //    get { return ServiceLocator.Instance.GetService<IMemberCardThemeService>(); }
        //}
        //IRepository<MemberCardTheme> MemberCardThemeRepo
        //{
        //    get { return UnitOfWork.GetRepository<IRepository<MemberCardTheme>>(); }
        //}

        ///// <summary>
        ///// 会员卡分组表
        ///// </summary>
        //ICardThemeGroupService CardThemeGroupService
        //{
        //    get { return ServiceLocator.Instance.GetService<ICardThemeGroupService>(); }
        //}
        //IRepository<CardThemeGroup> CardThemeGroupRepo
        //{
        //    get { return UnitOfWork.GetRepository<IRepository<CardThemeGroup>>(); }
        //}

        ///// <summary>
        ///// 时间段
        ///// </summary>
        //ICardThemeGroupUseTimeService CardThemeGroupUseTimeService
        //{
        //    get { return ServiceLocator.Instance.GetService<ICardThemeGroupUseTimeService>(); }
        //}
        //IRepository<CardThemeGroupUseTime> CardThemeGroupUseTimeRepo
        //{
        //    get { return UnitOfWork.GetRepository<IRepository<CardThemeGroupUseTime>>(); }
        //}

        //IMemberGroupService MemberGroupService => ServiceLocator.Instance.GetService<IMemberGroupService>();

        //IRepository<RechargePlan> RechargePlanRepo
        //{
        //    get { return UnitOfWork.GetRepository<IRepository<RechargePlan>>(); }
        //}

        //IMemberGiftCardService MemberGiftCardService
        //{
        //    get { return ServiceLocator.Instance.GetService<IMemberGiftCardService>(); }
        //}

        //#region SystemSettingService

        //ISystemSettingService SystemSettingService
        //{
        //    get { return ServiceLocator.Instance.GetService<ISystemSettingService>(); }
        //}

        //private IRepository<MemberCardType> _memberCardTypeRepo;

        //public IRepository<MemberCardType> MemberCardTypeRepo
        //{
        //    get
        //    {
        //        if (_memberCardTypeRepo == null)
        //        {
        //            _memberCardTypeRepo = this.UnitOfWork.GetRepository<IRepository<MemberCardType>>();
        //        }
        //        return _memberCardTypeRepo;
        //    }
        //}

        //#endregion SystemSettingService

        //IRepository<MemberGroup> MemberGroupRepo
        //{
        //    get { return UnitOfWork.GetRepository<IRepository<MemberGroup>>(); }
        //}

        //IMemberGradeService MemberGradeService { get => ServiceLocator.Instance.GetService<IMemberGradeService>(); }

        //IRepository<MemberGiftCard> MemberGiftCardRepo { get => UnitOfWork.GetRepository<IRepository<MemberGiftCard>>(); }

        //IRepository<MemberGrade> MemberGradeRepo { get => UnitOfWork.GetRepository<IRepository<MemberGrade>>(); }

        //IMemberGradeHistoryService MemberGradeHistoryService { get => ServiceLocator.Instance.GetService<IMemberGradeHistoryService>(); }

        //IRepository<TradeHistory> TradeHistoryRepo { get => ServiceLocator.Instance.GetService<IRepository<TradeHistory>>(); }

        //IRepository<MemberPointHistory> MemberPointHistoryRepo { get => ServiceLocator.Instance.GetService<IRepository<MemberPointHistory>>(); }

        #endregion properties

        protected override bool DoValidate(MemberCard entity)
        {
            var exists = this.Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID);
            if (exists)
                throw new DomainException(String.Format("卡号 {0} 已存在。", entity.Code));
            return true;
        }

        public override MemberCard Add(MemberCard entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            return base.Add(entity);
        }

        public override bool Update(MemberCard entity)
        {
            if (entity == null)
                return false;
            var memberCard = Repository.GetByKey(entity.ID);
            if (memberCard == null)
                throw new DomainException("修改失败，数据不存在");
            if (memberCard.CardBalance == 0 && memberCard.Status == ECardStatus.UnActivate)
            {
                memberCard.CardBalance = entity.CardBalance;
            }
            memberCard.ExpiredDate = entity.ExpiredDate?.AddDays(1).AddSeconds(-1);
            memberCard.Remark = entity.Remark;
            return base.Update(memberCard);
        }

        public override bool Delete(Guid key)
        {
            var entity = Repository.GetByKey(key);
            if (entity == null)
                throw new DomainException("数据不存在");
            entity.IsDeleted = true;
            return Repository.Update(entity) > 0;
        }

        public bool ReportOrCancelLoss(string cardNumber, ECardStatus cardStatus)
        {
            var card = Repository.GetQueryable().Where(p => p.Code == cardNumber).FirstOrDefault();
            if (card == null)
                throw new DomainException("未找到对应的会员卡信息");
            card.Status = cardStatus;
            return base.Update(card);
        }

        private bool RuleExists(string rule)
        {
            return Repository.Exists(p => p.Code.StartsWith(rule));
        }

        private string GenerateCode(string rule, int index)
        {
            return string.Format("{0}{1}", rule, index.ToString("0000"));
        }

        private string GenerateVerifyCode(Random r)
        {
            return r.Next(999999).ToString("000000");
        }

        /// <summary>
        /// 生成虚拟微信会员卡
        /// </summary>
        /// <returns></returns>
        public MemberCard GenerateVirtualCard()
        {
            string cardNumber = string.Empty;
            bool existNumber = false;
            for (var i = 0; i <= 20; i++)
            {
                cardNumber = MakeCodeRuleService.GenerateCode(MakeCodeTypes.WeChatMemberCard);
                existNumber = Repository.Exists(t => t.Code == cardNumber);
                if (!existNumber)
                    break;
            }
            if (existNumber)
            {
                throw new DomainException("生成卡号失败，请重试");
            }
            var newCard = new MemberCard
            {
                Code = cardNumber,
                VerifyCode = "123456",
                BatchCode = "WeChat",
                CardTypeID = MemberCardTypes.PrePaidCard,//微信默认储值卡
                Status = ECardStatus.Activated,
                EffectiveDate = DateTime.Now,
                CreatedUserID = AppContext.CurrentSession.UserID,
                CreatedUser = AppContext.CurrentSession.UserName,
                CreatedDate = DateTime.Now,
                IsVirtual = true,
            };
            return this.Add(newCard);
        }

        /// <summary>
        /// 校验是否可以充值
        /// </summary>
        /// <param name="rechargeInfo"></param>
        /// <returns></returns>
        public bool ValidateBeforeRecharge(RechargeInfoDto rechargeInfo)
        {
            if (rechargeInfo == null)
            {
                throw new DomainException("参数不正确");
            }
            if (rechargeInfo.RechargeAmount <= 0 || rechargeInfo.GiveAmount < 0)
            {
                throw new DomainException("金额错误");
            }
            if (!string.IsNullOrEmpty(rechargeInfo.OutTradeNo))//校验是否是重复充值回调
            {
                var sameOutTradeNoCount = RechargeHistoryService.Count(t => t.OutTradeNo == rechargeInfo.OutTradeNo);
                if (sameOutTradeNoCount > 0)
                {
                    throw new DomainException(string.Format("{0}已经交易，重复回调。", rechargeInfo.OutTradeNo));
                }
            }
            //var rechargePlan = RechargePlanService.Get(rechargeInfo.RechargePlanID);
            //if (rechargePlan == null || rechargePlan.IsAvailable == false
            //    || rechargePlan.EffectiveDate > DateTime.Now || rechargePlan.ExpiredDate < DateTime.Now)
            //{
            //    throw new DomainException("方案不存在或已失效，请重新选择");
            //}
            //if (rechargePlan.RechargeAmount != rechargeInfo.RechargeAmount
            //    || rechargePlan.GiveAmount != rechargeInfo.GiveAmount)
            //{
            //    throw new DomainException("方案金额数据不正确");
            //}
            var card = this.Repository.GetInclude(t => t.CardType)
                .Where(t => t.ID == rechargeInfo.CardID)
                .FirstOrDefault();
            if (card == null)
            {
                throw new DomainException("卡不存在");
            }
            //if (card.CardType != null && false == card.CardType.AllowRecharge)
            //{
            //    throw new DomainException("卡片不允许储值");
            //}
            //var tempCardBalance = card.CardBalance + (rechargeInfo.RechargeAmount + rechargeInfo.GiveAmount);
            //if (card.CardType != null && card.CardType.MaxRecharge.HasValue && tempCardBalance > card.CardType.MaxRecharge)
            //{
            //    throw new DomainException("超过卡片储值余额上限");
            //}
            //var member = MemberService.GetMemberByCardNumber(card.Code);
            //验证储值次数
            //if (rechargePlan.MaxRechargeCount > 0)
            //{
            //    int count = 0;
            //    if (member != null)
            //    {
            //        count = RechargeHistoryService.Count(p =>
            //            (p.CardID == rechargeInfo.CardID || p.MemberID == member.ID)
            //            && p.RechargePlanId == rechargePlan.ID);
            //    }
            //    else
            //    {
            //        count = RechargeHistoryService.Count(p => p.CardID == rechargeInfo.CardID && p.RechargePlanId == rechargePlan.ID);
            //    }
            //    if (count >= rechargePlan.MaxRechargeCount)
            //        throw new DomainException(string.Format("方案“{0}”只允许单个会员储值{1}次", rechargePlan.Name, rechargePlan.MaxRechargeCount));
            //}
            return true;
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="rechargeInfo">储值信息</param>
        /// <param name="tradeSource">交易来源</param>
        /// <returns></returns>
        public CardTradeResultDto Recharge(RechargeInfoDto rechargeInfo, ETradeSource tradeSource)
        {
            this.ValidateBeforeRecharge(rechargeInfo);
            //var rechargePlan = RechargePlanService.Get(rechargeInfo.RechargePlanID);
            var card = this.Repository.GetInclude(t => t.CardType)
                .Where(t => t.ID == rechargeInfo.CardID)
                .FirstOrDefault();
            var member = MemberService.GetMemberByCardNumber(card.Code);
            var result = new CardTradeResultDto()
            {
                CardNumber = card.Code,
                CardType = card.CardType != null ? card.CardType.Name : string.Empty,
                BeforeBalance = card.CardBalance
            };
            this.UnitOfWork.BeginTransaction();
            try
            {
                card.CardBalance += (rechargeInfo.RechargeAmount + rechargeInfo.GiveAmount);
                card.TotalRecharge += rechargeInfo.RechargeAmount;
                card.TotalGive += rechargeInfo.GiveAmount;
                this.Repository.Update(card);
                var history = new RechargeHistory
                {
                    CardID = rechargeInfo.CardID,
                    CardNumber = card.Code,
                    CardBalance = card.CardBalance,
                    TradeAmount = rechargeInfo.RechargeAmount,
                    GiveAmount = rechargeInfo.GiveAmount,
                    OutTradeNo = rechargeInfo.OutTradeNo,
                    CreatedUser = rechargeInfo.OperateUser,
                    PaymentType = rechargeInfo.PaymentType,
                    TradeSource = tradeSource,
                    //RechargePlanId = rechargePlan.ID,
                    TradeDepartmentID = AppContext.CurrentSession.DepartmentID,
                };
                var lastrechargehistory = RechargeHistoryRepo.GetQueryable(false).OrderByDescending(t => t.CreatedDate).FirstOrDefault();
                if (lastrechargehistory != null && lastrechargehistory.CreatedDate.Date != DateTime.Now.Date)
                {
                    MakeCodeRuleService.ResetCode(MakeCodeTypes.RechargeBill);
                }
                history.TradeNo = MakeCodeRuleService.GenerateCode(MakeCodeTypes.RechargeBill);
                if (member != null)
                {
                    history.MemberID = member.ID;
                    result.MemberName = member.Name;
                }
                if (card.CardTypeID != MemberCardTypes.GiftCard)
                {
                    RechargeHistoryService.Add(history);
                }
                this.UnitOfWork.CommitTransaction();
                result.TradeNo = history.TradeNo;
                result.TradeAmount = (rechargeInfo.RechargeAmount + rechargeInfo.GiveAmount);
                result.AfterBalance = card.CardBalance;
                result.TotalRecharge = card.TotalRecharge;
                result.TotalConsume = card.TotalConsume;
                if (card.CardTypeID != MemberCardTypes.GiftCard)
                {
                    RechargeNoticeToWeChat(rechargeInfo.RechargeAmount, member, result.AfterBalance);
                    //if (member != null)
                    //    MemberGradeService.UseMemberGradeRights(member.ID, false, rechargeInfo.RechargeAmount);
                }
                return result;
            }
            catch (Exception ex)
            {
                this.UnitOfWork.RollbackTransaction();
                AppContext.Logger.Error("会员充值出现异常，", ex);
                throw ex;
            }
        }

        void RechargeNoticeToWeChat(decimal rechargeAmount, Member member, decimal afterBalance)
        {
            if (member == null || string.IsNullOrEmpty(member.WeChatOpenID))
                return;
            var templateId = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_MemberRecharge);
            if (string.IsNullOrEmpty(templateId))
                return;
            var message = new WeChatTemplateMessageDto
            {
                touser = member.WeChatOpenID,
                template_id = templateId,
                url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/MemberCard?mch={AppContext.CurrentSession.MerchantCode}",
                data = new System.Dynamic.ExpandoObject(),
            };
            message.data.first = new WeChatTemplateMessageDto.MessageData("您好，您的会员卡充值成功。");
            message.data.accountType = new WeChatTemplateMessageDto.MessageData("会员卡号");
            message.data.account = new WeChatTemplateMessageDto.MessageData(member.CardNumber);
            message.data.amount = new WeChatTemplateMessageDto.MessageData(rechargeAmount.ToString("f") + "元", "#FF4040");
            message.data.result = new WeChatTemplateMessageDto.MessageData("充值成功");
            message.data.remark = new WeChatTemplateMessageDto.MessageData($"当前余额：{afterBalance.ToString("f")}元");
            WeChatService.SendWeChatNotifyAsync(message);
        }

        /// <summary>
        /// 获取会员卡类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public MemberCardTypeDto GetCardType(string code)
        {
            return Repository.GetQueryable(false)
                .Where(t => t.Code == code)
                .Select(t => new MemberCardTypeDto()
                {
                    Name = t.CardType != null ? t.CardType.Name : string.Empty,
                    AllowStoreActivate = t.CardType != null ? t.CardType.AllowStoreActivate : true,
                })
                .FirstOrDefault();
        }

        /// <summary>
        /// 校验会员卡的有效性
        /// </summary>
        /// <param name="memberCard"></param>
        /// <returns></returns>
        public bool VerifyCode(MemberCardFilter memberCard)
        {
            if (!string.IsNullOrEmpty(memberCard.MobilePhoneNo))
            {
                var existmobileno = this.MemberService.Exists(m => m.MobilePhoneNo == memberCard.MobilePhoneNo);
                if (existmobileno)
                {
                    throw new DomainException("该手机号码已使用!");
                }
            }
            return Repository.Exists(p => p.Code == memberCard.Code && p.VerifyCode == memberCard.VerifyCode);
        }

        /// <summary>
        /// 根据卡号或者手机号码获取卡信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public MemberCardDto GetCardInfoByNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
                return null;
            var memberCard = this.Repository.GetInclude(t => t.CardType, false)
                .Where(t => t.Code == number)
                .FirstOrDefault();
            Member member = null;
            if (memberCard == null)
            {
                member = MemberService.GetMemberByMobilePhone(number);
                if (member == null)
                    return null;
                memberCard = member.Card;
            }
            else
            {
                member = MemberService.GetMemberByCardNumber(number);
            }
            var cardInfo = new MemberCardDto
            {
                CardID = memberCard.ID,
                CardNumber = memberCard.Code,
                CardTypeID = memberCard.CardType != null ? memberCard.CardType.ID.ToString() : string.Empty,
                CardType = memberCard.CardType != null ? memberCard.CardType.Name : string.Empty,
                AllowDiscount = memberCard.CardType != null ? memberCard.CardType.AllowDiscount : true,
                AllowRecharge = memberCard.CardType != null ? memberCard.CardType.AllowRecharge : true,
                CardStatus = memberCard.Status.GetDescription(),
                EffectiveDate = memberCard.EffectiveDate.HasValue ? memberCard.EffectiveDate.Value.ToDateString() : string.Empty,
                //ExpiredDate = memberCard.ExpiredDate.HasValue ? memberCard.ExpiredDate.Value.ToDateString() : string.Empty,
                CardBalance = memberCard.CardBalance,
                VerifyCode = memberCard.VerifyCode,
                IsVirtual = memberCard.IsVirtual,

            };
            if (memberCard.ExpiredDate.HasValue)
            {
                cardInfo.ExpiredDate = memberCard.ExpiredDate.Value.Date.AddDays(1).AddSeconds(-1).ToDateTimeString();
            }
            if (member != null)
            {
                cardInfo.MemberID = member.ID;
                cardInfo.MemberName = member.Name;
                cardInfo.MobilePhoneNo = member.MobilePhoneNo;
                cardInfo.Birthday = member.Birthday.HasValue ? member.Birthday.Value.ToDateString() : string.Empty;
                cardInfo.Sex = member.Sex.GetDescription();
                cardInfo.MemberGroup = member.MemberGroup == null ? "普通会员" : member.MemberGroup.Name;
                cardInfo.IsActivate = !string.IsNullOrEmpty(member.MobilePhoneNo);//&& !string.IsNullOrEmpty(member.WeChatOpenID);
                cardInfo.MemberPoint = member.Point;
                if (member.MemberGrade != null)
                {
                    cardInfo.MemberGrade = member.MemberGrade.Name;
                    cardInfo.IsAllowPointPayment = member.MemberGrade.IsAllowPointPayment;
                    cardInfo.PonitExchangeValue = member.MemberGrade.PonitExchangeValue;
                    cardInfo.DiscountRate = member.MemberGrade.DiscountRate;
                    //if (!member.MemberGrade.DiscountRate.HasValue)
                    //{
                    //    cardInfo.MemberDiscountRight = member.MemberGrade.GradeRights != null ? member.MemberGrade.GradeRights.Where(t => t.RightType == EGradeRightType.Discount).Select(t => new MemberPosRightDto
                    //    {
                    //        PosRightID = t.PosRightID,
                    //        PosRightCode = t.PosRightCode,
                    //        PosRightName = t.PosRightName,
                    //    }).ToList() : null;
                    //}
                    //cardInfo.MemberProductRight = member.MemberGrade.GradeRights != null ? member.MemberGrade.GradeRights.Where(t => t.RightType == EGradeRightType.Product).Select(t => new MemberPosRightDto
                    //{
                    //    PosRightID = t.PosRightID,
                    //    PosRightCode = t.PosRightCode,
                    //    PosRightName = t.PosRightName,
                    //}).ToList() : null;
                }
            }
            //if (memberCard.CardTypeID.ToString() == "00000000-0000-0000-0000-000000000003")
            //{
            //    var FindMemberGiftCard = MemberGiftCarRepo.GetQueryableAllData(false).Where(t => t.GiftCardID == memberCard.ID).FirstOrDefault();
            //    if (FindMemberGiftCard != null)
            //    {
            //        var MemberCardThemeID = FindMemberGiftCard.MemberCardThemeID;
            //        var FindMemberCardTheme = MemberCardThemeRepo.GetQueryableAllData(true).Where(t => t.ID == MemberCardThemeID).FirstOrDefault();
            //        if (FindMemberCardTheme != null)
            //        {
            //            var DataTimeList = new List<GiftCardUserTime>();
            //            var CardThemeGroupID = FindMemberCardTheme.CardThemeGroupID;
            //            var FindCardThemeGroupUseTime = CardThemeGroupUseTimeRepo.GetQueryableAllData(false).Where(t => t.TCardThemeGroupID == CardThemeGroupID).ToArray(); //时间
            //            if (FindCardThemeGroupUseTime != null)
            //            {
            //                for (var i = 0; i < FindCardThemeGroupUseTime.Length; i++)
            //                {
            //                    DataTimeList.Add(new GiftCardUserTime
            //                    {
            //                        BeginTime = FindCardThemeGroupUseTime[i].BeginTime,
            //                        EndTime = FindCardThemeGroupUseTime[i].EndTime,
            //                    });
            //                }
            //            }
            //            var FindCardThemeGroup = CardThemeGroupRepo.GetQueryableAllData(false).Where(t => t.ID == CardThemeGroupID).FirstOrDefault();
            //            if (FindCardThemeGroup != null)
            //            {
            //                cardInfo.UserCode = FindCardThemeGroup.DepartmentCode;
            //                cardInfo.UserGiftCardStartTime = FindCardThemeGroup.GiftCardStartTime;
            //                cardInfo.UserGiftCardEndTime = FindCardThemeGroup.GiftCardEndTime;
            //                cardInfo.UserIsAvailable = FindCardThemeGroup.IsAvailable;
            //                cardInfo.UserTimeSlot = FindCardThemeGroup.TimeSlot;
            //                cardInfo.UserWeek = FindCardThemeGroup.week;
            //                cardInfo.IsNotFixationDate = FindCardThemeGroup.IsNotFixationDate;
            //                cardInfo.EffectiveDaysOfAfterBuy = FindCardThemeGroup.EffectiveDaysOfAfterBuy;
            //                cardInfo.EffectiveDays = FindCardThemeGroup.EffectiveDays;
            //            }
            //            cardInfo.GiftCardUserTimeList = DataTimeList;
            //        }
            //    }
            //}
            return cardInfo;
        }

        /// <summary>
        /// 调整金额
        /// </summary>
        /// <param name="adjustBalanceDto"></param>
        /// <returns></returns>
        public bool AdjustBalance(AdjustBalanceDto adjustBalanceDto)
        {
            switch (adjustBalanceDto.AdjustType)
            {
                case EAdjustType.Decrease:
                    return DecreaseBalance(adjustBalanceDto);

                case EAdjustType.Increase:
                    return IncreaseBalance(adjustBalanceDto);

                default:
                    throw new DomainException("请选择正确的调整类别！");
            }
        }

        /// <summary>
        /// 增加余额
        /// </summary>
        /// <param name="adjustBalanceDto"></param>
        /// <returns></returns>
        private bool IncreaseBalance(AdjustBalanceDto adjustBalanceDto)
        {
            var card = GetCardByNumber(adjustBalanceDto.CardNumber);
            var member = MemberService.GetMemberByCardNumber(card.Code);
            UnitOfWork.BeginTransaction();
            try
            {
                var addResult = RechargeHistoryService.Add(new RechargeHistory
                {
                    Card = null,
                    CardBalance = card.CardBalance,
                    CardID = card.ID,
                    CardNumber = card.Code,
                    CreatedDate = DateTime.Now,
                    CreatedUser = AppContext.CurrentSession.UserName,
                    CreatedUserID = AppContext.CurrentSession.UserID,
                    DrawReceiptMoney = 0,
                    DrawReceiptDepartment = null,
                    DrawReceiptUser = null,
                    DrawReceiptUserId = null,
                    GiveAmount = adjustBalanceDto.AdjustBalance,
                    HasDrawReceipt = false,
                    ID = Util.NewID(),
                    IsDeleted = false,
                    Member = null,
                    MemberID = member.ID,
                    OutTradeNo = null,
                    PaymentType = EPaymentType.AdjustBalance,
                    RechargePlanId = null,
                    Remark = adjustBalanceDto.AdjustMark,
                    TradeAmount = 0,
                    TradeDepartment = null,
                    TradeDepartmentID = AppContext.CurrentSession.DepartmentID,
                    TradeSource = ETradeSource.Portal,
                    TradeNo = MakeCodeRuleService.GenerateCode(MakeCodeTypes.RechargeBill),
                    BusinessType = EBusinessType.AdjustBalance
                });

                card.CardBalance += adjustBalanceDto.AdjustBalance;
                card.TotalRecharge += adjustBalanceDto.AdjustBalance;
                Repository.Update(card);
                AdjustNoticeToWeChat(member.WeChatOpenID, true, adjustBalanceDto.AdjustBalance, card.CardBalance, adjustBalanceDto.AdjustMark);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                AppContext.Logger.Error("MemberCardService.IncreaseBalance 发生异常。", ex);
                UnitOfWork.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// 扣减余额
        /// </summary>
        /// <param name="adjustBalanceDto"></param>
        /// <returns></returns>
        private bool DecreaseBalance(AdjustBalanceDto adjustBalanceDto)
        {
            var card = GetCardByNumber(adjustBalanceDto.CardNumber);
            if (card.CardBalance < adjustBalanceDto.AdjustBalance)
                throw new DomainException(string.Format("减少金额不能大于当前余额"));

            var member = MemberService.GetMemberByCardNumber(card.Code);
            UnitOfWork.BeginTransaction();
            try
            {
                TradeHistoryService.Add(new TradeHistory
                {
                    Card = null,
                    CardBalance = card.CardBalance,
                    CardID = card.ID,
                    CardNumber = card.Code,
                    CreatedDate = DateTime.Now,
                    CreatedUser = AppContext.CurrentSession.UserName,
                    CreatedUserID = AppContext.CurrentSession.UserID,
                    ID = Util.NewID(),
                    IsDeleted = false,
                    Member = null,
                    MemberID = member.ID,
                    OutTradeNo = null,
                    Remark = adjustBalanceDto.AdjustMark,
                    TradeAmount = adjustBalanceDto.AdjustBalance,
                    TradeDepartment = null,
                    TradeDepartmentID = AppContext.CurrentSession.DepartmentID,
                    TradeSource = ETradeSource.Portal,
                    TradeNo = MakeCodeRuleService.GenerateCode(MakeCodeTypes.ConsumeBill),
                    BusinessType = EBusinessType.AdjustBalance,
                    ConsumeType = EConsumeType.CardBalance,
                    UseBalanceAmount = adjustBalanceDto.AdjustBalance,
                });

                card.CardBalance -= adjustBalanceDto.AdjustBalance;
                card.TotalConsume += adjustBalanceDto.AdjustBalance;
                Repository.Update(card);
                AdjustNoticeToWeChat(member.WeChatOpenID, false, adjustBalanceDto.AdjustBalance, card.CardBalance, adjustBalanceDto.AdjustMark);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                AppContext.Logger.Error("MemberCardService.DecreaseBalance 发生异常。", ex);
                UnitOfWork.RollbackTransaction();
                throw ex;
            }
        }

        public MemberCard GetCardByNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
                return null;
            //先查找会员卡号
            var memberCard = this.Repository.GetQueryable(false)
                .Where(t => t.Code == number)
                .FirstOrDefault();
            Member member = null;
            if (memberCard == null) //如果通过卡号找不到信息，再通过手机号码查找
            {
                member = MemberService.GetMemberByMobilePhone(number);
                if (member == null)
                    throw new DomainException("找不到对应的会员卡");
                memberCard = member.Card;
            }
            else
            {
                if (memberCard.ExpiredDate.HasValue)
                {
                    memberCard.ExpiredDate = memberCard.ExpiredDate.Value.Date.AddDays(1).AddSeconds(-1);
                }
            }
            return memberCard;
        }

        /// <summary>
        /// 发送微信调整通知
        /// </summary>
        /// <param name="weChatOpenID">接受的微信openID</param>
        /// <param name="isIncrease">是否增加</param>
        /// <param name="adjustAmount">调整金额</param>
        /// <param name="afterBalance">卡内余额</param>
        /// <param name="adjustRemark">调整原因</param>
        private void AdjustNoticeToWeChat(string weChatOpenID, bool isIncrease, decimal adjustAmount, decimal afterBalance, string adjustRemark)
        {
            if (string.IsNullOrEmpty(weChatOpenID))
                return;
            var templateId = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_MemberAdjust);
            if (string.IsNullOrEmpty(templateId))
                return;
            var message = new WeChatTemplateMessageDto
            {
                touser = weChatOpenID,
                template_id = templateId,
                url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/PerCenter?mch={AppContext.CurrentSession.MerchantCode}",
                data = new System.Dynamic.ExpandoObject(),
            };
            message.data.first = new WeChatTemplateMessageDto.MessageData("您的会员账户调整如下：");
            message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(DateTime.Now.ToDateTimeString());
            message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(isIncrease ? "增加" : "扣减");
            message.data.keyword3 = new WeChatTemplateMessageDto.MessageData($"{adjustAmount.ToString("f")}元", "#FF4040");
            message.data.keyword4 = new WeChatTemplateMessageDto.MessageData($"{afterBalance.ToString("f")}元");
            message.data.remark = new WeChatTemplateMessageDto.MessageData(adjustRemark);
            WeChatService.SendWeChatNotifyAsync(message);
        }

        public PagedResultDto<MemberCard> QueryData(MemberCardFilter filter)
        {
            var result = new PagedResultDto<MemberCard>();
            var queryable = Repository.GetQueryable(false);
            if (filter == null || filter.All)
            {
                result.TotalCount = queryable.Count();
                result.Items = queryable.ToArray();
                return result;
            }
            if (!string.IsNullOrEmpty(filter.Code))
            {
                queryable = queryable.Where(p => p.Code.Contains(filter.Code));
            }
            if (filter.Codes != null && filter.Codes.Count > 0)
            {
                queryable = queryable.Where(p => filter.Codes.Contains(p.Code));
            }
            if (!string.IsNullOrEmpty(filter.StartCode))
            {
                queryable = queryable.Where(p => string.Compare(p.Code, filter.StartCode) >= 0);
            }
            if (!string.IsNullOrEmpty(filter.EndCode))
            {
                queryable = queryable.Where(p => string.Compare(p.Code, filter.EndCode) <= 0);
            }
            if (!string.IsNullOrEmpty(filter.BatchCode))
            {
                queryable = queryable.Where(p => p.BatchCode.Contains(filter.BatchCode));
            }
            //if (filter.MemberGroupID.HasValue)
            //{
            //    queryable = queryable.Where(t => t.MemberGroupID == filter.MemberGroupID);
            //}
            //if (filter.MemberGradeID.HasValue)
            //    queryable = queryable.Where(t => t.MemberGradeID == filter.MemberGradeID);
            if (filter.CardStatus.HasValue)
                queryable = queryable.Where(p => p.Status == filter.CardStatus.Value);
            if (filter.CardTypeID.HasValue)
                queryable = queryable.Where(p => p.CardTypeID == filter.CardTypeID.Value);

            result.TotalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(p => p.Code).Skip(filter.Start.Value).Take(filter.Limit.Value);
            var items = queryable.ToArray();
            //items.ForEach(t =>
            //{
            //    if (t.MemberGroupID == null)
            //    {
            //        t.MemberGroupID = Guid.Empty;
            //    }
            //});
            result.Items = items;
            return result;
        }

        /// <summary>
        /// 预生成
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<MemberCard> PreGenerate(MemberCardFilter filter)
        {
            ValidatePreGenerate(filter);
            var rule = filter.GenerateRule.Trim(' ');
            if (rule.Length != 4)
                throw new DomainException("生成规则必须是四位");
            if (RuleExists(rule))
                throw new DomainException(string.Format("生成规则:{0} 已使用", rule));

            var date = DateTime.Now;
            var r = new Random();
            var result = new List<MemberCard>();
            for (int i = 1; i <= filter.Count.Value; i++)
            {
                result.Add(new MemberCard
                {
                    ID = Util.NewID(),
                    BatchCode = filter.BatchCode,
                    CardTypeID = filter.CardTypeID.Value,
                    Code = GenerateCode(rule, i),
                    CreatedDate = date,
                    CreatedUser = AppContext.CurrentSession.UserName,
                    CreatedUserID = AppContext.CurrentSession.UserID,
                    EffectiveDate = date,
                    //ExpiredDate = date.AddYears(100),
                    Status = ECardStatus.UnActivate,
                    VerifyCode = GenerateVerifyCode(r),
                    CardBalance = filter.CardBalance.Value,
                    //MemberGroupID = filter.MemberGroupID,
                    //MemberGradeID = filter.MemberGradeID,
                });
            }
            return result;
        }

        #region public

        ///// <summary>
        ///// 批量保存
        ///// </summary>
        ///// <param name="memberCards"></param>
        ///// <returns></returns>
        //public string BatchSave(IEnumerable<MemberCard> memberCards)
        //{
        //    var result = string.Empty;
        //    var firstCard = memberCards.First();
        //    var rule = firstCard.Code.Substring(0, 4);
        //    if (RuleExists(rule))
        //        throw new DomainException(string.Format("生成规则:{0} 已使用", rule));
        //    try
        //    {
        //        var oldBatchCode = firstCard.BatchCode;
        //        var batchCode = MakeCodeRuleService.GenerateCode(MakeCodeTypes.MemberCardBatchCode);
        //        if (oldBatchCode != batchCode)
        //        {
        //            result = batchCode;
        //        }
        //        foreach (var memberCard in memberCards)
        //        {
        //            memberCard.ID = Util.NewID();
        //            memberCard.BatchCode = batchCode;
        //        }

        //        UnitOfWork.BeginTransaction();
        //        memberCards.ForEach((p) => { p.TotalRecharge += p.CardBalance; });
        //        Repository.Add(memberCards);

        //        #region 生成会员卡时不再产生储值记录而是在激活的时候产生储值记录

        //        //foreach (var memberCard in memberCards)
        //        //{
        //        //    if (memberCard.CardBalance == 0m)
        //        //        continue;
        //        //    var now = DateTime.Now;
        //        //    RechargeHistoryService.Add(new RechargeHistory
        //        //    {
        //        //        Card = null,
        //        //        CardBalance = memberCard.CardBalance,
        //        //        CardID = memberCard.ID,
        //        //        CardNumber = memberCard.Code,
        //        //        CreatedDate = now,
        //        //        CreatedUser = AppContext.CurrentSession.UserName,
        //        //        CreatedUserID = AppContext.CurrentSession.UserID,
        //        //        GiveAmount = 0,
        //        //        ID = Guid.Empty,
        //        //        IsDeleted = false,
        //        //        Member = null,
        //        //        MemberID = null,
        //        //        OutTradeNo = null,
        //        //        PaymentType = EPaymentType.Initial,
        //        //        Remark = null,
        //        //        TradeAmount = memberCard.CardBalance,
        //        //        TradeDepartment = null,
        //        //        TradeDepartmentID = AppContext.CurrentSession.DepartmentID,
        //        //        TradeNo = MakeCodeRuleService.GenerateCode(MakeCodeTypes.RechargeBill),
        //        //        TradeSource = ETradeSource.Portal
        //        //    });
        //        //}

        //        #endregion 生成会员卡时不再产生储值记录而是在激活的时候产生储值记录

        //        UnitOfWork.CommitTransaction();
        //    }
        //    catch (Exception ex)
        //    {
        //        result = "error";
        //        UnitOfWork.RollbackTransaction();
        //        throw new DomainException(ex.Message);
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 校验是否可以激活
        ///// </summary>
        ///// <param name="info"></param>
        ///// <param name="clientType"></param>
        ///// <returns></returns>
        //public bool ValidateBeforeActivate(MembercardActivateInfo info, EClientType clientType)
        //{
        //    if (info == null)
        //        throw new DomainException("参数不正确");
        //    var card = Repository.GetInclude(t => t.CardType, false).Where(p => p.Code == info.Code && p.VerifyCode == info.VerifyCode).FirstOrDefault();
        //    if (card == null)
        //        throw new DomainException("指定的卡号不存在，或者校验码错误！");
        //    if (!string.IsNullOrEmpty(info.MobilePhoneNo) && !info.IsFromActivateCardWidthoutOpenId)
        //    {
        //        var mobilePhoneExist = MemberService.Exists(t => t.MobilePhoneNo == info.MobilePhoneNo);
        //        if (mobilePhoneExist)
        //            throw new DomainException("手机号码已绑定会员卡");
        //    }
        //    var member = MemberService.Query(p => p.CardNumber == card.Code).FirstOrDefault();
        //    if (clientType == EClientType.WeChat)
        //    {
        //        if (member != null && !string.IsNullOrEmpty(member.WeChatOpenID))
        //            throw new DomainException("卡片已经激活，不能再次激活！");
        //    }
        //    else
        //    {
        //        if (card.Status != ECardStatus.UnActivate)
        //            throw new DomainException("卡片已经激活，不能再次激活！");
        //    }
        //    if (clientType == EClientType.Portal && !card.CardType.AllowStoreActivate)
        //        throw new DomainException("卡片不允许门店激活");
        //    return true;
        //}

        ///// <summary>
        ///// 激活会员卡
        ///// </summary>
        ///// <param name="info"></param>
        ///// <returns></returns>
        //public bool Activate(MembercardActivateInfo info, EClientType clientType)
        //{
        //    var giftCard = Repository.GetInclude(t => t.CardType, false).Where(t => t.Code == info.Code && t.CardTypeID == MemberCardTypes.GiftCard).FirstOrDefault();
        //    if (giftCard != null)
        //    {
        //        if (giftCard.Status == ECardStatus.Activated)
        //            throw new DomainException("卡片已经激活，不能再次激活！");
        //        if (giftCard.Status == ECardStatus.Lost)
        //            throw new DomainException("卡片已挂失");
        //        if (!giftCard.CardType.AllowStoreActivate && clientType == EClientType.Portal)
        //            throw new DomainException($"礼品卡不允许门店激活");
        //        return GiftCardBatchActivate(new GiftCardBatchActivateDto
        //        {
        //            GiftCardIds = new List<Guid> { giftCard.ID },
        //            ExpiredDate = info.ExpiredDate,
        //        });
        //    }
        //    ValidateBeforeActivate(info, clientType);
        //    var card = Repository.GetQueryable().Where(p => p.Code == info.Code && p.VerifyCode == info.VerifyCode).FirstOrDefault();
        //    var member = MemberService.Query(p => p.CardNumber == card.Code).FirstOrDefault();
        //    if (!info.IsFromActivateCardWidthoutOpenId)
        //    {
        //        if (info.ExpiredDate.HasValue)
        //        {
        //            card.ExpiredDate = info.ExpiredDate.Value.AddDays(1).AddSeconds(-1);
        //        }
        //        card.Remark = info.Remark;
        //        card.Status = ECardStatus.Activated;
        //        card.EffectiveDate = DateTime.Now;
        //        card.LastUpdateDate = DateTime.Now;
        //        card.LastUpdateUserID = AppContext.CurrentSession.UserID;
        //        card.LastUpdateUser = AppContext.CurrentSession.UserName;
        //        UnitOfWork.BeginTransaction();
        //        try
        //        {
        //            Repository.Update(card);
        //            if (string.IsNullOrEmpty(info.MemberName))
        //                info.MemberName = string.Empty;
        //            if (member == null)
        //            {
        //                var memberGroup = MemberGroupRepo.GetQueryable(false);
        //                member = new Member
        //                {
        //                    ID = Util.NewID(),
        //                    Name = info.MemberName,
        //                    CardID = card.ID,
        //                    CardNumber = card.Code,
        //                    MobilePhoneNo = info.MobilePhoneNo,
        //                    CreatedDate = DateTime.Now,
        //                    Password = info.Password,
        //                    Sex = info.MemberSex,
        //                    Birthday = info.Birthday,
        //                    WeChatOpenID = info.WeChatOpenID,
        //                    MemberGroupID = memberGroup.Any(t => t.ID == card.MemberGroupID) ? card.MemberGroupID : null,
        //                };

        //                var gradePoint = 0;
        //                if (card.MemberGradeID.HasValue)
        //                {
        //                    var beforeMemberGradeID = member.MemberGradeID.GetValueOrDefault();
        //                    var memberGrade = MemberGradeRepo.GetQueryable(false).Where(t => t.ID == card.MemberGradeID).FirstOrDefault();
        //                    member.MemberGradeID = card.MemberGradeID;
        //                    gradePoint = memberGrade.GradePoint;
        //                    MemberService.Add(member);
        //                    MemberGradeHistoryService.Add(new MemberGradeHistory
        //                    {
        //                        MemberID = member.ID,
        //                        BeforeMemberGradeID = beforeMemberGradeID,
        //                        AfterMemberGradeID = memberGrade.ID,
        //                    });
        //                }
        //                else
        //                {
        //                    MemberService.Add(member);
        //                }
        //                try
        //                {
        //                    var setResult = MemberService.AdjustMemberPoint(member.ID, EMemberPointType.Register, 0);
        //                    if (!setResult)
        //                        AppContext.Logger.Error("会员注册送积分操作失败");

        //                    if (gradePoint > 0)
        //                    {
        //                        setResult = MemberService.AdjustMemberPoint(member.ID, EMemberPointType.MemberGradeUpgrade, gradePoint);
        //                        if (!setResult)
        //                            AppContext.Logger.Error("会员卡激活关联会员等级送积分操作失败");
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    AppContext.Logger.Error($"会员注册送积分操作失败,{ex.Message}");
        //                }
        //            }
        //            else
        //            {
        //                member.MobilePhoneNo = info.MobilePhoneNo;
        //                member.LastUpdateDate = DateTime.Now;
        //                member.LastUpdateUserID = AppContext.CurrentSession.UserID;
        //                member.LastUpdateUser = AppContext.CurrentSession.UserName;
        //                member.CardID = card.ID;
        //                member.CardNumber = info.Code;
        //                member.Password = info.Password;
        //                member.Name = info.MemberName;
        //                member.Sex = info.MemberSex;
        //                member.Birthday = info.Birthday;
        //                member.WeChatOpenID = info.WeChatOpenID;
        //                MemberService.Update(member);
        //            }
        //            //记录初始余额储值记录
        //            if (card.CardBalance > 0)
        //            {
        //                RechargeHistoryService.Add(new RechargeHistory
        //                {
        //                    Card = null,
        //                    CardBalance = card.CardBalance,
        //                    CardID = card.ID,
        //                    CardNumber = card.Code,
        //                    CreatedDate = DateTime.Now,
        //                    CreatedUser = AppContext.CurrentSession.UserName,
        //                    CreatedUserID = AppContext.CurrentSession.UserID,
        //                    GiveAmount = 0,
        //                    Member = null,
        //                    MemberID = member.ID,
        //                    OutTradeNo = null,
        //                    PaymentType = EPaymentType.Initial,
        //                    Remark = null,
        //                    TradeAmount = card.CardBalance,
        //                    TradeDepartment = null,
        //                    TradeDepartmentID = AppContext.CurrentSession.DepartmentID,
        //                    TradeNo = MakeCodeRuleService.GenerateCode(MakeCodeTypes.RechargeBill),
        //                    TradeSource = ETradeSource.Portal
        //                });
        //            }
        //            UnitOfWork.CommitTransaction();
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            UnitOfWork.RollbackTransaction();
        //            throw new DomainException("操作发生异常：" + ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        if (!string.IsNullOrEmpty(info.Password))//card.CardType.AllowRecharge
        //        {
        //            member.Password = info.Password;
        //        }
        //        member.LastUpdateDate = DateTime.Now;
        //        member.LastUpdateUserID = AppContext.CurrentSession.UserID;
        //        member.LastUpdateUser = AppContext.CurrentSession.UserName;
        //        member.WeChatOpenID = info.WeChatOpenID;
        //        MemberService.Update(member);

        //        return true;
        //    }
        //}

        //private bool CheckBuyGiftCard(GiftCardActivateInfo info)
        //{
        //    if (info == null || string.IsNullOrEmpty(info.OpenID) || info.RechargePlans == null || info.RechargePlans.Count < 1 || string.IsNullOrEmpty(info.OrderNo))
        //    {
        //        AppContext.Logger.Error("礼品卡绑定失败，信息缺失");
        //        throw new DomainException("礼品卡绑定失败，信息缺失");
        //    }
        //    //if (info.Count <= 0)
        //    //{
        //    //    AppContext.Logger.Error("购买的礼品卡数量为零");
        //    //    throw new DomainException("购买的礼品卡数量为零");
        //    //}
        //    var exists = MemberGiftCardRepo.Exists(t => t.OrderNo == info.OrderNo);
        //    if (exists)
        //    {
        //        AppContext.Logger.Error("一次支付只能购买一次，不能重复购买");
        //        throw new DomainException("一次支付只能购买一次，不能重复购买");
        //    }
        //    return true;
        //}

        //public GiftCardBindingResult GiftCardBinding(GiftCardActivateInfo info)
        //{
        //    var result = new GiftCardBindingResult();
        //    CheckBuyGiftCard(info);
        //    var now = DateTime.Now;
        //    var giftCardId = MemberCardTypes.GiftCard.ToString();
        //    DateTime? effectiveDate = null;
        //    DateTime? expiredDate = null;
        //    if (info.MemberCardThemeID.HasValue)
        //    {
        //        var cardTheme = MemberCardThemeRepo.GetInclude(t => t.CardThemeGroup, false).Where(t => t.ID == info.MemberCardThemeID).FirstOrDefault();
        //        if (cardTheme != null && cardTheme.CardThemeGroup != null)
        //        {
        //            if (cardTheme.CardThemeGroup.IsNotFixationDate)
        //            {
        //                effectiveDate = DateTime.Now.Date.AddDays(cardTheme.CardThemeGroup.EffectiveDaysOfAfterBuy);
        //                expiredDate = effectiveDate.Value.AddDays(cardTheme.CardThemeGroup.EffectiveDays);
        //            }
        //            else
        //            {
        //                effectiveDate = cardTheme.CardThemeGroup.GiftCardStartTime;
        //                expiredDate = cardTheme.CardThemeGroup.GiftCardEndTime;
        //            }
        //        }
        //    }

        //    UnitOfWork.BeginTransaction();
        //    try
        //    {
        //        foreach (var rechargeplan in info.RechargePlans)
        //        {
        //            var rechargePlan = RechargePlanRepo.GetQueryable(false).Where(t => t.IsAvailable && t.Code == rechargeplan.RechargePlanCode && t.EffectiveDate < now && t.ExpiredDate > now && t.MatchCardType.Contains(giftCardId)).FirstOrDefault();
        //            if (rechargePlan == null)
        //            {
        //                AppContext.Logger.Error("礼品卡绑定失败，找不到对应的储值方案");
        //                throw new DomainException("礼品卡绑定失败，找不到对应的储值方案");
        //            }

        //            var cardBalance = rechargePlan.RechargeAmount;
        //            var totalGive = (decimal)0;
        //            if (rechargePlan.PlanType == EPlanType.FullGive)
        //            {
        //                totalGive = rechargePlan.GiveAmount;
        //                cardBalance += rechargePlan.GiveAmount;
        //            }

        //            for (int i = 0; i < rechargeplan.Quantity; i++)
        //            {
        //                var card = GenerateGiftCard();
        //                if (card == null)
        //                {
        //                    AppContext.Logger.Error("生成礼品卡失败");
        //                    throw new DomainException("生成礼品卡失败");
        //                }
        //                card.CardBalance = cardBalance;
        //                card.TotalGive = totalGive;
        //                card.TotalRecharge = rechargePlan.RechargeAmount;
        //                card.EffectiveDate = effectiveDate.HasValue ? effectiveDate : card.EffectiveDate;
        //                card.ExpiredDate = expiredDate.HasValue ? expiredDate : card.ExpiredDate;
        //                Repository.Update(card);

        //                MemberGiftCardService.Add(new MemberGiftCard
        //                {
        //                    OpenID = info.OpenID,
        //                    GiftCardID = card.ID,
        //                    RechargePlanCode = rechargeplan.RechargePlanCode,
        //                    OrderNo = info.OrderNo,
        //                    MemberCardThemeID = info.MemberCardThemeID,
        //                });

        //                var history = new RechargeHistory
        //                {
        //                    CardID = card.ID,
        //                    CardNumber = card.Code,
        //                    CardBalance = card.CardBalance,
        //                    TradeAmount = card.CardBalance,
        //                    GiveAmount = 0,
        //                    OutTradeNo = info.OutTradeNo,
        //                    TradeNo = info.OrderNo,
        //                    CreatedUser = AppContext.CurrentSession.UserName,
        //                    PaymentType = EPaymentType.WeChat,
        //                    TradeSource = ETradeSource.WeChat,
        //                    RechargePlanId = null,
        //                    TradeDepartmentID = new Guid("00000000-0000-0000-0000-000000000001"),
        //                };
        //                RechargeHistoryService.Add(history);

        //            }

        //            result.Count += rechargeplan.Quantity;
        //            result.Amount += cardBalance;
        //        }
        //        UnitOfWork.CommitTransaction();
        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        UnitOfWork.RollbackTransaction();
        //        AppContext.Logger.Error($"{e.Message}");
        //        throw new DomainException($"{e.Message}");
        //    }
        //}

        //public bool GiftCardBatchActivate(GiftCardBatchActivateDto info)
        //{
        //    if (info == null || info.GiftCardIds == null || info.GiftCardIds.Count() < 1)
        //        throw new DomainException("请选择要激活的礼品卡");
        //    var giftCardType = MemberCardTypeRepo.GetQueryable(false).Where(t => t.ID == MemberCardTypes.GiftCard).FirstOrDefault();
        //    if (giftCardType == null)
        //        throw new DomainException("不存在礼品卡卡片类型");
        //    if (!giftCardType.AllowStoreActivate)
        //        throw new DomainException("礼品卡不允许门店激活");
        //    UnitOfWork.BeginTransaction();
        //    try
        //    {
        //        foreach (var id in info.GiftCardIds)
        //        {
        //            var card = Repository.GetQueryable().Where(t => t.ID == id).FirstOrDefault();
        //            if (card != null)
        //            {
        //                card.Status = ECardStatus.Activated;
        //                card.EffectiveDate = DateTime.Now;
        //                card.LastUpdateDate = DateTime.Now;
        //                card.LastUpdateUserID = AppContext.CurrentSession.UserID;
        //                card.LastUpdateUser = AppContext.CurrentSession.UserName;
        //                if (info.ExpiredDate.HasValue)
        //                    card.ExpiredDate = info.ExpiredDate.Value.AddDays(1).AddSeconds(-1);
        //                card.CardBalance = info.CardBalance;
        //                if (!string.IsNullOrEmpty(info.Remark))
        //                    card.Remark = info.Remark;
        //                Repository.Update(card);
        //            }
        //        }
        //        UnitOfWork.CommitTransaction();
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        UnitOfWork.RollbackTransaction();
        //        throw new DomainException($"激活礼品卡失败,{e.Message}");
        //    }
        //}

        ///// <summary>
        ///// 校验是否可以交易
        ///// </summary>
        ///// <param name="consumeInfo"></param>
        ///// <returns></returns>
        //public bool ValidateBeforeConsume(ConsumeInfoDto consumeInfo)
        //{
        //    return ValidateConsume(consumeInfo).Result;
        //}

        ///// <summary>
        ///// 校验是否可以交易
        ///// </summary>
        ///// <param name="consumeInfo"></param>
        ///// <returns></returns>
        //public (bool Result, MemberCard CardInfo, Member MemberInfo) ValidateConsume(ConsumeInfoDto consumeInfo)
        //{
        //    if (consumeInfo == null)
        //    {
        //        throw new DomainException("参数不正确");
        //    }
        //    var card = this.Repository.GetQueryable()
        //        .Where(t => t.Code == consumeInfo.CardNumber)
        //        .FirstOrDefault();
        //    if (card == null)
        //    {
        //        throw new DomainException("卡不存在");
        //    }
        //    if (card.Status != ECardStatus.Activated)
        //    {
        //        throw new DomainException(string.Format("卡片状态为 {0}, 不允许消费", card.Status.GetDescription()));
        //    }
        //    if (!card.EffectiveDate.HasValue || card.EffectiveDate.Value.Date > DateTime.Today)
        //    {
        //        throw new DomainException("卡片尚未生效");
        //    }
        //    if (card.ExpiredDate.HasValue && card.ExpiredDate.Value.Date < DateTime.Today)
        //    {
        //        throw new DomainException("卡片已过期, 截止日期：" + card.ExpiredDate.Value.ToDateString());
        //    }
        //    //if (consumeInfo.IsDiscount && !card.CardType.AllowDiscount)
        //    //{
        //    //    throw new DomainException("该卡不允许打折");
        //    //}
        //    if (card.CardBalance < consumeInfo.UseBalanceAmount)
        //    {
        //        throw new DomainException("余额不足");
        //    }
        //    var member = MemberRepo.GetInclude(t => t.OwnerGroup).Where(m => m.CardNumber == card.Code).FirstOrDefault();
        //    if (member == null && card.CardTypeID != MemberCardTypes.GiftCard)
        //    {
        //        throw new DomainException("会员身份不存在，请重新激活");
        //    }
        //    if (member != null && member.Point < consumeInfo.UseMemberPoint)
        //        throw new DomainException("积分不足");
        //    if (consumeInfo.UseBalanceAmount > 0 || !string.IsNullOrEmpty(consumeInfo.MemberPassword))
        //    {
        //        if (card.CardTypeID == MemberCardTypes.GiftCard)
        //        {
        //            if (!string.IsNullOrEmpty(consumeInfo.MemberPassword) && !consumeInfo.MemberPassword.Equals(card.VerifyCode))
        //                throw new DomainException("验证码不正确");
        //        }
        //        else
        //        {
        //            var tradePassword = Util.EncryptPassword(card.Code, consumeInfo.MemberPassword);
        //            if (!tradePassword.Equals(member.Password))
        //            {
        //                throw new DomainException("密码不正确");
        //            }
        //        }
        //    }
        //    return (true, card, member);
        //}

        ///// <summary>
        ///// 消费
        ///// </summary>
        ///// <param name="consumeInfo"></param>
        ///// <returns></returns>
        //public CardTradeResultDto Consume(ConsumeInfoDto consumeInfo, ETradeSource tradeSource)
        //{
        //    var validateResult = ValidateConsume(consumeInfo);
        //    if (validateResult.Result == false)
        //        throw new DomainException("校验失败");
        //    var card = validateResult.CardInfo;
        //    var member = validateResult.MemberInfo;
        //    var tradeAmount = consumeInfo.TradeAmount;//consumeInfo.IsGiftCard ? consumeInfo.UseGiftCardBalanceAmount : (consumeInfo.UseBalanceAmount > 0 ? consumeInfo.UseBalanceAmount : consumeInfo.TradeAmount);
        //    var result = new CardTradeResultDto
        //    {
        //        CardNumber = card.Code,
        //        CardType = card.CardType != null ? card.CardType.Name : string.Empty,
        //        BeforeBalance = card.CardBalance,
        //        AfterBalance = consumeInfo.IsGiftCard ? card.CardBalance - consumeInfo.UseGiftCardBalanceAmount : card.CardBalance - consumeInfo.UseBalanceAmount,
        //        MemberName = member?.Name,
        //        TradeAmount = tradeAmount,
        //        MemberGroup = member?.OwnerGroup?.Name
        //    };
        //    this.UnitOfWork.BeginTransaction();
        //    try
        //    {
        //        if (consumeInfo.UseBalanceAmount > 0 && !consumeInfo.IsGiftCard)
        //        {
        //            card.CardBalance -= consumeInfo.UseBalanceAmount;
        //            card.TotalConsume += consumeInfo.UseBalanceAmount;
        //            this.Repository.Update(card);
        //        }
        //        else if (consumeInfo.UseGiftCardBalanceAmount > 0 && consumeInfo.IsGiftCard)
        //        {
        //            card.CardBalance -= consumeInfo.UseGiftCardBalanceAmount;
        //            card.TotalConsume += consumeInfo.UseGiftCardBalanceAmount;
        //            this.Repository.Update(card);
        //        }
        //        var history = new TradeHistory();

        //        if (consumeInfo.UseMemberPoint > 0)
        //        {
        //            MemberService.AdjustMemberPoint(member.ID, EMemberPointType.PosDeductionUse, -1 * consumeInfo.UseMemberPoint, consumeInfo.OutTradeNo);
        //        }
        //        history = new TradeHistory
        //        {
        //            CardID = card.ID,
        //            CardNumber = card.Code,
        //            CardBalance = card.CardBalance,
        //            TradeAmount = tradeAmount,
        //            OutTradeNo = consumeInfo.OutTradeNo,
        //            CreatedUser = consumeInfo.OperateUser,
        //            TradeSource = tradeSource,
        //            TradeNo = MakeCodeRuleService.GenerateCode(MakeCodeTypes.ConsumeBill),
        //            MemberID = member?.ID,
        //            ConsumeType = consumeInfo.UseBalanceAmount > 0 ? EConsumeType.CardBalance : EConsumeType.Discount,
        //            UseBalanceAmount = consumeInfo.IsGiftCard ? consumeInfo.UseGiftCardBalanceAmount : consumeInfo.UseBalanceAmount,
        //            MemberGradeID = member?.MemberGradeID,
        //        };
        //        TradeHistoryService.Add(history);

        //        if (member != null && !member.OwnerDepartmentID.HasValue)
        //        {
        //            member.OwnerDepartmentID = AppContext.CurrentSession.DepartmentID;
        //            MemberService.Update(member);
        //        }

        //        this.UnitOfWork.CommitTransaction();
        //        result.TradeNo = history.TradeNo;
        //        result.AfterBalance = card.CardBalance;
        //        result.TotalRecharge = card.TotalRecharge;
        //        result.TotalConsume = card.TotalConsume;

        //        //var useMemberPointPaymentFinishOrder = false;
        //        //if (card.CardTypeID != MemberCardTypes.GiftCard)
        //        //{
        //        //    var previewUsePointPaymentResult = MemberService.PreviewUsePointPayment(card.Code, tradeAmount);
        //        //    useMemberPointPaymentFinishOrder = previewUsePointPaymentResult.ExchangeMoney >= tradeAmount;
        //        //}
        //        //if (consumeInfo.UseBalanceAmount == consumeInfo.TradeAmount || consumeInfo.UseGiftCardBalanceAmount == consumeInfo.TradeAmount || useMemberPointPaymentFinishOrder)
        //        if (consumeInfo.IsCheckOut)
        //        {
        //            if (card.CardTypeID != MemberCardTypes.GiftCard)
        //            {
        //                consumeInfo.PaymentDetail = $"余额支付{consumeInfo.UseBalanceAmount.ToString("0.##")}元";
        //            }
        //            else
        //            {
        //                if (consumeInfo.UseGiftCardBalanceAmount > 0 && consumeInfo.UseGiftCardBalanceAmount != consumeInfo.TradeAmount)
        //                    consumeInfo.PaymentDetail = $"礼品卡支付{consumeInfo.UseGiftCardBalanceAmount.ToString("0.##")}元";
        //                else
        //                    consumeInfo.PaymentDetail = "礼品卡支付";
        //            }
        //            if (consumeInfo.UseMemberPoint > 0)
        //                consumeInfo.PaymentDetail += $",积分抵扣{consumeInfo.UseMemberPoint}";

        //            if (card.CardTypeID != MemberCardTypes.GiftCard)
        //            {
        //                var useMemberGradeRightsResult = MemberGradeService.UseMemberGradeRights(member.ID, true, consumeInfo.TradeAmount);
        //                ConsumeNoticeToWeChat(consumeInfo.TradeAmount, member, result.AfterBalance, consumeInfo.PaymentDetail, consumeInfo.UseBalanceAmount > 0, useMemberGradeRightsResult.GiftPoint);
        //                if (useMemberGradeRightsResult.IsUpGrade)
        //                    UpGradeNoticeToWeChat(member, useMemberGradeRightsResult);
        //            }
        //            else
        //            {
        //                var memberGiftCard = MemberGiftCardRepo.GetQueryable(false).Where(t => t.GiftCardID == card.ID).FirstOrDefault();
        //                if (memberGiftCard != null)
        //                    ConsumeNoticeToWeChat(consumeInfo.TradeAmount, new Member { WeChatOpenID = memberGiftCard.OpenID }, result.AfterBalance, consumeInfo.PaymentDetail, consumeInfo.UseGiftCardBalanceAmount > 0, 0, true);
        //            }
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        AppContext.Logger.Error("MemberCardService.Consume 发生异常。", ex);
        //        this.UnitOfWork.RollbackTransaction();
        //        throw ex;
        //    }
        //}

        //public List<CardTradeResultDto> ConsumeByBatchCard(List<ConsumeInfoDto> consumeInfoList, ETradeSource tradeSource)
        //{
        //    var result = new List<CardTradeResultDto>();

        //    foreach (var info in consumeInfoList)
        //    {
        //        var item = Consume(info, tradeSource);
        //        if (item != null)
        //            result.Add(item);
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// 生成礼品卡
        ///// </summary>
        ///// <returns></returns>
        //public MemberCard GenerateGiftCard()
        //{
        //    string cardNumber = string.Empty;
        //    bool existNumber = false;
        //    for (var i = 0; i <= 20; i++)
        //    {
        //        cardNumber = MakeCodeRuleService.GenerateCode(MakeCodeTypes.WeChatMemberCard);
        //        existNumber = Repository.Exists(t => t.Code == cardNumber);
        //        if (!existNumber)
        //            break;
        //    }
        //    if (existNumber)
        //    {
        //        AppContext.Logger.Error("生成卡号失败，请联系管理员");
        //        return null;
        //    }
        //    var newCard = new MemberCard
        //    {
        //        Code = cardNumber,
        //        VerifyCode = "123456",
        //        BatchCode = "GiftCard",
        //        CardTypeID = MemberCardTypes.GiftCard,
        //        Status = ECardStatus.Activated,
        //        EffectiveDate = DateTime.Now,
        //        CreatedUserID = AppContext.CurrentSession.UserID,
        //        CreatedUser = AppContext.CurrentSession.UserName,
        //        CreatedDate = DateTime.Now,
        //        IsVirtual = true,
        //    };
        //    return this.Add(newCard);
        //}

        ///// <summary>
        ///// 换卡
        ///// </summary>
        ///// <param name="oldCardID">原卡ID</param>
        ///// <param name="newCardNumber">新卡卡号</param>
        ///// <param name="verifyCode">新卡校验码</param>
        ///// <returns></returns>
        //public MemberCard ChangeCard(Guid oldCardID, string newCardNumber, string verifyCode)
        //{
        //    var newCard = Repository.Get(p => p.Code == newCardNumber && p.VerifyCode == verifyCode);
        //    if (newCard == null)
        //        throw new DomainException("指定的卡号不存在，或者校验码错误！");
        //    if (newCard.Status != ECardStatus.UnActivate)
        //        throw new DomainException("卡片已经激活！");
        //    if (newCard.CardBalance != 0)
        //        throw new DomainException("指定的卡片有余额不允许换到该卡！");
        //    var oldCard = Repository.GetByKey(oldCardID);
        //    if (oldCard == null)
        //        throw new DomainException("原卡不存在！");
        //    if (newCard.CardTypeID == MemberCardTypes.DiscountCard && newCard.CardTypeID != oldCard.CardTypeID)
        //        throw new DomainException("不可以从储值卡换到折扣卡。");
        //    newCard.CardBalance = oldCard.CardBalance;
        //    newCard.Status = ECardStatus.Activated;
        //    newCard.LastUpdateDate = DateTime.Now;
        //    newCard.LastUpdateUserID = AppContext.CurrentSession.UserID;
        //    newCard.LastUpdateUser = AppContext.CurrentSession.UserName;
        //    oldCard.Status = ECardStatus.Lost;
        //    oldCard.CardBalance = 0;
        //    Repository.Update(oldCard);
        //    Repository.Update(newCard);
        //    return newCard;
        //}

        ///// <summary>
        ///// 校验卡片是否有效
        ///// </summary>
        ///// <param name="cardNumber"></param>
        ///// <param name="verifyCode"></param>
        ///// <returns></returns>
        ///// <exception cref="DomainException">卡号或检验码不能为空</exception>
        //public bool VerifyCard(string cardNumber, string verifyCode)
        //{
        //    if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(verifyCode))
        //        throw new DomainException("卡号或检验码不能为空");
        //    var memberCard = Repository.GetQueryable(false).Where(p => p.Code == cardNumber && p.VerifyCode == verifyCode).FirstOrDefault();
        //    if (memberCard == null)
        //    {
        //        return false;
        //    }
        //    var member = MemberRepo.GetQueryable(false).Where(t => t.CardID == memberCard.ID).FirstOrDefault();
        //    if (member == null)
        //    {
        //        return true;
        //    }
        //    return string.IsNullOrEmpty(member.MobilePhoneNo) && string.IsNullOrEmpty(member.WeChatOpenID);
        //}

        //public IEnumerable<MemberCardExportDto> FillGenerateEntities(IEnumerable<MemberCard> entities)
        //{
        //    var result = new List<MemberCardExportDto>();
        //    var memberGroupQueryable = MemberGroupRepo.GetQueryable(false);
        //    var memberCardTypes = MemberCardTypeRepo.GetQueryable()
        //        .Select(s => new
        //        {
        //            ID = s.ID,
        //            Name = s.Name
        //        }).ToArray();
        //    foreach (var item in entities)
        //    {
        //        var cardType = memberCardTypes.FirstOrDefault(f => f.ID == item.CardTypeID);
        //        item.CardType = new MemberCardType
        //        {
        //            ID = item.CardTypeID.HasValue ? item.CardTypeID.Value : Guid.Parse("00000000-0000-0000-0000-000000000000"),
        //            Name = cardType != null ? cardType.Name : string.Empty
        //        };
        //    }
        //    result = entities.MapTo<List<MemberCardExportDto>>();
        //    result.ForEach(t =>
        //    {
        //        var memberGroup = memberGroupQueryable.Where(m => m.ID == t.MemberGroupID).FirstOrDefault();
        //        if (memberGroup != null)
        //        {
        //            t.MemberGroup = memberGroup.Name;
        //        }
        //        else
        //        {
        //            t.MemberGroup = "普通会员";
        //        }
        //        var memberGrade = MemberGradeRepo.GetQueryable(false).Where(g => g.ID == t.MemberGradeID).FirstOrDefault();
        //        if (memberGrade != null)
        //            t.MemberGrade = memberGrade.Name;
        //        t.CardTypeDesc = t.CardType != null ? t.CardType.Name : string.Empty;
        //        if (t.CardType != null && t.CardType.ID == MemberCardTypes.GiftCard)
        //            t.URL = $"{AppContext.Settings.SiteDomain}/Mobile/GiftCardBinding?mch={AppContext.CurrentSession.CompanyCode}&CardCode={t.Code}";
        //        else
        //            t.URL = $"{AppContext.Settings.OnlinePayService}/JH.aspx?mch={AppContext.CurrentSession.CompanyCode}&c={t.Code}&v={t.VerifyCode}";
        //        t.SecurityCode = GenerateSecurityCode(t.Code);
        //    });
        //    return result;
        //}

        #endregion public

        private Random GetRandom()
        {
            var tick = DateTime.Now.Ticks;
            return new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
        }

        private string GenerateSecurityCode(string cardCode)
        {
            if (string.IsNullOrEmpty(cardCode))
                return string.Empty;
            var securityCode = string.Empty;
            foreach (var c in cardCode)
            {
                var item = (char)(c + 49);
                securityCode += item;
            }
            var random = GetRandom();
            for (var i = 0; i < 4; i++)
            {
                securityCode += (char)random.Next(97, 122);
            }
            return securityCode + cardCode;
        }

        private void ValidatePreGenerate(MemberCardFilter filter)
        {
            if (filter == null
               || filter.IsGenerate == null
               || string.IsNullOrEmpty(filter.GenerateRule)
               || string.IsNullOrEmpty(filter.BatchCode)
               || filter.Count == null
               || filter.CardTypeID == null
               || filter.CardBalance == null)
                throw new DomainException("参数不正确");

            var cardType = MemberCardTypeService.Get(filter.CardTypeID.Value);

            if (filter.CardTypeID == MemberCardTypes.GiftCard && filter.CardBalance > 0)
                throw new DomainException("礼品卡不允许设置初始余额，请设置初始余额为零");

            if (!cardType.AllowRecharge && filter.CardBalance > 0m && filter.CardTypeID != MemberCardTypes.GiftCard)
                throw new DomainException(string.Format("卡片类型“{0}”不允许储值，请将初始余额设为零。", cardType.Name));

            if (cardType.MaxRecharge.HasValue && cardType.MaxRecharge < filter.CardBalance.Value && filter.CardTypeID != MemberCardTypes.GiftCard)
                throw new DomainException(string.Format("初始余额不应大于储值上限。卡片类型“{0}”的储值上限为{1:f}。", cardType.Name, cardType.MaxRecharge));
        }

        #region private

        //void UpGradeNoticeToWeChat(Member member, UseMemberGradeRightsResult useMemberGradeRightsResult)
        //{
        //    if (member == null || string.IsNullOrEmpty(member.WeChatOpenID) || useMemberGradeRightsResult == null || string.IsNullOrEmpty(useMemberGradeRightsResult.UpGradeName))
        //        return;
        //    var templateId = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_UpGrade);
        //    if (string.IsNullOrEmpty(templateId))
        //        return;
        //    var message = new WeChatTemplateMessageDto
        //    {
        //        touser = member.WeChatOpenID,
        //        template_id = templateId,
        //        url = string.Format("{0}/member/MainIndex.aspx?openid={1}&companyCode={2}",
        //            AppContext.Settings.OnlinePayService,
        //            member.WeChatOpenID,
        //            AppContext.CurrentSession.CompanyCode),
        //        data = new System.Dynamic.ExpandoObject(),
        //    };
        //    message.data.first = new WeChatTemplateMessageDto.MessageData($"您已成功升级为{useMemberGradeRightsResult.UpGradeName}!");
        //    message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(DateTime.Now.ToDateTimeString());
        //    message.data.keyword2 = new WeChatTemplateMessageDto.MessageData("点击'详情'查看您的会员特权~");//useMemberGradeRightsResult.GradeRightsDesc
        //    WeChatService.SendWeChatNotifyAsync(message);
        //}

        ///// <summary>
        ///// 发送微信消费通知
        ///// </summary>
        ///// <param name="tradeAmount">交易金额</param>
        ///// <param name="member">会员信息</param>
        ///// <param name="afterBalance">卡片余额</param>
        ///// <param name="paymentType">支付方式</param>
        ///// <param name="showBalance">显示余额</param>
        //void ConsumeNoticeToWeChat(decimal tradeAmount, Member member, decimal afterBalance, string paymentType, bool showBalance, int giftPoint, bool isGiftCard = false)
        //{
        //    if (member == null || string.IsNullOrEmpty(member.WeChatOpenID))
        //        return;
        //    var templateId = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_MemberConsume);
        //    var message = new WeChatTemplateMessageDto
        //    {
        //        touser = member.WeChatOpenID,
        //        template_id = templateId,
        //        url = string.Format("{0}/member/MainIndex.aspx?openid={1}&companyCode={2}",
        //            AppContext.Settings.OnlinePayService,
        //            member.WeChatOpenID,
        //            AppContext.CurrentSession.CompanyCode),
        //        data = new System.Dynamic.ExpandoObject(),
        //    };
        //    if (!isGiftCard)
        //        message.data.first = new WeChatTemplateMessageDto.MessageData("尊敬的会员，您本次的消费信息如下：");
        //    else
        //        message.data.first = new WeChatTemplateMessageDto.MessageData("您本次的礼品卡消费信息如下：");
        //    message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(AppContext.CurrentSession.DepartmentName);
        //    message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(tradeAmount.ToString("f") + "元", "#FF4040");
        //    message.data.keyword3 = new WeChatTemplateMessageDto.MessageData(paymentType);
        //    message.data.keyword4 = new WeChatTemplateMessageDto.MessageData($"{giftPoint}（累计积分：{member.Point}）");
        //    message.data.keyword5 = new WeChatTemplateMessageDto.MessageData(DateTime.Now.ToDateTimeString());
        //    var remark = $"当前余额：{afterBalance.ToString("f")}元";//showBalance ? string.Empty : $"当前余额：{afterBalance.ToString("f")}元";
        //    message.data.remark = new WeChatTemplateMessageDto.MessageData(remark);
        //    WeChatService.SendWeChatNotifyAsync(message);
        //}

        ///// <summary>
        ///// 结账会员微信通知
        ///// </summary>
        ///// <param name="checkOutNotice"></param>
        ///// <returns></returns>
        //public bool CheckOutNoticeToWeChat(CheckOutNotice checkOutNotice)
        //{
        //    if (checkOutNotice == null)
        //        return false;
        //    var tradeHistory = TradeHistoryRepo.GetIncludes(false, "Member", "Card").Where(t => t.OutTradeNo == checkOutNotice.OutTradeNo).ToArray();
        //    foreach (var trade in tradeHistory)
        //    {
        //        try
        //        {
        //            var member = trade.Member;
        //            var card = trade.Card;
        //            var useMemberGradeRightsResult = new UseMemberGradeRightsResult();
        //            var paymentType = $"余额支付";
        //            if (card != null)
        //            {
        //                if (card.CardTypeID != MemberCardTypes.GiftCard)
        //                {
        //                    useMemberGradeRightsResult = MemberGradeService.UseMemberGradeRights(member.ID, true, checkOutNotice.TradeAmount);
        //                    var deductionUsePoint = -MemberPointHistoryRepo.GetQueryable(false).Where(t => t.MemberID == member.ID && t.OutTradeNo == checkOutNotice.OutTradeNo && t.Source == EMemberPointType.PosDeductionUse).GroupBy(t => 1).Select(t => t.Sum(s => s.Point)).FirstOrDefault();

        //                    if (deductionUsePoint > 0)
        //                        paymentType += $"{trade.UseBalanceAmount.ToString("0.##")}元,积分抵扣{deductionUsePoint}";
        //                    else
        //                        paymentType += $"{trade.UseBalanceAmount.ToString("0.##")}元";
        //                }
        //                else
        //                {
        //                    var memberGiftCard = MemberGiftCardRepo.GetQueryable(false).Where(t => t.GiftCardID == card.ID).FirstOrDefault();
        //                    member = new Member { WeChatOpenID = memberGiftCard.OpenID };
        //                    paymentType = $"礼品卡支付{trade.UseBalanceAmount.ToString("0.##")}元";
        //                }
        //            }
        //            member.Point += useMemberGradeRightsResult.GiftPoint;
        //            ConsumeNoticeToWeChat(checkOutNotice.TradeAmount, member, trade.CardBalance, paymentType, true, useMemberGradeRightsResult.GiftPoint);
        //            if (useMemberGradeRightsResult.IsUpGrade)
        //                UpGradeNoticeToWeChat(member, useMemberGradeRightsResult);
        //        }
        //        catch (Exception e)
        //        {
        //            AppContext.Logger.Error($"结账发送微信消费通知出现异常:{e.Message}");
        //        }
        //    }
        //    return true;
        //}

        //public bool BatchModifyRemark(BatchModifyRemarkDto modifyInfo)
        //{
        //    if (modifyInfo == null || modifyInfo.CardIds == null || modifyInfo.CardIds.Count() < 1)
        //        throw new DomainException("参数错误");
        //    var cards = Repository.GetQueryable().Where(t => modifyInfo.CardIds.Contains(t.ID)).ToArray();
        //    if (cards.Count() < 1)
        //        return false;
        //    cards.ForEach(t =>
        //    {
        //        t.Remark = modifyInfo.Remark;
        //    });
        //    return Repository.Update(cards) > 0;
        //}

        #endregion private
    }
}
