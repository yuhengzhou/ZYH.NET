using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Web.UI;
using System.Xml.Linq;

namespace ZYH.WebControl_V
{
    [ToolboxBitmap(typeof(ListBox), "Image.ToolBox.ListBox.png")]
    public class ListBox : ListControl_Base
    {
        public ListBox() : base(ListControlTypes.ListBox) { }

        #region Event
        public override void OnRaiseCallbackEvent(CallbackParameters CallbackParameters, CallbackResult CallbackResult)
        {
            base.OnRaiseCallbackEvent(CallbackParameters, CallbackResult);
            _Data.Html = RenderListBox(true);
            CallbackResult.ControlReserved = _Data.ToXML();
        }
        #endregion

        #region Property
        private System.Web.UI.WebControls.ListSelectionMode _SelectionMode = System.Web.UI.WebControls.ListSelectionMode.Single;
        [DefaultValue(typeof(System.Web.UI.WebControls.ListSelectionMode), "Single"), Browsable(true), Category("Behavior")]
        public System.Web.UI.WebControls.ListSelectionMode SelectionMode
        {
            get { return _SelectionMode; }
            set { _SelectionMode = value; }
        }
        #endregion

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (DesignMode)
            {
                System.Web.UI.WebControls.ListBox _ListBox_Design = new System.Web.UI.WebControls.ListBox();
                _ListBox_Design.ID = "lbx_D";
                _ListBox_Design.ControlStyle.CopyFrom(this.ControlStyle);
                foreach (ListItem it in Items) { _ListBox_Design.Items.Add(it.Text); }
                if (_ListBox_Design.Items.Count == 0) { _ListBox_Design.Items.Add("[" + ID + "]"); }
                _ListBox_Design.Width = this.Width; _ListBox_Design.Height = this.Height;
                writer.Write(RenderControl(_ListBox_Design));
            }
            else
            {
                if (!this.Enabled) writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Disabled, "disabled");
                writer.AddAttribute("Settings", _Settings.ToJson_Js());
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(RenderListBox(false));
                writer.RenderEndTag();
                _HiddenField.Value = Convert.ToBase64String(new System.Text.UTF8Encoding().GetBytes(_Data.ToXML()));
                writer.Write(RenderControl(_HiddenField));
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer) { }
        public override void RenderEndTag(HtmlTextWriter writer) { }

        private string RenderListBox(bool RenderItemsOnly)
        {
            _ListBox lbx = new _ListBox(RenderItemsOnly);
            lbx.Attributes.Add("id", this.ClientID); lbx.Attributes.Add("name", this.UniqueID);
            lbx.ControlStyle.CopyFrom(this.ControlStyle);
            lbx.SelectionMode = this.SelectionMode;
            foreach (ListItem it in Items)
            {
                System.Web.UI.WebControls.ListItem i = new System.Web.UI.WebControls.ListItem(it.Text, it.Value, it.Enabled); i.Selected = it.Selected;
                lbx.Items.Add(i);
            }
            return RenderControl(lbx);
        }
    }


    [ToolboxItem(false)]
    internal class _ListBox : System.Web.UI.WebControls.ListBox
    {
        bool RenderItemsOnly;
        internal _ListBox(bool _RenderItemsOnly) { RenderItemsOnly = _RenderItemsOnly; }
        public override void RenderBeginTag(HtmlTextWriter writer) { if (!RenderItemsOnly) { base.RenderBeginTag(writer); } }
        public override void RenderEndTag(HtmlTextWriter writer) { if (!RenderItemsOnly) { base.RenderEndTag(writer); } }
    }

}
