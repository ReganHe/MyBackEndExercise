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
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Text;
using OAuth2Client.Common.Utils;
namespace OAuth2Client.Common.UI
{

    /// <summary>
    /// BasicPage 的摘要说明
    /// </summary>
    public class BasicPage : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            Server.ScriptTimeout = 90;//默认脚本过期时间
            base.OnInit(e);

        }
        public static string StaticKey = System.Configuration.ConfigurationManager.AppSettings["OAuth2Client:StaticKey"];
        /// <summary>
        /// 获取querystring
        /// </summary>
        /// <param name="s">参数名</param>
        /// <returns>返回值</returns>
        public string q(string s)
        {
            if (HttpContext.Current.Request.QueryString[s] != null && HttpContext.Current.Request.QueryString[s] != "")
            {
                return HttpContext.Current.Request.QueryString[s].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取post得到的参数
        /// </summary>
        /// <param name="s">参数名</param>
        /// <returns>返回值</returns>
        public string f(string s)
        {
            if (HttpContext.Current.Request.Form[s] != null && HttpContext.Current.Request.Form[s] != "")
            {
                return HttpContext.Current.Request.Form[s].ToString();
            }
            return string.Empty;
        }
        /// <summary>
        /// 返回整数，默认为t
        /// </summary>
        /// <param name="s">参数值</param>
        /// <returns>返回值</returns>
        public int Str2Int(string s, int t)
        {
            return OAuth2Client.Common.Utils.Validator.StrToInt(s, t);
        }

        /// <summary>
        /// 返回整数，默认为0
        /// </summary>
        /// <param name="s">参数值</param>
        /// <returns>返回值</returns>
        public int Str2Int(string s)
        {
            return Str2Int(s, 0);
        }
        /// <summary>
        /// 返回整数，默认为t
        /// </summary>
        /// <param name="s">参数值</param>
        /// <returns>返回值</returns>
        public long Str2Long(string s, long t)
        {
            return OAuth2Client.Common.Utils.Validator.StrToLong(s, t);
        }
        public long Str2Long(string s)
        {
            return Str2Long(s, 0);
        }
        /// <summary>
        /// 返回双精度浮点数，默认为0
        /// </summary>
        /// <param name="s">参数值</param>
        /// <returns>返回值</returns>
        public Double Str2Double(string s)
        {
            return Str2Double(s, 0);
        }
        public Double Str2Double(string s, Double t)
        {
            return OAuth2Client.Common.Utils.Validator.StrToDouble(s, t);
        }

        /// <summary>
        /// 返回整数，默认为0
        /// </summary>
        /// <param name="s">参数值</param>
        /// <returns>返回值</returns>

        /// <summary>
        /// 返回非空字符串，默认为"0"
        /// </summary>
        /// <param name="s">参数值</param>
        /// <returns>返回值</returns>
        public string Str2Str(string s)
        {
            return OAuth2Client.Common.Utils.Validator.StrToInt(s, 0).ToString();
        }
        /// <summary>
        /// 判断oauth接口是否已经启用
        /// </summary>
        /// <param name="_oauthcode"></param>
        public void CheckOAuthState(string _oauthcode)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_config/OAuth2.config");
            OAuth2Client.Common.Utils.XmlControl XmlTool = new OAuth2Client.Common.Utils.XmlControl(strXmlFile);
                string _Enabled = XmlTool.GetText("Apps/App[Code=\"" + _oauthcode + "\"]/Enabled");
            XmlTool.Dispose();
            if (_Enabled=="1")
                return;
            Response.Write("接口未启动");
            Response.End();
        }
    }
}
