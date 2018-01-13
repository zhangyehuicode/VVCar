using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace YEF.Core
{
    /// <summary>
    /// 类型<see cref="Enum"/>扩展方法类
    /// </summary>
    public static class EnumExtensions
    {
        private const char EnumSeperator = ',';

        /// <summary>
        /// 获取枚举值<see cref="DescriptionAttribute"/>值
        /// </summary>
        /// <param name="enumeration">枚举值</param>
        /// <returns></returns>        
        public static string GetDescription(this Enum enumeration)
        {
            if (enumeration == null)
                return string.Empty;
            var entries = enumeration.ToString().Split(EnumSeperator)
                .Select(e => e.Trim())
                .ToArray();
            var description = new string[entries.Length];
            var enumType = enumeration.GetType();
            for (var i = 0; i < entries.Length; i++)
            {
                var fieldInfo = enumeration.GetType().GetField(entries[i]);
                var descAttr = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
                description[i] = descAttr == null ? entries[i] : descAttr.Description;
            }
            return string.Join(", ", description);
        }

        /// <summary>
        ///  获取<see cref="DescriptionAttribute"/>值
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static String GetDescription(this IEnumerable<object> attributes)
        {
            if (attributes == null)
                return String.Empty;

            foreach (var attr in attributes)
            {
                if (attr is DescriptionAttribute)
                    return (attr as DescriptionAttribute).Description;
            }
            return string.Empty;
        }

        /// <summary>
        /// 将一个或多个枚举常数的名称或数字值的字符串表示转换成等效的枚举对象。 用于指示转换是否成功的返回值。
        /// </summary>
        /// <typeparam name="TEnum">要将 value 转换为的枚举类型。</typeparam>
        /// <param name="value"> 要转换的枚举名称或基础值的字符串表示形式。</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>如果 value 参数成功转换，则返回类型为 TEnum 的一个对象，其值由 value 表示；否则为 defaultValue。</returns>
        public static TEnum ToEnum<TEnum>(this String value, TEnum defaultValue) where TEnum : struct
        {
            if (String.IsNullOrEmpty(value))
                return defaultValue;
            TEnum result;
            bool parseResult = Enum.TryParse<TEnum>(value, out result);
            return parseResult ? result : defaultValue;
        }
    }
}
