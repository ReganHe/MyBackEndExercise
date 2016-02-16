using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace OAuth2Client.Common.OAuth2.OAuths
{
    public class baiduOAuth : OAuth2Base
    {
        internal override OAuthServer server
        {
            get
            {
                return OAuthServer.baidu;
            }
        }
        internal override string OAuthUrl
        {
            get
            {
                return "https://openapi.baidu.com/oauth/2.0/authorize?response_type=code&client_id={0}&redirect_uri={1}&state={2}";
            }
        }
        internal override string TokenUrl
        {
            get
            {
                return "https://openapi.baidu.com/oauth/2.0/token";
            }
        }
        internal string UserInfoUrl = "https://openapi.baidu.com/rest/2.0/passport/users/getInfo?access_token={0}";
        public override string GetAuthorizeURL()
        {
            return string.Format(OAuthUrl, AppKey, System.Web.HttpUtility.UrlEncode(CallbackUrl), "baidu");
        }
        public override bool Authorize()
        {
            if (!string.IsNullOrEmpty(code))
            {
                string result = GetToken("GET","baidu");//一次性返回数据。
                if (!string.IsNullOrEmpty(result))
                {
                    JObject jo = JObject.Parse(result);
                    try
                    {
                        token = jo["access_token"].ToString();
                        double d = 0;
                        if (double.TryParse(jo["expires_in"].ToString(), out d) && d > 0)
                        {
                            expiresTime = DateTime.Now.AddSeconds(d);
                        }
                        //读取QQ账号和头像
                        result = wc.DownloadString(string.Format(UserInfoUrl, token));
                        if (!string.IsNullOrEmpty(result))
                        {
                            openID = Tool.GetJosnValue(result, "userid");
                            nickName = Tool.GetJosnValue(result, "username");
                            headUrl = "http://tb.himg.baidu.com/sys/portrait/item/" + Tool.GetJosnValue(result, "portrait");//获取头像
                            return true;
                        }
                        else
                        {
                            CYQ.Data.Log.WriteLogToTxt("baiduOAuth.Authorize():" + result);
                        }

                    }
                    catch (Exception err)
                    {
                        CYQ.Data.Log.WriteLogToTxt(result + "\r\n" + err);
                    }
                }
                else
                    CYQ.Data.Log.WriteLogToTxt(result);
            }
            return false;
        }
    }
}
