using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core;

namespace VVCar
{
    /// <summary>
    /// 指定用于验证请求的 System.Security.Principal.IPrincipal 的Api授权筛选器。
    /// </summary>
    public class ApiAuthorizeAttribute : System.Web.Http.AuthorizeAttribute, IOverrideFilter
    {
        /// <summary>
        /// 是否需要登录
        /// </summary>
        public bool NeedLogin { get; set; }

        /// <summary>
        /// 是否需要商户号
        /// </summary>
        public bool NeedCompanyCode { get; set; }

        /// <summary>
        /// 为操作授权时调用。
        /// </summary>
        /// <param name="actionContext">上下文。</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (SkipAuthorization(actionContext))
            {
                if (NeedCompanyCode)
                {
                    CheckCompanyCode(actionContext);
                }
                else
                {
                    var identity = GetSystemPrincipal("", "");
                    actionContext.RequestContext.Principal = new ClaimsPrincipal(identity);
                }
                return;
            }
            else if (NeedLogin)//如果需要登录验证，则判断UserID是否存在。
            {
                if (AppContext.CurrentSession.UserID == Guid.Empty)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new { Message = "已拒绝为此请求授权。" });
                }
                if (string.IsNullOrEmpty(AppContext.CurrentSession.DepartmentCode) || AppContext.CurrentSession.DepartmentID == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new { Message = "门店信息为空。" });
                }
                if (AppContext.CurrentSession.MerchantID == Guid.Empty)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new { Message = "商户信息为空。" });
                }
            }
            base.OnAuthorization(actionContext);
        }

        /// <summary>
        /// 检查商户号
        /// </summary>
        /// <param name="actionContext"></param>
        private static void CheckCompanyCode(HttpActionContext actionContext)
        {
            string companyCode = string.Empty;
            string merchantId = string.Empty;

            //if (AppContext.Settings.IsDynamicCompany)
            //{
            var headerIds = actionContext.Request.Headers.FirstOrDefault(t => t.Key.ToLower() == Constants.HttpHeaderCompanyCode.ToLower());
            if (headerIds.Value != null)
            {
                headerIds.Value.ForEach(t =>
                {
                    if (!string.IsNullOrEmpty(t))
                        companyCode = t;
                });
            }
            else
            {
                companyCode = string.Empty;
            }
            //companyCode = headerIds.Value == null ? string.Empty : headerIds.Value.FirstOrDefault();
            if (string.IsNullOrEmpty(companyCode))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new { Message = "缺少商户信息。" });
                return;
            }
            //}
            var merchantService = ServiceLocator.Instance.GetService<IMerchantService>();
            var totalCount = 0;
            var merchant = merchantService.Search(new MerchantFilter { Code = companyCode }, out totalCount).FirstOrDefault();
            if (merchant == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new { Message = "商户不存在。" });
                return;
            }
            merchantId = merchant.ID.ToString();

            var identity = GetSystemPrincipal(merchantId, companyCode);
            actionContext.RequestContext.Principal = new ClaimsPrincipal(identity);
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        static ClaimsIdentity GetSystemPrincipal(string merchantId, string companyCode = "")
        {
            string departmentId = "00000000-0000-0000-0000-000000000001";
            var departmentName = "system";
            var departmentCode = "";
            if (AppContext.Settings.IsStoreService)
            {
                departmentId = AppContext.DepartmentID.GetValueOrDefault().ToString();
                departmentName = AppContext.DepartmentName;
                departmentCode = AppContext.DepartmentCode;
            }
            if (string.IsNullOrEmpty(companyCode) && !AppContext.Settings.IsDynamicCompany)
            {
                companyCode = AppContext.Settings.CompanyCode;
            }
            if (departmentId == null)
                departmentId = "00000000-0000-0000-0000-000000000001";
            if (departmentName == null)
                departmentName = "system";

            var identity = new ClaimsIdentity();
            //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "00000000-0000-0000-0000-000000000000"));
            //identity.AddClaim(new Claim(ClaimTypes.Name, "system"));
            //identity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.UserCode, "system"));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, AppContext.CurrentSession.UserID.ToString()));
            if (!string.IsNullOrEmpty(AppContext.CurrentSession.UserName))
                identity.AddClaim(new Claim(ClaimTypes.Name, AppContext.CurrentSession.UserName));
            else
                identity.AddClaim(new Claim(ClaimTypes.Name, "system"));
            if (!string.IsNullOrEmpty(AppContext.CurrentSession.UserCode))
                identity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.UserCode, AppContext.CurrentSession.UserCode));
            else
                identity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.UserCode, "system"));
            identity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.DepartmentId, departmentId));
            identity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.DepartmentName, departmentName));
            identity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.DepartmentCode, departmentCode));
            identity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.MerchantCode, companyCode));
            identity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.MerchantID, merchantId));

            return identity;
        }

        #region IOverrideFilter 成员

        /// <summary>
        /// Gets the filters to override.
        /// </summary>
        /// <value>
        /// The filters to override.
        /// </value>
        public Type FiltersToOverride
        {
            get { return typeof(IAuthorizationFilter); }
        }

        #endregion
    }
}