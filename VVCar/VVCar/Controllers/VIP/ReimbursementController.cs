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
using VVCar.VIP.Services.DomainServices;
using YEF.Core;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 业务报销
    /// </summary>
    [RoutePrefix("api/Reimbursement")]
    public class ReimbursementController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="reimbursementService"></param>
        public ReimbursementController(IReimbursementService reimbursementService)
        {
            ReimbursementService = reimbursementService;
        }

        IReimbursementService ReimbursementService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="reimbursement"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonActionResult<Reimbursement> AddReimbursement(Reimbursement reimbursement)
        {
            return SafeExecute(() =>
            {
                return ReimbursementService.Add(reimbursement);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut, AllowAnonymous]
        public JsonActionResult<bool> Update(Reimbursement entity)
        {
            return SafeExecute(() =>
            {
                return ReimbursementService.Update(entity);
            });
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<bool> BatchDelete(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return ReimbursementService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost, Route("ApproveReimbursement"), AllowAnonymous]
        public JsonActionResult<bool> ApproveReimbursement(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return ReimbursementService.ApproveReimbursement(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 批量反审核
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost, Route("AntiApproveReimbursement"), AllowAnonymous]
        public JsonActionResult<bool> AntiApproveReimbursement(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return ReimbursementService.AntiApproveReimbursement(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<ReimbursementDto> Search([FromUri]ReimbursementFilter filter)
        {
            return SafeGetPagedData<ReimbursementDto>((result) =>
            {
                if (!ModelState.IsValid)
                    throw new DomainException("查询参数错误");
                var totalCount = 0;
                var data = ReimbursementService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
