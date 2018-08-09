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
    /// 会员分组
    /// </summary>
    [RoutePrefix("api/MemberGroup")]
    public class MemberGroupController : BaseApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberGroupController"/> class.
        /// </summary>
        /// <param name="memberGroupService">The member group service.</param>
        public MemberGroupController(IMemberGroupService memberGroupService)
        {
            MemberGroupService = memberGroupService;
        }

        IMemberGroupService MemberGroupService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<MemberGroup> Add(MemberGroup entity)
        {
            return SafeExecute(() =>
            {
                return MemberGroupService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(MemberGroup entity)
        {
            return SafeExecute(() =>
            {
                return MemberGroupService.Update(entity);
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
                return MemberGroupService.Delete(id);
            });
        }

        /// <summary>
        /// 获取会员分类列表，用户会员分类选择下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("LiteData")]
        public JsonActionResult<IList<IDCodeNameDto>> GetLiteData()
        {
            return SafeExecute(() =>
            {
                var categories = MemberGroupService.GetLiteData();
                return categories;
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<MemberGroup> Search([FromUri]MemberGroupFilter filter)
        {
            return SafeGetPagedData<MemberGroup>((result) =>
            {
                var totalCount = 0;
                var data = MemberGroupService.Search(filter, out totalCount);
                result.TotalCount = totalCount;
                result.Data = data;
            });
        }

        /// <summary>
        /// 获取树形结构数据
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        [HttpGet, Route("GetTree")]
        public TreeActionResult<MemberGroupTreeDto> GetTree(Guid? parentID)
        {
            return SafeGetTreeData(() =>
            {
                var memberGroups = MemberGroupService.GetTreeData(parentID);
                return memberGroups;
            });
        }

        /// <summary>
        /// 获取树形数据包含会员信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetTreeDataContainsMember")]
        public TreeActionResult<MemberGroupTreeDto> GetTreeDataContainsMember([FromUri]MemberGroupFilter filter)
        {
            return SafeGetTreeData(() =>
            {
                var data = MemberGroupService.GetTreeDataContainsMember(filter);
                return data;
            });
        }
    }
}
