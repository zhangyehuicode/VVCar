using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Utility
{
    /// <summary>
    /// 提供基础的HTTP功能
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        /// HTTP GET方法，返回string
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public static string GetString(string requestUri)
        {
            HttpClient client = new HttpClient();
            return client.GetStringAsync(requestUri).Result;
        }

        /// <summary>
        /// 发送post请求
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <param name="appendAuth"></param>
        /// <returns></returns>
        public static string Post(string requestUrl, string content, string contentType)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.Method = "POST";
                request.ContentType = contentType;
                request.ContentLength = 0;

                if (!string.IsNullOrEmpty(content))
                {
                    byte[] data = Encoding.UTF8.GetBytes(content);
                    request.ContentLength = data.Length;
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                return GetResponseString(request.GetResponse());
            }
            catch (WebException webEx)
            {
                try
                {
                    if (webEx.Status == WebExceptionStatus.ConnectFailure)
                    {
                        //Log.Error("HttpHelper.POST", string.Format("远程POST :{0}", "链接服务器失败，请检查网络链接或服务器配置。"));
                    }
                    return GetResponseString(webEx.Response);
                }
                catch (Exception ex)
                {
                    //Log.Error("HttpHelper.POST", string.Format("远程POST :{0}", ex.ToString()));
                }
            }
            catch (Exception ex)
            {
                //Log.Error("HttpHelper.POST", string.Format("远程POST :{0}", ex.ToString()));
            }
            return "";
        }

        /// <summary>
        /// 获取响应字符串
        /// </summary>
        /// <param name="webResponse"></param>
        /// <returns></returns>
        static string GetResponseString(WebResponse webResponse)
        {
            string responseString;
            using (var response = (HttpWebResponse)webResponse)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    responseString = "无身份信息，请重新登陆";
                    return responseString;
                }
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseString = reader.ReadToEnd();
                }
                response.Close();
            }
            return responseString;
        }
    }
}
