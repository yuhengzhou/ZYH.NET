using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace ZYH.WebControl_V
{
    [DefaultEvent("Click"), ParseChildren(false), ControlBuilder(typeof(LinkButtonControlBuilder)), System.Drawing.ToolboxBitmap(typeof(LinkButton), "Image.ToolBox.LinkButton.bmp")]
    public class LinkButton : CallbackControl_Base
    {
        private bool _textSetByAddParsedSubObject;
        private Image _img;

        public LinkButton() : base(System.Web.UI.HtmlTextWriterTag.A) { }

        #region Event
        public delegate void ClickHandler(object sender, CallbackEventArgs e);
        public event ClickHandler Click;

        public override void OnRaiseCallbackEvent(CallbackParameters CallbackParameters, CallbackResult CallbackResult)
        {
            this.CommandArg = CallbackParameters.ControlReserved;
            CallbackEventArgs e = new CallbackEventArgs();
            e.CallbackManager = this.CallbackManager;
            e.ArgFromClient = CallbackParameters.ArgFromClient;
            e.Command = CallbackParameters.Command;
            e.CommandArg = CallbackParameters.CommandArg;
            e.CallbackOptions = this._CallbackOptions;
            if (Click != null) Click(this, e);
            CallbackResult.ArgToClient = e.ArgToClient;
            CallbackResult.CallbackOptions = e.CallbackOptions;
            CallbackResult.ControlReserved = "";
        }
        #endregion

        #region Property
        private string _Text = "";
        [DefaultValue(""), Description(""), Browsable(true), Category("Appearance"), PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public string Text
        {
            get { return _Text; }
            set
            { if (this.HasControls()) { this.Controls.Clear(); } _Text = value; }
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

        private string _ImgUrl_MouseOver = "";
        [DefaultValue(""), Description(""), Browsable(true), Category("Appearance"), System.ComponentModel.Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ImgUrl_MouseOver
        {
            get { return _ImgUrl_MouseOver; }
            set
            { _ImgUrl_MouseOver = value; }
        }
        #endregion

        protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
        {
            if (!Enabled) writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
            base.AddAttributesToRender(writer);

            if (_ImgUrl != "")
            {
                if (_img == null) { _img = new Image(); this.Controls.Add(_img); }
                _img.BorderWidth = 0;
                _img.ID = "Img";
                _img.ImageUrl = _ImgUrl;
                if (ImgUrl_MouseOver != "" && Enabled)
                {
                    string MouseOverAction = "SwitchImg('" + _img.ClientID + "','" + ResolveClientUrl(ImgUrl_MouseOver) + "');";
                    string MouseOutAction = "SwitchImg('" + _img.ClientID + "','" + ResolveClientUrl(ImgUrl) + "');";
                    writer.AddAttribute("onmouseover", MouseOverAction);
                    writer.AddAttribute("onmouseout", MouseOutAction);
                }
            }
            if (DesignMode)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
            }
            else
            {
                if (Enabled)
                {
                    string st = "javascript:LinkButton_DoCallback(" + CallbackManager.ClientInstanceName + ",'" + UniqueID + "','" + ClientID + "','" + Convert.ToInt32(CallbackCommands.LinkButton_Click) + "', '" + CommandArg + "'," + ((ClientEvent_BeforeCallback == "") ? "null" : ClientEvent_BeforeCallback) + "," + ((ClientEvent_AfterCallback == "") ? "null" : ClientEvent_AfterCallback) + "," + ((Click == null) ? "true" : "false") + ",'" + EventTriggerHierarchyID + "'," + SerializableObjectBase.ToJson_Js(_CallbackOptions) + ");";
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, st);
                }
            }
        }

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            if (this.HasControls())
            {
                base.RenderContents(writer);
            }
            else
            {
                if (Text != "")
                { writer.Write(Text); }
                else
                {
                    if (ImgUrl == "")
                    {
                        if (DesignMode) { writer.Write("[" + ID + "]"); } else { writer.Write(ID); }
                    }
                }
            }
        }

        protected override void AddParsedSubObject(object obj)
        {
            if (this.HasControls())
            {
                base.AddParsedSubObject(obj);
            }
            else if (obj is LiteralControl)
            {
                if (this._textSetByAddParsedSubObject)
                {
                    this.Text = this.Text + ((LiteralControl)obj).Text;
                }
                else
                {
                    this.Text = ((LiteralControl)obj).Text;
                }
                this._textSetByAddParsedSubObject = true;
            }
            else
            {
                string text = this.Text;
                if (text.Length != 0)
                {
                    this.Text = null;
                    base.AddParsedSubObject(new LiteralControl(text));
                }
                base.AddParsedSubObject(obj);
            }
        }

        protected override void OnBeforeRender()
        {
            base.OnBeforeRender();
            if (Enabled)
            {
                string st;
                if (!CallbackManager.Scripts.IsScriptRegistered("LinkButtonJs"))
                {
                    st = Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Js.LinkButton.js");
                    CallbackManager.Scripts.RegisterClientScriptInclude("LinkButtonJs", st);
                }

            }
        }
    }
}
