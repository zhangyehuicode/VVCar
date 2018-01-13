using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// 新建一个 <see cref="Guid"/>与<see cref="DateTime"/>混合构成的适用于DB的可排序的<see cref="Guid"/>
        /// </summary>
        /// <returns>Comb Guid数据</returns>
        public static Guid NewID()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();
            DateTime dtBase = new DateTime(1900, 1, 1);
            DateTime dtNow = DateTime.Now;
            //获取用于生成byte字符串的天数与毫秒数
            TimeSpan days = new TimeSpan(dtNow.Ticks - dtBase.Ticks);
            TimeSpan msecs = new TimeSpan(dtNow.Ticks - (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day).Ticks));
            //转换成byte数组
            //注意SqlServer的时间计数只能精确到1/300秒
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            //反转字节以符合SqlServer的排序
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            //把字节复制到Guid中
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            return new Guid(guidArray);
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <returns></returns>
        public static string EncryptPassword(string userCode, string password)
        {
            var fixedMark = "-_-VVCar-_-";
            String sourceString = string.Concat(fixedMark, userCode.ToUpper(), password);
            return YEF.Core.Utils.HashUtil.GetMD5(sourceString);
        }
    }
}
