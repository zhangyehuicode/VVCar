using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace YEF.Core.Utils
{
    /// <summary>
    /// Hash工具类
    /// </summary>
    public static class HashUtil
    {
        /// <summary>
        /// 计算字符串MD5值
        /// </summary>
        /// <param name="source">要计算的值</param>
        /// <returns></returns>
        public static string GetMD5(string source)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            return GetMD5(bytes);
        }

        /// <summary>
        /// 计算byte数组MD5值
        /// </summary>
        /// <param name="bytes">要计算的byte数组</param>
        /// <returns></returns>
        public static string GetMD5(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            MD5 hash = new MD5CryptoServiceProvider();
            bytes = hash.ComputeHash(bytes);
            foreach (byte b in bytes)
            {
                builder.AppendFormat("{0:X2}", b);
            }
            return builder.ToString();
        }
    }
}
