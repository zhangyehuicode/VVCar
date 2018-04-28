using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VVCar.Models
{
    /// <summary>
    /// 统一返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonResult<T>
    {
        public JsonResult()
        {
            IsSuccessful = true;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}