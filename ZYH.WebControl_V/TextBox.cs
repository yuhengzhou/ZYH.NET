using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZYH.WebControl_V
{
    [DefaultEvent("KeyUp"), System.Drawing.ToolboxBitmap(typeof(TextBox), "Image.ToolBox.TextBox.bmp")]
    public class TextBox : CallbackControl_Base
    {
        public System.Web.UI.WebControls.TextBox _TextBox = new System.Web.UI.WebControls.TextBox();
        private TextBoxData _Data = new TextBoxData();
        private TextBoxSettings _Settings = new TextBoxSettings();

        #region Event
        public delegate void KeyDownHandler(object sender, TextBoxEventArg e);
        public event KeyDownHandler KeyDown;
        public delegate void KeyUpHandler(object sender, TextBoxEventArg e);
        /// <summary>
        /// This event is fired by either keyup or paste. e.KeyCode=0 when fired by paste.
        /// </summary>
        public event KeyUpHandler KeyUp;
        public delegate void GetFocusHandler(object sender, TextBoxEventArg e);
        public event GetFocusHandler GetFocus;
        public delegate void LostFocusHandler(object sender, TextBoxEventArg e);
        public event LostFocusHandler LostFocus;

        public override void OnRaiseCallbackEvent(CallbackParameters CallbackParameters, CallbackResult CallbackResult)
        {
            TextBoxEventArg e = new TextBoxEventArg();
            e.CallbackManager = this.CallbackManager;
            e.ArgFromClient = CallbackParameters.ArgFromClient;
            e.Command = CallbackParameters.Command;
            e.CommandArg = this.CommandArg;
            e.CallbackOptions = this._CallbackOptions;
            e.Text = this.Text;
            e.Alt = _Data.Alt;
            e.Ctrl = _Data.Ctrl;
            e.Shift = _Data.Shift;
            e.KeyCode = _Data.KeyCode;
            e.CursorPosition = _Data.CursorPosition;
            switch (e.Command)
            {
                case CallbackCommands.TextBox_KeyDown:
                    OnKeyDown(e);
                    break;
                case CallbackCommands.TextBox_KeyUp:
                    OnKeyUp(e);
                    break;
                case CallbackCommands.TextBox_GetFocus:
                    OnGetFocus(e);
                    break;
                case CallbackCommands.TextBox_LostFocus:
                    OnLostFocus(e);
                    break;
            }
            CallbackResult.ArgToClient = e.ArgToClient;
            CallbackResult.CallbackOptions = e.CallbackOptions;
            _Data.Alt = e.Alt;
            _Data.Ctrl = e.Ctrl;
            _Data.Shift = e.Shift;
            _Data.KeyCode = e.KeyCode;
            _Data.Text = e.Text;
            _Data.CursorPosition = e.CursorPosition;
            CallbackResult.ControlReserved = _Data.ToXML();
        }

        public virtual void OnKeyDown(TextBoxEventArg e) { if (KeyDown != null) KeyDown(this, e); }

        public virtual void OnKeyUp(TextBoxEventArg e) { if (KeyUp != null) KeyUp(this, e); }

        public virtual void OnGetFocus(TextBoxEventArg e) { if (GetFocus != null) GetFocus(this, e); }

        public virtual void OnLostFocus(TextBoxEventArg e) { if (LostFocus != null) LostFocus(this, e); }
        #endregion

        #region Property
        [DefaultValue(typeof(System.Web.UI.WebControls.TextBoxMode), "SingleLine")]
        public System.Web.UI.WebControls.TextBoxMode TextMode
        {
            get { return _TextBox.TextMode; }
            set { _TextBox.TextMode = value; }
        }

        [DefaultValue("")]
        public string Text
        {
            get { return _TextBox.Text; }
            set { _TextBox.Text = value; }
        }

        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return _TextBox.ReadOnly; }
            set { _TextBox.ReadOnly = value; }
        }

        [DefaultValue(true)]
        public bool Wrap
        {
            get { return _TextBox.Wrap; }
            set { _TextBox.Wrap = value; }
        }

        [DefaultValue(0)]
        public int Columns
        {
            get { return _TextBox.Columns; }
            set { _TextBox.Columns = value; }
        }

        [DefaultValue(0)]
        public int Rows
        {
            get { return _TextBox.Rows; }
            set { _TextBox.Rows = value; }
        }

        [DefaultValue(0)]
        public int MaxLength
        {
            get { return _TextBox.MaxLength; }
            set { _TextBox.MaxLength = value; }
        }

        [DefaultValue(0)]
        public override short TabIndex
        {
            get { return _TextBox.TabIndex; }
            set { _TextBox.TabIndex = value; }
        }

        [DefaultValue(200), Category("Behavior"), Description("if > 0, delay the event until the value of minisecond after last key up.")]
        public int KeyUpEventDelay
        {
            get { return _Settings.KeyUpEventDelay; }
            set { _Settings.KeyUpEventDelay = value; }
        }

        [Category("Appearance"), Description("Get or set alternating back color of text box."), DefaultValue(typeof(System.Drawing.Color), "MintCream"), RefreshProperties(RefreshProperties.All), TypeConverter(typeof(System.Web.UI.WebControls.WebColorConverter))]
        public System.Drawing.Color OnFocusBackColor
        {
            get { return System.Drawing.ColorTranslator.FromHtml(_Settings.OnFocusBackColor); }
            set { _Settings.OnFocusBackColor = System.Drawing.ColorTranslator.ToHtml(value); }
        }

        private System.Drawing.Color _ReadOnlyBackColor = System.Drawing.Color.PapayaWhip;
        [Category("Appearance"), Description("Get or set alternating back color of text box."), DefaultValue(typeof(System.Drawing.Color), "PapayaWhip"), RefreshProperties(RefreshProperties.All), TypeConverter(typeof(System.Web.UI.WebControls.WebColorConverter))]
        public System.Drawing.Color ReadOnlyBackColor
        {
            get { return _ReadOnlyBackColor; }
            set { _ReadOnlyBackColor = value; }
        }

        #region Client Event
        [Category("Behavior"), Description("not applicable."), DefaultValue("not applicable")]
        public new string ClientEvent_BeforeCallback { get { return "not applicable"; } }

        [Category("Behavior"), Description("not applicable."), DefaultValue("not applicable")]
        public new string ClientEvent_AfterCallback { get { return "not applicable"; } }

        [Category("Behavior"), DefaultValue(""), Description("function BeforeCall(SenderID, e_BeforeCall) {} var e_BeforeCall = new Object(); e_BeforeCall.CancelCall. = false; e_BeforeCall.ArgFromClient = ''; e_BeforeCall.ControlReserved = ControlReserved; e_BeforeCall.Text = text; ")]
        public string ClientEvent_BeforeCallback_GetFocus
        {
            get { return _Settings.ClientEvent_BeforeCallback_GetFocus; }
            set { _Settings.ClientEvent_BeforeCallback_GetFocus = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function AfterCall(SenderID, e_AfterCall) {} var e_AfterCall = new Object(); e_AfterCall.CancelCall. = false; e_AfterCall.ArgFromClient = ''; e_AfterCall.ControlReserved = ControlReserved; e_AfterCall.Text = text; ")]
        public string ClientEvent_AfterCallback_GetFocus
        {
            get { return _Settings.ClientEvent_AfterCallback_GetFocus; }
            set { _Settings.ClientEvent_AfterCallback_GetFocus = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function BeforeCall(SenderID, e_BeforeCall) {} var e_BeforeCall = new Object(); e_BeforeCall.CancelCall. = false; e_BeforeCall.ArgFromClient = ''; e_BeforeCall.ControlReserved = ControlReserved; e_BeforeCall.Text = text; ")]
        public string ClientEvent_BeforeCallback_LostFocus
        {
            get { return _Settings.ClientEvent_BeforeCallback_LostFocus; }
            set { _Settings.ClientEvent_BeforeCallback_LostFocus = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function AfterCall(SenderID, e_AfterCall) {} var e_AfterCall = new Object(); e_AfterCall.CancelCall. = false; e_AfterCall.ArgFromClient = ''; e_AfterCall.ControlReserved = ControlReserved; e_AfterCall.Text = text; ")]
        public string ClientEvent_AfterCallback_LostFocus
        {
            get { return _Settings.ClientEvent_AfterCallback_LostFocus; }
            set { _Settings.ClientEvent_AfterCallback_LostFocus = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function BeforeCall(SenderID, e_BeforeCall) {} var e_BeforeCall = new Object(); e_BeforeCall.CancelCall. = false; e_BeforeCall.ArgFromClient = ''; e_BeforeCall.ControlReserved = ControlReserved; e_BeforeCall.Text = text; ")]
        public string ClientEvent_BeforeCallback_KeyDown
        {
            get { return _Settings.ClientEvent_BeforeCallback_KeyDown; }
            set { _Settings.ClientEvent_BeforeCallback_KeyDown = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function AfterCall(SenderID, e_AfterCall) {} var e_AfterCall = new Object(); e_AfterCall.CancelCall. = false; e_AfterCall.ArgFromClient = ''; e_AfterCall.ControlReserved = ControlReserved; e_AfterCall.Text = text; ")]
        public string ClientEvent_AfterCallback_KeyDown
        {
            get { return _Settings.ClientEvent_AfterCallback_KeyDown; }
            set { _Settings.ClientEvent_AfterCallback_KeyDown = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("This event is fired by either keyup or paste. e.KeyCode=0 when fired by paste. function BeforeCall(SenderID, e_BeforeCall) {} var e_BeforeCall = new Object(); e_BeforeCall.CancelCall. = false; e_BeforeCall.ArgFromClient = ''; e_BeforeCall.ControlReserved = ControlReserved; e_BeforeCall.Text = text; ")]
        public string ClientEvent_BeforeCallback_KeyUp
        {
            get { return _Settings.ClientEvent_BeforeCallback_KeyUp; }
            set { _Settings.ClientEvent_BeforeCallback_KeyUp = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("This event is fired by either keyup or paste. e.KeyCode=0 when fired by paste. function AfterCall(SenderID, e_AfterCall) {} var e_AfterCall = new Object(); e_AfterCall.CancelCall. = false; e_AfterCall.ArgFromClient = ''; e_AfterCall.ControlReserved = ControlReserved; e_AfterCall.Text = text; ")]
        public string ClientEvent_AfterCallback_KeyUp
        {
            get { return _Settings.ClientEvent_AfterCallback_KeyUp; }
            set { _Settings.ClientEvent_AfterCallback_KeyUp = value; }
        }


        #endregion

        #endregion

        private void InitPostData()
        {
            string st = Page.Request.Params[UniqueID];
            if (st != null) { Text = st; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (Page.IsPostBack)
            {
                string xml;
                if (Page.IsCallback)
                {
                    if (CallbackManager.CallbackID == null) CallbackManager.InitCallbackParams();
                    if (CallbackManager.CallbackID == UniqueID)
                    {
                        xml = CallbackManager.CallbackParameters.ControlReserved;
                        _Data = SerializableObjectBase.CreateFromXML<TextBoxData>(xml);
                        Text = _Data.Text;
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

        protected override void OnBeforeRender()
        {
            _Settings.CallbackManagerClientInstance = CallbackManager.ClientInstanceName;
            _Settings.UniqueID = this.UniqueID;
            _Settings.EventTriggerHierarchyID = this.EventTriggerHierarchyID;
            _Settings.CommandArg = this.CommandArg;

            _Settings.ServerEvents_KeyDown = (KeyDown == null) ? false : true;
            _Settings.ServerEvents_KeyUp = (KeyUp == null) ? false : true;
            _Settings.ServerEvents_GetFocus = (GetFocus == null) ? false : true;
            _Settings.ServerEvents_LostFocus = (LostFocus == null) ? false : true;

            if (!ReadOnly && Enabled)
            {
                if (!CallbackManager.Scripts.IsScriptRegistered("TextBoxJs")) { CallbackManager.Scripts.RegisterClientScriptInclude("TextBoxJs", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Js.TextBox.js")); }
                string st = "";
                st += "var " + ClientID + "Instance = new Class_TextBox('" + this.ClientID + "'," + base._CallbackOptions.ToJson_Js() + "," + _Settings.ToJson_Js() + ");\r\n";
                st += "" + ClientID + "Instance.Init();\r\n";
                CallbackManager.Scripts.RegisterStartupScript(ClientID + "InitJs", st);
            }
            base.OnBeforeRender();
        }

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            _TextBox.Attributes.Add("name", UniqueID); _TextBox.Attributes.Add("id", ClientID);
            _TextBox.ControlStyle.CopyFrom(this.ControlStyle);
            if (ReadOnly)
            {
                _TextBox.Attributes.Add("readonly", "readonly");
                _TextBox.Style.Add("background-color", System.Drawing.ColorTranslator.ToHtml(ReadOnlyBackColor));
            }
            writer.Write(RenderControl(_TextBox));
        }

        public override void RenderBeginTag(System.Web.UI.HtmlTextWriter writer) { }
        public override void RenderEndTag(System.Web.UI.HtmlTextWriter writer) { }
    }
}
