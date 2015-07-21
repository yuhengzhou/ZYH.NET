using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Web.Services;

namespace ZYH.WebControl_V
{
    [Designer(typeof(DesignMode.CallbackManager)), System.Drawing.ToolboxBitmap(typeof(CallbackManager), "Image.ToolBox.CallbackManager.bmp"), DefaultEvent("Navigate")]
    public class CallbackManager : Base
    {
        internal HistoryPoint AddedHistoryPoint = null;

        //private HiddenField _hd_Settings = new HiddenField();
        private HiddenField _hd_ControlStates = new HiddenField();
        private HiddenField _hd_ViewStates = new HiddenField();
        private HiddenField _hd_ScriptKeys = new HiddenField();

        private CallbackManagerSetting _Settings;
        private bool _EnableHitoryBeforeLoad = false;
        public CallbackManager()
            : base(HtmlTextWriterTag.Span)
        {
            _PreRegisteredScriptResources = new ScriptResourceCollection();
            _Settings = new CallbackManagerSetting();
        }

        #region Event
        public delegate void RegisterJavascriptHandler(CallbackEventArgs e);
        public event RegisterJavascriptHandler RegisterJavascript;
        public delegate void SaveControlStatesHandler(CallbackEventArgs e);
        public event SaveControlStatesHandler SaveControlStates;
        public delegate void NavigateHandler(NavigateEventArgs e);
        public event NavigateHandler Navigate;

        public virtual void OnRegisterJavascript()
        {
            CallbackEventArgs e = new CallbackEventArgs();
            e.CallbackManager = this;
            if (RegisterJavascript != null) RegisterJavascript(e);
        }

        public virtual void OnSaveControlStates()
        {
            ControlStates.Clear();
            CallbackEventArgs e = new CallbackEventArgs();
            e.CallbackManager = this;
            if (SaveControlStates != null) SaveControlStates(e);
        }

        public virtual void OnNavigate(NavigateEventArgs e)
        {
            if (Navigate != null) Navigate(e);
        }

        #endregion

        #region Property
        [DefaultValue("CallbackManagerInstance"), Description(""), Browsable(true), Category("Behavior")]
        public string ClientInstanceName
        {
            get { return _Settings.ClientInstanceName; }
            set { _Settings.ClientInstanceName = value; }
        }

        private string _CallbackID;
        [Browsable(false)]
        public string CallbackID
        {
            get { return _CallbackID; }
        }

        private CallbackParameters _CallbackParameters;
        [Browsable(false)]
        public CallbackParameters CallbackParameters
        {
            get { return _CallbackParameters; }
        }

        private string _CallerEventTriggerHierarchyID;
        [Category("Behavior"), Description("Performance critical. In order to throw event, the event trigger control must be loaded when Page class executes RaiseCallbackEvent method. In AJAX dynamic loading mode, this property will be used for partially initiate necessray controls hierarchy during callback to significantly gain performance."), PersistenceMode(PersistenceMode.Attribute), DefaultValue(""), Browsable(false)]
        public string CallerEventTriggerHierarchyID
        {
            get { return _CallerEventTriggerHierarchyID; }
        }

        private bool _EnablePageViewState = false;
        [DefaultValue(false), Description("Equivalent EnableViewState=\"true/false\""), Browsable(true), Category("Behavior")]
        public bool EnablePageViewState
        {
            get { return _EnablePageViewState; }
            set { _EnablePageViewState = value; }
        }

        private bool _EnablePageEventValidation = true;
        [DefaultValue(true), Description("Equivalent EnableEventValidation=\"true/false\""), Browsable(true), Category("Behavior")]
        public bool EnablePageEventValidation
        {
            get { return _EnablePageEventValidation; }
            set { _EnablePageEventValidation = value; }
        }

        private bool _EnablePageMethods = false;
        [DefaultValue(false), Description("Enable to directly call server side static page behind functions from javascript. This is non OOP call. It will not init page class and cannot render HTML using server side code. Sample: C#FunctionName([parameter1, parameter2, ...], Function_BeforeCall, Function_AfterCall); Function parameters and return can be seralizable object. The return value in Function_AfterCall's parameter e.Return"), Browsable(true), Category("Behavior")]
        public bool EnablePageMethods
        {
            get { return _EnablePageMethods; }
            set { _EnablePageMethods = value; }
        }

        [DefaultValue(false), Description(""), Browsable(true), Category("Behavior")]
        public bool EnableHistory
        {
            get { return _Settings.EnableHistory; }
            set { _Settings.EnableHistory = value; }
        }

        [DefaultValue(15), Description("Seconds"), Browsable(true), Category("Behavior")]
        public int CallWaitingTimeOut
        {
            get { return _Settings.CallWaitingTimeOut; }
            set { _Settings.CallWaitingTimeOut = value; }
        }

        [DefaultValue(typeof(CallWaitingBehaviors), "MovingProcessingIcon"), Description(""), Browsable(true), Category("Appearance")]
        public CallWaitingBehaviors CallWaitingBehavior
        {
            get { return _Settings.CallWaitingBehavior; }
            set { _Settings.CallWaitingBehavior = value; }
        }

        [DefaultValue(typeof(Color), "Black"), Description(""), Browsable(true), Category("Appearance"), TypeConverter(typeof(WebColorConverter))]
        public Color CallWaitingCoverColor
        {
            get { return System.Drawing.ColorTranslator.FromHtml(_Settings.CallWaitingCoverColor); }
            set { _Settings.CallWaitingCoverColor = System.Drawing.ColorTranslator.ToHtml(value); }
        }

        [DefaultValue(typeof(decimal), "0.3"), Description(""), Browsable(true), Category("Appearance")]
        public decimal CallWaitingCoverTransparency
        {
            get { return _Settings.CallWaitingCoverTransparency; }
            set { _Settings.CallWaitingCoverTransparency = value; }
        }

        [DefaultValue(""), Description(""), Browsable(true), Category("Appearance"), System.ComponentModel.Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), System.ComponentModel.RefreshProperties(RefreshProperties.All)]
        public string CallWaitingIcon
        {
            get { return _Settings.CallWaitingIcon; }
            set { _Settings.CallWaitingIcon = value; }
        }

        [Category("Behavior"), DefaultValue(typeof(OnExceptionActions), "DebugDetail"), Browsable(true), Description("")]
        public OnExceptionActions OnExceptionActions
        {
            get { return _Settings.OnExceptionActions; }
            set { _Settings.OnExceptionActions = value; }
        }

        [Category("Behavior"), DefaultValue(""), Browsable(true), Description("function ExceptionHandler(SenderClientID, Status, ErrorThrown, Context) {}")]
        public string ClientEvent_OnException
        {
            get { return _Settings.ClientEvent_OnException; }
            set { _Settings.ClientEvent_OnException = value; }
        }

        [Category("Behavior"), DefaultValue(""), Browsable(true), Description("function CallTimeOutHandler(SenderID, Context) {}")]
        public string ClientEvent_OnCallTimeOut
        {
            get { return _Settings.ClientEvent_OnCallTimeOut; }
            set { _Settings.ClientEvent_OnCallTimeOut = value; }
        }

        [Category("Behavior"), DefaultValue(""), Browsable(true), Description("")]
        public string ClientEvent_BeforeCall_Navigate
        {
            get { return _Settings.ClientEvent_BeforeCall_Navigate; }
            set { _Settings.ClientEvent_BeforeCall_Navigate = value; }
        }
        [Category("Behavior"), DefaultValue(""), Browsable(true), Description("")]
        public string ClientEvent_AfterCall_Navigate
        {
            get { return _Settings.ClientEvent_AfterCall_Navigate; }
            set { _Settings.ClientEvent_AfterCall_Navigate = value; }
        }

        private ViewStateCollection _ViewStates = new ViewStateCollection();
        [Browsable(false)]
        public ViewStateCollection ViewStates { get { if (_CallbackParameters != null && !_CallbackParameters.CallbackOptions.UpdateViewState) { throw new Exception("Please set \"" + _CallbackParameters.CallbackID + ".UpdateViewState = true\" to enable ViewStates."); } return _ViewStates; } }

        private ControlStateCollection _ControlStates = new ControlStateCollection();
        [Browsable(false), XmlElement("ControlStates")]
        public ControlStateCollection ControlStates { get { return _ControlStates; } }

        private ScriptCollection _Scripts = new ScriptCollection();
        [Browsable(false)]
        public ScriptCollection Scripts { get { return _Scripts; } }

        private ScriptResourceCollection _PreRegisteredScriptResources;
        [Browsable(true), Category("Behavior"), PersistenceMode(PersistenceMode.InnerProperty), MergableProperty(false), TemplateContainer(typeof(ScriptResource)), TemplateInstance(TemplateInstance.Multiple), Description("Script Libraries"), DefaultValue((string)null)]
        public ScriptResourceCollection PreRegisteredScriptResources { get { return _PreRegisteredScriptResources; } }

        [Browsable(false)]
        private RefrenceCollection _Refrence = new RefrenceCollection();
        public RefrenceCollection Refrence
        {
            get { return _Refrence; }
            set { _Refrence = value; }
        }
        #endregion

        protected void InitScriptResources()
        {
            _PreRegisteredScriptResources.Add(new ScriptResource("JQuery.min", "latest", "http://code.jquery.com/jquery-latest.min.js", ScriptResourceLoadFrom.BuildIn));
            _PreRegisteredScriptResources.Add(new ScriptResource("JQuery", "1.8.3", "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.js", ScriptResourceLoadFrom.Disabled));
            _PreRegisteredScriptResources.Add(new ScriptResource("JQuery.min", "1.8.3", "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js", ScriptResourceLoadFrom.Disabled));
            _PreRegisteredScriptResources.Add(new ScriptResource("JQuery-vsdoc", "1.8.3", "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3-vsdoc.js", ScriptResourceLoadFrom.Disabled));
            _PreRegisteredScriptResources.Add(new ScriptResource("JQuery.Ui", "1.9.2", "http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.js", ScriptResourceLoadFrom.Disabled));
            _PreRegisteredScriptResources.Add(new ScriptResource("JQuery.Ui.min", "1.9.2", "http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js", ScriptResourceLoadFrom.Disabled));
        }

        internal void InitCallbackParams()
        {
            string _CallbackOptions = Page.Request.Params["_CallbackOptions"];
            if (!string.IsNullOrEmpty(_CallbackOptions))
            {
                _CallbackParameters = new WebControl_V.CallbackParameters();
                _CallbackParameters.CallbackID = Page.Request.Params["__CALLBACKID"];
                this._CallbackID = _CallbackParameters.CallbackID;
                _CallbackParameters.CallbackOptions = SerializableObjectBase.CreateFromJson_Js<CallbackOptions>(_CallbackOptions);
                if (_CallbackParameters.CallbackOptions.UpdateViewState)
                {
                    _CallbackParameters.ViewStates = SerializableObjectBase.CreateFromXML<ViewStateCollection>(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(Page.Request.Params["_ViewStates"])));
                    this._ViewStates = _CallbackParameters.ViewStates;
                }
                if (_CallbackParameters.CallbackOptions.UpdateControlState)
                {
                    _CallbackParameters.ControlStates = SerializableObjectBase.CreateFromXML<ControlStateCollection>(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(Page.Request.Params["_ControlStates"])));
                    this._ControlStates = _CallbackParameters.ControlStates;
                }
                _CallbackParameters.ControlReserved = Page.Request.Params["__CALLBACKPARAM"];
                _CallbackParameters.RegisteredScriptKeys = SerializableObjectBase.CreateFromJson_Js<List<string>>(Page.Request.Params["_RegisteredScriptKeys"]);
                this._Scripts.RegisteredScriptKeys = _CallbackParameters.RegisteredScriptKeys;
                _CallbackParameters.Command = (CallbackCommands)Convert.ToInt32(Page.Request.Params["_Command"]);
                _CallbackParameters.CommandArg = Page.Request.Params["_CommandArg"];
                _CallbackParameters.EventTriggerHierarchyID = Page.Request.Params["_EventTriggerHierarchyID"];
                this._CallerEventTriggerHierarchyID = _CallbackParameters.EventTriggerHierarchyID;
                _CallbackParameters.ArgFromClient = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(Page.Request.Params["_ArgFromClient"]));
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (!DesignMode)
            {
                if (!_EnablePageViewState) Page.EnableViewState = _EnablePageViewState;
                if (!_EnablePageEventValidation) Page.EnableEventValidation = _EnablePageEventValidation;
            }

            InitScriptResources();
            base.OnInit(e);

            _hd_ControlStates.ID = "ControlStates"; this.Controls.Add(_hd_ControlStates);
            _hd_ViewStates.ID = "ViewStates"; this.Controls.Add(_hd_ViewStates);
            _hd_ScriptKeys.ID = "ScriptKeys"; this.Controls.Add(_hd_ScriptKeys);
            var lk = new System.Web.UI.WebControls.LinkButton(); this.Controls.Add(lk);

            if (Page.IsPostBack)
            {
                if (Page.IsCallback)
                {
                    InitCallbackParams();
                }
                else
                {
                    _ViewStates = SerializableObjectBase.CreateFromXML<ViewStateCollection>(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(Page.Request.Params[_hd_ViewStates.UniqueID])));
                    _ControlStates = SerializableObjectBase.CreateFromXML<ControlStateCollection>(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(Page.Request.Params[_hd_ControlStates.UniqueID])));

                }
            }
            if (!Page.IsCallback) { Page.RegisterRequiresControlState(this); }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.EnableHistory)
            {
                _EnableHitoryBeforeLoad = true;
                History h = new History();
                h.ID = "History";
                this.Controls.Add(h);
                if (!Scripts.IsScriptRegistered(ClientID + "InitJs"))
                {
                    string st = ClientInstanceName + ".EnableHistory('" + h.ClientID + "','" + h.UniqueID + "'," + SerializableObjectBase.ToJson_Js(h.CallbackOptions) + ");\r\n";
                    Scripts.RegisterStartupScript(ClientID + "InitJs", st);
                }
            }
        }

        protected override object SaveControlState()
        {
            if (!Page.IsCallback)
            {
                OnRegisterJavascript();
                RegisterPageMethodsJs();
                RegisterJsOnInitLoad();
            }
            return base.SaveControlState();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!Page.IsCallback && !DesignMode)
            {
                OnSaveControlStates();

                //this.Attributes.Add("Settings", _Settings.ToJson_Js());
                _hd_ControlStates.Value = Convert.ToBase64String(new System.Text.UTF8Encoding().GetBytes(SerializableObjectBase.ToXML(ControlStates)));
                _hd_ViewStates.Value = Convert.ToBase64String(new System.Text.UTF8Encoding().GetBytes(SerializableObjectBase.ToXML(ViewStates)));
                var L = new List<string>();
                foreach (var s in Scripts) { L.Add(s.Key); }
                _hd_ScriptKeys.Value = SerializableObjectBase.ToJson_Js(L);
            }

            base.Render(writer);
        }

        private void RegisterJsOnInitLoad()
        {
            foreach (ScriptResource sl in PreRegisteredScriptResources)
            {
                switch (sl.LoadFrom)
                {
                    case ScriptResourceLoadFrom.BuildIn:
                        switch (sl.Name)
                        {
                            case "JQuery":
                                Page.ClientScript.RegisterClientScriptInclude("JQuery", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Js.jquery-1.8.3.js"));
                                break;
                            case "JQuery.min":
                                Page.ClientScript.RegisterClientScriptInclude("JQuery", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Js.jquery-1.8.3.min.js"));
                                break;
                            case "JQuery.Ui":
                                Page.ClientScript.RegisterClientScriptInclude("JQuery.Ui", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Js.jquery-ui-1.9.2.custom.js"));
                                break;
                            case "JQuery.Ui.min":
                                Page.ClientScript.RegisterClientScriptInclude("JQuery.Ui", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Js.jquery-ui-1.9.2.custom.min.js"));
                                break;
                        }
                        break;
                    case ScriptResourceLoadFrom.CDN:
                        Page.ClientScript.RegisterClientScriptInclude(sl.Name + "_" + sl.Version, sl.URL);
                        break;
                }
            }

            if (EnableHistory) { if (!_EnableHitoryBeforeLoad) { throw new Exception("EnableHistory must be set before Page Load."); } }
            if (Navigate != null) { _Settings.ServerEvents_Navigate = true; }

            if (_Settings.CallWaitingIcon == "")
            { _Settings.CallWaitingIcon = Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Image.Processing.gif"); }
            else
            { _Settings.CallWaitingIcon = Page.ResolveClientUrl(_Settings.CallWaitingIcon); }

            if (!Page.ClientScript.IsClientScriptIncludeRegistered("CallbackManager"))
            {
                string JsUrl;
#if DEBUG
                JsUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Js.CallbackManager.js");
#else
               JsUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Js.CallbackManager.min.js");
#endif
                Page.ClientScript.RegisterClientScriptInclude("CallbackManager", JsUrl);
            }
            if (!Page.ClientScript.IsStartupScriptRegistered("CallbackManager_Init"))
            {
                string st = "var " + this.ClientInstanceName + "= new Class_CallbackManager('" + ClientID + "'," + _Settings.ToJson_Js() + ");\r\n";
                //st += this.ClientInstanceName + ".Init();\r\n";
                st += this.ClientInstanceName + ".Init();\r\n";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallbackManager_Init", st, true);
            }

            Scripts.FinalizeScriptsToClient(Page);
            foreach (var s in this.Scripts)
            {
                switch (s.Type)
                {
                    case ScriptTypes.Css:
                        HtmlGenericControl css = new HtmlGenericControl("link");
                        css.Attributes.Add("href", s.Javascript);
                        css.Attributes.Add("type", "text/css");
                        css.Attributes.Add("rel", "stylesheet");
                        Page.Header.Controls.Add(css);
                        break;
                    case ScriptTypes.Include:
                        Page.ClientScript.RegisterClientScriptInclude(this.GetType(), s.Key, s.Javascript);
                        break;
                    case ScriptTypes.Block:
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), s.Key, s.Javascript, true);
                        break;
                    case ScriptTypes.Startup:
                        Page.ClientScript.RegisterStartupScript(this.GetType(), s.Key, s.Javascript, true);
                        break;
                }
            }
        }

        private void RegisterPageMethodsJs()
        {
            string st = ClientInstanceName + ".PageMethodPath='" + Page.AppRelativeVirtualPath.Remove(0, Page.AppRelativeTemplateSourceDirectory.Length) + "';\r\n";
            MethodInfo[] methodInfos = Page.GetType().BaseType.GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (MethodInfo method in methodInfos)
            {
                foreach (Attribute attribute in method.GetCustomAttributes(true))
                {
                    if (attribute is WebMethodAttribute)
                    {
                        var MethodName = method.Name;
                        st += "function " + MethodName + "(";
                        var Parameters = method.GetParameters();
                        string stParameters = "{";
                        for (int i = 0; i < Parameters.Length; i++)
                        {
                            var ParameterName = Parameters[i].Name;
                            if (i > 0) { st += ","; stParameters += ","; }
                            st += ParameterName;
                            stParameters += "\"" + ParameterName + "\":" + ParameterName;
                        }
                        stParameters += "}";
                        st += ",Function_BeforeCall,Function_AfterCall){\r\n";
                        st += ClientInstanceName + ".CallPageMethod('" + MethodName + "'," + stParameters + ",Function_BeforeCall,Function_AfterCall);\r\n";
                        st += "}\r\n";
                    }
                }
            }
            this.Scripts.RegisterStartupScript("PageMethodsJs", st);
        }

        public void ReaseException(string ExceptionMessage)
        {
            var st = "0|" + ExceptionMessage.Length + "|Exception|" + ExceptionMessage;
            Page.Response.Write(st);
            Page.Response.End();
        }

        public void AddHistoryPoint(string Entry, string Title)
        {
            AddedHistoryPoint = new HistoryPoint() { Entry = Entry, Title = Title };
        }

        #region DesignMode
        internal string ShowControlInf_Design()
        {
            var p = new System.Web.UI.WebControls.Panel(); p.Width = 250; p.BorderWidth = 1; p.BorderColor = Color.Gold; p.BorderStyle = System.Web.UI.WebControls.BorderStyle.Ridge; p.ForeColor = Color.RoyalBlue; p.Font.Size = new System.Web.UI.WebControls.FontUnit(FontSize.Small); p.BackColor = Color.FromArgb(255, 255, 200);
            var l = new Literal(); p.Controls.Add(l);
            l.Text = "CallbackManager. Version: V<br/>Yu Heng Zhou<br/>2013 Jan, Canada";
            string st = this.RenderControl(p);
            return st;
        }
        #endregion
    }

    [Serializable]
    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    public class RefrenceCollection : System.Collections.IEnumerable
    {
        private Dictionary<string, object> D = new Dictionary<string, object>();

        public object this[string Key]
        {
            get { return Get(Key); }
            set { D[Key] = value; }
        }

        public void Add(string Key, Object Element)
        {
            D[Key] = Element;
        }

        public T Get<T>(string Key)
        {
            object o = D[Key];
            if (o == null) return default(T);
            if (o.GetType().IsSubclassOf(typeof(Control)))
            {
                var c = (Control)o;
                if (c.Page == null) return default(T);
            }
            return (T)o;
        }

        public object Get(string Key)
        {
            object o = D[Key];
            if (o == null) return null;
            if (o.GetType().IsSubclassOf(typeof(Control)))
            {
                var c = (Control)o;
                if (c.Page == null) return null;
            }
            return o;
        }

        public System.Collections.IEnumerator GetEnumerator() { return D.GetEnumerator(); }

        public int Count { get { return D.Count; } }
    }
}
