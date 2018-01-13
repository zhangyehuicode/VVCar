using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core
{
    /// <summary>
    /// 类型<see cref="System.Random"/>扩展方法类
    /// </summary>
    public static class RandomExtensions
    {
        static char[] _numberPattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char[] _letterPattern = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L','M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        static char[] _letterAndNumberPattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P','Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        /// <summary>
        /// 获取指定的长度的随机数字字符串
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length">要获取随机数长度</param>
        /// <returns>指定长度的随机数字符串</returns>
        public static string NextNumberString(this Random random, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length");
            int n = _numberPattern.Length;
            StringBuilder randomBuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                randomBuilder.Append(_numberPattern[rnd]);
            }
            return randomBuilder.ToString();
        }

        /// <summary>
        /// 获取指定的长度的随机字母字符串
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length">要获取随机数长度</param>
        /// <returns>指定长度的随机字母组成字符串</returns>
        public static string NextLetterString(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            int n = _letterPattern.Length;
            StringBuilder randomBuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                randomBuilder.Append(_letterPattern[rnd]);
            }
            return randomBuilder.ToString();
        }

        /// <summary>
        /// 获取指定的长度的随机字母和数字字符串
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length">要获取随机数长度</param>
        /// <returns>指定长度的随机字母和数字组成字符串</returns>
        public static string NextLetterAndNumberString(this Random random, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length");
            int n = _letterAndNumberPattern.Length;
            StringBuilder randomBuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                randomBuilder.Append(_letterAndNumberPattern[rnd]);
            }
            return randomBuilder.ToString();
        }
    }
}
