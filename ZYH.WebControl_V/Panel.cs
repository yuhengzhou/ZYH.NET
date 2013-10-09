using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;

namespace ZYH.WebControl_V
{
    [DefaultEvent("LoadFromServer"), ParseChildren(false), PersistChildren(true), System.Drawing.ToolboxBitmap(typeof(Panel), "Image.ToolBox.Panel.bmp"), Description("This control does not support postback mode. Any postback operation will cause all Window controls close and information entered inside these Windows will be lost.")]
    public class Panel : CallbackControl_Base
    {
        //public Panel() : base(HtmlTextWriterTag.Div) { }

        PanelSetting _Settings = new PanelSetting();

        #region Event
        public delegate void LoadFromServerHandler(object sender, PanelEventArgs e);
        public event LoadFromServerHandler LoadFromServer;

        protected virtual void OnLoadFromServer(PanelEventArgs e)
        {
            if (LoadFromServer != null) LoadFromServer(this, e);
        }

        public override void OnRaiseCallbackEvent(CallbackParameters CallbackParameters, CallbackResult CallbackResult)
        {
            PanelEventArgs e = new PanelEventArgs();
            e.CallbackManager = this.CallbackManager;
            e.ArgFromClient = CallbackParameters.ArgFromClient;
            e.Command = CallbackParameters.Command;
            e.CommandArg = this.CommandArg;
            e.CallbackOptions = _CallbackOptions;
            switch (e.Command)
            {
                case CallbackCommands.Panel_LoadFromServer:
                    e.IsInitLoad = true;
                    e.RenderMe = true;
                    e.IsLoaded = true;
                    OnLoadFromServer(e);
                    break;
            }
            this.IsLoaded = e.IsLoaded;
            this.ArgFromClient_PreviousLoad = e.ArgFromClient;
            CallbackResult.ArgToClient = e.ArgToClient;
            CallbackResult.CallbackOptions = e.CallbackOptions;
            CallbackResult.ControlReserved = e.HtmlToBeLoaded;
        }
        #endregion

        #region Property
        private System.Web.UI.WebControls.ScrollBars _ScrollBars = System.Web.UI.WebControls.ScrollBars.None;
        [DefaultValue(typeof(System.Web.UI.WebControls.ScrollBars), "None"), Browsable(true)]
        public System.Web.UI.WebControls.ScrollBars ScrollBars
        {
            get { return _ScrollBars; }
            set { _ScrollBars = value; }
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

        private string _LoadCommand = "";
        [DefaultValue(""), Browsable(false)]
        public string LoadCommand
        {
            get { return _LoadCommand; }
            set { _LoadCommand = value; }
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

        [Category("Behavior"), DefaultValue(""), Description("Set to generate the client method to call server and fire server event 'LoadFromServer'. function [ClientMethod_LoadFromServer](ArgToServer){}")]
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

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.IsLoaded)
            {
                PanelEventArgs ee = new PanelEventArgs();
                ee.CallbackManager = this.CallbackManager;
                ee.ArgFromClient = this.ArgFromClient_PreviousLoad;
                ee.Command = CallbackCommands.Panel_LoadFromServer;
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
            if (Enabled)
            {
                if (!CallbackManager.Scripts.IsScriptRegistered("PanelJs")) { CallbackManager.Scripts.RegisterClientScriptInclude("PanelJs", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Js.Panel.js")); }
                string st = "";
                st += "var " + ClientID + "Instance = new Class_Panel('" + this.ClientID + "'," + base._CallbackOptions.ToJson_Js() + "," + _Settings.ToJson_Js() + ");\r\n";
                st += "" + ClientID + "Instance.Init();\r\n";
                CallbackManager.Scripts.RegisterStartupScript(ClientID + "InitJs", st);

                if (LoadFromServer != null)
                {
                    if (!CallbackManager.Scripts.IsScriptRegistered(ClientID + "_LoadFromServerJs"))
                    {
                        if (ClientMethod_LoadFromServer == "") throw new Exception("Please set 'ClientMethod_LoadFromServer' for control '" + ID + "'.");
                        st = "";
                        st += "function " + ClientMethod_LoadFromServer + "(ArgToServer) {\r\n";
                        st += ClientID + "Instance.OnLoadFromServer(ArgToServer);\r\n";
                        st += "}\r\n";
                        CallbackManager.Scripts.RegisterClientScriptBlock(ClientID + "_LoadFromServerJs", st);
                    }
                }
            }
            base.OnBeforeRender();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            this.AddAttributesToRender(writer);
            if (DesignMode)
            {
                if (this.Width.IsEmpty) writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "1px");
                if (this.BorderStyle == System.Web.UI.WebControls.BorderStyle.NotSet) writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "solid");
                if (this.BorderColor.IsEmpty) writer.AddStyleAttribute(HtmlTextWriterStyle.BorderColor, "Silver");
                if (this.Width.IsEmpty) writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
                if (this.Height.IsEmpty) writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "20px");
            }
            switch (ScrollBars)
            {
                case System.Web.UI.WebControls.ScrollBars.Horizontal:
                    writer.AddStyleAttribute(HtmlTextWriterStyle.OverflowX, "scroll");
                    break;
                case System.Web.UI.WebControls.ScrollBars.Vertical:
                    writer.AddStyleAttribute(HtmlTextWriterStyle.OverflowY, "scroll");
                    break;
                case System.Web.UI.WebControls.ScrollBars.Both:
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "scroll");
                    break;
                case System.Web.UI.WebControls.ScrollBars.Auto:
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
                    break;
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.RenderContents(writer);
            writer.RenderEndTag();
        }

        protected override void OnSaveControlStates(CallbackEventArgs e)
        {
            base.OnSaveControlStates(e);
            if (EnableCallbackControlStatus)
            {
                CallbackManager.ControlStates[this.ClientID, "IL"] = this.IsLoaded.ToString();
                if (this.ArgFromClient_PreviousLoad != "") CallbackManager.ControlStates[this.ClientID, "AFC_PL"] = this.ArgFromClient_PreviousLoad;
                if (this.LoadCommand != "") CallbackManager.ControlStates[this.ClientID, "LC"] = this.LoadCommand;
            }
        }

        protected override void OnRestoreControlState()
        {
            base.OnRestoreControlState();
            if (EnableCallbackControlStatus)
            {
                if (CallbackManager.ControlStates[this.ClientID] != null)
                {
                     this.IsLoaded = Convert.ToBoolean(CallbackManager.ControlStates[this.ClientID]["IL"]);
                    var o = CallbackManager.ControlStates[this.ClientID]["AFC_PL"];
                    if (o != null) this.ArgFromClient_PreviousLoad = o;
                    o = CallbackManager.ControlStates[this.ClientID]["LC"];
                    if (o != null) this.LoadCommand = o;
                }
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer) { }
        public override void RenderEndTag(System.Web.UI.HtmlTextWriter writer) { }
    }
}
