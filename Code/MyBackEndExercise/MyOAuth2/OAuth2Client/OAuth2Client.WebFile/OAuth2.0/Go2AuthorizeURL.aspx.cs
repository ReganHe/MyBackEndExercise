using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OAuth2Client.Common.OAuth2;
namespace OAuth2Client.WebFile.OAUTH2
{
    public partial class _Go2AuthorizeURL : OAuth2Client.Common.UI.BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _oauth_code = q("oauth_code");
            CheckOAuthState(_oauth_code);
            OAuth2Client.Common.Utils.Session.Del("OAuth2");
            OAuth2Base ob = OAuth2Factory.ServerList[_oauth_code]; 
            Response.Redirect(ob.GetAuthorizeURL());
        }
    }
}