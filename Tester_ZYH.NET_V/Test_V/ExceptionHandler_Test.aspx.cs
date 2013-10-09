using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Test_V
{
    public class ZYHException : Exception { public ZYHException(string m) : base(m) { } }

    public partial class ExceptionHandler_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsCallback && CallbackManager1.CallbackID == "LinkButton4")
            {
                throw new ZYHException("LinkButton4");
            }
            if (IsCallback && CallbackManager1.CallbackID == "LinkButton_SessionTimeout")
            {

                CallbackManager1.ReaseException("Session Timeout");
            }

            if (!IsCallback)
            {
                var lk = new ZYH.WebControl_V.LinkButton();
                lk.Text = "Exception - Event Triger Control is Not Been Loaded";
                lk.Click += new ZYH.WebControl_V.LinkButton.ClickHandler(lk_Click);
                PlaceHolder_ExceptionEventTrigerControlNotLoaded.Controls.Add(lk);
            }
            if (!IsPostBack)
            {
                var lk1 = new LinkButton();
                lk1.Text = "Exception - Event Triger Control is Not Been Loaded (postback)";
                lk1.Click += new EventHandler(lk1_Click);
                PlaceHolder_ExceptionEventTrigerControlNotLoaded_postback.Controls.Add(lk1);
            }
        }

        void lk1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void lk_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void LinkButton2_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            System.Threading.Thread.Sleep(2500);
        }

        protected void LinkButton3_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            var z = 0;
            var x = 5 / z;
        }

        protected void LinkButton4_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {

        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            throw new Exception("postback error");
        }

        protected void LinkButton_SessionTimeout_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {

        }
    }
}