using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime;

namespace ZYH.WebControl_V.OtherClass
{
[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), ParseChildren(false), DefaultProperty("Text"), ControlBuilder(typeof(LinkButtonControlBuilder)), Designer("System.Web.UI.Design.WebControls.LinkButtonDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), ToolboxData("<{0}:LinkButton runat=\"server\">LinkButton</{0}:LinkButton>"), SupportsEventValidation, DefaultEvent("Click")]
public class LinkButton : WebControl, IButtonControl, IPostBackEventHandler
{
    // Fields
    private bool _textSetByAddParsedSubObject;
    private static readonly object EventClick = new object();
    private static readonly object EventCommand = new object();

    // Events
    [Category("Action"), WebDescription("LinkButton_OnClick")]
    public event EventHandler Click
    {
        add
        {
            base.Events.AddHandler(EventClick, value);
        }
        remove
        {
            base.Events.RemoveHandler(EventClick, value);
        }
    }

    [Category("Action"), WebDescription("Button_OnCommand")]
    public event CommandEventHandler Command
    {
        add
        {
            base.Events.AddHandler(EventCommand, value);
        }
        remove
        {
            base.Events.RemoveHandler(EventCommand, value);
        }
    }

    // Methods
    public LinkButton() : base(HtmlTextWriterTag.A)
    {
    }

    protected override void AddAttributesToRender(HtmlTextWriter writer)
    {
        if (this.Page != null)
        {
            this.Page.VerifyRenderingInServerForm(this);
        }
        string str = Util.EnsureEndWithSemiColon(this.OnClientClick);
        if (base.HasAttributes)
        {
            string str2 = base.Attributes["onclick"];
            if (str2 != null)
            {
                str = str + Util.EnsureEndWithSemiColon(str2);
                base.Attributes.Remove("onclick");
            }
        }
        if (str.Length > 0)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, str);
        }
        bool isEnabled = base.IsEnabled;
        if ((this.Enabled && !isEnabled) && this.SupportsDisabledAttribute)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
        }
        base.AddAttributesToRender(writer);
        if (isEnabled && (this.Page != null))
        {
            PostBackOptions postBackOptions = this.GetPostBackOptions();
            string postBackEventReference = null;
            if (postBackOptions != null)
            {
                postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(postBackOptions, true);
            }
            if (string.IsNullOrEmpty(postBackEventReference))
            {
                postBackEventReference = "javascript:void(0)";
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Href, postBackEventReference);
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
                this.Text = this.Text + ((LiteralControl) obj).Text;
            }
            else
            {
                this.Text = ((LiteralControl) obj).Text;
            }
            this._textSetByAddParsedSubObject = true;
        }
        else
        {
            string text = this.Text;
            if (text.Length != 0)
            {
                this.Text = string.Empty;
                base.AddParsedSubObject(new LiteralControl(text));
            }
            base.AddParsedSubObject(obj);
        }
    }

    protected virtual PostBackOptions GetPostBackOptions()
    {
        PostBackOptions options = new PostBackOptions(this, string.Empty);
        options.RequiresJavaScriptProtocol = true;
        if (!string.IsNullOrEmpty(this.PostBackUrl))
        {
            options.ActionUrl = HttpUtility.UrlPathEncode(base.ResolveClientUrl(this.PostBackUrl));
            if ((!base.DesignMode && (this.Page != null)) && string.Equals(this.Page.Request.Browser.Browser, "IE", StringComparison.OrdinalIgnoreCase))
            {
                options.ActionUrl = Util.QuoteJScriptString(options.ActionUrl, true);
            }
        }
        if (this.CausesValidation && (this.Page.GetValidators(this.ValidationGroup).Count > 0))
        {
            options.PerformValidation = true;
            options.ValidationGroup = this.ValidationGroup;
        }
        return options;
    }

    protected override void LoadViewState(object savedState)
    {
        if (savedState != null)
        {
            base.LoadViewState(savedState);
            if ((((string) this.ViewState["Text"]) != null) && this.HasControls())
            {
                this.Controls.Clear();
            }
        }
    }

    protected virtual void OnClick(EventArgs e)
    {
        EventHandler handler = (EventHandler) base.Events[EventClick];
        if (handler != null)
        {
            handler(this, e);
        }
    }

    protected virtual void OnCommand(CommandEventArgs e)
    {
        CommandEventHandler handler = (CommandEventHandler) base.Events[EventCommand];
        if (handler != null)
        {
            handler(this, e);
        }
        base.RaiseBubbleEvent(this, e);
    }

    protected internal override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if ((this.Page != null) && this.Enabled)
        {
            this.Page.RegisterPostBackScript();
            if ((this.CausesValidation && (this.Page.GetValidators(this.ValidationGroup).Count > 0)) || !string.IsNullOrEmpty(this.PostBackUrl))
            {
                this.Page.RegisterWebFormsScript();
            }
        }
    }

    protected virtual void RaisePostBackEvent(string eventArgument)
    {
        base.ValidateEvent(this.UniqueID, eventArgument);
        if (this.CausesValidation)
        {
            this.Page.Validate(this.ValidationGroup);
        }
        this.OnClick(EventArgs.Empty);
        this.OnCommand(new CommandEventArgs(this.CommandName, this.CommandArgument));
    }

    protected internal override void RenderContents(HtmlTextWriter writer)
    {
        if (base.HasRenderingData())
        {
            base.RenderContents(writer);
        }
        else
        {
            writer.Write(this.Text);
        }
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
    {
        this.RaisePostBackEvent(eventArgument);
    }

    // Properties
    [Themeable(false), DefaultValue(true), Category("Behavior"), WebDescription("Button_CausesValidation")]
    public virtual bool CausesValidation
    {
        get
        {
            object obj2 = this.ViewState["CausesValidation"];
            if (obj2 != null)
            {
                return (bool) obj2;
            }
            return true;
        }
        set
        {
            this.ViewState["CausesValidation"] = value;
        }
    }

    [Themeable(false), DefaultValue(""), Bindable(true), Category("Behavior"), WebDescription("WebControl_CommandArgument")]
    public string CommandArgument
    {
        get
        {
            string str = (string) this.ViewState["CommandArgument"];
            if (str != null)
            {
                return str;
            }
            return string.Empty;
        }
        set
        {
            this.ViewState["CommandArgument"] = value;
        }
    }

    [DefaultValue(""), Themeable(false), Category("Behavior"), WebDescription("WebControl_CommandName")]
    public string CommandName
    {
        get
        {
            string str = (string) this.ViewState["CommandName"];
            if (str != null)
            {
                return str;
            }
            return string.Empty;
        }
        set
        {
            this.ViewState["CommandName"] = value;
        }
    }

    [DefaultValue(""), Themeable(false), Category("Behavior"), WebDescription("Button_OnClientClick")]
    public virtual string OnClientClick
    {
        get
        {
            string str = (string) this.ViewState["OnClientClick"];
            if (str == null)
            {
                return string.Empty;
            }
            return str;
        }
        set
        {
            this.ViewState["OnClientClick"] = value;
        }
    }

    [WebDescription("Button_PostBackUrl"), Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Themeable(false), UrlProperty("*.aspx"), Category("Behavior"), DefaultValue("")]
    public virtual string PostBackUrl
    {
        get
        {
            string str = (string) this.ViewState["PostBackUrl"];
            if (str != null)
            {
                return str;
            }
            return string.Empty;
        }
        set
        {
            this.ViewState["PostBackUrl"] = value;
        }
    }

    internal override bool RequiresLegacyRendering
    {
        get
        {
            return true;
        }
    }

    public override bool SupportsDisabledAttribute
    {
        get
        {
            return (this.RenderingCompatibility < VersionUtil.Framework40);
        }
    }

    [WebDescription("LinkButton_Text"), Bindable(true), Category("Appearance"), DefaultValue(""), Localizable(true), PersistenceMode(PersistenceMode.InnerDefaultProperty)]
    public virtual string Text
    {
        get
        {
            object obj2 = this.ViewState["Text"];
            if (obj2 != null)
            {
                return (string) obj2;
            }
            return string.Empty;
        }
        set
        {
            if (this.HasControls())
            {
                this.Controls.Clear();
            }
            this.ViewState["Text"] = value;
        }
    }

    [DefaultValue(""), Themeable(false), Category("Behavior"), WebDescription("PostBackControl_ValidationGroup")]
    public virtual string ValidationGroup
    {
        get
        {
            string str = (string) this.ViewState["ValidationGroup"];
            if (str != null)
            {
                return str;
            }
            return string.Empty;
        }
        set
        {
            this.ViewState["ValidationGroup"] = value;
        }
    }
}

 
 

}
