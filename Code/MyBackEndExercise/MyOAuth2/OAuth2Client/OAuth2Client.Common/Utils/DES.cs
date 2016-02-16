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
using System.Security.Cryptography;
using System.Text;

namespace OAuth2Client.Common.Utils
{
    public static class DES
    {
        #region DES加密解密
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string DESEncrypt(string encryptString, string encryptKey)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                des.Key = UTF8Encoding.UTF8.GetBytes(encryptKey);
                des.IV = UTF8Encoding.UTF8.GetBytes(encryptKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:x2}", b);
                }
                ret.ToString();
                return ret.ToString();
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        //解密方法  
        public static string DESDecrypt(string decryptString, string skey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputbytearray = new byte[decryptString.Length / 2];
            for (int x = 0; x < decryptString.Length / 2; x++)
            {
                int i = (Convert.ToInt32(decryptString.Substring(x * 2, 2), 16));
                inputbytearray[x] = (byte)i;
            }
            des.Key = UTF8Encoding.UTF8.GetBytes(skey);
            des.IV = UTF8Encoding.UTF8.GetBytes(skey);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputbytearray, 0, inputbytearray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();

            return Encoding.UTF8.GetString(ms.ToArray(), 0, ms.ToArray().Length);
        }
        #endregion
    }
}
