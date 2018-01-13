using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VVCar.Models
{
    /// <summary>
    /// 登录结果
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// 是否登录登录
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public string UserToken { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}