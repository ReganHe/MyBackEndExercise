using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace OAuth2Client.Common.OAuth2.OAuths
{
    public class renrenOAuth : OAuth2Base
    {
        internal override OAuthServer server
        {
            get
            {
                return OAuthServer.renren;
            }
        }
        internal override string OAuthUrl
        {
            get
            {
                return "https://graph.renren.com/oauth/authorize?response_type=code&client_id={0}&redirect_uri={1}&state={2}";
            }
        }
        internal override string TokenUrl
        {
            get
            {
                return "https://graph.renren.com/oauth/token";
            }
        }
        internal string UserInfoUrl = "https://api.renren.com/v2/user/get?access_token={0}&userId={1}";
        public override string GetAuthorizeURL()
        {
            return string.Format(OAuthUrl, AppKey, System.Web.HttpUtility.UrlEncode(CallbackUrl), "renren");
        }
        public override bool Authorize()
        {
            if (!string.IsNullOrEmpty(code))
            {
                string result = GetToken("GET","renren");//一次性返回数据。
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
                        openID = jo["user"]["id"].ToString();
                        if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(openID))
                        {
                            nickName = jo["user"]["name"].ToString();
                            headUrl = jo["user"]["avatar"][0]["url"].ToString();//获取头像
                            return true;
                        }
                        else
                        {
                            CYQ.Data.Log.WriteLogToTxt("renrenOAuth.Authorize():" + result);
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
