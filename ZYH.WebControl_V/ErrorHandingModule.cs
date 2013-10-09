using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ZYH.WebControl_V
{
    public class ErrorHandingModule : IHttpModule
    {
        #region IHttpModule Members
        public void Init(HttpApplication application)
        {
            application.Error += new EventHandler(Application_Error);
        }
        public void Dispose() { }
        #endregion

        public void Application_Error(object sender, EventArgs e)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx.Request.Form.AllKeys.Contains("__CALLBACKID"))
            {
                Exception ex = ctx.Server.GetLastError();
                if (ex.InnerException != null) ex = ex.InnerException;

                ServerSideExcetion _ServerSideExcetion = new ServerSideExcetion();
                _ServerSideExcetion.Source = ex.Source;
                _ServerSideExcetion.Message = ex.Message;
                _ServerSideExcetion.StackTrace = ex.StackTrace;

                ctx.Response.Write("e" + _ServerSideExcetion.ToXML());
                ctx.Response.Flush();
                ctx.Response.End();
            }
        }
    }
}
