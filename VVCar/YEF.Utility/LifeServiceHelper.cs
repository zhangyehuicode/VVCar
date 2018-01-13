using System;
using System.Net.Http;

namespace YEF.Utility
{
    /// <summary>
    /// 生活服务帮助类
    /// </summary>
    public static class LifeServiceHelper
    {
        /// <summary>
        /// 获取实况天气信息
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public static string GetNowWeather(string city)
        {
            try
            {
                //docs: http://www.kancloud.cn/hefengyun/weather/224326
                var url = $"https://free-api.heweather.com/v5/now?city={city}&key=aa1ce0a5d1004ea99c899ca181f6fa69";
                var client = new HttpClient();
                var json = client.GetStringAsync(url).Result;
                var weatherData = JsonHelper.DeserializeObject<dynamic>(json);
                return weatherData.HeWeather5[0].now.cond.txt;
            }
            catch (Exception)
            {
            }
            return "未知";
        }
    }
}
