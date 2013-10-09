using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;

namespace ZYH.WebControl_V
{
    public abstract class CallbackControl_Base : Base, ICallbackEventHandler
    {
        CallbackResult _CallbackResult;
        protected string _ArgFromClient = "";
        protected string _ArgToClient = "";
        protected CallbackOptions _CallbackOptions = new CallbackOptions();
        Exception _Ex = null;

        #region New
        public CallbackControl_Base() : base() { }
        public CallbackControl_Base(HtmlTextWriterTag tag) : base(tag) { }
        public CallbackControl_Base(string tag) : base(tag) { }
        #endregion

        #region Event
        public delegate void RestoreStateHandler(object sender, CallbackEventArgs e);
        public event RestoreStateHandler RestoreState;
        public delegate void SaveStateHandler(object sender, CallbackEventArgs e);
        public event SaveStateHandler SaveState;
        public delegate void RenderHandler(object sender, CallbackEventArgs e);
        public event RenderHandler BeforeRender;

        protected virtual void OnRestoreControlState()
        {
            if (EnableCallbackControlStatus)
            {
                this.Visible = Convert.ToBoolean(CallbackManager.ControlStates[this.ClientID]["Visible"]);
                this.Enabled = Convert.ToBoolean(CallbackManager.ControlStates[this.ClientID]["Enabled"]);
            }
            CallbackEventArgs ee = new CallbackEventArgs();
            ee.CallbackManager = _CallbackManager;
            if (RestoreState != null) RestoreState(this, ee);
        }

        protected virtual void OnSaveControlStates(CallbackEventArgs e)
        {
            if (EnableCallbackControlStatus)
            {
                CallbackManager.ControlStates[this.ClientID, "Visible"] = this.Visible.ToString();
                CallbackManager.ControlStates[this.ClientID, "Enabled"] = this.Enabled.ToString();
            }
            CallbackEventArgs ee = new CallbackEventArgs();
            ee.CallbackManager = _CallbackManager;
            if (SaveState != null) SaveState(this, ee);
        }

        protected virtual void OnBeforeRender()
        {
            CallbackEventArgs ee = new CallbackEventArgs();
            ee.CallbackManager = _CallbackManager;
            if (BeforeRender != null) BeforeRender(this, ee);
        }

        public string GetCallbackResult()
        {
            _CallbackManager.OnRegisterJavascript();
            string rt = "";
            if (_Ex == null)
            {
                try
                {
                    CallbackManager.OnSaveControlStates();
                    if (_CallbackResult.CallbackOptions.UpdateControlState) { _CallbackResult.ControlStates = CallbackManager.ControlStates; }
                    if (_CallbackResult.CallbackOptions.UpdateViewState) { _CallbackResult.ViewStates = CallbackManager.ViewStates; }
                    _CallbackManager.Scripts.FinalizeScriptsToClient(Page);
                    _CallbackResult.Scripts = CallbackManager.Scripts;

                    string CallbackOptions_Json = SerializableObjectBase.ToJson_Js(_CallbackResult.CallbackOptions);
                    rt += CallbackOptions_Json.Length + "|CallbackOptions|" + CallbackOptions_Json;
                    if (_CallbackResult.ControlStates != null)
                    {
                        string ControlStates_Xml = SerializableObjectBase.ToXML(_CallbackResult.ControlStates);
                        rt += "|" + ControlStates_Xml.Length + "|ControlStates|" + ControlStates_Xml;
                    }
                    if (_CallbackResult.ViewStates != null)
                    {
                        string ViewStates_Xml = SerializableObjectBase.ToXML(_CallbackResult.ViewStates);
                        rt += "|" + ViewStates_Xml.Length + "|ViewStates|" + ViewStates_Xml;
                    }
                    if (_CallbackResult.Scripts != null)
                    {
                        string Scripts_Xml = SerializableObjectBase.ToXML(_CallbackResult.Scripts);
                        rt += "|" + Scripts_Xml.Length + "|Scripts|" + Scripts_Xml;
                    }
                    if (_CallbackResult.ControlReserved != null)
                    {
                        rt += "|" + _CallbackResult.ControlReserved.Length + "|ControlReserved|" + _CallbackResult.ControlReserved;
                    }
                    rt += "|" + _CallbackResult.ArgToClient.Length + "|ArgToClient|" + _CallbackResult.ArgToClient;
                    if (CallbackManager.AddedHistoryPoint != null)
                    {
                        _CallbackResult.AddedHistoryPoint = CallbackManager.AddedHistoryPoint;
                        var st = _CallbackResult.AddedHistoryPoint.ToString();
                        rt += "|" + st.Length + "|HistoryPoint|" + st;
                    }
                    return rt;
                }
                catch (Exception ex)
                {
                    _Ex = ex;
                }
            }
            return rt;
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            try
            {
                _CallbackResult = new CallbackResult();
                OnRaiseCallbackEvent(this.CallbackManager.CallbackParameters, this._CallbackResult);
            }
            catch (Exception ex)
            {
                _Ex = ex;
            }
        }

        public abstract void OnRaiseCallbackEvent(CallbackParameters CallbackParameters, CallbackResult CallbackResult);
        #endregion

        #region Property
        private CallbackManager _CallbackManager;
        [Browsable(false)]
        public CallbackManager CallbackManager
        {
            get { return _CallbackManager; }
            set { _CallbackManager = value; }
        }

        private string _CommandArg = "";
        [Category("Behavior"), DefaultValue(""), Browsable(true), Description(""), Bindable(true)]
        public string CommandArg
        {
            get { return _CommandArg; }
            set { _CommandArg = value; }
        }

        private string _EventTriggerHierarchyID = "";
        [Category("Behavior"), DefaultValue(""), Browsable(true), Description("Performance critical. In order to throw event, the event trigger control must be loaded when Page class execute RaiseCallbackEvent method. This property will be used for partially initiate controls hierarchy during callback.")]
        public string EventTriggerHierarchyID
        {
            get { return _EventTriggerHierarchyID; }
            set { _EventTriggerHierarchyID = value; }
        }

        private bool _EnableCallbackControlStatus = false;
        [Category("Behavior"), DefaultValue(false), Browsable(true), Description("")]
        public bool EnableCallbackControlStatus
        {
            get { return _EnableCallbackControlStatus; }
            set { _EnableCallbackControlStatus = value; }
        }

        private string _ClientEvent_BeforeCallback = "";
        [Category("Behavior"), DefaultValue(""), Browsable(true), Description("function [BeforeCall](SenderID, e_BeforeCall) {} var e_BeforeCall = new Object(); e_BeforeCall.CancelCall = false/true; e_BeforeCall.ArgFromClient = ''; e_BeforeCall.CommandArg = CommandArg; e_BeforeCall.ControlReserved = [ControlReserved]; e_BeforeCall.EventTriggerHierarchyID = EventTriggerHierarchyID;. More items could be added.")]
        public string ClientEvent_BeforeCallback
        {
            get { return _ClientEvent_BeforeCallback; }
            set { _ClientEvent_BeforeCallback = value; }
        }

        private string _ClientEvent_AfterCallback = "";
        [Category("Behavior"), DefaultValue(""), Browsable(true), Description("function [AfterCall](SenderID, e_AfterCall) {} //var e_AfterCall = new Object(); e_AfterCall.ControlReserved = ControlReserved; e_AfterCall.ArgToClient = ArgToClient; e_AfterCall.AfterCallAction = AfterCallAction; e_AfterCall.ClientInstance = Self; e_AfterCall.Context = Context; More properties could be added by Function_AfterCallProcessing.")]

        public string ClientEvent_AfterCallback
        {
            get { return _ClientEvent_AfterCallback; }
            set { _ClientEvent_AfterCallback = value; }
        }

        [DefaultValue("NA"), Description("Get or set container element(s) ClientId (Use comma to separate if multiple). For these container element(s), all inner 'input' tag data will be included into Post Body, for ASP.NET auto-restores those controls values on server side. Default value is 'NA', no data will be included into Post Body. Set to 'String.Empty', 'All', or 'Form', it will include whole Form data into Post Body."), Browsable(true), Category("Behavior")]
        public string PostDataContainer { get { return _CallbackOptions.PostDataContainer; } set { _CallbackOptions.PostDataContainer = value; } }

        [DefaultValue(typeof(AsynchronousCallOptions), "AbortPreviousCalls"), Description("Options to handle Asynchronous Calls conflict."), Browsable(true), Category("Behavior")]
        public AsynchronousCallOptions AsyncCallOption { get { return _CallbackOptions.AsyncCallOption; } set { _CallbackOptions.AsyncCallOption = value; } }

        [DefaultValue(true), Description("Whether this request can be aborted by other later request or not. Options to handle Asynchronous Calls conflict."), Browsable(true), Category("Behavior")]
        public bool CanBeAborted { get { return _CallbackOptions.CanBeAborted; } set { _CallbackOptions.CanBeAborted = value; } }

        [DefaultValue(typeof(AfterCallActions), "NA"), Description(""), Browsable(true), Category("Behavior")]
        public AfterCallActions AfterCallAction { get { return _CallbackOptions.AfterCallAction; } set { _CallbackOptions.AfterCallAction = value; } }

        [DefaultValue(true), Description(""), Browsable(true), Category("Behavior")]
        public bool UpdateViewState { get { return _CallbackOptions.UpdateViewState; } set { _CallbackOptions.UpdateViewState = value; } }

        [DefaultValue(true), Browsable(true), Category("Behavior"), Description("Whether update callback ControlState during the call. if \"false\", callback information will not contain ControlState and any ControlState change on server side will not be stored during this call.")]
        public bool UpdateControlState { get { return _CallbackOptions.UpdateControlState; } set { _CallbackOptions.UpdateControlState = value; } }

        #endregion

        protected CallbackManager FindCallbackManager()
        {
            foreach (Control c in Page.Controls)
            {
                if (c.GetType() == typeof(CallbackManager)) return (CallbackManager)c;
                Control rc = FindCallbackManager(c);
                if (rc != null) return (CallbackManager)rc;
            }
            return null;
        }

        protected CallbackManager FindCallbackManager(Control c)
        {
            foreach (Control cc in c.Controls)
            {
                if (cc.GetType() == typeof(CallbackManager)) return (CallbackManager)cc;
                Control rc = FindCallbackManager(cc);
                if (rc != null) return (CallbackManager)rc;
            }
            return null;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!DesignMode)
            {
                if (_CallbackManager == null) _CallbackManager = FindCallbackManager();
                if (_CallbackManager == null) throw new Exception("Please add a CallbackManager control on the page.");
                if (Page.IsPostBack) OnRestoreControlState();
                CallbackManager.SaveControlStates += new CallbackManager.SaveControlStatesHandler(OnSaveControlStates);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            OnBeforeRender();
            //Page.ClientScript.GetCallbackEventReference(this, "", "null", ClientID, true);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (Page.IsCallback) OnBeforeRender();
            base.Render(writer);
        }

        protected override void OnUnload(EventArgs e)
        {
            if (_Ex != null) { throw new Exception("Unhadled excetion occrred in " + ID + " server side event handler. See InnerException for detail", _Ex); }
            base.OnUnload(e);
        }
    }
}
