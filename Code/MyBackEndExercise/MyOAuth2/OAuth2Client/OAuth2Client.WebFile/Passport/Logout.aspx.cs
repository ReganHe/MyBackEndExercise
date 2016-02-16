using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OAuth2Client.WebFile.Passport
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OAuth2Client.Common.Utils.Session.Del("OAuth2");
            Response.Redirect("/Default.aspx");
        }
    }
}