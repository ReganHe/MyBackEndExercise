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
using System.Data;
using System.Web.UI;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;//包含必要的库
namespace OAuth2Client.Common.Utils
{
    /// <summary>
    /// 抓取远程页面内容
    /// </summary>
    public static class HttpHelper
    {
        //以GET方式抓取远程页面内容
        public static string Get_Http(string url, int timeout, Encoding encoding)
        {
            string strResult = string.Empty;
            if (url.Length < 10)//没那么短的域名
                return "$UrlIsFalse$";
            try
            {
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Timeout = timeout;
                request.Method = "Get";
                WebResponse response = request.GetResponse();
                Stream s = response.GetResponseStream();
                StreamReader sr = new StreamReader(s, encoding);
                strResult = sr.ReadToEnd();
                s.Close();
                sr.Close();
            }
            catch (Exception)
            {
                strResult = "$GetFalse$";
            }
            return strResult;
        }
        //以GET方式抓取远程页面内容
        public static string Get_Http(string url, Encoding encoding)
        {
            string strResult;
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Timeout = 19600;
                myRequest.Method = "Get";
                // 获取结果数据
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), encoding);
                strResult = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ee)
            {
                strResult = ee.Message;
            }
            return strResult;
        }
        //以POST方式抓取远程页面内容
        //postData为参数列表
        public static string Post_Http(string url, string postData, Encoding encoding)
        {
            string strResult = null;
            try
            {
                byte[] POST =encoding.GetBytes(postData);
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Timeout = 19600;
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";

                myRequest.ContentLength = POST.Length;
                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(POST, 0, POST.Length); //设置POST
                newStream.Close();
                // 获取结果数据
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), encoding);
                strResult = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }
            return strResult;
        }
    }
}
