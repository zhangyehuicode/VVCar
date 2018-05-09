using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 卡券模板 领域服务接口
    /// </summary>
    public partial interface ICouponTemplateService : IDomainService<IRepository<CouponTemplate>, CouponTemplate, Guid>
    {
        /// <summary>
        ///获取特定报表类型包含的优惠券
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<CouponTemplate> Query(CouponTemplateFilter filter, out int totalCount);

        /// <summary>
        /// 获取推荐会员卡
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductDto> GetRecommendCouponTemplate();

        /// <summary>
        /// 获取会员卡
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductDto> GetCardOfCouponTemplate();

        /// <summary>
        /// 按id查询CouponTemplateDto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CouponTemplateDto GetCouponTemplateDto(Guid id);

        /// <summary>
        ///获取优惠券模板信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<CouponTemplateDto> CouponTemplateInfo(CouponTemplateFilter filter, out int totalCount);

        /// <summary>
        /// 获取有效优惠券模板信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<CouponTemplateDto> GetValidCouponTemplateInfo(CouponTemplateFilter filter, out int totalCount);

        /// <summary>
        /// 更改卡券模板投放状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool UpdateStatus(CouponTemplateDto entity);

        /// <summary>
        /// 更改卡券审核状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool UpdateAproveStatus(CouponTemplateDto entity);

        /// <summary>
        /// 获取可以投放的优惠券模板列表
        /// </summary>
        /// <returns></returns>
        IList<CouponTemplateDto> GetCanDeliveryCouponTemplateList();

        /// <summary>
        /// 获取领券中心优惠券
        /// </summary>
        /// <returns></returns>
        IEnumerable<CouponTemplate> GetCenterCouponTemplate();
    }
}
