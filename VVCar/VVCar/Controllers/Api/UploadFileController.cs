﻿using System;
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
        static int _maxSizeLength = 1024 * 1024 * 3;

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
                    result.errorMessage = "不允许上传超过 3M 的文件";
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
        /// 上传车牌
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadPlate")]
        public UploadFileResult UploadPlate()
        {
            var result = UploadAction("Pictures/Plate");
            if (!string.IsNullOrEmpty(result.FileUrl) && result.FileUrl.Length > 1)
            {
                var prfilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, result.FileUrl.Substring(1));
                result.PRResult = GetPRResult(prfilename);
            }
            return result;
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
                    result.errorMessage = "不允许上传超过 3M 的文件";
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
