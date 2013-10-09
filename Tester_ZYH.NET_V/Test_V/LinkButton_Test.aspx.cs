using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Test_V
{
    public partial class LinkButton_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList1.Items.Add("AAA");
                DropDownList1.Items.Add("BBB");
                DropDownList1.Items.Add("CCC");
                DropDownList1.Items.Add("AAAA");
                DropDownList1.Items.Add("BBBB");
                DropDownList1.Items.Add("CCCC");

                CallbackManager1.ViewStates["kkk"] = "kkkkk";
                
                //Session["N"] = 0;
            }
        }

        protected void LinkButton1_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {

        }

        protected void LinkButton2_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
        }

        protected void LinkButton2_SaveState(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            e.CallbackManager.ControlStates[LinkButton2.ID, "VVVVV"] = "true";
        }

        protected void LinkButton2_RestoreState(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            var v = e.CallbackManager.ControlStates[LinkButton2.ID, "VVVVV"];
        }

        protected void LinkButton3_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            //if (!e.CallbackManager.Scripts.IsScriptRegistered("Include")) e.CallbackManager.Scripts.RegisterClientScriptInclude("Include", "~/Test_V/RegisterJsTest.js");
            e.CallbackManager.Scripts.RegisterClientScriptInclude("Include", "~/Test_V/RegisterJsTest.js", "1.1");
            e.CallbackManager.Scripts.RegisterStartupScript("StartUp", "ALERT();");
            e.ArgToClient = "x\n\r|y";
        }

        protected void LinkButton4_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
        }

        protected void LinkButton6_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
        }

        protected void LinkButton7_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}