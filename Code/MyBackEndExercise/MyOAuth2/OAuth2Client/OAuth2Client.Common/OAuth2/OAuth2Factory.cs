using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
namespace OAuth2Client.Common.OAuth2
{
    /// <summary>
    /// 授权工厂类
    /// </summary>
    public class OAuth2Factory
    {
        /// <summary>
        /// 获取当前的授权类型。
        /// </summary>
        
        public static OAuth2Base Current
        {
            get
            {
                if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request != null)
                {
                    string url = System.Web.HttpContext.Current.Request.Url.Query;
                    if (url.IndexOf("state=") > -1)
                    {
                        string code = Tool.QueryString(url, "code");
                        string state = Tool.QueryString(url, "state");
                        if (ServerList.ContainsKey(state))
                        {
                            OAuth2Base ob = ServerList[state];
                            ob.code = code;
                            System.Web.HttpContext.Current.Session["OAuth2"] = ob;//对象存进Session，后期授权后会增加引用。
                            return ob;
                        }
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// 读取或设置当前Session存档的授权类型。 (注销用户时可以将此值置为Null)
        /// </summary>
        public static OAuth2Base SessionOAuth
        {
            get
            {
                if (System.Web.HttpContext.Current.Session != null)
                {
                    object o = System.Web.HttpContext.Current.Session["OAuth2"];
                    if (o != null)
                    {
                        return o as OAuth2Base;
                    }
                }
                return null;
            }
            set
            {
                System.Web.HttpContext.Current.Session["OAuth2"] = value;
            }
        }
        static Dictionary<string, OAuth2Base> _ServerList;
        /// <summary>
        /// 获取所有的类型（新开发的OAuth2需要到这里注册添加一下）
        /// </summary>
        public static Dictionary<string, OAuth2Base> ServerList
        {
            get
            {
                if (_ServerList == null)
                {
                    _ServerList = new Dictionary<string, OAuth2Base>(StringComparer.OrdinalIgnoreCase);
                    _ServerList.Add(OAuthServer.weibo.ToString(), new OAuths.weiboOAuth());//新浪微博
                    _ServerList.Add(OAuthServer.qq.ToString(), new OAuths.qqOAuth());//QQ微博
                    _ServerList.Add(OAuthServer.taoBao.ToString(), new OAuths.taobaoOAuth());//淘宝
                    _ServerList.Add(OAuthServer.renren.ToString(), new OAuths.renrenOAuth());//人人网
                    _ServerList.Add(OAuthServer.weixin.ToString(), new OAuths.weixinOAuth());//微信
                    _ServerList.Add(OAuthServer.baidu.ToString(), new OAuths.baiduOAuth());//百度账号
                    _ServerList.Add(OAuthServer.jd.ToString(), new OAuths.jdOAuth());//京东账号
                    _ServerList.Add(OAuthServer.jumbot.ToString(), new OAuths.jumbotOAuth());//将博账号
                }
                return _ServerList;
            }
        }
    }
}
