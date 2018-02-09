using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 会员等级
    /// </summary>
    [RoutePrefix("api/MemberGrade")]
    public class MemberGradeController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 会员等级
        /// </summary>
        /// <param name="memberGradeService"></param>
        public MemberGradeController(IMemberGradeService memberGradeService)
        {
            this.MemberGradeService = memberGradeService;
        }

        #endregion

        #region properties
        IMemberGradeService MemberGradeService { get; set; }
        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<MemberGrade> Add(MemberGrade entity)
        {
            return SafeExecute(() =>
            {
                return MemberGradeService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(MemberGrade entity)
        {
            return SafeExecute(() =>
            {
                return MemberGradeService.Update(entity);
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public JsonActionResult<bool> Delete(Guid id)
        {
            return SafeExecute(() =>
            {
                return MemberGradeService.Delete(id);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("Search")]
        public PagedActionResult<MemberGrade> Search([FromUri]MemberGradeFilter filter)
        {
            return SafeGetPagedData<MemberGrade>((result) =>
            {
                var totalCount = 0;
                var data = MemberGradeService.Search(filter, out totalCount);
                result.TotalCount = totalCount;
                result.Data = data;
            });
        }

        /// <summary>
        /// 设置等级状态
        /// </summary>
        /// <param name="memberGradeID">The member grade identifier.</param>
        /// <param name="status">status</param>
        /// <returns></returns>
        [HttpPut, Route("{memberGradeID}/ChangeStatus/{status}")]
        public JsonActionResult<bool> ChangeStatus(Guid memberGradeID, EMemberGradeStatus status)
        {
            return SafeExecute(() =>
            {
                return MemberGradeService.ChangeStatus(memberGradeID, status);
            });
        }

        /// <summary>
        /// 设置开闭状态
        /// </summary>
        /// <param name="memberGradeID">The member grade identifier.</param>
        /// <param name="isNotOpen">if set to <c>true</c> [is not open].</param>
        /// <returns></returns>
        [HttpGet, Route("ChangeOpen")]
        public JsonActionResult<bool> ChangeOpen(Guid memberGradeID, bool isNotOpen)
        {
            return SafeExecute(() =>
            {
                return MemberGradeService.ChangeOpen(memberGradeID, isNotOpen);
            });
        }

        /// <summary>
        /// 检查会员降级
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("CheckGradeDegrade"), AllowAnonymous]
        public JsonActionResult<bool> CheckGradeDegrade()
        {
            return SafeExecute(() =>
            {
                MemberGradeService.CheckGradeDegrade();
                return true;
            });
        }
    }
}
