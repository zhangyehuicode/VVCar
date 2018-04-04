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
    /// 产品类别
    /// </summary>
    public interface IProductCategoryService : IDomainService<IRepository<ProductCategory>, ProductCategory, Guid>
    {
        /// <summary>
        /// 获取树型数据
        /// </summary>
        /// <param name="parentID">上级ID</param>
        /// <returns></returns>
        IEnumerable<ProductCategoryTreeDto> GetTreeData(Guid? parentID);

        /// <summary>
        /// 根据条件查询产品分类
        /// </summary>
        /// <param name="filter">keywords(分类名称，)</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns>产品分类数据集</returns>
        IEnumerable<ProductCategory> Search(ProductCategoryFilter filter, out int totalCount);

        /// <summary>
        /// 获取精简结构数据
        /// </summary>
        /// <returns></returns>
        IList<IDCodeNameDto> GetLiteData();
    }
}
