using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Services
{
    public interface IMemberCardService : IDomainService<IRepository<MemberCard>, MemberCard, Guid>
    {
        PagedResultDto<MemberCard> QueryData(MemberCardFilter filter);

        /// <summary>
        /// 预生成
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<MemberCard> PreGenerate(MemberCardFilter filter);

        ///// <summary>
        ///// 批量保存
        ///// </summary>
        ///// <param name="memberCards"></param>
        ///// <returns></returns>
        //string BatchSave(IEnumerable<MemberCard> memberCards);

        /// <summary>
        /// 校验卡的有效性
        /// </summary>
        /// <param name="memberCard"></param>
        /// <returns></returns>
        bool VerifyCode(MemberCardFilter memberCard);

        /// <summary>
        /// 获取会员卡类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        MemberCardTypeDto GetCardType(string code);

        ///// <summary>
        ///// 校验是否可以激活
        ///// </summary>
        ///// <param name="info"></param>
        ///// <param name="clientType"></param>
        ///// <returns></returns>
        //bool ValidateBeforeActivate(MembercardActivateInfo info, EClientType clientType);

        ///// <summary>
        ///// 激活会员卡
        ///// </summary>
        ///// <param name="info">激活信息</param>
        ///// <param name="clientType">客户端类型</param>
        ///// <returns></returns>
        //bool Activate(MembercardActivateInfo info, EClientType clientType);

        /// <summary>
        /// 根据卡号或者手机号码获取卡信息
        /// </summary>
        /// <param name="number">会员卡号或者手机号码</param>
        /// <returns></returns>
        MemberCardDto GetCardInfoByNumber(string number);

        /// <summary>
        /// 校验是否可以充值
        /// </summary>
        /// <param name="rechargeInfo"></param>
        /// <returns></returns>
        bool ValidateBeforeRecharge(RechargeInfoDto rechargeInfo);

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="rechargeInfo">储值信息</param>
        /// <param name="tradeSource">交易来源</param>
        /// <returns></returns>
        CardTradeResultDto Recharge(RechargeInfoDto rechargeInfo, ETradeSource tradeSource);

        ///// <summary>
        ///// 校验是否可以交易
        ///// </summary>
        ///// <param name="consumeInfo"></param>
        ///// <returns></returns>
        //bool ValidateBeforeConsume(ConsumeInfoDto consumeInfo);

        ///// <summary>
        ///// 消费交易
        ///// </summary>
        ///// <param name="consumeInfo">交易信息</param>
        ///// <param name="tradeSource">交易来源</param>
        ///// <returns></returns>
        //CardTradeResultDto Consume(ConsumeInfoDto consumeInfo, ETradeSource tradeSource);

        /// <summary>
        /// 生成虚拟微信会员卡
        /// </summary>
        /// <returns></returns>
        MemberCard GenerateVirtualCard();

        ///// <summary>
        ///// 换卡
        ///// </summary>
        ///// <param name="oldCardID">原卡ID</param>
        ///// <param name="newCardNumber">新卡卡号</param>
        ///// <param name="verifyCode">新卡校验码</param>
        ///// <returns></returns>
        //MemberCard ChangeCard(Guid oldCardID, string newCardNumber, string verifyCode);

        /// <summary>
        /// 调整金额
        /// </summary>
        /// <param name="adjustBalanceDto"></param>
        /// <returns></returns>
        bool AdjustBalance(AdjustBalanceDto adjustBalanceDto);

        ///// <summary>
        ///// 校验卡片是否有效
        ///// </summary>
        ///// <param name="cardNumber"></param>
        ///// <param name="verifyCode"></param>
        ///// <returns></returns>
        //bool VerifyCard(string cardNumber, string verifyCode);

        ///// <summary>
        ///// 填充预生成卡实体
        ///// </summary>
        ///// <param name="entities"></param>
        //IEnumerable<MemberCardExportDto> FillGenerateEntities(IEnumerable<MemberCard> entities);

        ///// <summary>
        ///// 礼品卡批量激活
        ///// </summary>
        ///// <param name="giftCardIds">The gift card ids.</param>
        ///// <param name="expiredDate">The expired date.</param>
        ///// <returns></returns>
        //bool GiftCardBatchActivate(GiftCardBatchActivateDto BatchActivateInfo);

        ///// <summary>
        ///// 礼品卡绑定
        ///// </summary>
        ///// <param name="info">The information.</param>
        ///// <returns></returns>
        //GiftCardBindingResult GiftCardBinding(GiftCardActivateInfo info);

        ///// <summary>
        ///// 批量修改备注
        ///// </summary>
        ///// <param name="modifyInfo">The modify information.</param>
        ///// <returns></returns>
        //bool BatchModifyRemark(BatchModifyRemarkDto modifyInfo);

        ///// <summary>
        ///// 消费
        ///// </summary>
        ///// <param name="consumeInfoList">The consume information list.</param>
        ///// <param name="tradeSource">The trade source.</param>
        ///// <returns></returns>
        //List<CardTradeResultDto> ConsumeByBatchCard(List<ConsumeInfoDto> consumeInfoList, ETradeSource tradeSource);

        /// <summary>
        /// 挂失或解挂
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <param name="cardStatus">The card status.</param>
        /// <returns></returns>
        bool ReportOrCancelLoss(string cardNumber, ECardStatus cardStatus);

        ///// <summary>
        ///// 结账会员微信通知
        ///// </summary>
        //bool CheckOutNoticeToWeChat(CheckOutNotice checkOutNotice);
    }
}
