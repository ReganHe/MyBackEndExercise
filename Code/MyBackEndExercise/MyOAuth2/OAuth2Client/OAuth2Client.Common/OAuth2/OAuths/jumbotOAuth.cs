using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace OAuth2Client.Common.OAuth2.OAuths
{
    public class jumbotOAuth : OAuth2Base
    {
        internal override OAuthServer server
        {
            get
            {
                return OAuthServer.jumbot;
            }
        }
        internal override string OAuthUrl
        {
            get
            {
                return "http://oauth2server.net/authorize?response_type=code&client_id={0}&redirect_uri={1}&state={2}";
            }
        }
        internal override string TokenUrl
        {
            get
            {
                return "http://oauth2server.net/token";
            }
        }
        internal string UserInfoUrl = "http://oauth2server.net/userinfo?access_token={0}&userid={1}";
        public override string GetAuthorizeURL()
        {
            return string.Format(OAuthUrl, AppKey, System.Web.HttpUtility.UrlEncode(CallbackUrl), "jumbot");
        }
        public override bool Authorize()
        {
            if (!string.IsNullOrEmpty(code))
            {
                string result = GetToken("POST", "jumbot");//一次性返回数据。
                //分解result;
                if (!string.IsNullOrEmpty(result))
                {
                    JObject jo = JObject.Parse(result);
                    try
                    {
                        token = jo["access_token"].ToString();
                        if (!string.IsNullOrEmpty(token))
                        {
                            double d = 0;
                            if (double.TryParse(jo["expires_in"].ToString(), out d) && d > 0)
                            {
                                expiresTime = DateTime.Now.AddSeconds(d);
                            }
                            //读取OpenID
                            openID = jo["userid"].ToString();
                            if (!string.IsNullOrEmpty(openID))
                            {
                                //获取将博通行证昵称和头像
                                result = wc.DownloadString(string.Format(UserInfoUrl, token, openID));
                                if (!string.IsNullOrEmpty(result)) //返回：callback( {"client_id":"YOUR_APPID","openid":"YOUR_OPENID"} ); 
                                {
                                    nickName = Tool.GetJosnValue(result, "nickname");
                                    headUrl = Tool.GetJosnValue(result, "headimgurl");
                                    return true;
                                }
                            }
                            else
                            {
                                CYQ.Data.Log.WriteLogToTxt("jumbotOAuth.Authorize():" + result);
                            }
                        }
                        else
                        {
                            CYQ.Data.Log.WriteLogToTxt("jumbotOAuth.Authorize():" + result);
                        }

                    }
                    catch (Exception err)
                    {
                        CYQ.Data.Log.WriteLogToTxt(err);
                    }
                }
            }
            return false;
        }
    }
}
