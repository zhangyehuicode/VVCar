using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using YEF.Core;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using VVCar.BaseData.Domain.Services;
using VVCar.BaseData.Domain.Filters;

namespace VVCar.Providers
{
    /// <summary>
    /// OAuth Provider
    /// </summary>
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        #region fields

        #region clientId
        /// <summary>
        /// 管理后台ClientId
        /// </summary>
        const string AdminPortalClientId = "adminportal";

        /// <summary>
        /// SSO ClientId
        /// </summary>
        const string SsoClientId = "sso";

        /// <summary>
        /// 商户端ClientId
        /// </summary>
        const string StaffClientId = "staffportal";

        #endregion

        /// <summary>
        /// 商户号标识
        /// </summary>
        const string COMPANY_CODE = "companycode";

        /// <summary>
        /// 公钥
        /// </summary>
        static string _PublicKey;

        /// <summary>
        /// 允许的clientId 列表
        /// </summary>
        static IList<string> _ClientIdList;
        #endregion

        #region ctor.
        /// <summary>
        /// OAuth Provider
        /// </summary>
        public ApplicationOAuthProvider()
        {
        }

        static ApplicationOAuthProvider()
        {
            _PublicKey = "cheyinzi";
            _ClientIdList = new List<string>();
            for (int i = 1; i <= 40; i++)
            {
                _ClientIdList.Add(string.Concat("cheyinzi", i.PadLeft(3, '0')));
            }
        }
        #endregion

        /// <summary>
        /// Called when a request to the Token endpoint arrives with a "grant_type" of "password". This occurs when the user has provided name and password
        /// credentials directly into the client application's user interface, and the client application is using those to acquire an "access_token" and
        /// optional "refresh_token". If the web application supports the
        /// resource owner credentials grant type it must validate the context.Username and context.Password as appropriate. To issue an
        /// access token the context.Validated must be called with a new ticket containing the claims about the resource owner which should be associated
        /// with the access token. The application should take appropriate measures to ensure that the endpoint isn’t abused by malicious callers.
        /// The default behavior is to reject this grant type.
        /// See also http://tools.ietf.org/html/rfc6749#section-4.3.2
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>
        /// Task to enable asynchronous execution
        /// </returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var companyCode = context.OwinContext.Get<string>(COMPANY_CODE);
            BaseData.Domain.Dtos.UserInfoDto loginUser = null;
            var mch = string.Empty;
            var isAgent = false;
            var isGeneralMerchant = false;
            try
            {
                //当前环境为动态指定数据库时需要将CompanyCode写入到令牌中
                //if (AppContext.Settings.IsDynamicCompany)
                //{
                //var identity = new ClaimsIdentity();
                //identity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.MerchantCode, companyCode));
                //Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
                //}
                var userService = ServiceLocator.Instance.GetService<IUserService>();
                if (SsoClientId.Equals(context.ClientId))//如果是sso登录请求
                {
                    loginUser = userService.SsoLogin(context.UserName, context.Password, companyCode);
                }
                else
                {
                    loginUser = userService.Login(context.UserName, context.Password, companyCode);
                }
                if (loginUser == null)
                {
                    throw new DomainException("用户名或密码不正确。");
                }
                if (AdminPortalClientId.Equals(context.ClientId) && !loginUser.CanLoginAdminPortal)
                {
                    throw new DomainException("用户没有登录管理后台的权限。");
                }
                var merchantService = ServiceLocator.Instance.GetService<IMerchantService>();
                var total = 0;
                var merchant = merchantService.Search(new MerchantFilter { ID = loginUser.MerchantID }, out total).FirstOrDefault();
                if (merchant != null)
                {
                    mch = merchant.Code;
                    isAgent = merchant.IsAgent;
                    isGeneralMerchant = merchant.IsGeneralMerchant;
                    var identity = new ClaimsIdentity();
                    identity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.MerchantCode, mch));
                    Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
                }
            }
            catch (DomainException domainEx)
            {
                context.SetError("invalid_grant", domainEx.Message);
                return;
            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", ex.Message);
                return;
            }
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginUser.ID.ToString()));
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, loginUser.Name));
            oAuthIdentity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.UserCode, loginUser.Code));
            oAuthIdentity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.DepartmentId, loginUser.DepartmentID.ToString()));
            oAuthIdentity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.DepartmentName, loginUser.DepartmentName));
            oAuthIdentity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.DepartmentCode, loginUser.DepartmentCode));
            oAuthIdentity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.MerchantCode, mch));
            oAuthIdentity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.MerchantID, loginUser.MerchantID.ToString()));
            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                { "userCode", loginUser.Code },
                { "userName", loginUser.Name },
                { "companyCode", mch},
                { "isAgent",isAgent?"true":"false"},
                { "isGeneralMerchant",isGeneralMerchant?"true":"false"},
            });
            var ticket = new AuthenticationTicket(oAuthIdentity, props);
            context.Validated(ticket);
            await base.GrantResourceOwnerCredentials(context);
        }

        private bool IsFromAdminPortal(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return AdminPortalClientId.Equals(context.ClientId);
        }

        /// <summary>
        /// Called when a request to the Token endpoint arrives with a "grant_type" of "client_credentials". This occurs when a registered client
        /// application wishes to acquire an "access_token" to interact with protected resources on it's own behalf, rather than on behalf of an authenticated user.
        /// If the web application supports the client credentials it may assume the context.ClientId has been validated by the ValidateClientAuthentication call.
        /// To issue an access token the context.Validated must be called with a new ticket containing the claims about the client application which should be associated
        /// with the access token. The application should take appropriate measures to ensure that the endpoint isn’t abused by malicious callers.
        /// The default behavior is to reject this grant type.
        /// See also http://tools.ietf.org/html/rfc6749#section-4.4.2
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>
        /// Task to enable asynchronous execution
        /// </returns>
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var companyCode = context.OwinContext.Get<string>(COMPANY_CODE);
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.MerchantCode, companyCode));
            if (!string.IsNullOrEmpty(companyCode))
            {
                var merchantService = ServiceLocator.Instance.GetService<IMerchantService>();
                var totalCount = 0;
                var merchant = merchantService.Search(new MerchantFilter { Code = companyCode }, out totalCount).FirstOrDefault();
                if (merchant != null)
                    oAuthIdentity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.MerchantID, merchant.ID.ToString()));
            }
            if (AppContext.DepartmentID.HasValue)
            {
                oAuthIdentity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.DepartmentId, AppContext.DepartmentID.Value.ToString()));
                oAuthIdentity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.DepartmentName, AppContext.DepartmentName));
                oAuthIdentity.AddClaim(new Claim(YEF.Core.Security.ClaimTypes.DepartmentCode, AppContext.DepartmentCode));
            }

            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.ClientId));

            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            context.Validated(ticket);

            return base.GrantClientCredentials(context);
        }

        /// <summary>
        /// Called to validate that the origin of the request is a registered "client_id", and that the correct credentials for that client are
        /// present on the request. If the web application accepts Basic authentication credentials,
        /// context.TryGetBasicCredentials(out clientId, out clientSecret) may be called to acquire those values if present in the request header. If the web
        /// application accepts "client_id" and "client_secret" as form encoded POST parameters,
        /// context.TryGetFormCredentials(out clientId, out clientSecret) may be called to acquire those values if present in the request body.
        /// If context.Validated is not called the request will not proceed further.
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>
        /// Task to enable asynchronous execution
        /// </returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //string companyCode = AppContext.Settings.CompanyCode;
            //if (AppContext.Settings.IsDynamicCompany)
            //{
            string companyCode = context.Parameters["companycode"];
            if (string.IsNullOrEmpty(companyCode))
            {
                context.SetError("invalid_params", "请输入商户号");
                //return base.ValidateClientAuthentication(context);
            }
            //}
            context.OwinContext.Set(COMPANY_CODE, companyCode);
            string clientId;
            string clientSecret;
            context.TryGetFormCredentials(out clientId, out clientSecret);
            if (AdminPortalClientId.Equals(clientId))//管理后台，授权通过
            {
                context.Validated(clientId);
            }
            else if (SsoClientId.Equals(clientId))
            {
                context.Validated(clientId);
            }
            else if (_ClientIdList.Contains(clientId) && _PublicKey.Equals(clientSecret))//校验前端私钥和公钥
            {
                context.Validated(clientId);
            }
            else if (StaffClientId.Equals(clientId))
            {
                context.Validated(clientId);
            }

            return base.ValidateClientAuthentication(context);
        }

        /// <summary>
        /// Tokens the endpoint.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                if (property.Key.StartsWith("."))
                    continue;
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return base.TokenEndpoint(context);
        }
    }
}