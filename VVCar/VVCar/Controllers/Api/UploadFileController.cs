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

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 文件上传Api
    /// </summary>
    [RoutePrefix("api/UploadFile"), AllowAnonymous]
    public class UploadFileController : ApiController
    {
        static int _maxSizeLength = 1024 * 1024;

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
                    result.errorMessage = "不允许上传超过 1M 的文件";
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

        private UploadFileResult UploadAction(string targetDirPath)
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
                if (string.IsNullOrEmpty(file.FileName))
                {
                    result.errorMessage = "没有需要上传的文件";
                    return result;
                }
                if (file.ContentLength > _maxSizeLength)
                {
                    result.errorMessage = "不允许上传超过 1M 的文件";
                    return result;
                }
                var fileName = string.Concat(DateTime.Now.ToString("yyyyMMddHHmmssfff"), Path.GetExtension(file.FileName));
                var targetDir = Path.Combine(AppContext.PathInfo.RootPath, targetDirPath);

                if (!Directory.Exists(targetDir))
                    Directory.CreateDirectory(targetDir);
                string targetPath = Path.Combine(targetDir, fileName);
                file.SaveAs(targetPath);
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
