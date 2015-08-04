using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;

namespace ZYH.WebControl_V
{
    [DefaultEvent("LoadFromServer"), ParseChildren(false), PersistChildren(true), System.Drawing.ToolboxBitmap(typeof(Window), "Image.ToolBox.Window.png"), Description("This control does not support postback mode. Any postback operation will cause all Window controls close and information entered inside these Windows will be lost.")]
    public class Window : CallbackControl_Base
    {
        WindowData _Data = new WindowData();
        WindowSetting _Settings = new WindowSetting();

        #region Event
        public delegate void LoadFromServerHandler(object sender, WindowEventArgs e);
        public event LoadFromServerHandler LoadFromServer;
        public delegate void CloseHandler(object sender, WindowEventArgs e);
        public event CloseHandler Close;

        protected virtual void OnLoadFromServer(WindowEventArgs e)
        {
            if (LoadFromServer != null) LoadFromServer(this, e);
        }

        protected virtual void OnClose(WindowEventArgs e)
        {
            if (Close != null) Close(this, e);
        }

        public override void OnRaiseCallbackEvent(CallbackParameters CallbackParameters, CallbackResult CallbackResult)
        {
            WindowEventArgs e = new WindowEventArgs();
            e.CallbackManager = this.CallbackManager;
            e.ArgFromClient = CallbackParameters.ArgFromClient;
            e.Command = CallbackParameters.Command;
            e.CommandArg = this.CommandArg;
            e.CallbackOptions = _CallbackOptions;
            e.Title = this.Title;
            switch (e.Command)
            {
                case CallbackCommands.Window_LoadFromServer:
                    e.IsInitLoad = true;
                    e.RenderMe = true;
                    e.IsLoaded = true;
                    OnLoadFromServer(e);
                    break;
                case CallbackCommands.Window_Close:
                    e.IsInitLoad = false;
                    e.RenderMe = false;
                    e.IsLoaded = false;
                    OnClose(e);
                    break;
            }
            this.IsLoaded = e.IsLoaded;
            this.ArgFromClient_PreviousLoad = e.ArgFromClient;
            CallbackResult.ArgToClient = e.ArgToClient;
            CallbackResult.CallbackOptions = e.CallbackOptions;
            this.Title = e.Title;
            _Data.Title = e.Title;
            _Data.HtmlToBeLoad = e.HtmlToBeLoaded;
            CallbackResult.ControlReserved = _Data.ToXML();
        }
        #endregion

        #region Property
        [DefaultValue(false)]
        public bool HasMinimalButton
        {
            get { return _Settings.HasMinimalButton; }
            set { _Settings.HasMinimalButton = value; }
        }

        [DefaultValue(true)]
        public bool HasCloseButton
        {
            get { return _Settings.HasCloseButton; }
            set { _Settings.HasCloseButton = value; }
        }

        [DefaultValue(true)]
        public bool DragLimit0x0
        {
            get { return _Settings.DragLimit0x0; }
            set { _Settings.DragLimit0x0 = value; }
        }

        private string _CloseButtonUrl = "";
        [DefaultValue(""), Description(""), Browsable(true), Category("Appearance"), System.ComponentModel.Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), System.ComponentModel.RefreshProperties(RefreshProperties.All)]
        public string CloseButtonUrl
        {
            get { return _CloseButtonUrl; }
            set { _CloseButtonUrl = value; }
        }

        private string _CloseButtonUrl_MouseOver = "";
        [DefaultValue(""), Description(""), Browsable(true), Category("Appearance"), System.ComponentModel.Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), System.ComponentModel.RefreshProperties(RefreshProperties.All)]
        public string CloseButtonUrl_MouseOver
        {
            get { return _CloseButtonUrl_MouseOver; }
            set { _CloseButtonUrl_MouseOver = value; }
        }

        private string _MinimalButtonUrl = "";
        [DefaultValue(""), Description(""), Browsable(true), Category("Appearance"), System.ComponentModel.Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), System.ComponentModel.RefreshProperties(RefreshProperties.All)]
        public string MinimalButtonUrl
        {
            get { return _MinimalButtonUrl; }
            set { _MinimalButtonUrl = value; }
        }

        private string _MinimalButtonUrl_MouseOver = "";
        [DefaultValue(""), Description(""), Browsable(true), Category("Appearance"), System.ComponentModel.Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), System.ComponentModel.RefreshProperties(RefreshProperties.All)]
        public string MinimalButtonUrl_MouseOver
        {
            get { return _MinimalButtonUrl_MouseOver; }
            set { _MinimalButtonUrl_MouseOver = value; }
        }

        [DefaultValue(true)]
        public bool DragAble
        {
            get { return _Settings.DragAble; }
            set { _Settings.DragAble = value; }
        }

        [DefaultValue(false), Description("This will Use CallbackManager's conver to conver screen.")]
        public bool IsModelWindow
        {
            get { return _Settings.IsModelWindow; }
            set { _Settings.IsModelWindow = value; }
        }

        private bool _IsLoaded = false;
        [DefaultValue(false), Browsable(false)]
        public bool IsLoaded
        {
            get { return _IsLoaded; }
            set { _IsLoaded = value; }
        }

        private string _ArgFromClient_PreviousLoad = "";
        [DefaultValue(""), Browsable(false)]
        public string ArgFromClient_PreviousLoad
        {
            get { return _ArgFromClient_PreviousLoad; }
            set { _ArgFromClient_PreviousLoad = value; }
        }

        private bool _EnableCallbackControlStatus = true;
        [Category("Behavior"), DefaultValue(true), Browsable(true), Description("")]
        public new bool EnableCallbackControlStatus
        {
            get { return _EnableCallbackControlStatus; }
            set { _EnableCallbackControlStatus = value; }
        }

        [Category("Behavior"), DefaultValue("not applicable"), Description("not applicable.")]
        public new string ClientEvent_BeforeCallback { get { return "not applicable"; } }

        [Category("Behavior"), DefaultValue("not applicable"), Description("not applicable.")]
        public new string ClientEvent_AfterCallback { get { return "not applicable"; } }

        [Category("Behavior"), DefaultValue(""), Description("Set to generate the client method to call server and fire server event 'LoadFromServer'. function [ClientMethod_LoadFromServer](ArgToServer, Top, Left){... }")]
        public string ClientMethod_LoadFromServer
        {
            get { return _Settings.ClientMethod_LoadFromServer; }
            set { _Settings.ClientMethod_LoadFromServer = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function [BeforeCallback_LoadFromServer](SenderID, e_BeforeCall) {... }")]
        public string ClientEvent_BeforeCallback_LoadFromServer
        {
            get { return _Settings.ClientEvent_BeforeCallback_LoadFromServer; }
            set { _Settings.ClientEvent_BeforeCallback_LoadFromServer = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function [AfterCallback_LoadFromServer](SenderID, e_AfterCall) {... }")]
        public string ClientEvent_AfterCallback_LoadFromServer
        {
            get { return _Settings.ClientEvent_AfterCallback_LoadFromServer; }
            set { _Settings.ClientEvent_AfterCallback_LoadFromServer = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("will be throwed on client side before call server envent 'Close'. function [BeforeCallback_Close](SenderID, e_BeforeCall) {} var e_BeforeCall = new Object(); e_BeforeCall.CancelCall = false/true; e_BeforeCall.ArgFromClient = ''; e_BeforeCall.CommandArg = CommandArg; e_BeforeCall.ControlReserved = [ControlReserved]; e_BeforeCall.EventTriggerHierarchyID = EventTriggerHierarchyID;")]
        public string ClientEvent_BeforeCallback_Close
        {
            get { return _Settings.ClientEvent_BeforeCallback_Close; }
            set { _Settings.ClientEvent_BeforeCallback_Close = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function [AfterCallback_Close](SenderID, e_AfterCall) {... }")]
        public string ClientEvent_AfterCallback_Close
        {
            get { return _Settings.ClientEvent_AfterCallback_Close; }
            set { _Settings.ClientEvent_AfterCallback_Close = value; }
        }

        [DefaultValue(""), Description(""), Browsable(true), Category("Appearance")]
        public string CssClass_Header
        {
            get { return _Settings.CssClass_Header; }
            set { _Settings.CssClass_Header = value; }
        }
        [DefaultValue(""), Description(""), Browsable(true), Category("Appearance")]
        public string CssClass_CloseBotton
        {
            get { return _Settings.CssClass_CloseBotton; }
            set { _Settings.CssClass_CloseBotton = value; }
        }
        [DefaultValue(""), Description(""), Browsable(true), Category("Appearance")]
        public string CssClass_MinimalBotton
        {
            get { return _Settings.CssClass_MinimalBotton; }
            set { _Settings.CssClass_MinimalBotton = value; }
        }
        [DefaultValue(""), Description(""), Browsable(true), Category("Appearance")]
        public new string CssClass
        {
            get { return _Settings.CssClass_Frame; }
            set { _Settings.CssClass_Frame = value; }
        }

        [DefaultValue(""), Description(""), Browsable(true), Category("Appearance")]
        public new string Title
        {
            get { return _Settings.Title; }
            set { _Settings.Title = value; }
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (DesignMode) this.BorderWidth = 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.IsLoaded)
            {
                WindowEventArgs ee = new WindowEventArgs();
                ee.CallbackManager = this.CallbackManager;
                ee.ArgFromClient = this.ArgFromClient_PreviousLoad;
                ee.Command = CallbackCommands.Window_LoadFromServer;
                ee.CommandArg = this.CommandArg;
                ee.CallbackOptions = this._CallbackOptions;
                ee.IsInitLoad = false;
                ee.RenderMe = false;
                OnLoadFromServer(ee);
            }
        }

        protected override void OnBeforeRender()
        {
            _Settings.CallbackManagerClientInstance = CallbackManager.ClientInstanceName;
            _Settings.UniqueID = this.UniqueID;
            _Settings.EventTriggerHierarchyID = this.EventTriggerHierarchyID;
            _Settings.CommandArg = this.CommandArg;

            _Settings.ServerEvents_LoadFromServer = (LoadFromServer == null) ? false : true;
            _Settings.ServerEvents_Close = (Close == null) ? false : true;
            _Settings.Width = this.Width.IsEmpty ? -1 : Convert.ToInt32(this.Width.Value);
            _Settings.Height = this.Height.IsEmpty ? -1 : Convert.ToInt32(this.Height.Value);
            _Settings.CloseButtonUrl = _CloseButtonUrl == "" ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Image.CloseWhite.png") : _CloseButtonUrl;
            _Settings.CloseButtonUrl_MouseOver = _CloseButtonUrl_MouseOver == "" ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Image.CloseWhite_Over.png") : _CloseButtonUrl_MouseOver;
            _Settings.MinimalButtonUrl = _MinimalButtonUrl == "" ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Image.Mini.png") : _MinimalButtonUrl;
            _Settings.MinimalButtonUrl_MouseOver = _MinimalButtonUrl_MouseOver == "" ? Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Image.MiniOver.png") : _MinimalButtonUrl_MouseOver;
            if (Enabled)
            {
                if (!CallbackManager.Scripts.IsScriptRegistered("WindowJs")) { CallbackManager.Scripts.RegisterClientScriptInclude("WindowJs", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Js.Window.js")); }
                string st = "";
                st += "var " + ClientID + "Instance = new Class_Window('" + this.ClientID + "'," + base._CallbackOptions.ToJson_Js() + "," + _Settings.ToJson_Js() + ");\r\n";
                st += "" + ClientID + "Instance.Init();\r\n";
                CallbackManager.Scripts.RegisterStartupScript(ClientID + "InitJs", st);

                if (LoadFromServer != null)
                {
                    if (!CallbackManager.Scripts.IsScriptRegistered(ClientID + "_LoadFromServerJs"))
                    {
                        if (ClientMethod_LoadFromServer == "") throw new Exception("Please set 'ClientMethod_LoadFromServer' for control '" + ID + "'.");
                        st = "";
                        st += "function " + ClientMethod_LoadFromServer + "(ArgToServer,Top,Left) {\r\n";
                        st += ClientID + "Instance.OnLoadFromServer(ArgToServer,Top,Left);\r\n";
                        st += "}\r\n";
                        CallbackManager.Scripts.RegisterClientScriptBlock(ClientID + "_LoadFromServerJs", st);
                    }
                }
            }
            base.OnBeforeRender();
        }

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            if (DesignMode)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, (this.Width.IsEmpty ? 300 : this.Width.Value).ToString() + "px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, (this.Height.IsEmpty ? 200 : this.Height.Value).ToString() + "px");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write("[" + ID + "]<br/> Yu Heng Zhou <br/>Apr 2012, Canada");
                writer.RenderEndTag();
            }
            else
            {
                writer.Write("<div id='" + this.ClientID + "' style='display:none;'>");
                writer.Write("</div>");
                if (this.HasControls())
                {
                    writer.Write("<div id='" + this.ClientID + "InnerHtml' style='display:none;'>");
                    base.RenderContents(writer);
                    writer.Write("</div>");
                }
            }
        }

        protected override void OnSaveControlStates(CallbackEventArgs e)
        {
            base.OnSaveControlStates(e);
            if (EnableCallbackControlStatus)
            {
                CallbackManager.ControlStates[this.ClientID, "IL"] = this.IsLoaded.ToString();
                if (this.ArgFromClient_PreviousLoad != "") CallbackManager.ControlStates[this.ClientID, "AFC_PL"] = this.ArgFromClient_PreviousLoad;
            }
        }

        protected override void OnRestoreControlState()
        {
            base.OnRestoreControlState();
            if (EnableCallbackControlStatus)
            {
                if (CallbackManager.ControlStates[this.ClientID] != null)
                {
                    if (Page.IsCallback) this.IsLoaded = Convert.ToBoolean(CallbackManager.ControlStates[this.ClientID]["IL"]);
                    var o = CallbackManager.ControlStates[this.ClientID]["AFC_PL"];
                    if (o != null) this.ArgFromClient_PreviousLoad = o;
                }
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer) { if (DesignMode) base.RenderBeginTag(writer); }
        public override void RenderEndTag(System.Web.UI.HtmlTextWriter writer) { if (DesignMode) base.RenderEndTag(writer); }

    }
}
