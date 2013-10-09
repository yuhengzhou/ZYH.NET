using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Xml.Linq;

namespace ZYH.WebControl_V
{
    [System.Drawing.ToolboxBitmap(typeof(DropDownList), "Image.ToolBox.DropDownList.bmp")]
    public class DropDownList : ListControl_Base
    {
        public DropDownList() : base(ListControlTypes.DropDownList) { }

        public override void OnRaiseCallbackEvent(CallbackParameters CallbackParameters, CallbackResult CallbackResult)
        {
            base.OnRaiseCallbackEvent(CallbackParameters, CallbackResult);
            _Data.Html = RenderDropDownList(true);
            CallbackResult.ControlReserved = _Data.ToXML();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (DesignMode)
            {
                System.Web.UI.WebControls.DropDownList _DropDownList_Design = new System.Web.UI.WebControls.DropDownList();
                _DropDownList_Design.ID = "ddl_D";
                _DropDownList_Design.ControlStyle.CopyFrom(this.ControlStyle);
                foreach (ListItem it in Items) { _DropDownList_Design.Items.Add(it.Text); }
                if (_DropDownList_Design.Items.Count == 0) { _DropDownList_Design.Items.Add("[" + ID + "]"); }
                writer.Write(RenderControl(_DropDownList_Design));
            }
            else
            {
                if (Items.Count > 0 && SelectedItems.Count == 0) { Items[0].Selected = true; }
                if (!this.Enabled) writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Disabled, "disabled");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(RenderDropDownList(false));
                writer.RenderEndTag();
                _HiddenField.Value = Convert.ToBase64String(new System.Text.UTF8Encoding().GetBytes(_Data.ToXML()));
                writer.Write(RenderControl(_HiddenField));
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer) { }
        public override void RenderEndTag(HtmlTextWriter writer) { }

        private string RenderDropDownList(bool RenderItemsOnly)
        {
            _DropDownList ddl = new _DropDownList(RenderItemsOnly);
            ddl.Attributes.Add("id", this.ClientID); ddl.Attributes.Add("name", this.UniqueID);
            ddl.ControlStyle.CopyFrom(this.ControlStyle);
            //if (!RenderItemsOnly) {ddl.Attributes.Add("Settings", _Settings.ToJson_Js()); }
            foreach (ListItem it in Items)
            {
                System.Web.UI.WebControls.ListItem i = new System.Web.UI.WebControls.ListItem(it.Text, it.Value, it.Enabled); i.Selected = it.Selected;
                ddl.Items.Add(i);
            }
            return RenderControl(ddl);
        }

    }

    [ToolboxItem(false)]
    internal class _DropDownList : System.Web.UI.WebControls.DropDownList
    {
        bool RenderItemsOnly;
        internal _DropDownList(bool _RenderItemsOnly) { RenderItemsOnly = _RenderItemsOnly; }
        public override void RenderBeginTag(HtmlTextWriter writer) { if (!RenderItemsOnly) { base.RenderBeginTag(writer); } }
        public override void RenderEndTag(HtmlTextWriter writer) { if (!RenderItemsOnly) { base.RenderEndTag(writer); } }
    }
}
