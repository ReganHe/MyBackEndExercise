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
using OAuth2Client.Common.Utils;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace OAuth2Client.WebFile.passport
{
    public partial class _register_third : OAuth2Client.Common.UI.BasicPage
    {
        public string OAuthCode = "";
        public string OpenID = "";
        public string NickName = "";
        public string HeadUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string _sn = q("sn");
            string OAuth_Info = OAuth2Client.Common.Utils.DES.DESDecrypt(_sn, StaticKey);
            try
            {
                JObject jsonObj = JObject.Parse(OAuth_Info);
                OAuthCode = jsonObj["oauthcode"].ToString();
                OpenID = jsonObj["openid"].ToString();
                NickName = jsonObj["nickname"].ToString();
                HeadUrl = jsonObj["headurl"].ToString();
            }
            catch (Exception ex)
            {
                CYQ.Data.Log.WriteLogToTxt(OAuth_Info);
            }
            //这里就是您自己的业务啦
        }
    }
}
