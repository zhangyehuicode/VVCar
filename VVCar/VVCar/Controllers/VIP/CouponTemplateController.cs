using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;
using YEF.Utility;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 卡券模板
    /// </summary>
    [RoutePrefix("api/CouponTemplate")]
    public class CouponTemplateController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 卡券模板
        /// </summary>
        /// <param name="couponTemplateService"></param>
        public CouponTemplateController(ICouponTemplateService couponTemplateService)
        {
            CouponTemplateService = couponTemplateService;
        }

        #endregion ctor.

        #region properties

        ICouponTemplateService CouponTemplateService { get; set; }

        #endregion properties

        /// <summary>
        /// 新增卡券模板
        /// </summary>
        /// <param name="entity">卡券模板</param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<CouponTemplate> Add(CouponTemplate entity)
        {
            return SafeExecute(() =>
            {
                return CouponTemplateService.Add(entity);
            });
        }

        /// <summary>
        /// 更新卡券模板
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, Route("Update")]
        public JsonActionResult<bool> Update(CouponTemplate entity)
        {
            return SafeExecute(() =>
            {
                return CouponTemplateService.Update(entity);
            });
        }

        /// <summary>
        /// 删除卡券
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [HttpGet]
        [Route("Delete")]
        public JsonActionResult<bool> Delete(Guid id)
        {
            return SafeExecute(() =>
            {
                return this.CouponTemplateService.Delete(id);
            });
        }

        /// <summary>
        /// 更改卡券模板投放状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateStatus")]
        public JsonActionResult<bool> UpdateStatus(CouponTemplateDto entity)
        {
            return SafeExecute(() =>
            {
                return this.CouponTemplateService.UpdateStatus(entity);
            });
        }

        /// <summary>
        /// 更改卡券审核状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateAproveStatus")]
        public JsonActionResult<bool> UpdateAproveStatus(CouponTemplateDto entity)
        {
            return SafeExecute(() =>
            {
                return this.CouponTemplateService.UpdateAproveStatus(entity);
            });
        }

        /// <summary>
        /// 生成优惠券领取二维码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GenerateQRCode")]
        public JsonActionResult<string> GenerateQRCode(string url)
        {
            return SafeExecute(() =>
            {
                var bmp = QrHelper.Create(url);

                var fileName = string.Concat(DateTime.Now.ToString("yyyyMMddHHmmssfff"), ".png");
                var targetDir = Path.Combine(AppContext.PathInfo.RootPath, "Pictures", "CouponTemplateDeliveryQrCode");
                if (!Directory.Exists(targetDir))
                    Directory.CreateDirectory(targetDir);
                string targetPath = Path.Combine(targetDir, fileName);

                bmp.Save(targetPath);
                var result = Path.Combine(AppContext.Settings.SiteDomain, "Pictures", "CouponTemplateDeliveryQrCode", fileName);
                return result;
            });
        }

        /// <summary>
        /// 下载优惠券二维码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GenerateQRCodeFile")]
        public JsonActionResult<string> GenerateQRCodeFile(string url)
        {
            return SafeExecute(() =>
            {
                Application word = new Application();
                object nothing = Missing.Value;
                Document doc = word.Documents.Add();

                string imgName = Path.GetFileName(url);
                string imgPath = Path.Combine(AppContext.PathInfo.RootPath, "Pictures", "CouponTemplateDeliveryQrCode", imgName);

                var fileName = string.Concat(DateTime.Now.ToString("yyyyMMddHHmmssfff"), ".docx");
                var targetDir = Path.Combine(AppContext.PathInfo.RootPath, "Files", "CouponTemplateDeliveryQrCodeFiles");
                if (!Directory.Exists(targetDir))
                    Directory.CreateDirectory(targetDir);
                //word保存路径
                object targetPath = Path.Combine(targetDir, fileName);

                object range = doc.Paragraphs.Last.Range;
                object linkToFile = false;
                object saveWithDoc = true;
                doc.InlineShapes.AddPicture(imgPath, ref linkToFile, ref saveWithDoc, ref range);
                object format = WdSaveFormat.wdFormatDocumentDefault;
                doc.SaveAs(ref targetPath, ref format);

                doc.Close();
                word.Quit();

                string result = Path.Combine(AppContext.Settings.SiteDomain, "Files", "CouponTemplateDeliveryQrCodeFiles", fileName);

                return result;
            });
        }

        /// <summary>
        ///获取特定报表类型包含的优惠券
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<CouponTemplate> Query([FromUri]CouponTemplateFilter filter)
        {
            return SafeGetPagedData<CouponTemplate>((result) =>
            {
                var totalCount = 0;
                var data = this.CouponTemplateService.Query(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 根据id查询CouponTemplateDto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("GetCouponTemplateDto/{id}")]
        public JsonActionResult<CouponTemplateDto> GetCouponTemplateDto(Guid id)
        {
            return SafeExecute(() =>
            {
                return CouponTemplateService.GetCouponTemplateDto(id);
            });
        }

        /// <summary>
        ///获取优惠券模板信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCouponTemplateInfo"), AllowAnonymous]
        public PagedActionResult<CouponTemplateDto> GetCouponTemplateInfo([FromUri]CouponTemplateFilter filter)
        {
            return SafeGetPagedData<CouponTemplateDto>((result) =>
            {
                var totalCount = 0;
                var data = this.CouponTemplateService.CouponTemplateInfo(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        ///获取优惠券模板信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetValidCouponTemplateInfo")]
        public PagedActionResult<CouponTemplateDto> GetValidCouponTemplateInfo([FromUri]CouponTemplateFilter filter)
        {
            return SafeGetPagedData<CouponTemplateDto>((result) =>
            {
                var totalCount = 0;
                var data = this.CouponTemplateService.GetValidCouponTemplateInfo(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 获取推荐会员卡
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetRecommendCouponTemplate"), AllowAnonymous]
        public PagedActionResult<ProductDto> GetRecommendCouponTemplate()
        {
            return SafeGetPagedData<ProductDto>((result) =>
            {
                var data = CouponTemplateService.GetRecommendCouponTemplate();
                result.Data = data;
            });
        }

        /// <summary>
        /// 获取会员卡
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetCardOfCouponTemplate"), AllowAnonymous]
        public PagedActionResult<ProductDto> GetCardOfCouponTemplate()
        {
            return SafeGetPagedData<ProductDto>((result) =>
            {
                var data = CouponTemplateService.GetCardOfCouponTemplate();
                result.Data = data;
            });
        }

        /// <summary>
        /// 获取领券中心优惠券
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetCenterCouponTemplate"), AllowAnonymous]
        public PagedActionResult<CouponTemplate> GetCenterCouponTemplate()
        {
            return SafeGetPagedData<CouponTemplate>((result) =>
            {
                var data = CouponTemplateService.GetCenterCouponTemplate();
                result.Data = data;
            });
        }

        /// <summary>
        /// 获取游戏抽奖优惠券
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetGameCouponTemplate"), AllowAnonymous]
        public PagedActionResult<CouponTemplate> GetGameCouponTemplate()
        {
            return SafeGetPagedData<CouponTemplate>((result) =>
            {
                result.Data = CouponTemplateService.GetGameCouponTemplate();
            });
        }

        /// <summary>
        /// 更改卡券状态
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet, Route("ChangeApproveStatus"), AllowAnonymous]
        public JsonActionResult<bool> ChangeApproveStatus(Guid templateId, EApproveStatus status)
        {
            return SafeExecute(() =>
            {
                return CouponTemplateService.ChangeApproveStatus(templateId, status);
            });
        }

        /// <summary>
        /// 设置消费返积分比例
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        [HttpGet, Route("SetConsumePointRate")]
        public JsonActionResult<bool> SetConsumePointRate(Guid id, decimal rate)
        {
            return SafeExecute(() =>
            {
                return CouponTemplateService.SetConsumePointRate(id, rate);
            });
        }

        /// <summary>
        /// 设置股东卡消费返积分比例和折扣系数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="consumePointRate"></param>
        /// <param name="discountRate"></param>
        /// <returns></returns>
        [HttpGet, Route("SetConsumePointRateAndDiscountRate")]
        public JsonActionResult<bool> SetConsumePointRateAndDiscountRate(Guid id, decimal consumePointRate, decimal discountRate)
        {
            return SafeExecute(() =>
            {
                return CouponTemplateService.SetConsumePointRateAndDiscountRate(id, consumePointRate, discountRate);
            });
        }

        /// <summary>
        /// 小程序卡券设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("PutInApplet")]
        public JsonActionResult<bool> PutInApplet(Guid id)
        {
            return SafeExecute(() =>
            {
                return CouponTemplateService.PutInApplet(id);
            });
        }
    }
}
