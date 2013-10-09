using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace ZYH.WebControl_V
{
    [System.Drawing.ToolboxBitmap(typeof(CheckBoxList), "Image.ToolBox.CheckBoxList.bmp")]
    public class CheckBoxList : CheckRadioList_Base
    {
        public CheckBoxList() : base(ListControlTypes.CheckBoxList) { }

        public override void OnRaiseCallbackEvent(CallbackParameters CallbackParameters, CallbackResult CallbackResult)
        {
            base.OnRaiseCallbackEvent(CallbackParameters, CallbackResult);
            _Data.Items.ResolveImgUrl(Page);
            CallbackResult.ControlReserved = _Data.ToXML();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (DesignMode)
            {
                System.Web.UI.WebControls.CheckBoxList _CheckBoxList_Design = new System.Web.UI.WebControls.CheckBoxList();
                _CheckBoxList_Design.ID = "ckl_D";
                _CheckBoxList_Design.TextAlign = this.TextAlign;
                _CheckBoxList_Design.RepeatColumns = this.RepeatColumns;
                _CheckBoxList_Design.RepeatDirection = this.RepeatDirection;
                _CheckBoxList_Design.CellPadding = this.CellPadding;
                _CheckBoxList_Design.CellSpacing = this.CellSpacing;
                foreach (ListItem it in Items) { _CheckBoxList_Design.Items.Add(it.Text); }
                if (_CheckBoxList_Design.Items.Count == 0) { _CheckBoxList_Design.Items.Add("[" + ID + "]"); }
                this.Controls.Add(_CheckBoxList_Design);
                writer.Write(RenderControl(_CheckBoxList_Design));
            }
            else
            {
                this.ControlStyle.AddAttributesToRender(writer, this);
                if (!this.Enabled) writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Disabled, "disabled");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
                if (CellPadding != -1) writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, CellPadding.ToString());
                if (CellSpacing != -1) writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, CellSpacing.ToString());
                //writer.AddAttribute("Settings", _Settings.ToJson_Js());
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderEndTag();
                _Data.Items.ResolveImgUrl(Page);
                _HiddenField.Value = Convert.ToBase64String(new System.Text.UTF8Encoding().GetBytes(_Data.ToXML()));
                writer.Write(RenderControl(_HiddenField));
                writer.RenderEndTag();
            }
        }
    }
}
