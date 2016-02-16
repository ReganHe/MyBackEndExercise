using System;
using System.Collections.Generic;
using System.Text;

namespace OAuth2Client.Common.OAuth2.OAuths
{
    public class weiboOAuth : OAuth2Base
    {
        internal override OAuthServer server
        {
            get
            {
                return OAuthServer.weibo;
            }
        }
        internal override string OAuthUrl
        {
            get
            {
                return "https://api.weibo.com/oauth2/authorize?response_type=code&client_id={0}&redirect_uri={1}&state={2}";
            }
        }
        internal override string TokenUrl
        {
            get
            {
                return "https://api.weibo.com/oauth2/access_token";
            }
        }
        internal string UserInfoUrl = "https://api.weibo.com/2/users/show.json?access_token={0}&uid={1}";
        public override string GetAuthorizeURL()
        {
            return string.Format(OAuthUrl, AppKey, System.Web.HttpUtility.UrlEncode(CallbackUrl), "Weibo");
        }
        public override bool Authorize()
        {
            if (!string.IsNullOrEmpty(code))
            {
                string result = GetToken("POST", "Weibo");//一次性返回数据。
                //分解result;
                if (!string.IsNullOrEmpty(result))
                {
                    try
                    {
                        Dictionary<string, string> items = CYQ.Data.Tool.JsonHelper.Split(result);
                        if (items != null && items.Count > 0)
                        {
                            foreach (KeyValuePair<string, string> item in items)
                            {
                                switch (item.Key)
                                {
                                    case "access_token":
                                        token = item.Value;
                                        break;
                                    case "expires_in":
                                        double d = 0;
                                        if (double.TryParse(item.Value, out d) && d > 0)
                                        {
                                            expiresTime = DateTime.Now.AddSeconds(d);
                                        }
                                        break;
                                    case "uid":
                                        openID = item.Value;
                                        break;
                                }
                            }
                            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(openID))
                            {
                                //获取微博昵称和头像
                                result = wc.DownloadString(string.Format(UserInfoUrl, token, openID));
                                if (!string.IsNullOrEmpty(result)) //返回：callback( {"client_id":"YOUR_APPID","openid":"YOUR_OPENID"} ); 
                                {
                                    nickName = Tool.GetJosnValue(result, "screen_name");
                                    headUrl = Tool.GetJosnValue(result, "profile_image_url");//获取头像
                                    return true;
                                }
                            }
                            else
                            {
                                CYQ.Data.Log.WriteLogToTxt("WeiBoOAuth.Authorize():" + result);
                            }
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
