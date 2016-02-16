using System;
using System.Collections.Generic;
using System.Text;

namespace OAuth2Client.Common.OAuth2.OAuths
{
   public class taobaoOAuth : OAuth2Base
    {
        internal override OAuthServer server
        {
            get
            {
                return OAuthServer.taoBao;
            }
        }
        internal override string OAuthUrl
        {
            get
            {
                return "https://oauth.taobao.com/authorize?response_type=code&client_id={0}&redirect_uri={1}&state={2}";
            }
        }
        internal override string TokenUrl
        {
            get
            {
                return "https://oauth.taobao.com/token";
            }
        }
        public override string GetAuthorizeURL()
        {
            return string.Format(OAuthUrl, AppKey, System.Web.HttpUtility.UrlEncode(CallbackUrl), "taobao");
        }
        public override bool Authorize()
        {
            if (!string.IsNullOrEmpty(code))
            {
                string result = GetToken("POST", "taobao");//一次性返回数据。
                //分解result;
                if (!string.IsNullOrEmpty(result))
                {
                    try
                    {
                        token = Tool.GetJosnValue(result, "access_token");
                        if (!string.IsNullOrEmpty(token))
                        {
                            double d = 0;
                            if (double.TryParse(Tool.GetJosnValue(result, "expires_in"), out d))
                            {
                                expiresTime = DateTime.Now.AddSeconds(d);
                            }
                            //读取OpenID
                            openID = Tool.GetJosnValue(result, "taobao_user_id");
                            nickName = Tool.GetJosnValue(result, "taobao_user_nick");
                            headUrl = "";
                            return true;

                        }
                        else
                        {
                            CYQ.Data.Log.WriteLogToTxt("taobaoOAuth.Authorize():" + result);
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
