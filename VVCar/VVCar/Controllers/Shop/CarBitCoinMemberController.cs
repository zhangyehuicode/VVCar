﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
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
        public JsonActionResult<CarBitCoinMember> Register(CarBitCoinMemberRegister entity)
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

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<CarBitCoinMember> Search([FromUri]CarBitCoinMemberFilter filter)
        {
            return SafeGetPagedData<CarBitCoinMember>((result) =>
            {
                var totalCount = 0;
                var data = CarBitCoinMemberService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 查询车比特记录
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("SearchCarBitCoinRecord"), AllowAnonymous]
        public PagedActionResult<CarBitCoinRecordDto> SearchCarBitCoinRecord([FromUri]CarBitCoinRecordFilter filter)
        {
            return SafeGetPagedData<CarBitCoinRecordDto>((result) =>
            {
                var totalCount = 0;
                var data = CarBitCoinMemberService.SearchCarBitCoinRecord(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 赠送车比特
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost, Route("GiveAwayCarBitCoin")]
        public JsonActionResult<bool> GiveAwayCarBitCoin(GiveAwayCarBitCoinParam param)
        {
            return SafeExecute(() =>
            {
                return CarBitCoinMemberService.GiveAwayCarBitCoin(param);
            });
        }
    }
}
