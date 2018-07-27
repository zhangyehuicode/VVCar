using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 门店分类
    /// </summary>
    [RoutePrefix("api/AgentDepartmentCategory")]
    public class AgentDepartmentCategoryController : BaseApiController
    {
        /// <summary>
        /// API 初始化
        /// </summary>
        /// <param name="agentDepartmentCategoryService"></param>
        public AgentDepartmentCategoryController(IAgentDepartmentCategoryService agentDepartmentCategoryService)
        {
            AgentDepartmentCategoryService = agentDepartmentCategoryService;
        }

        IAgentDepartmentCategoryService AgentDepartmentCategoryService { get; set; }

        /// <summary>
        /// 添加门店分类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<AgentDepartmentCategory> Add(AgentDepartmentCategory entity)
        {
            return SafeExecute(() => this.AgentDepartmentCategoryService.Add(entity));
        }

        /// <summary>
        /// 删除门店分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public JsonActionResult<bool> Delete(Guid id)
        {
            return SafeExecute(() =>
            {
                return AgentDepartmentCategoryService.Delete(id);
            });
        }

        /// <summary>
        /// 更新门店分类
        /// </summary>
        /// <param name="agentDepartmentCategory"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(AgentDepartmentCategory agentDepartmentCategory)
        {
            return SafeExecute(() =>
            {
                return AgentDepartmentCategoryService.Update(agentDepartmentCategory);
            });
        }

        /// <summary>
        /// 获取树形结构数据
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        [HttpGet, Route("GetTree")]
        public TreeActionResult<AgentDepartmentCategoryTreeDto> GetTree(Guid? parentID)
        {
            return SafeGetTreeData(() =>
            {
                var agentDepartmentCategories = AgentDepartmentCategoryService.GetTreeData(parentID);
                return agentDepartmentCategories;
            });
        }

        /// <summary>
        /// 获取门店分类列表, 用于门店分类选择下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("LiteData"), AllowAnonymous]
        public JsonActionResult<IList<IDCodeNameDto>> GetLiteData()
        {
            return SafeExecute(() =>
            {
                var categories = AgentDepartmentCategoryService.GetLiteData();
                return categories;
            });
        }

        /// <summary>
        /// 根据条件查询门店分类
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<AgentDepartmentCategory> Search([FromUri]AgentDepartmentCategoryFilter filter)
        {
            return SafeGetPagedData<AgentDepartmentCategory>((result) =>
            {
                int totalCount = 0;
                var data = AgentDepartmentCategoryService.Search(filter, out totalCount);
                result.TotalCount = totalCount;
                result.Data = data;
            });
        }
    }
}
