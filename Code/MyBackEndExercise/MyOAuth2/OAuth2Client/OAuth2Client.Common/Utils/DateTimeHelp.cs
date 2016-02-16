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
    /// DateTime操作类
    /// </summary>
    public static class DateTimeHelp
    {
        public static long ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;            //除10000调整为13位
            return t;
        }
    }
}
