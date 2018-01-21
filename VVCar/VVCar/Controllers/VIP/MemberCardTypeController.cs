using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 卡片类型
    /// </summary>
    [RoutePrefix("api/MemberCardType")]
    public class MemberCardTypeController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 卡片类型
        /// </summary>
        /// <param name="cardTypeService"></param>
        public MemberCardTypeController(IMemberCardTypeService cardTypeService)
        {
            this.MemberCardTypeService = cardTypeService;
        }

        #endregion

        #region properties

        private IMemberCardTypeService MemberCardTypeService { get; set; }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="cardType">卡片类型</param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<MemberCardType> NewMemberCardType(MemberCardType cardType)
        {
            return SafeExecute(() =>
            {
                return this.MemberCardTypeService.Add(cardType);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="cardType">卡片类型</param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> UpdateMemberCardType(MemberCardType cardType)
        {
            return SafeExecute(() =>
            {
                return this.MemberCardTypeService.Update(cardType);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<MemberCardType> Search([FromUri]MemberCardTypeFilter filter)
        {
            return SafeGetPagedData<MemberCardType>((result) =>
            {
                if (!ModelState.IsValid)//表示没有过滤参数成功匹配，判定为错误请求。
                {
                    throw new DomainException("查询参数错误。");
                }
                int totalCount = 0;
                var pagedData = this.MemberCardTypeService.Search(filter, out totalCount);
                result.Data = pagedData;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 获取可用的储值方案
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("UsableTypes")]
        public JsonActionResult<IEnumerable<MemberCardTypeDto>> GetUsableTypes()
        {
            return SafeExecute(() =>
            {
                return this.MemberCardTypeService.GetUsableTypes();
            });
        }
    }
}
