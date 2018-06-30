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
    /// 车比特产品领域服务接口
    /// </summary>
    public interface ICarBitCoinProductService : IDomainService<IRepository<CarBitCoinProduct>, CarBitCoinProduct, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<CarBitCoinProduct> Search(CarBitCoinProductFilter filter, out int totalCount);

        /// <summary>
        /// 获取可上架产品
        /// </summary>
        /// <returns></returns>
        IEnumerable<CarBitCoinProductDto> GetCarBitCoinProduct();

        /// <summary>
        /// 调整索引
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool AdjustIndex(AdjustIndexParam param);

        /// <summary>
        /// 更改发布状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ChangePublishStatus(Guid id);

        /// <summary>
        /// 获取轻量型产品档案数据
        /// </summary>
        /// <returns></returns>
        IList<CarBitCoinProductCategoryLiteDto> GetCarBitCoinProductLiteData();

        /// <summary>
        /// 获取推荐产品
        /// </summary>
        /// <returns></returns>
        IEnumerable<CarBitCoinProduct> GetRecommendCarBitCoinProduct();

        /// <summary>
        /// 获取引擎商品
        /// </summary>
        /// <returns></returns>
        IEnumerable<CarBitCoinProduct> GetEngineProduct();
    }
}
