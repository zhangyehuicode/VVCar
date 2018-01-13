using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using YEF.Utility;

namespace System.Net.Http
{
    /// <summary>
    /// HttpClient 扩展方法
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// 以json格式post数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient client, string requestUri, T value)
        {
            var valueJson = JsonHelper.Serialize(value);
            HttpContent jsonContent = new StringContent(valueJson);
            jsonContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return client.PostAsync(requestUri, jsonContent);
        }
    }
}
