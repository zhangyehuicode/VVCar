using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 会员礼品卡领域服务接口
    /// </summary>
    public interface IMemberGiftCardService : IDomainService<IRepository<MemberGiftCard>, MemberGiftCard, Guid>
    {
        /// <summary>
        /// 获取礼品卡
        /// </summary>
        /// <param name="openId">The open identifier.</param>
        /// <returns></returns>
        IEnumerable<MemberCardThemeDto> GetMyMemberGiftCard(string openId);

        /// <summary>
        /// 关联实体卡
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="cardCode"></param>
        /// <returns></returns>
        bool GiftCardBinding(string openId, string cardCode);

        /// <summary>
        /// 礼品卡接收
        /// </summary>
        /// <param name="giftCardReceive">The gift card receive.</param>
        /// <returns></returns>
        bool GiftCardReceive(GiftCardReceiveDto giftCardReceive);

        /// <summary>
        /// 通过卡号获取礼品卡信息
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        MemberCard GetGiftCardByNumber(string number);

        /// <summary>
        /// 官网礼品卡查询
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        GiftCardDtoOfOW GetGiftCardOfOW(string number);

        /// <summary>
        /// pos购买礼品卡回调
        /// </summary>
        /// <param name="buyGiftCardByPosInfo">The buy gift card by position information.</param>
        /// <returns></returns>
        bool BuyGiftCardByPos(BuyGiftCardByPosDto buyGiftCardByPosInfo);
    }
}
