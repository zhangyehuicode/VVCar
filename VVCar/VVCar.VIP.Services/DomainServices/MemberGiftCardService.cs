using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using VVCar.VIP.Domain;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    ///  会员礼品卡领域服务接口实现
    /// </summary>
    public class MemberGiftCardService : DomainServiceBase<IRepository<MemberGiftCard>, MemberGiftCard, Guid>, IMemberGiftCardService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberGiftCardService"/> class.
        /// </summary>
        public MemberGiftCardService() { }

        #region properties

        IRepository<MemberCard> MemberCardRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<MemberCard>>(); }
        }

        IRepository<MemberCardTheme> MemberCardThemeRepo
        {
            get => UnitOfWork.GetRepository<IRepository<MemberCardTheme>>();
        }

        IRechargeHistoryService RechargeHistoryService { get => ServiceLocator.Instance.GetService<IRechargeHistoryService>(); }

        IRepository<Department> DepartmentRepo { get => UnitOfWork.GetRepository<IRepository<Department>>(); }

        #endregion

        protected override bool DoValidate(MemberGiftCard entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.OpenID) || entity.CardID == null)
                return false;
            return true;
        }

        public override MemberGiftCard Add(MemberGiftCard entity)
        {
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            return base.Add(entity);
        }

        public IEnumerable<MemberCardThemeDto> GetMyMemberGiftCard(string openId)
        {
            var result = new List<MemberCardThemeDto>();
            var cardInfo = Repository.GetInclude(t => t.MemberCardTheme, false).Where(t => t.OpenID == openId).Select(t => new
            {
                GiftCardID = t.CardID,
                CardThemeImgUrl = t.MemberCardTheme.ImgUrl,
            }).ToArray();

            cardInfo.ForEach(item =>
            {
                var card = MemberCardRepo.GetQueryable(false).Where(t => item.GiftCardID == t.ID && t.Status == ECardStatus.Activated && t.CardBalance > 0).FirstOrDefault();
                if (card != null)
                {
                    var resItem = new MemberCardThemeDto();
                    resItem = card.MapTo<MemberCardThemeDto>();
                    resItem.CardThemeImgUrl = item.CardThemeImgUrl;
                    result.Add(resItem);
                }
            });

            return result;
        }

        public MemberCard GetGiftCardByNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
                throw new DomainException("参数错误");
            return MemberCardRepo.GetQueryable(false).Where(t => t.Code == number && t.CardTypeID == MemberCardTypes.GiftCard).FirstOrDefault();
        }

        public GiftCardDtoOfOW GetGiftCardOfOW(string number)
        {
            if (string.IsNullOrEmpty(number))
                throw new DomainException("参数错误");
            return MemberCardRepo.GetQueryable(false)
                    .Where(t => t.Code == number && t.CardTypeID == MemberCardTypes.GiftCard && t.Status != ECardStatus.UnActivate)
                    .Select(t => new GiftCardDtoOfOW
                    {
                        CardNumber = t.Code,
                        CardBalance = t.CardBalance,
                        ExpiredDate = t.ExpiredDate,
                    }).FirstOrDefault();
        }

        public bool GiftCardBinding(string openId, string cardCode)
        {
            if (string.IsNullOrEmpty(openId) || string.IsNullOrEmpty(cardCode))
                throw new DomainException("参数错误");
            var card = MemberCardRepo.GetQueryable(false).Where(t => t.Code == cardCode && !t.IsVirtual && t.CardTypeID == MemberCardTypes.GiftCard).FirstOrDefault();
            if (card == null)
                throw new DomainException("未找到对应的卡片");
            if (card.Status == ECardStatus.UnActivate)
                throw new DomainException("卡片未激活");
            if (card.Status == ECardStatus.Lost)
                throw new DomainException("卡片已挂失");
            var exists = Repository.Exists(t => t.CardID == card.ID);
            if (exists)
                throw new DomainException("卡片已绑定");
            Add(new MemberGiftCard
            {
                OpenID = openId,
                CardID = card.ID,
            });
            return true;
        }

        public bool GiftCardReceive(GiftCardReceiveDto giftCardReceive)
        {
            if (giftCardReceive == null || string.IsNullOrEmpty(giftCardReceive.OwnerOpenID) || string.IsNullOrEmpty(giftCardReceive.ReceiverOpenID) || giftCardReceive.GiftCardCodes == null || giftCardReceive.GiftCardCodes.Count < 1)
                throw new DomainException("参数错误");
            var exists = Repository.Exists(t => t.OpenID == giftCardReceive.OwnerOpenID);
            if (!exists)
                throw new DomainException("不存在赠送用户信息");
            var cardCount = Repository.GetQueryable(false).Where(t => t.OpenID == giftCardReceive.OwnerOpenID && giftCardReceive.GiftCardCodes.Contains(t.Card.Code)).Count();
            if (cardCount < giftCardReceive.GiftCardCodes.Count)
                throw new DomainException("礼品卡不存在或者已被领取");

            var giftCardCountOfZeroBalance = 0;
            foreach (var code in giftCardReceive.GiftCardCodes)
            {
                var card = MemberCardRepo.GetQueryable(false).Where(t => t.Code == code && t.CardTypeID == MemberCardTypes.GiftCard).FirstOrDefault();
                if (card != null && card.CardBalance == 0)
                    giftCardCountOfZeroBalance++;
            }
            if (giftCardCountOfZeroBalance == giftCardReceive.GiftCardCodes.Count)
                throw new DomainException("卡片已使用");

            UnitOfWork.BeginTransaction();
            try
            {
                var giftCards = Repository.GetQueryable(true).Where(t => t.OpenID == giftCardReceive.OwnerOpenID && giftCardReceive.GiftCardCodes.Contains(t.Card.Code)).ToArray();
                giftCards.ForEach(t =>
                {
                    t.OpenID = giftCardReceive.ReceiverOpenID;
                });
                Repository.Update(giftCards);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException($"礼品卡接收失败，{e.Message}");
            }
        }

        public bool BuyGiftCardByPos(BuyGiftCardByPosDto buyGiftCardByPosInfo)
        {
            if (buyGiftCardByPosInfo == null || buyGiftCardByPosInfo.Numbers == null || buyGiftCardByPosInfo.Numbers.Count < 1)
            {
                AppContext.Logger.Error("pos购买礼品卡回调，参数错误");
                return false;
            }
            if (string.IsNullOrEmpty(buyGiftCardByPosInfo.Description))
                buyGiftCardByPosInfo.Description = "已售";
            foreach (var number in buyGiftCardByPosInfo.Numbers)
            {
                try
                {
                    var card = MemberCardRepo.GetQueryable().Where(t => t.CardTypeID == MemberCardTypes.GiftCard && t.Code == number).FirstOrDefault();
                    if (card == null)
                        continue;
                    if (string.IsNullOrEmpty(card.Remark))
                        card.Remark = buyGiftCardByPosInfo.Description;
                    else
                        card.Remark += $"-{ buyGiftCardByPosInfo.Description}";
                    MemberCardRepo.Update(card);
                    var dept = DepartmentRepo.GetQueryable(false).Where(t => t.Code == buyGiftCardByPosInfo.DepartmentCode).FirstOrDefault();
                    var history = new RechargeHistory
                    {
                        CardID = card.ID,
                        CardNumber = card.Code,
                        CardBalance = card.CardBalance,
                        TradeAmount = card.CardBalance,
                        GiveAmount = 0,
                        TradeNo = buyGiftCardByPosInfo.OutTradeNo,
                        OutTradeNo = buyGiftCardByPosInfo.OutTradeNo,
                        CreatedUser = AppContext.CurrentSession.UserName,
                        PaymentType = buyGiftCardByPosInfo.PaymentType,
                        TradeDepartmentID = dept != null ? dept.ID : new Guid("00000000-0000-0000-0000-000000000001"),
                        TradeSource = ETradeSource.WeChat,
                        RechargePlanId = null,
                    };
                    RechargeHistoryService.Add(history);
                }
                catch (Exception e)
                {
                    AppContext.Logger.Error($"pos购买礼品卡回调出现异常，{e.Message}");
                }
            }
            return true;
        }

    }
}
