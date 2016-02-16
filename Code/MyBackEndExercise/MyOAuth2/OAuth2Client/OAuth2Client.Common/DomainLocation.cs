using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;
namespace OAuth2Client.Common
{
    public class DomainLocation : IHttpModule
    {
        public static string WebDomain = System.Configuration.ConfigurationManager.AppSettings["OAuth2Client:WebDomain"];
       
        public void Dispose()
        {
        }
        public void Init(HttpApplication context)
        {
            context.AuthorizeRequest += (new EventHandler(Process301));
        }
        public void Process301(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpRequest request = app.Context.Request;
            string lRequestedPath = request.Url.DnsSafeHost.ToString();
            if (lRequestedPath.Equals(WebDomain))
                return;
            //app.Response.Write(request.RawUrl.ToString());
            //app.Response.End();
            app.Response.StatusCode = 301;
            app.Response.AddHeader("Location", "http://" + WebDomain + request.RawUrl.ToString().Trim());
            app.Response.End();
        }
    }
}