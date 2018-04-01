using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core
{
    /// <summary>
    /// 会话信息
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// 商户号
        /// </summary>
        string CompanyCode { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        Guid UserID { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        Guid DepartmentID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        string DepartmentName { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        string DepartmentCode { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        Guid MerchantID { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        string MerchantCode { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        string MerchantName { get; set; }
    }

    /// <summary>
    /// ISession 扩展方法
    /// </summary>
    public static class ISessionExtensions
    {
        /// <summary>
        /// 获取session 文本化数
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns></returns>
        public static string ToSessionString(this ISession session)
        {
            return $"CompanyCode: {session.CompanyCode}, DepartmentName:{session.DepartmentName}, UserID:{session.UserID}, UserCode:{session.UserCode}";
        }
    }
}
