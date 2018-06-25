using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// Coupon DomainService Interface
    /// </summary>
    public partial interface ICouponService : IDomainService<IRepository<Coupon>, Coupon, Guid>
    {
        /// <summary>
        /// 检查优惠券是否可以被使用
        /// </summary>
        /// <param name="coupon"></param>
        /// <param name="departmentCode"></param>
        /// <param name="verifyMode"></param>
        /// <param name="consumeMoney"></param>
        void CheckCoupon(Coupon coupon, string departmentCode, EVerificationMode verifyMode, decimal consumeMoney = 0);

        /// <summary>
        /// 领取卡券
        /// </summary>
        /// <param name="receiveCouponDto"></param>
        /// <param name="sendNotify">发送通知</param>
        IEnumerable<Guid> ReceiveCoupons(ReceiveCouponDto receiveCouponDto, bool sendNotify = false);

        /// <summary>
        /// 用户openId
        /// </summary>
        /// <param name="userOpenID"></param>
        /// <returns></returns>
        IEnumerable<CouponBaseInfoDto> GetAvailableCouponList(string userOpenID);

        /// <summary>
        /// 领券中心可用列表 
        /// </summary>
        /// <param name="userOpenId"></param>
        /// <param name="Cid">卡券类型</param>
        /// <returns></returns>
        IEnumerable<CouponBaseInfoDto> GetCenterAvailableCouponList(string userOpenId, ECouponType Cid);

        /// <summary>
        /// 获取卡券信息
        /// </summary>
        /// <param name="couponID"></param>
        /// <returns></returns>
        CouponFullInfoDto GetCouponInfoByTemplateID(Guid templateID, string openId);

        /// <summary>
        /// 获取卡券信息
        /// </summary>
        /// <param name="couponID"></param>
        /// <returns></returns>
        CouponFullInfoDto GetCouponInfo(Guid couponID);

        /// <summary>
        /// 获取卡券信息
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        CouponFullInfoDto GetCouponInfo(string couponCode);

        /// <summary>
        /// 获取卡券适用门店信息
        /// </summary>
        /// <param name="templateID"></param>
        /// <returns></returns>
        IEnumerable<CouponApplyStoreDto> GetCouponApplyStoreInfo(Guid templateID);

        /// <summary>
        /// 核销优惠券
        /// </summary>
        /// <param name="verifyDto">核销优惠券DTO</param>
        /// <returns></returns>
        bool VerifyCoupon(VerifyCouponDto verifyDto);

        /// <summary>
        /// 是否拥有优惠券
        /// </summary>
        /// <param name="userOpenID"></param>
        /// <param name="couponTemplateID"></param>
        /// <returns></returns>
        bool IsReceivedCoupon(string userOpenID, Guid couponTemplateID);

        /// <summary>
        /// 检查优惠券是否有效
        /// </summary>
        /// <param name="checkDto"></param>
        /// <returns></returns>
        CouponInfoDto CheckCoupon(CheckCouponDto checkDto);

        /// <summary>
        /// 优惠券报表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<CouponReportDto> CouponReportData(CouponReportFilter filter);

        /// <summary>
        ///导出卡券报表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<CouponReportExportDto> ExportCouponReport(CouponReportFilter filter);

        /// <summary>
        /// 整体报表数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<CouponTotalReportDto> CouponTotalReportData(CouponReportFilter filter, ref int totalCount);

        /// <summary>
        ///Excel添加标题行
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        void AddTitleToExcel(CouponReportFilter filter, string filePath, bool isTotalReport);

        /// <summary>
        /// 获取特殊优惠券
        /// </summary>
        /// <param name="departmentCode"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<CouponInfoDto> GetSpecialCoupon(string departmentCode, ref int totalCount);

        /// <summary>
        /// 领取卡券(带回CouponCode)
        /// </summary>
        /// <param name="receiveCouponDto"></param>
        /// <returns></returns>
        IEnumerable<string> ReceiveCouponsWidthCode(ReceiveCouponDto receiveCouponDto);

        /// <summary>
        /// 获取领取记录
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<CouponDto> GetCoupon(CouponFilter filter, ref int totalCount);

        /// <summary>
        ///发送即将过期的卡券通知 
        /// </summary>
        void SendCouponExpiredNotify();

        /// <summary>
        /// 赠送卡券
        /// </summary>
        /// <param name="couponGivenDto"></param>
        /// <returns></returns>
        bool GiveAwayCoupon(CouponGivenDto couponGivenDto);

        /// <summary>
        /// 获取优惠券状态信息
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        CouponStatusInfoDto GetCouponStatus(string couponCode);

        /// <summary>
        /// 领券中心领取、兑换优惠券
        /// </summary>
        /// <param name="receiveCouponDto"></param>
        /// <param name="sendNotify"></param>
        /// <returns></returns>
        bool CenterReceiveCoupon(ReceiveCouponDto receiveCouponDto, bool sendNotify = false);

        /// <summary>
        /// 执行领取卡券
        /// </summary>
        /// <param name="receiveCouponDto"></param>
        /// <param name="sendNotify"></param>
        /// <returns></returns>
        IEnumerable<Coupon> ReceiveCouponsAtcion(ReceiveCouponDto receiveCouponDto, bool sendNotify = false);
    }
}
