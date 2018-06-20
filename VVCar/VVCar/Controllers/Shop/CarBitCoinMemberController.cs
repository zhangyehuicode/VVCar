using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Shop
{
    /// <summary>
    /// 车比特会员
    /// </summary>
    [RoutePrefix("api/CarBitCoinMember")]
    public class CarBitCoinMemberController : BaseApiController
    {
        public CarBitCoinMemberController(ICarBitCoinMemberService carBitCoinMemberService)
        {
            CarBitCoinMemberService = carBitCoinMemberService;
        }

        ICarBitCoinMemberService CarBitCoinMemberService { get; set; }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, Route("Register"), AllowAnonymous]
        public JsonActionResult<CarBitCoinMember> Register(CarBitCoinMember entity)
        {
            return SafeExecute(() =>
            {
                return CarBitCoinMemberService.Register(entity);
            });
        }

        /// <summary>
        /// 获取车比特会员通过OpenID
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        [HttpGet, Route("GetCarBitCoinMemberByOpenID"), AllowAnonymous]
        public JsonActionResult<CarBitCoinMember> GetCarBitCoinMemberByOpenID(string openId)
        {
            return SafeExecute(() =>
            {
                return CarBitCoinMemberService.GetCarBitCoinMemberByOpenID(openId);
            });
        }
    }
}
