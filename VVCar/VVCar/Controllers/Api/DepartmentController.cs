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
    /// 门店
    /// </summary>
    [RoutePrefix("api/Department")]
    public class DepartmentController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 门店
        /// </summary>
        /// <param name="deptService"></param>
        public DepartmentController(IDepartmentService deptService)
        {
            this.DepartmentService = deptService;
        }

        #endregion

        #region properties
        IDepartmentService DepartmentService { get; set; }
        #endregion

        /// <summary>
        /// 新增门店
        /// </summary>
        /// <param name="newDepartment">门店</param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<Department> AddDepartment(Department newDepartment)
        {
            return SafeExecute(() =>
            {
                return this.DepartmentService.Add(newDepartment);
            });
        }

        /// <summary>
        /// 删除门店
        /// </summary>
        /// <param name="id">门店ID</param>
        /// <returns></returns>
        [HttpDelete]
        public JsonActionResult<bool> DeleteDepartment(Guid id)
        {
            return SafeExecute(() =>
            {
                return this.DepartmentService.Delete(id);
            });
        }

        /// <summary>
        /// 更新门店
        /// </summary>
        /// <param name="department">门店</param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> UpdateDepartment(Department department)
        {
            return SafeExecute(() =>
            {
                return this.DepartmentService.Update(department);
            });
        }

        /// <summary>
        /// 获取门店数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonActionResult<Department> GetDepartment(Guid id)
        {
            return SafeExecute(() =>
            {
                return this.DepartmentService.Get(id);
            });
        }

        /// <summary>
        /// 查询门店
        /// </summary>
        /// <param name="filter">部门过滤条件</param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<Department> Search([FromUri]DepartmentFilter filter)
        {
            return SafeGetPagedData<Department>((result) =>
            {
                if (!ModelState.IsValid)//表示没有过滤参数成功匹配，判定为错误请求。
                {
                    throw new DomainException("查询参数错误。");
                }
                var pagedData = this.DepartmentService.QueryData(filter);
                result.Data = pagedData.Items;
                result.TotalCount = pagedData.TotalCount;
            });
        }

        /// <summary>
        /// 更具Code查询门店
        /// </summary>
        /// <param name="filter">部门过滤条件</param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous, Route("QueryCodeData")]
        public PagedActionResult<Department> QueryCodeData([FromUri]DepartmentFilter filter)
        {
            return SafeGetPagedData<Department>((result) =>
            {

                var pagedData = this.DepartmentService.QueryCodeData(filter);
                result.Data = pagedData.Items;
                result.TotalCount = pagedData.TotalCount;
            });
        }

        /// <summary>
        ///RechargeHistoryDepartmentCmb加载门店名称 
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]

        public JsonActionResult<IEnumerable<Department>> GetDepartmentInfo(string departmentName)
        {
            return SafeExecute(() =>
            {
                return this.DepartmentService.GetDepartmentInfo();
            });
        }

        /// <summary>
        /// 获取树型结构数据
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet, Route("GetTree/{parentId}")]
        public TreeActionResult<DepartmentTreeDto> GetTree(Guid? parentId)
        {
            return SafeGetTreeData(() =>
            {
                var deptTreeData = this.DepartmentService.GetTreeData(parentId);
                if (parentId.HasValue)
                    return deptTreeData;

                var rootDepartment = new DepartmentTreeDto()
                {
                    Text = "门店列表",
                    leaf = false,
                    expanded = true,
                };
                if (deptTreeData != null && deptTreeData.Count() > 0)
                {
                    rootDepartment.Children = deptTreeData;
                }
                return new DepartmentTreeDto[1] { rootDepartment };
            });
        }

        /// <summary>
        /// 获取门店列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("DepartmentList")]
        public JsonActionResult<IEnumerable<DepartmentLiteDto>> GetDepartmentLiteData()
        {
            return SafeExecute<IEnumerable<DepartmentLiteDto>>(() =>
            {
                return DepartmentService.GetDepartmentLiteData();
            });
        }

        /// <summary>
        /// 设置门店位置信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost, Route("SetDepartmentLocation"), AllowAnonymous]
        public JsonActionResult<bool> SetDepartmentLocation(DepartmentLocationDto param)
        {
            return SafeExecute(() =>
            {
                return DepartmentService.SetDepartmentLocation(param);
            });
        }

        /// <summary>
        /// 获取门店地理位置
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetDepartmentLocation"), AllowAnonymous]
        public JsonActionResult<DepartmentLocationDto> GetDepartmentLocation()
        {
            return SafeExecute(() =>
            {
                return DepartmentService.GetDepartmentLocation();
            });
        }
    }
}
