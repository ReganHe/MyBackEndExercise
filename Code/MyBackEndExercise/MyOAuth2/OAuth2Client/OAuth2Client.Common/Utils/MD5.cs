/*
 * 程序名称: OAuth2Client
 * 
 * 程序版本: 1.x
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */

using System;
namespace OAuth2Client.Common.Utils
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public static class MD5
    {
        /// <summary>
        /// 64位双重MD5小写
        /// </summary>
        /// <returns></returns>
        public static string Last64(string s)
        {
            if (s.Length != 32)
                return "";
            string s1 = s.Substring(0, 16);
            string s2 = s.Substring(16, 16);
            return Lower32(s1) + Lower32(s2);
        }
        /// <summary>
        /// 32位大写
        /// </summary>
        /// <returns></returns>
        public static string Upper32(string s)
        {
            s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
            return s.ToUpper();
        }
        /// <summary>
        /// 32位小写
        /// </summary>
        /// <returns></returns>
        public static string Lower32(string s)
        {
            s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
            return s.ToLower();
        }
        /// <summary>
        /// 16位大写
        /// </summary>
        /// <returns></returns>
        public static string Upper16(string s)
        {
            s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
            return s.ToUpper().Substring(8, 16);
        }
        /// <summary>
        /// 16位小写
        /// </summary>
        /// <returns></returns>
        public static string Lower16(string s)
        {
            s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
            return s.ToLower().Substring(8, 16);
        }
    }
}
