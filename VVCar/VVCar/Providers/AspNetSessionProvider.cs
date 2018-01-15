using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using YEF.Core;
using YEF.Core.Session;
using Microsoft.AspNet.Identity;

namespace VVCar.Providers
{
    /// <summary>
    /// WebApi Session实现类
    /// </summary>
    public class AspNetSessionProvider : ISession, ISessionProvider
    {
        #region methods
        string GetClaimsPrincipalValue(string claimType)
        {
            var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (claimsPrincipal == null)
            {
                return null;
            }

            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == claimType);
            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                return null;
            }
            return claim.Value;
        }
        #endregion

        #region ISessionProvider 成员

        /// <summary>
        /// 获取会话对象
        /// </summary>
        /// <returns></returns>
        public ISession GetSession()
        {
            return this;
        }

        #endregion

        #region ISession 成员

        /// <summary>
        /// 商户号
        /// </summary>
        public string CompanyCode
        {
            get
            {
                return GetClaimsPrincipalValue(YEF.Core.Security.ClaimTypes.MerchantCode);
            }
            set { }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID
        {
            get
            {
                var userId = Thread.CurrentPrincipal.Identity.GetUserId();
                return string.IsNullOrEmpty(userId) ? Guid.Empty : Guid.Parse(userId);
            }
            set { }
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserCode
        {
            get
            {
                return GetClaimsPrincipalValue(YEF.Core.Security.ClaimTypes.UserCode);
            }
            set { }
        }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get
            {
                return Thread.CurrentPrincipal.Identity.Name;
            }
            set { }
        }

        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DepartmentID
        {
            get
            {
                var deptId = GetClaimsPrincipalValue(YEF.Core.Security.ClaimTypes.DepartmentId);
                return string.IsNullOrEmpty(deptId) ? Guid.Empty : Guid.Parse(deptId);
            }
            set { }
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName
        {
            get
            {
                return GetClaimsPrincipalValue(YEF.Core.Security.ClaimTypes.DepartmentName);
            }
            set { }
        }

        /// <summary>
        /// 商户ID
        /// </summary>
        public Guid MerchantID
        {
            get
            {
                var mchId = GetClaimsPrincipalValue(YEF.Core.Security.ClaimTypes.MerchantID);
                return string.IsNullOrEmpty(mchId) ? Guid.Empty : Guid.Parse(mchId);
            }
            set { }
        }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MerchantCode
        {
            get
            {
                return GetClaimsPrincipalValue(YEF.Core.Security.ClaimTypes.MerchantCode);
            }
            set { }
        }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerchantName
        {
            get
            {
                return GetClaimsPrincipalValue(YEF.Core.Security.ClaimTypes.MerchantName);
            }
            set { }
        }

        #endregion
    }
}