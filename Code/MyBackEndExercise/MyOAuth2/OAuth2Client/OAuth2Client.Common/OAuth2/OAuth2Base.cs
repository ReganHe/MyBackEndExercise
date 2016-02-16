using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using CYQ.Data.Table;
namespace OAuth2Client.Common.OAuth2
{
    /// <summary>
    /// 授权基类
    /// </summary>
    public abstract class OAuth2Base
    {
        protected WebClient wc = new WebClient();
        public OAuth2Base()
        {
            wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers.Add("Pragma", "no-cache");
        }
         #region 基础属性
        /// <summary>
        /// 返回的开放ID。
        /// </summary>
        public string openID = string.Empty;
        /// <summary>
        /// 访问的Token
        /// </summary>
        public string token = string.Empty;
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime expiresTime;

        /// <summary>
        /// 第三方账号昵称
        /// </summary>
        public string nickName = string.Empty;

        /// <summary>
        /// 第三方账号头像地址
        /// </summary>
        public string headUrl = string.Empty;
        /// <summary>
        /// 首次请求时返回的Code
        /// </summary>
        internal string code = string.Empty;
        internal abstract OAuthServer server
        {
            get;
        }
        #endregion

        #region 非公开的请求路径和Logo图片地址。

        internal abstract string OAuthUrl
        {
            get;
        }
        internal abstract string TokenUrl
        {
            get;
        }
        #endregion

        #region WebConfig对应的配置【AppKey、AppSecret、CallbackUrl】
        internal string AppKey
        {
            get
            {
                return OAuth2Client.Common.Utils.XmlCOM.GetText("~/_config/oauth2", "Apps/App[Code=\"" + server.ToString().ToLower() + "\"]/AppKey");
            }
        }
        internal string AppSecret
        {
            get
            {
                return OAuth2Client.Common.Utils.XmlCOM.GetText("~/_config/oauth2", "Apps/App[Code=\"" + server.ToString().ToLower() + "\"]/AppSecret");
            }
        }
        internal string CallbackUrl
        {
            get
            {
                return OAuth2Client.Common.Utils.XmlCOM.GetText("~/_config/oauth2", "Apps/App[Code=\"" + server.ToString().ToLower() + "\"]/CallbackUrl");
            }
        }
        #endregion

        #region 基础方法

        /// <summary>
        /// 获得Token
        /// </summary>
        /// <returns></returns>
        protected string GetToken(string method,string oauthcode)
        {
            string result = string.Empty;
            try
            {
                string para = "grant_type=authorization_code&client_id=" + AppKey + "&client_secret=" + AppSecret + "&code=" + code + "&state=" + server;
                if (oauthcode.ToLower() == "weixin")
                    para = "grant_type=authorization_code&appid=" + AppKey + "&secret=" + AppSecret + "&code=" + code + "&state=" + server;

                para += "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(CallbackUrl) + "&rnd=" + DateTime.Now.Second;
                if (method.ToUpper() == "POST")
                {
                    if (string.IsNullOrEmpty(wc.Headers["Content-Type"]))
                    {
                        wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    }
                    result = wc.UploadString(TokenUrl, method, para);
                }
                else
                {
                    result = wc.DownloadString(TokenUrl + "?" + para);
                }
            }
            catch(Exception err)
            {
                CYQ.Data.Log.WriteLogToTxt(err);
            }
            return result;
        }
        public abstract string GetAuthorizeURL();
        /// <summary>
        /// 获取是否通过授权。
        /// </summary>
        public abstract bool Authorize();
        /// <param name="bindAccount">返回绑定的账号（若未绑定返回空）</param>
        public bool Authorize(out string bindAccount)
        {
            bindAccount = string.Empty;
            if (Authorize())
            {
                bindAccount = GetBindAccount();
                return true;
            }
            return false;
        }

        #endregion

        #region 关联绑定账号

        /// <summary>
        /// 读取已经绑定的账号
        /// </summary>
        /// <returns></returns>
        public string GetBindAccount()
        {
            string account = string.Empty;
            using (OAuth2Account oa = new OAuth2Account())
            {
                if (oa.Fill(string.Format("OAuthServer='{0}' and OpenID='{1}'", server, openID)))
                {
                    oa.Token = token;
                    oa.ExpireTime = expiresTime;
                    oa.NickName = nickName;
                    oa.HeadUrl = headUrl;
                    oa.Update();//更新token和过期时间
                    account = oa.BindAccount;
                }
            }
            return account;
        }
        /// <summary>
        /// 添加绑定账号
        /// </summary>
        /// <param name="bindAccount"></param>
        /// <returns></returns>
        public bool SetBindAccount(string bindAccount)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(openID) && !string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(bindAccount))
            {
                using (OAuth2Account oa = new OAuth2Account())
                {
                    if (!oa.Exists(string.Format("OAuthServer='{0}' and OpenID='{1}'", server, openID)))
                    {
                        oa.OAuthServer = server.ToString();
                        oa.Token = token;
                        oa.OpenID = openID;
                        oa.ExpireTime = expiresTime;
                        oa.BindAccount = bindAccount;
                        oa.NickName = nickName;
                        oa.HeadUrl = headUrl;
                        result = oa.Insert(CYQ.Data.InsertOp.None);
                    }
                }
            }
            return result;
        }
        #endregion
    }
    /// <summary>
    /// 提供授权的服务商
    /// </summary>
    public enum OAuthServer
    {
        /// <summary>
        /// 新浪微博
        /// </summary>
        weibo,
        /// <summary>
        /// 腾讯QQ
        /// </summary>
        qq,
        /// <summary>
        /// 淘宝网
        /// </summary>
        taoBao,
        /// <summary>
        /// 人人网
        /// </summary>
        renren,
        /// <summary>
        /// 微信
        /// </summary>
        weixin,
        /// <summary>
        /// 百度账号
        /// </summary>
        baidu,
        /// <summary>
        /// 京东账号
        /// </summary>
        jd,
        /// <summary>
        /// 将博账号
        /// </summary>
        jumbot,
    }
}
