using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using VVCar.Models;
using YEF.Core;

namespace VVCar.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginController : Controller
    {
        /// <summary>
        /// Login/Index
        /// </summary>
        /// <returns></returns>
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.Title = AppContext.Settings.SystemTitle + " - 登录";
            ViewBag.ShowCompanyCode = AppContext.Settings.IsDynamicCompany;
            return View();
        }

        public ActionResult Page()
        {
            ViewBag.Title = AppContext.Settings.SystemTitle;
            return View();
        }

        /// <summary>
        /// Post Login/Index
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Index(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new LoginResult { ErrorMsg = "参数不正确" });
            }
            var password = Util.EncryptPassword(model.UserName, model.Password);
            var loginResult = DoLogin("adminportal", model.CompanyCode, model.UserName, password);
            return Json(loginResult);
        }

        /// <summary>
        /// SSO
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult SSO(string sid)
        {
            if (string.IsNullOrEmpty(sid))
                return RedirectToAction("Index");
            try
            {
                byte[] ssoBytes = Convert.FromBase64String(sid);
                string ssoString = System.Text.Encoding.UTF8.GetString(ssoBytes);
                var ssoData = ssoString.Split('&');
                var companyCode = ssoData[0];
                var username = ssoData[1];
                var password = ssoData[2];
                var verifyDate = DateTime.Parse(ssoData[3]);
                if (verifyDate < DateTime.Now.AddMinutes(-5) || verifyDate > DateTime.Now.AddMinutes(5))
                    throw new Exception("参数错误，错误代码：-1");
                var loginResult = DoLogin("sso", companyCode, username, password);
                if (!loginResult.IsSuccess)
                {
                    throw new Exception(loginResult.ErrorMsg);
                }
                var jsStr = string.Format(@"<script>sessionStorage.setItem('userCode', '{0}');
                                                sessionStorage.setItem('userName', '{1}');
                                                sessionStorage.setItem('userToken', '{2}');
                                                window.location.href = '/';</script>",
                                                loginResult.UserCode,
                                                loginResult.UserName,
                                                loginResult.UserToken);
                return Content(jsStr);
            }
            catch (Exception ex)
            {
                AppContext.Logger.Error("Login/SSO 出现错误。", ex);
                return Content("参数错误,错误代码：-2");
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="companyCode">The company code.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        LoginResult DoLogin(string clientId, string companyCode, string username, string password)
        {
            var loginResult = new LoginResult();
            try
            {
                if (string.IsNullOrEmpty(companyCode))//AppContext.Settings.IsDynamicCompany &&
                {
                    throw new Exception("商户号不能为空");
                }
                var httpClient = new HttpClient();
                var parameters = new Dictionary<string, string>();
                parameters.Add("companycode", companyCode);
                parameters.Add("UserName", username);
                parameters.Add("Password", password);
                parameters.Add("grant_type", "password");
                parameters.Add("client_id", clientId);
                var response = httpClient.PostAsync(string.Format("http://{0}/token", Request.Url.Authority), new FormUrlEncodedContent(parameters)).Result;
                var tokenResult = response.Content.ReadAsStringAsync().Result;
                var tokenObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenResult);
                if (tokenObj == null)
                {
                    loginResult.ErrorMsg = "登录失败，内部服务器错误，请联系管理员";
                    return loginResult;
                }
                if (response.IsSuccessStatusCode)
                {
                    if (!tokenObj.ContainsKey("access_token"))
                    {
                        loginResult.ErrorMsg = "登录失败，请检查用户名或密码是否输入正确";
                        return loginResult;
                    }
                    loginResult.IsSuccess = true;
                    loginResult.UserCode = tokenObj["userCode"];
                    loginResult.UserName = tokenObj["userName"];
                    loginResult.CompanyCode = tokenObj["companyCode"];
                    loginResult.IsAgent = tokenObj["isAgent"];
                    loginResult.IsGeneralMerchant = tokenObj["isGeneralMerchant"];
                    loginResult.UserToken = string.Concat(tokenObj["token_type"], " ", tokenObj["access_token"]);
                }
                else
                {
                    loginResult.ErrorMsg = string.Format("登录失败，{0}", tokenObj["error_description"]);
                }
            }
            catch (Exception ex)
            {
                loginResult.ErrorMsg = string.Format("登录失败，{0}", ex);
            }
            return loginResult;
        }
    }
}
