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
using System.Web.Caching;
using System.Text;

namespace OAuth2Client.Common.Utils
{
    /// <summary>
    /// App操作类
    /// </summary>
    public static class App
    {
        public static string Url
        {
            get
            {
                if (HttpContext.Current.Request.Url.Port == 80)
                    return "http://" + HttpContext.Current.Request.Url.Host;
                else
                    return "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
            }
        }
        /// <summary>
        /// 应用程序路径，以/结尾
        /// </summary>
        /// <returns>如:/，/cms/</returns>
        public static string Path
        {
            get
            {
                string _ApplicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                if (_ApplicationPath != "/")
                    _ApplicationPath += "/";
                return _ApplicationPath;
            }
        }
    }
}
