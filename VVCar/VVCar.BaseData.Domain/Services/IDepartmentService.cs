using System;
using System.Collections.Generic;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;
using YEF.Core.Dtos;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Dtos;

namespace VVCar.BaseData.Domain.Services
{
    /// <summary>
    /// 门店 领域服务接口
    /// </summary>
    public partial interface IDepartmentService : IDomainService<IRepository<Department>, Department, Guid>
    {
        /// <summary>
        /// 门店查询
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        PagedResultDto<Department> QueryData(DepartmentFilter filter);

        /// <summary>
        /// 更具Code门店查询
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        PagedResultDto<Department> QueryCodeData(DepartmentFilter filter);

        /// <summary>
        /// 获取树型数据
        /// </summary>
        /// <param name="parentID">上级ID</param>
        /// <returns></returns>
        IEnumerable<DepartmentTreeDto> GetTreeData(Guid? parentID);

        /// <summary>
        /// RechargeHistoryDepartmentCmb加载门店名称 
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        IEnumerable<Department> GetDepartmentInfo();

        /// <summary>
        /// 创建商户店铺
        /// </summary>
        /// <param name="newStoreData"></param>
        /// <returns></returns>
        bool CreateMchStore(MchCreateStoreDto newStoreData);

        /// <summary>
        /// 获取门店列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<StoreInfoDto> GetStoreList();

        /// <summary>
        /// 获取门店Lite数据
        /// </summary>
        /// <returns></returns>
        IList<DepartmentLiteDto> GetDepartmentLiteData();

        /// <summary>
        /// 获取门店Lite数据
        /// </summary>
        /// <returns></returns>
        DepartmentLiteDto GetDepartmentLiteData(string deptCode);
    }
}
