using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;

namespace ZYH.WebControl_V
{
    [DefaultEvent("CheckedChanged"), System.Drawing.ToolboxBitmap(typeof(CheckBox), "Image.ToolBox.CheckBox.bmp")]
    public class CheckBox : CallbackControl_Base
    {
        #region Event
        public delegate void CheckedChangedHandler(object sender, CheckBoxEventArgs e);
        public event CheckedChangedHandler CheckedChanged;

        public override void OnRaiseCallbackEvent(CallbackParameters CallbackParameters, CallbackResult CallbackResult)
        {
            this.CommandArg = CallbackParameters.CommandArg;
            CheckBoxEventArgs e = new CheckBoxEventArgs();
            e.ArgFromClient = CallbackParameters.ArgFromClient;
            e.Command = CallbackParameters.Command;
            e.CallbackManager = this.CallbackManager;
            e.CommandArg = this.CommandArg;
            e.Checked = this.Checked;
            e.CallbackOptions = this._CallbackOptions;
            if (CheckedChanged != null) CheckedChanged(this, e);
            CallbackResult.ArgToClient = e.ArgToClient;
            CallbackResult.CallbackOptions = e.CallbackOptions;
            CallbackResult.ControlReserved = e.Checked.ToString();
        }
        #endregion

        #region Property
        private bool _Checked = false;
        [DefaultValue(false)]
        public bool Checked
        {
            get { return _Checked; }
            set { _Checked = value; }
        }

        private string _Text = "";
        [DefaultValue("")]
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        private string _ImgUrl = "";
        [DefaultValue(""), Description(""), Browsable(true), Category("Appearance"), System.ComponentModel.Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), System.ComponentModel.RefreshProperties(RefreshProperties.All)]
        public string ImgUrl
        {
            get { return _ImgUrl; }
            set
            {
                _ImgUrl = value;
            }
        }

        private System.Web.UI.WebControls.TextAlign _TextAlign = System.Web.UI.WebControls.TextAlign.Right;
        [DefaultValue(typeof(System.Web.UI.WebControls.TextAlign), "Right")]
        public System.Web.UI.WebControls.TextAlign TextAlign
        {
            get { return _TextAlign; }
            set { _TextAlign = value; }
        }
        #endregion

        private void InitPostData()
        {
            object o = Page.Request.Params[this.UniqueID];
            if (o != null && o.ToString() == "on") _Checked = true; else _Checked = false;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Page.IsPostBack)
            {
                if (Page.IsCallback)
                {
                    if (CallbackManager.CallbackID == null) CallbackManager.InitCallbackParams();
                    if (CallbackManager.CallbackID == this.UniqueID)
                    {
                        string chked = CallbackManager.CallbackParameters.ControlReserved;
                        _Checked = Convert.ToBoolean(chked);
                    }
                    else if (CallbackManager.CallbackParameters.CallbackOptions.PostDataContainer != "NA")
                    {
                        InitPostData();
                    }
                }
                else
                {
                    InitPostData();
                }
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            this.ControlStyle.AddAttributesToRender(writer, this);
            if (!this.Enabled) writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Disabled, "disabled");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            switch (_TextAlign)
            {
                case System.Web.UI.WebControls.TextAlign.Left:
                    RenderLable(writer);
                    RenderInputTag(writer);
                    break;
                case System.Web.UI.WebControls.TextAlign.Right:
                    RenderInputTag(writer);
                    RenderLable(writer);
                    break;
            }
            writer.RenderEndTag();
            base.RenderContents(writer);
        }

        private void RenderLable(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.For, this.ClientID);
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            if (DesignMode && _Text == "" && _ImgUrl == "")
                writer.Write("[" + ID + "]");
            else
                if (_ImgUrl != "")
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.For, this.ClientID);
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, Page.ResolveClientUrl(_ImgUrl));
                    writer.AddAttribute(HtmlTextWriterAttribute.Style, "border-width:0px;");
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag();
                }
            if (_Text != "") writer.Write(System.Web.HttpUtility.HtmlEncode(_Text));
            writer.RenderEndTag();
        }

        private void RenderInputTag(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
            if (this.UniqueID != null) writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
            if (_Checked) writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
            if (AccessKey.Length > 0) writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, AccessKey);
            if (TabIndex != 0) writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, TabIndex.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (!DesignMode)
            {
                string st = "javascript:CheckBox_OnCheckedChanged(" + CallbackManager.ClientInstanceName + ",'" + UniqueID + "','" + ClientID + "','" + Convert.ToInt32(CallbackCommands.CheckBox_CheckChanged) + "','" + CommandArg + "'," + ((ClientEvent_BeforeCallback == "") ? "null" : ClientEvent_BeforeCallback) + "," + ((ClientEvent_AfterCallback == "") ? "null" : ClientEvent_AfterCallback) + "," + ((CheckedChanged == null) ? "true" : "false") + ",'" + EventTriggerHierarchyID + "'," + this._CallbackOptions.ToJson_Js() + ");";
                writer.AddAttribute("onclick", st);
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        protected override void OnBeforeRender()
        {
            if (Enabled)
            {
                string st;
                if (!CallbackManager.Scripts.IsScriptRegistered("CheckBoxJs"))
                {
                    st = Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Js.CheckBox.js");
                    CallbackManager.Scripts.RegisterClientScriptInclude("CheckBoxJs", st);
                }
            }
            base.OnBeforeRender();
        }

        public override void RenderBeginTag(HtmlTextWriter writer) { }
        public override void RenderEndTag(HtmlTextWriter writer) { }
    }
}
