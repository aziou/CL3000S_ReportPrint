using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Function
{
    /// <summary>
    /// 实现Base64加密解密
    /// </summary>
    public sealed class Base64
    {
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string Encrypt(string source)
        {
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(source);
            try
            {
                string str = Convert.ToBase64String(bytes, Base64FormattingOptions.None);
                return str.Substring(0, str.Length - 1);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string result)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(result+"=");
                return System.Text.Encoding.ASCII.GetString(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Encrypt2(string source)
        {
            return System.Web.HttpUtility.UrlEncodeUnicode(source);
            //byte[] bytes = System.Text.Encoding.ASCII.GetBytes(source);
            //try
            //{
            //    string str = Convert.ToBase64String(bytes, Base64FormattingOptions.None);
            //    return str;//.Substring(0, str.Length - 1);
            //}
            //catch
            //{
            //    return string.Empty;
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string Decrypt2(string result)
        {
            //try
            //{
            //    byte[] bytes = Convert.FromBase64String(result);
            //    return System.Text.Encoding.ASCII.GetString(bytes);
            //}
            //catch
            //{
            //    return string.Empty;
            //}
            return System.Web.HttpUtility.UrlDecode(result);
        }

    }
}
