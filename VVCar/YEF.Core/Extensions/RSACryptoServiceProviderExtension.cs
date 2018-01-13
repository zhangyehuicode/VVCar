using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace YEF.Core.Extensions
{
    /// <summary>
    /// 类型<see cref="System.Security.Cryptography.RSACryptoServiceProvider"/>扩展方法类
    /// </summary>
    public static class RSACryptoServiceProviderExtension
    {
        /// <summary>
        /// 私钥加密
        /// </summary>
        /// <param name="rsaProvider"></param>
        /// <param name="rgb"></param>
        /// <returns></returns>
        public static string EncryptUsePrivate(this RSACryptoServiceProvider rsaProvider, string rgb)
        {
            var bytes = EncryptUsePrivate(rsaProvider, Encoding.UTF8.GetBytes(rgb));
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 私钥加密
        /// </summary>
        /// <param name="rsaProvider"></param>
        /// <param name="rgb"></param>
        /// <returns></returns>
        public static byte[] EncryptUsePrivate(this RSACryptoServiceProvider rsaProvider, byte[] rgb)
        {
            if (rgb == null)
                throw new ArgumentNullException("rgb");
            if (rsaProvider.PublicOnly)
                throw new InvalidOperationException("没有加载私钥");

            int maxDataLength = (rsaProvider.KeySize / 8) - 6;
            if (rgb.Length > maxDataLength)
                throw new ArgumentOutOfRangeException("rgb",
                    string.Format("要加密的数据长度({0} bytes) 不能大于当前密钥长度({0} bits, {1} bytes)",
                    rgb.Length, rsaProvider.KeySize, maxDataLength));

            // Add 4 byte padding to the data, and convert to BigInteger struct
            BigInteger numData = GetBig(AddPadding(rgb));

            RSAParameters rsaParams = rsaProvider.ExportParameters(true);
            BigInteger D = GetBig(rsaParams.D);
            BigInteger Modulus = GetBig(rsaParams.Modulus);
            BigInteger encData = BigInteger.ModPow(numData, D, Modulus);

            return encData.ToByteArray();
        }

        /// <summary>
        /// 公钥解密
        /// </summary>
        /// <param name="rsaProvider">rsaProvider</param>
        /// <param name="rgb"></param>
        /// <returns></returns>
        public static string DecryptUsePublic(this RSACryptoServiceProvider rsaProvider, string rgb)
        {
            var bytes = DecryptUsePublic(rsaProvider, Convert.FromBase64String(rgb));
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// 公钥解密
        /// </summary>
        /// <param name="rsaProvider">rsaProvider</param>
        /// <param name="rgb"></param>
        /// <returns></returns>
        public static byte[] DecryptUsePublic(this RSACryptoServiceProvider rsaProvider, byte[] rgb)
        {
            if (rgb == null)
                throw new ArgumentNullException("rgb");
            BigInteger numEncData = new BigInteger(rgb);

            RSAParameters rsaParams = rsaProvider.ExportParameters(false);
            BigInteger Exponent = GetBig(rsaParams.Exponent);
            BigInteger Modulus = GetBig(rsaParams.Modulus);

            BigInteger decData = BigInteger.ModPow(numEncData, Exponent, Modulus);

            byte[] data = decData.ToByteArray();
            byte[] result = new byte[data.Length - 1];
            Array.Copy(data, result, result.Length);
            result = RemovePadding(result);

            Array.Reverse(result);
            return result;
        }

        static BigInteger GetBig(byte[] data)
        {
            byte[] inArr = (byte[])data.Clone();
            Array.Reverse(inArr);  // Reverse the byte order
            byte[] final = new byte[inArr.Length + 1];  // Add an empty byte at the end, to simulate unsigned BigInteger (no negatives!)
            Array.Copy(inArr, final, inArr.Length);

            return new BigInteger(final);
        }

        // Add 4 byte random padding, first bit *Always On*
        static byte[] AddPadding(byte[] data)
        {
            Random rnd = new Random();
            byte[] paddings = new byte[4];
            rnd.NextBytes(paddings);
            paddings[0] = (byte)(paddings[0] | 128);

            byte[] results = new byte[data.Length + 4];

            Array.Copy(paddings, results, 4);
            Array.Copy(data, 0, results, 4, data.Length);
            return results;
        }

        static byte[] RemovePadding(byte[] data)
        {
            byte[] results = new byte[data.Length - 4];
            Array.Copy(data, results, results.Length);
            return results;
        }
    }
}
