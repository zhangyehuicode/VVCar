using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VVCar.Models
{
    /// <summary>
    /// 上传文件结果
    /// </summary>
    public class UploadFileResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 原始文件名称
        /// </summary>
        public string OriginalFileName { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FileUrl { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string errorMessage { get; set; }

        /// <summary>
        /// 车牌识别结果
        /// </summary>
        public string PRResult { get; set; }
    }
}