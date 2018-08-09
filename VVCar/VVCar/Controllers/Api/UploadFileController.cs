using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using VVCar.Models;
using YEF.Core;
using YEF.Utility;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 文件上传Api
    /// </summary>
    [RoutePrefix("api/UploadFile"), AllowAnonymous]
    public class UploadFileController : ApiController
    {
        static int _maxSizeLength = 1024 * 1024 * 10;

        /// <summary>
        /// 上传卡券图片
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("Coupon")]
        public UploadFileResult UploadFile()
        {
            var result = new UploadFileResult();
            if (HttpContext.Current.Request.Files.Count < 1)
            {
                result.errorMessage = "没有需要上传的文件";
                return result;
            }
            try
            {
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                if (file.ContentLength > _maxSizeLength)
                {
                    result.errorMessage = "不允许上传超过 10M 的文件";
                    return result;
                }
                var fileName = string.Concat("TEMP_", DateTime.Now.ToString("yyyyMMddHHmmssfff"), Path.GetExtension(file.FileName));
                var targetDir = Path.Combine(AppContext.PathInfo.RootPath, "Pictures/CouponImg");
                if (!Directory.Exists(targetDir))
                    Directory.CreateDirectory(targetDir);
                string targetPath = Path.Combine(targetDir, fileName);
                file.SaveAs(targetPath);
                result.OriginalFileName = file.FileName;
                result.FileName = fileName;
                result.FileUrl = $"/Pictures/CouponImg/{fileName}";
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.errorMessage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 上传积分生成设置分享图标
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("MemberPointShare")]
        public UploadFileResult UploadIcon()
        {
            return UploadAction("Pictures/MemberPointShareIcon");
        }

        /// <summary>
        /// 上传卡片主题图片
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadCardTheme")]
        public UploadFileResult UploadCardTheme()
        {
            return UploadAction("Pictures/MemberCardTheme");
        }

        /// <summary>
        /// 上传商品图片
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadProduct")]
        public UploadFileResult UploadPointGoods()
        {
            return UploadAction("Pictures/Product");
        }

        /// <summary>
        /// 上传图文消息封面图片
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadArticle")]
        public UploadFileResult UploadArticle()
        {
            return UploadAction("Pictures/Article");
        }

        /// <summary>
        /// 上传图文介绍图片
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadGraphicIntroduction")]
        public UploadFileResult UploadGraphicIntroduction()
        {
            return UploadAction("Pictures/GraphicIntroduction");
        }

        /// <summary>
        /// 上传车比特商品图片
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadCarBitCoinProduct")]
        public UploadFileResult UploadCarBitCoinProduct()
        {
            return UploadAction("Pictures/CarBitCoinProduct");
        }

        private string GetPRResult(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return "";
            var httpClient = new HttpClient();
            var url = $"{AppContext.Settings.PRDomain}/api/PlateRecognition?filename={filename}";
            var responseData = httpClient.GetStringAsync(url).Result;
            if (string.IsNullOrEmpty(responseData))
                return "";
            var result = JsonHelper.DeserializeObject<JsonResult<PRResult>>(responseData);
            return result.Data.License;
        }

        /// <summary>
        /// 微信公众号二维码图片
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadQRCode")]
        public UploadFileResult UploadQRCode()
        {
            return UploadAction("Pictures/QRCode");
        }

        /// <summary>
        /// 上传营业执照图片
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadLicense")]
        public UploadFileResult UploadLicense()
        {
            return UploadAction("Pictures/License");
        }

        /// <summary>
        /// 上传门店图片
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadDepartment")]
        public UploadFileResult UploadDepartment()
        {
            return UploadAction("Pictures/Department");
        }

        /// <summary>
        /// 上传身份证照片
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadIDCard")]
        public UploadFileResult UploadIDCard()
        {
            return UploadAction("Pictures/IDCard");
        }

        /// <summary>
        /// 上传车牌
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadPlate")]
        public void UploadPlate()
        {
            var result = UploadAction("Pictures/Plate");
            if (!string.IsNullOrEmpty(result.FileUrl) && result.FileUrl.Length > 1)
            {
                var prfilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, result.FileUrl.Substring(1));
                result.PRResult = GetPRResult(prfilename);
            }
            HttpContext.Current.Response.Write(JsonHelper.Serialize(result));
            HttpContext.Current.Response.End();
            //return result;
        }

        /// <summary>
        /// 上传软件使用课程
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadSuperClass")]
        public UploadFileResult UploadSuperClass()
        {
            return UploadAction("Video/SuperClass");
        }

        /// <summary>
        /// 上传商品落地课程
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadGoodsLandingClass")]
        public UploadFileResult UploadGoodsLandingClass()
        {
            return UploadAction("Video/GoodsLandingClass");
        }

        /// <summary>
        /// 上传会员数据
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadMemberExcel")]
        public UploadFileResult UploadMemberExcel()
        {
            return UploadAction(Path.Combine(AppContext.PathInfo.AppDataPath, "Upload/Excel/Member"));
        }

        /// <summary>
        /// 上传报销单
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadReimbursement")]
        public UploadFileResult UploadReimbursement()
        {
            return UploadAction("Pictures/Reimbursement");
        }

        private UploadFileResult UploadAction(string targetDirPath)
        {
            AppContext.Logger.Debug($"EnterUploadAction");
            var result = new UploadFileResult();
            AppContext.Logger.Debug($"HttpContext.Current.Request.Files.Count:{HttpContext.Current.Request.Files.Count}");
            if (HttpContext.Current.Request.Files.Count < 1)
            {
                result.errorMessage = "没有需要上传的文件";
                return result;
            }
            try
            {
                AppContext.Logger.Debug($"EnterUploadActionTry");
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                AppContext.Logger.Debug($"file.FileName:{file.FileName}");
                if (string.IsNullOrEmpty(file.FileName))
                {
                    result.errorMessage = "没有需要上传的文件";
                    return result;
                }
                if (file.ContentLength > _maxSizeLength)
                {
                    result.errorMessage = "不允许上传超过 10M 的文件";
                    return result;
                }
                var fileName = string.Concat(DateTime.Now.ToString("yyyyMMddHHmmssfff"), Path.GetExtension(file.FileName));
                var targetDir = Path.Combine(AppContext.PathInfo.RootPath, targetDirPath);

                if (!Directory.Exists(targetDir))
                    Directory.CreateDirectory(targetDir);
                string targetPath = Path.Combine(targetDir, fileName);
                AppContext.Logger.Debug($"targetPath:{targetPath}");
                file.SaveAs(targetPath);
                AppContext.Logger.Debug($"SaveAsEnd");
                result.OriginalFileName = file.FileName;
                result.FileName = fileName;
                result.FileUrl = $"/{targetDirPath}/{fileName}";
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }
}
