using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace ZYH.WebControl_V
{
    [System.Drawing.ToolboxBitmap(typeof(RadioButtonList), "Image.ToolBox.RadioButtonList.bmp")]
    public class RadioButtonList : CheckRadioList_Base
    {
        public RadioButtonList() : base(ListControlTypes.RadioButtonList) { }

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
                System.Web.UI.WebControls.RadioButtonList _RadioButtonList_Design = new System.Web.UI.WebControls.RadioButtonList();
                _RadioButtonList_Design.ID = "rbl_D";
                _RadioButtonList_Design.TextAlign = this.TextAlign;
                _RadioButtonList_Design.RepeatColumns = this.RepeatColumns;
                _RadioButtonList_Design.RepeatDirection = this.RepeatDirection;
                _RadioButtonList_Design.CellPadding = this.CellPadding;
                _RadioButtonList_Design.CellSpacing = this.CellSpacing;
                foreach (ListItem it in Items) { _RadioButtonList_Design.Items.Add(it.Text); }
                if (_RadioButtonList_Design.Items.Count == 0) { _RadioButtonList_Design.Items.Add("[" + ID + "]"); }
                this.Controls.Add(_RadioButtonList_Design);
                writer.Write(RenderControl(_RadioButtonList_Design));
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
