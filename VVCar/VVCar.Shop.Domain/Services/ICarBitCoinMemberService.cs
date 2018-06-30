using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
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

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<CarBitCoinMember> Search(CarBitCoinMemberFilter filter, out int totalCount);

        /// <summary>
        /// 计算马力
        /// </summary>
        /// <param name="mobilePhoneNo"></param>
        /// <returns></returns>
        int CalculateHorsepower(string mobilePhoneNo);

        /// <summary>
        /// 计算马力并保存
        /// </summary>
        /// <param name="carBitMemberId"></param>
        /// <returns></returns>
        bool CalculateHorsepowerSave(Guid carBitMemberId);

        /// <summary>
        /// 赠送车比特
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool GiveAwayCarBitCoin(GiveAwayCarBitCoinParam param);
    }
}
