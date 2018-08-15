using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Common;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;
using YEF.Core.Export;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 卡券
    /// </summary>
    [RoutePrefix("api/Coupon")]
    public class CouponController : BaseApiController
    {
        /// <summary>
        /// 卡券
        /// </summary>
        /// <param name="couponService"></param>
        /// <param name="givenCouponRecordService"></param>
        public CouponController(ICouponService couponService, IGivenCouponRecordService givenCouponRecordService)
        {
            CouponService = couponService;
            GivenCouponRecordService = givenCouponRecordService;
        }

        ICouponService CouponService { get; set; }

        IGivenCouponRecordService GivenCouponRecordService { get; set; }

        /// <summary>
        /// 新增优惠券(领取优惠券)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<Coupon> Add(Coupon entity)
        {
            return SafeExecute(() =>
            {
                return CouponService.Add(entity);
            });
        }

        /// <summary>
        ///卡券报表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCouponReportData")]
        public PagedActionResult<CouponReportDto> GetCouponReportData([FromBody]CouponReportFilter filter)
        {
            return SafeGetPagedData<CouponReportDto>((result) =>
            {
                //if (!ModelState.IsValid)
                //{
                //    throw new DomainException("参数错误!");
                //}
                var data = this.CouponService.CouponReportData(filter);
                result.Data = data;
            });
        }

        /// <summary>
        ///下载卡券报表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ExportCouponReport")]
        public JsonActionResult<string> ExportCouponReport([FromBody]CouponReportFilter filter)
        {
            return SafeExecute(() =>
            {
                var exportHelper = new ExportHelper(new[]
                {
                    new ExportInfo("Date","日期"),
                    new ExportInfo("BrowseTime","浏览量"),
                    new ExportInfo("GetNumber","领取人数"),
                    new ExportInfo("GetTime","领取次数"),
                    new ExportInfo("VerificationNumber","核销人数"),
                    new ExportInfo("VerificationTime","核销次数"),
                });
                var data = new List<CouponReportExportDto>();
                if (filter.TimeAvailable)
                {
                    data = this.CouponService.ExportCouponReport(filter).ToList();
                }
                var res = exportHelper.Export(data);
                this.CouponService.AddTitleToExcel(filter, res, false);
                return res;
            });
        }

        /// <summary>
        ///下载卡券整体报表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ExportCouponTotalReport")]
        public JsonActionResult<string> ExportCouponTotalReport([FromBody]CouponReportFilter filter)
        {
            return SafeExecute(() =>
            {
                var exportHelper = new ExportHelper(new[]
                {
                    new ExportInfo("Name","优惠券标题"),
                    new ExportInfo("UseCondition","使用条件"),
                    new ExportInfo("CreateDate","创建时间"),
                    new ExportInfo("CouponTemplateCode","优惠券模板编号"),
                    new ExportInfo("BrowseTimes","浏览量"),
                    new ExportInfo("PutInDate","投放时间"),
                    new ExportInfo("GetNumber","领取人数"),
                    new ExportInfo("GetTimes","领取数量"),
                    new ExportInfo("Validity","有效期"),
                    new ExportInfo("VerificationNumber","核销人数"),
                    new ExportInfo("VerificationTimes","核销次数"),
                    new ExportInfo("VerificationRate","核销率"),
                    new ExportInfo("Remark","备注")
                });
                var data = new List<CouponTotalReportDto>();
                //if (filter.TimeAvailable)
                //{
                filter.Start = null;
                filter.Limit = null;
                var totalCount = 0;
                data = this.CouponService.CouponTotalReportData(filter, ref totalCount).ToList();
                //}
                var res = exportHelper.Export(data);
                this.CouponService.AddTitleToExcel(filter, res, true);
                return res;
            });
        }

        /// <summary>
        ///整体报表数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCouponTotalReportData")]
        public PagedActionResult<CouponTotalReportDto> GetCouponTotalReportData([FromBody]CouponReportFilter filter)
        {
            return SafeGetPagedData<CouponTotalReportDto>((result) =>
            {
                var totalCount = 0;
                var data = this.CouponService.CouponTotalReportData(filter, ref totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 批量领取优惠券
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost, Route("BulkReceiveCoupon")]
        public JsonActionResult<bool> BulkReceiveCoupons(BulkReceiveCouponDto dto)
        {
            return SafeExecute(() =>
            {
                if (dto == null)
                {
                    throw new DomainException("参数错误");
                }
                else if (dto.Members == null)
                {
                    throw new DomainException("选择的会员无效");
                }
                else if (dto.CouponTemplateIDs == null)
                {
                    throw new DomainException("选择的卡劵无效");
                }
                foreach (var item in dto.Members)
                {
                    try
                    {
                        var couponids = CouponService.ReceiveCoupons(new ReceiveCouponDto
                        {
                            ReceiveOpenID = item.ReceiveOpenID,
                            NickName = item.NickName,
                            MobilePhoneNo = item.MobilePhoneNo,
                            CouponTemplateIDs = dto.CouponTemplateIDs
                        });
                    }
                    catch
                    {

                    }
                }
                return true;
            });
        }

        /// <summary>
        /// 领取卡券(带回CouponCode)
        /// </summary>
        /// <param name="receiveCouponDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ReceiveCouponsWidthCode")]
        public JsonActionResult<string> ReceiveCouponsWidthCode(ReceiveCouponDto receiveCouponDto)
        {
            return SafeExecute(() =>
            {
                var couponcodes = this.CouponService.ReceiveCouponsWidthCode(receiveCouponDto);
                return couponcodes.FirstOrDefault();
            });
        }

        /// <summary>
        /// 获取领取记录
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("GetCoupon")]
        public PagedActionResult<CouponDto> GetCoupon([FromUri]CouponFilter filter)
        {
            return SafeGetPagedData<CouponDto>((result) =>
            {
                var totalCount = 0;
                var data = CouponService.GetCoupon(filter, ref totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 领取优惠券
        /// </summary>
        /// <param name="receiveCouponDto"></param>
        /// <returns></returns>
        [HttpPost, Route("ReceiveCoupon"), AllowAnonymous]
        public JsonActionResult<Guid> ReceiveCoupons(ReceiveCouponDto receiveCouponDto)
        {
            return SafeExecute(() =>
            {
                var couponids = CouponService.ReceiveCoupons(receiveCouponDto);
                return couponids.FirstOrDefault();
            });
        }

        /// <summary>
        /// 获取卡券信息
        /// </summary>
        /// <param name="ctid"></param>
        /// <param name="userOpenID"></param>
        /// <returns></returns>
        [HttpGet, Route("CouponInfo"), AllowAnonymous]
        public JsonActionResult<CouponFullInfoDto> GetCouponInfo(Guid ctid, string userOpenID)
        {
            return SafeExecute(() =>
            {
                return CouponService.GetCouponInfoByTemplateID(ctid, userOpenID);
            });
        }

        /// <summary>
        /// 获取卡券信息
        /// </summary>
        /// <param name="couponID"></param>
        /// <returns></returns>
        [HttpGet, Route("CouponInfoByID"), AllowAnonymous]
        public JsonActionResult<CouponFullInfoDto> GetCouponInfoByID(Guid couponID, string userOpenID)
        {
            return SafeExecute(() =>
            {
                return CouponService.GetCouponInfo(couponID);
            });
        }

        /// <summary>
        /// 获取卡券信息
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        [HttpGet, Route("GetCouponInfoByCode"), AllowAnonymous]
        public JsonActionResult<CouponFullInfoDto> GetCouponInfoByCode(string couponCode)
        {
            return SafeExecute(() =>
            {
                return CouponService.GetCouponInfo(couponCode);
            });
        }

        /// <summary>
        /// 获取赠送卡券记录
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("GetGivenCouponRecord")]
        public PagedActionResult<GivenCouponRecord> GetGivenCouponRecord([FromUri]GivenCouponFilter filter)
        {
            return SafeGetPagedData<GivenCouponRecord>((result) =>
            {
                var totalCount = 0;
                var data = GivenCouponRecordService.GetGivenCouponRecord(filter, ref totalCount);
                result.TotalCount = totalCount;
                result.Data = data;
            });
        }

        /// <summary>
        /// 获取卡券适用门店信息
        /// </summary>
        /// <param name="ctid"></param>
        /// <returns></returns>
        [HttpGet, Route("CouponStoreInfo"), AllowAnonymous]
        public JsonActionResult<IEnumerable<CouponApplyStoreDto>> GetCouponApplyStoreInfo(Guid ctid)
        {
            return SafeExecute(() =>
            {
                return CouponService.GetCouponApplyStoreInfo(ctid);
            });
        }

        /// <summary>
        /// 获取卡券适用商品信息
        /// </summary>
        /// <param name="templateID"></param>
        /// <returns></returns>
        [HttpGet, Route("GetCouponApplyProductInfo"), AllowAnonymous]
        public PagedActionResult<CouponApplyProductDto> GetCouponApplyProductInfo(Guid templateID)
        {
            return SafeGetPagedData<CouponApplyProductDto>((result) =>
            {
                result.Data = CouponService.GetCouponApplyProductInfo(templateID);
                result.TotalCount = result.Data.Count();
            });
        }

        /// <summary>
        /// 获取用户可用卡券
        /// </summary>
        /// <param name="userOpenID"></param>
        /// <returns></returns>
        [HttpGet, Route("GetAvailableCouponList"), AllowAnonymous]
        public PagedActionResult<CouponBaseInfoDto> GetAvailableCouponList(string userOpenID, Guid? userid)
        {
            return SafeGetPagedData<CouponBaseInfoDto>((result) =>
            {
                var data = CouponService.GetAvailableCouponList(userOpenID, userid);
                result.Data = data;
            });
        }
    }
}
