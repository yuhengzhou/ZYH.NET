using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Tester_ZYH.NET_V
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        //protected void Session_Start(object sender, EventArgs e)
        //{

        //}

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            System.IO.StreamReader R = new System.IO.StreamReader(Request.InputStream);
            var s = R.ReadToEnd();
            Request.InputStream.Seek(0, System.IO.SeekOrigin.Begin );
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Context.Server.GetLastError();
            string ExceptionName = ex.GetType().Name;
            if (ExceptionName != "HttpException" || ExceptionName != "HandledException")
            {
                System.IO.StreamReader R = new System.IO.StreamReader(Request.InputStream);
                var OriginalRequestString = R.ReadToEnd();

            }
        }

        //protected void Session_End(object sender, EventArgs e)
        //{

        //}

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}