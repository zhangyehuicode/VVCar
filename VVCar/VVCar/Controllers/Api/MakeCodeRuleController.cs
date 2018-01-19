using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 编码规则
    /// </summary>
    [RoutePrefix("api/MakeCodeRule")]
    public class MakeCodeRuleController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 编码规则
        /// </summary>
        /// <param name="makeCodeRuleService"></param>
        public MakeCodeRuleController(IMakeCodeRuleService makeCodeRuleService)
        {
            this.MakeCodeRuleService = makeCodeRuleService;
        }

        #endregion

        #region properties

        /// <summary>
        /// 编码规则 领域服务
        /// </summary>
        IMakeCodeRuleService MakeCodeRuleService { get; set; }

        #endregion

        /// <summary>
        /// 更新编码规则
        /// </summary>
        /// <param name="makeCodeRule">编码规则</param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> UpdateMakeCodeRule(MakeCodeRule makeCodeRule)
        {
            return SafeExecute(() =>
            {
                return this.MakeCodeRuleService.Update(makeCodeRule);
            });
        }

        /// <summary>
        /// 获取所有编码规则
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("AllData")]
        public JsonActionResult<IEnumerable<MakeCodeRule>> GetMakeCodeRules()
        {
            return SafeExecute<IEnumerable<MakeCodeRule>>(() =>
            {
                return this.MakeCodeRuleService.Query(null);
            });
        }

        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="codeType">类型</param>
        /// <returns></returns>
        [HttpGet, Route("GetCode/{codeType}")]
        public JsonActionResult<string> GetCode(string codeType)
        {
            return SafeExecute(() =>
            {
                return this.MakeCodeRuleService.GetCode(codeType);
            });
        }

        /// <summary>
        /// 生成编号
        /// </summary>
        /// <param name="codeType">类型</param>
        /// <returns></returns>
        [HttpGet, Route("GenerateCode/{codeType}")]
        public JsonActionResult<string> GenerateCode(string codeType)
        {
            return SafeExecute(() =>
            {
                return this.MakeCodeRuleService.GenerateCode(codeType);
            });
        }
    }
}
