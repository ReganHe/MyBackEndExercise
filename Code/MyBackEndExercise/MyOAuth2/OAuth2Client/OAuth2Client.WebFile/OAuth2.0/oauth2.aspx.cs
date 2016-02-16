using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using OAuth2Client.Common.OAuth2;

namespace OAuth2Client.WebFile.OAUTH2
{
    public partial class _oauth2 : OAuth2Client.Common.UI.BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _oauth_code = (this.lblOAuthCode.Text == "{$OAuthCode}") ? q("oauth_code") : this.lblOAuthCode.Text;
            CheckOAuthState(_oauth_code);
            OAuth2Base ob = OAuth2Factory.Current;
            if (ob != null) //说明用户点击了授权，并跳回登陆界面来
            {
                string account = string.Empty;
                if (ob.Authorize(out account))//检测是否授权成功，并返回绑定的账号（具体是绑定ID还是用户名，你的选择）
                {
                    string _NickName = ob.nickName;
                    string _HeadUrl = ob.headUrl;
                    string _json = "{\"oauthcode\":\"" + _oauth_code + "\",\"openid\":\"" + ob.openID + "\",\"nickname\":\"" + _NickName + "\",\"headurl\":\"" + _HeadUrl + "\"}";
                    CYQ.Data.Log.WriteLogToTxt(_json);
                    Response.Redirect("/Passport/register_third.aspx?sn=" + OAuth2Client.Common.Utils.DES.DESEncrypt(_json, StaticKey));

                }
                else
                {
                    CYQ.Data.Log.WriteLogToTxt(Request.Url.ToString());
                    Response.Redirect("/Default.aspx");
                }
            }
            else // 读取授权失败。
            {
                Response.Redirect("/Default.aspx");
            }
        }
    }
}