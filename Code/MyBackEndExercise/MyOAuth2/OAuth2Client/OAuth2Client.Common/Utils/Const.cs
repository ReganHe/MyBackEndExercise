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
using System.Web;
using System.Web.SessionState;
namespace OAuth2Client.Common.Utils
{
    /// <summary>
    /// 只读常量
    /// </summary>
    public class Const
    {
        /// <summary>
        /// 获得用户IP
        /// </summary>
        public static string GetUserIp
        {
            get
            {
                string ip;
                string[] temp;
                bool isErr = false;
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                else
                    ip = HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
                if (ip.Length > 15)
                    isErr = true;
                else
                {
                    temp = ip.Split('.');
                    if (temp.Length == 4)
                    {
                        for (int i = 0; i < temp.Length; i++)
                        {
                            if (temp[i].Length > 3) isErr = true;
                        }
                    }
                    else
                        isErr = true;
                }

                if (isErr)
                    return "1.1.1.1";
                else
                    return ip;
            }
        }
        /// <summary>
        /// 格式化IP
        /// </summary>
        public static string FormatIp(string ipStr)
        {
            string[] temp = ipStr.Split('.');
            string format = "";
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].Length < 3) temp[i] = Convert.ToString("000" + temp[i]).Substring(Convert.ToString("000" + temp[i]).Length - 3, 3);
                format += temp[i].ToString();
            }
            return format;
        }
        /// <summary>
        /// 来源地址
        /// </summary>
        public static string GetRefererUrl
        {
            get
            {
                if (HttpContext.Current.Request.ServerVariables["Http_Referer"] == null)
                    return "";
                else
                    return HttpContext.Current.Request.ServerVariables["Http_Referer"].ToString();
            }
        }
        /// <summary>
        /// 当前地址
        /// </summary>
        public static string GetCurrentUrl
        {
            get
            {
                string strUrl;
                strUrl = HttpContext.Current.Request.ServerVariables["Url"];
                if (HttpContext.Current.Request.QueryString.Count == 0) //如果无参数
                    return strUrl;
                else
                    return strUrl + "?" + HttpContext.Current.Request.ServerVariables["Query_String"];
            }

        }
    }

}
