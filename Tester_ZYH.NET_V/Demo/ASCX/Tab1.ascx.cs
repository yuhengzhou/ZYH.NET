using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Demo.ASCX
{
    public partial class Tab1 : UserControlBase
    {
        public System.Web.UI.HtmlControls.HtmlGenericControl _div_GV;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            MyPage._CallbackManager.Refrence.Add("Tab1", this);
            _div_GV = div_GV;
        }

        public string FillGV(bool IsInitLoad, bool RenderMe)
        {
            string rt = "";
            List<Person> ds = null;
            PersonCollection pc = PersonCollection.Load(Page);
            if (TextBox_Search.Text == "")
            { ds = pc.ToList(); }
            else
            { ds = pc.Where(x => x.Name.Contains(TextBox_Search.Text)).ToList(); }
            MyPage._CallbackManager.Refrence.Add("Tab1_Data", pc);
            GridView_ListPerson.DataSource = ds;
            GridView_ListPerson.DataBind();
            if (RenderMe) rt = MyPage._CallbackManager.RenderControl(GridView_ListPerson);
            return rt;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FillGV(IsInitLoad, false);
            if (!MyPage._CallbackManager.Scripts.IsScriptRegistered("Tab1_js"))
            {
                string st = "";
                st += "function " + ClientID + "_FillGV(id,e){" + "\r\n";
                st += "$('#" + _div_GV.ClientID + "').html(e.ArgToClient);" + "\r\n";
                st += "}" + "\r\n";
                MyPage._CallbackManager.Scripts.RegisterClientScriptBlock("Tab1_js", st);
            }
            TextBox_Search.ClientEvent_AfterCallback_KeyUp = ClientID + "_FillGV";
        }

        protected void Window_EditPerson_LoadFromServer(object sender, ZYH.WebControl_V.WindowEventArgs e)
        {
            UserControlBase c = (UserControlBase)LoadControl("~/Demo/ASCX/EditPerson.ascx");
            c.ID = "EditPerson";
            c.IsInitLoad = e.IsInitLoad;
            if (e.ArgFromClient == "")
            {
                c.PersonId = Guid.Empty; e.Title = "Add Person";
            }
            else
            {
                c.PersonId = new Guid(e.ArgFromClient); e.Title = "Edit Person";
            }
            Window_EditPerson.Controls.Add(c);
            if (e.RenderMe) { e.HtmlToBeLoaded = MyPage._CallbackManager.RenderControl(c); }
        }

        protected void GridView_ListPerson_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Person p = (Person)e.Row.DataItem;
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLink_Edit");
                hl.NavigateUrl = "javascript:Load_EditPerson('" + p.Id.ToString() + "');";

                e.Row.Cells[4].Text = p.Comment.Replace("\r\n", "<br/>");
            }
        }

        protected void TextBox_Search_KeyUp(object sender, ZYH.WebControl_V.TextBoxEventArg e)
        {
            e.ArgToClient = FillGV(IsInitLoad, true);
        }

    }
}