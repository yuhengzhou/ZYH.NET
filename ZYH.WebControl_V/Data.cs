using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.ComponentModel;
using System.Xml.Linq;

namespace ZYH.WebControl_V
{
    #region ControlState
    [Serializable, XmlRoot("ControlStates")]
    public class ControlStateCollection : List<ControlState>
    {
        public StateCollection this[string clientID]
        {
            get
            {
                ControlState c = this.Where(x => x.ClientID == clientID).FirstOrDefault();
                if (c == null) return null;
                return c.States;
            }
            set
            {
                if (!string.IsNullOrEmpty(clientID))
                {
                    ControlState c = this.Where(x => x.ClientID == clientID).FirstOrDefault();
                    if (c == null) { this.Add(new ControlState(clientID, value)); } else { c.States = value; }
                }
            }
        }

        public string this[string clientID, string attributName]
        {
            get
            {
                //var s = from a in this.Where(x => x.ClientID == clientID).FirstOrDefault().Attributes where a.Name == attributName select a.Value;
                StateCollection ac = this[clientID];
                if (ac == null) return null;
                return ac[attributName];
            }
            set
            {
                if (!string.IsNullOrEmpty(clientID) && !string.IsNullOrEmpty(attributName))
                {
                    StateCollection ac = this[clientID];
                    if (ac == null)
                    { this.Add(new ControlState(clientID, new StateCollection() { new State(attributName, value) })); }
                    else
                    { ac[attributName] = value; }
                }
            }
        }
    }

    [Serializable]
    public class ControlState
    {
        public ControlState() { }
        public ControlState(string clientID, StateCollection states) { ClientID = clientID; States = states; }
        [XmlElement("id")]
        public string ClientID;
        [XmlElement("s")]
        public StateCollection States;
    }

    [Serializable]
    public class StateCollection : List<State>
    {
        public string this[string attributename]
        {
            get
            {
                State a = this.Where(x => x.Name == attributename).FirstOrDefault();
                if (a == null) return null;
                return a.Value;
            }
            set
            {
                if (!string.IsNullOrEmpty(attributename))
                {
                    State a = this.Where(x => x.Name == attributename).FirstOrDefault();
                    if (a == null) { this.Add(new State(attributename, value)); } else { a.Value = value; }
                }
            }
        }
    }

    [Serializable]
    public class State
    {
        public State() { }
        public State(string name, string value) { Name = name; Value = value; }
        [XmlElement("n")]
        public string Name;
        [XmlElement("v")]
        public string Value;
    }
    #endregion

    #region ViewState
    [Serializable, XmlRoot("ViewStates")]
    public class ViewStateCollection : List<ViewState>
    {
        public string this[string Key]
        {
            get
            {
                ViewState v = this.Where(x => x.Key == Key).FirstOrDefault();
                if (v == null) return null;
                return v.Value;
            }
            set
            {
                if (!string.IsNullOrEmpty(Key))
                {
                    ViewState v = this.Where(x => x.Key == Key).FirstOrDefault();
                    if (v == null) { this.Add(new ViewState(Key, value)); } else { v.Value = value; }
                }
            }
        }
    }

    [Serializable]
    public class ViewState
    {
        public ViewState() { }
        public ViewState(string key, string value) { Key = key; Value = value; }
        [XmlElement("k")]
        public string Key;
        [XmlElement("v")]
        public string Value;
    }
    #endregion

    #region Scripts
    [Serializable, XmlRoot("Scripts")]
    public class ScriptCollection : List<Script>
    {
        private List<string> _RegisteredScriptKeys = new List<string>();
        public List<string> RegisteredScriptKeys
        {
            get { return _RegisteredScriptKeys; }
            set { _RegisteredScriptKeys = value; }
        }

        protected Script GetScriptByKey(string key) { return this.Where(x => x.Key == key).FirstOrDefault(); }

        public Script this[string key]
        {
            get { return GetScriptByKey(key); }
            set
            {
                if (!string.IsNullOrEmpty(key))
                {
                    Script item = GetScriptByKey(key);
                    if (item == null) { this.Add(value); } else { item = value; }
                }
            }
        }

        public bool IsScriptRegistered(string Key) { return _RegisteredScriptKeys.Contains(Key) || GetScriptByKey(Key) != null; }

        public void RegisterStartupScript(string key, string script) { this[key] = new Script(key, ScriptTypes.Startup, script); }

        public void RegisterClientScriptBlock(string key, string script) { this[key] = new Script(key, ScriptTypes.Block, script); }

        public void RegisterClientScriptInclude(string key, string script) { this[key] = new Script(key, ScriptTypes.Include, script); }
        public void RegisterClientScriptInclude(string key, string script, bool AlwaysForceRefresh) { this[key] = new Script(key, ScriptTypes.Include, script, AlwaysForceRefresh); }
        public void RegisterClientScriptInclude(string key, string script, string VersionForceRefresh) { this[key] = new Script(key, ScriptTypes.Include, script, VersionForceRefresh); }

        public void RegisterCss(string key, string script) { this[key] = new Script(key, ScriptTypes.Css, script); }
        public void RegisterCss(string key, string script, bool AlwaysForceRefresh) { this[key] = new Script(key, ScriptTypes.Css, script, AlwaysForceRefresh); }
        public void RegisterCss(string key, string script, string VersionForceRefresh) { this[key] = new Script(key, ScriptTypes.Css, script, VersionForceRefresh); }

        internal void FinalizeScriptsToClient(System.Web.UI.Page page)
        {
            ScriptCollection L = new ScriptCollection();
            L.AddRange(this);
            this.Clear();
            var q = L.OrderBy(x => Convert.ToInt32(x.Type));
            foreach (Script s in q)
            {
                if (s.Type == ScriptTypes.Startup || !this.RegisteredScriptKeys.Contains(s.Key))
                {
                    if (s.Type == ScriptTypes.Include || s.Type == ScriptTypes.Css) s.Javascript = page.ResolveClientUrl(s.Javascript);
                    this[s.Key] = s;
                }
            }
        }
    }

    [Serializable]
    public class Script
    {
        public Script() { }
        public Script(string key, ScriptTypes type, string javascript) { Key = key; Type = type; Javascript = javascript; }
        public Script(string key, ScriptTypes type, string javascript, bool alwaysForceRefresh) { Key = key; Type = type; Javascript = javascript; AlwaysForceRefresh = alwaysForceRefresh; }
        public Script(string key, ScriptTypes type, string javascript, string versionForceRefresh) { Key = key; Type = type; Javascript = javascript; VersionForceRefresh = versionForceRefresh; }
        [XmlElement("r"), DefaultValue(false)]
        public bool AlwaysForceRefresh = false;
        [XmlElement("v"), DefaultValue("")]
        public string VersionForceRefresh = "";
        [XmlElement("k")]
        public string Key;
        [XmlElement("t")]
        public ScriptTypes Type;
        [XmlIgnore]
        public string Javascript;
        [XmlElement("js")]
        public XmlCDataSection Javascript_CData
        {
            get { return new XmlDocument().CreateCDataSection(Javascript); }
            set { Javascript = value.Value; }
        }
    }
    #endregion

    #region CallbackParameters & CallbackResult
    public class CallbackParameters
    {
        public string CallbackID;
        public ControlStateCollection ControlStates = new ControlStateCollection();
        public ViewStateCollection ViewStates = new ViewStateCollection();
        public List<string> RegisteredScriptKeys = new List<string>();
        public CallbackCommands Command = CallbackCommands.NA;
        public string CommandArg;
        public string EventTriggerHierarchyID;
        public string ControlReserved;
        private System.Xml.Linq.XElement _ControlReserved_XElement;
        public System.Xml.Linq.XElement ControlReserved_XElement
        {
            get
            {
                if (ControlReserved == null) return null;
                if (_ControlReserved_XElement == null) _ControlReserved_XElement = XElement.Parse(ControlReserved);
                return _ControlReserved_XElement;
            }
        }
        public string ArgFromClient;
        public CallbackOptions CallbackOptions;
    }

    public class CallbackResult
    {
        public CallbackOptions CallbackOptions;
        public ControlStateCollection ControlStates;
        public ViewStateCollection ViewStates;
        public ScriptCollection Scripts;
        public string ControlReserved;
        public string ArgToClient = "";
        public HistoryPoint AddedHistoryPoint;
    }
    #endregion

    #region ListControl
    [XmlRoot("ListControl")]
    public class ListControlData : SerializableObjectBase
    {
        [XmlElement("IT")]
        public ListItemCollection Items = new ListItemCollection();
        [XmlIgnore]
        public string Html = "";
        [XmlElement("Html"), DefaultValue("")]
        public XmlCDataSection Html_CData
        {
            get { return new XmlDocument().CreateCDataSection(Html); }
            set { Html = value.Value; }
        }
    }

    [Serializable]
    public class ListItemCollection : List<ListItem>
    {
        public List<ListItem> SelectedItems
        { get { return this.Where(x => x.Selected).ToList(); } }

        public ListItem this[string Value]
        { get { return this.Where(x => x.Value == Value).FirstOrDefault(); } }

        internal void ResolveImgUrl(System.Web.UI.Page p)
        {
            foreach (ListItem it in this)
            { if (it.ImgUrl != "") it._ImgUrl_Client = p.ResolveClientUrl(it.ImgUrl); }
        }
    }

    [Serializable]
    public class ListItem
    {
        public ListItem() { }
        public ListItem(string text, string value) { _Text = text; _Value = value; }
        public ListItem(string imgUrl, string text, string value) { _ImgUrl = imgUrl; _Text = text; _Value = value; }

        private bool _Enabled = true;
        [DefaultValue(true), XmlElement("E")]
        public bool Enabled { get { return _Enabled; } set { _Enabled = value; } }
        private bool _Selected = false;
        [DefaultValue(false), XmlElement("S")]
        public bool Selected { get { return _Selected; } set { _Selected = value; } }
        private string _Text = "";
        [DefaultValue(""), XmlElement("T")]
        public string Text { get { return _Text; } set { _Text = value; } }
        private string _Value = "";
        [DefaultValue(""), XmlElement("V")]
        public string Value { get { return _Value; } set { _Value = value; } }
        private string _ImgUrl = "";
        [DefaultValue(""), XmlElement("I"), Description(""), Browsable(true), System.ComponentModel.Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), System.ComponentModel.RefreshProperties(RefreshProperties.All)]
        public string ImgUrl { get { return _ImgUrl; } set { _ImgUrl = value; } }

        internal string _ImgUrl_Client = "";
        [DefaultValue(""), XmlElement("IC")]
        public string ImgUrl_Client { get { return _ImgUrl_Client; } set { _ImgUrl_Client = value; } }

    }
    #endregion

    #region TextBox
    [Serializable, XmlRoot("TextBox")]
    public class TextBoxData : SerializableObjectBase
    {
        [DefaultValue("")]
        public string Text = "";
        [DefaultValue(0)]
        public int KeyCode;
        [DefaultValue(false)]
        public bool Ctrl = false;
        [DefaultValue(false)]
        public bool Alt = false;
        [DefaultValue(false)]
        public bool Shift = false;
        [DefaultValue(0), XmlElement("CP")]
        public int CursorPosition = 0;
        [DefaultValue(-1), XmlElement("SCPT")]
        public int SetCursorPositionTo = -1;
    }
    #endregion

    #region Window
    [Serializable, XmlRoot("Window")]
    public class WindowData : SerializableObjectBase
    {
        [XmlIgnore]
        public string HtmlToBeLoad = "";
        [XmlElement("Html"), DefaultValue("")]
        public XmlCDataSection HtmlToBeLoad_CData
        {
            get { return new XmlDocument().CreateCDataSection(HtmlToBeLoad); }
            set { HtmlToBeLoad = value.Value; }
        }

        [XmlIgnore]
        public string Title = "";
        [XmlElement("Title"), DefaultValue("")]
        public XmlCDataSection Title_CData
        {
            get { return new XmlDocument().CreateCDataSection(Title); }
            set { Title = value.Value; }
        }
    }
    #endregion

    #region ThemeSelector
    [XmlRoot("ThemeSelector")]
    public class ThemeSelectorData : SerializableObjectBase
    {
        [XmlElement("TH")]
        public ThemeCollection Themes = new ThemeCollection();

        public string ToXML(string ClientID)
        {
            for (int i = 0; i < this.Themes.Count; i++) { this.Themes[i].ClientId = ClientID + "_" + i; }
            return base.ToXML();
        }
    }

    [Serializable]
    public class ThemeCollection : List<Theme>
    {
        public Theme SelectedTheme
        { get { return this.Where(x => x.Selected).FirstOrDefault(); } }

        public Theme this[string Value]
        { get { return this.Where(x => x.Value == Value).FirstOrDefault(); } }

        public void SetSelectedTheme(string SelectedThemeValue)
        {
            var st = this.Where(x => x.Value == SelectedThemeValue);
            if (st.Count() == 0) throw new Exception("Invailid SelectedThemeValue");
            this.ForEach(x => x.Selected = false);
            var st1 = st.First();
            st1.Selected = true;
        }
    }

    [Serializable]
    public class Theme
    {
        private string _ClientId = "";
        [Browsable(false), DefaultValue(""), XmlElement("ID")]
        public string ClientId { get { return _ClientId; } set { _ClientId = value; } }
        private bool _Enabled = true;
        [DefaultValue(true), XmlElement("E")]
        public bool Enabled { get { return _Enabled; } set { _Enabled = value; } }
        private bool _Selected = false;
        [DefaultValue(false), XmlElement("S")]
        public bool Selected { get { return _Selected; } set { _Selected = value; } }
        private string _Text = "";
        [DefaultValue(""), XmlElement("T")]
        public string Text { get { return _Text; } set { _Text = value; } }
        private string _Value = "";
        [DefaultValue(""), XmlElement("V")]
        public string Value { get { return _Value; } set { _Value = value; } }
        private string _CssClass = "";
        [DefaultValue(""), XmlElement("C")]
        public string CssClass { get { return _CssClass; } set { _CssClass = value; } }
        private string _CssClass_Selected = "";
        [DefaultValue(""), XmlElement("CS")]
        public string CssClass_Selected { get { return _CssClass_Selected; } set { _CssClass_Selected = value; } }

    }
    #endregion
}
