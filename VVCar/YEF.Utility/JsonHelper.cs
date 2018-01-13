using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YEF.Utility
{
    public static class JsonHelper
    {
        /// <summary>
        /// 把对象序列化成Json字符串格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// 把对象序列化成Json字符串格式
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isIndented">是否格式化</param>
        /// <returns></returns>
        public static string Serialize(object obj, bool isIndented = false, bool isIgnoreNull = false)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = isIgnoreNull ? NullValueHandling.Ignore : NullValueHandling.Include,
            };
            return JsonConvert.SerializeObject(obj, isIndented ? Formatting.Indented : Formatting.None, settings);
        }

        /// <summary>
        /// 将JSON字符串转换成C#对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 把Json字符串转换为强类型对象
        /// </summary>
        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
