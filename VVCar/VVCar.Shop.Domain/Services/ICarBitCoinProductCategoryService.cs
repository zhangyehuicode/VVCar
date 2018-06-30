using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Domain.Services
{
    /// <summary>
    /// 车比特产品类别
    /// </summary>
    public interface ICarBitCoinProductCategoryService : IDomainService<IRepository<CarBitCoinProductCategory>, CarBitCoinProductCategory, Guid>
    {
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        IEnumerable<CarBitCoinProductCategoryTreeDto> GetTreeData(Guid? parentID);

        /// <summary>
        /// 根据条件查询产品分类
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<CarBitCoinProductCategory> Search(CarBitCoinProductCategoryFilter filter, out int totalCount);

        /// <summary>
        /// 获取精简数据结构
        /// </summary>
        /// <returns></returns>
        IList<IDCodeNameDto> GetLiteData();
    }
}
