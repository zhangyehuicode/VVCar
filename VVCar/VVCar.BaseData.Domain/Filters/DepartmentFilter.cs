using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Filters
{
    /// <summary>
    /// 门店过滤器
    /// </summary>
    public class DepartmentFilter : BasePageFilter
    {
        /// <summary>
        /// 部门编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 查询关键字，与Code、Name互斥
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 地区分区
        /// </summary>
        public string DistrictRegion { get; set; }

        /// <summary>
        /// 管理分区
        /// </summary>
        public string AdministrationRegion { get; set; }
    }
}
