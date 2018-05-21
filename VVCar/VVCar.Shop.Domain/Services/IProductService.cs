using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Domain.Services
{
    /// <summary>
    /// 产品领域服务接口
    /// </summary>
    public interface IProductService : IDomainService<IRepository<Product>, Product, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        IEnumerable<Product> Search(ProductFilter filter, out int totalCount);

        /// <summary>
        /// 获取可上架产品
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductDto> GetProduct();

        /// <summary>
        /// 调整索引
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        bool AdjustIndex(AdjustIndexParam param);

        /// <summary>
        /// 更改发布状态
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        bool ChangePublishStatus(Guid id);

        /// <summary>
        /// 获取轻量型产品档案数据
        /// </summary>
        /// <returns></returns>
        IList<ProductCategoryLiteDto> GetProductLiteData();

        /// <summary>
        /// 获取推荐产品
        /// </summary>
        /// <returns></returns>
        IEnumerable<Product> GetRecommendProduct();

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <returns></returns>
        IEnumerable<Product> GetAppointmentProduct();
    }
}
