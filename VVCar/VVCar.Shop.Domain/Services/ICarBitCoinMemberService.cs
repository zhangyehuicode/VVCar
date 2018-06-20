using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Domain.Services
{
    public interface ICarBitCoinMemberService : IDomainService<IRepository<CarBitCoinMember>, CarBitCoinMember, Guid>
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        CarBitCoinMember Register(CarBitCoinMember entity);

        /// <summary>
        /// 获取车比特会员通过OpenID
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        CarBitCoinMember GetCarBitCoinMemberByOpenID(string openId);

        /// <summary>
        /// 获取车比特会员通过手机号码
        /// </summary>
        /// <param name="mobilePhoneNo"></param>
        /// <returns></returns>
        CarBitCoinMember GetCarBitCoinMember(string mobilePhoneNo);

        /// <summary>
        /// 获取车比特会员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CarBitCoinMember GetCarBitCoinMember(Guid id);
    }
}
