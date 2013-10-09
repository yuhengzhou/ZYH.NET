using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Demo.ASCX
{
    public partial class EditPerson : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var x1 = this.MyPage._CallbackManager.Refrence.Get<PageBase>("MyPage");
            var x2 = this.MyPage._CallbackManager.Refrence.Get<List<Person>>("Tab1_Data");
            var x3 = this.MyPage._CallbackManager.Refrence.Get<Tab1>("Tab1");
            var x4 = this.MyPage._CallbackManager.Refrence[""];

            if (this.IsInitLoad)
            {
                if (this.PersonId != Guid.Empty)
                {
                    var per = x2.Where(x => x.Id == this.PersonId).First();
                    TextBox_Name.Text = per.Name;
                    TextBox_Age.Text = per.Age.ToString();
                    TextBox_Comment.Text = per.Comment;
                    Calendar_Birthday.Value = per.Birthday;
                    RadioButtonList_Sex.Items.Where(x => x.Value == Convert.ToInt32(per.Sex).ToString()).First().Selected = true;
                }
            }

            MyPage._CallbackManager.RegisterJavascript += new ZYH.WebControl_V.CallbackManager.RegisterJavascriptHandler(_CallbackManager_RegisterJavascript);
            ZYH.WebControl_V.Window p = (ZYH.WebControl_V.Window)this.Parent;
            HyperLink_Cancel.NavigateUrl = "javascript:" + p.ClientID + "Instance.Close();"; ;
            LinkButton_Save.PostDataContainer = "table_EditPoerson,div_TextBox_Search";
            LinkButton_Save.ClientEvent_AfterCallback = ClientID + "_Save_AfterCall";
        }

        void _CallbackManager_RegisterJavascript(ZYH.WebControl_V.CallbackEventArgs e)
        {
            if (!MyPage._CallbackManager.Scripts.IsScriptRegistered("EditPerson_js"))
            {
                ZYH.WebControl_V.Window p = (ZYH.WebControl_V.Window)this.Parent;
                var Tab1 = this.MyPage._CallbackManager.Refrence.Get<Tab1>("Tab1");

                string st = "";
                st += "function " + ClientID + "_Save_AfterCall(id,e){" + "\r\n";
                st += "if(e.ArgToClient.startsWith('error:')){$('#" + Label_ErrorMessage.ClientID + "').html(e.ArgToClient.substr(6)); return;}" + "\r\n";
                st += p.ClientID + "Instance.Close();" + "\r\n";
                if (Tab1 != null) st += "$('#" + Tab1._div_GV.ClientID + "').html(e.ArgToClient);" + "\r\n";
                st += "}" + "\r\n";
                MyPage._CallbackManager.Scripts.RegisterClientScriptBlock("EditPerson_js", st);
            }
        }

        protected void LinkButton_Save_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            string ErrorMsg = "";

            if (TextBox_Name.Text.Trim() == "") { ErrorMsg += "<li>Name is reqquired.</li>"; }
            int Age = 0;
            try { Age = Convert.ToInt32(TextBox_Age.Text); }
            catch { ErrorMsg += "<li>Invalid Age.</li>"; }
            if (ErrorMsg != "")
            {
                e.ArgToClient = "error:" + ErrorMsg;
                return;
            }
            Person p = new Person()
            {
                Id = this.PersonId,
                Name = TextBox_Name.Text.Trim(),
                Age = Age,
                Birthday=Calendar_Birthday.Value,
                Sex = (PersonSex)Convert.ToInt32(RadioButtonList_Sex.SelectedItems[0].Value),
                Comment = TextBox_Comment.Text
            };
            PersonCollection pc = PersonCollection.Load(Page);
            pc.Add(p);
            pc.Save(Page);

            var Tab1 = this.MyPage._CallbackManager.Refrence.Get<Tab1>("Tab1");
            if (Tab1 != null) e.ArgToClient = Tab1.FillGV(true, true);
        }

    }
}