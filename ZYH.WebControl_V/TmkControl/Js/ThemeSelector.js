function Class_ThemeSelector(ClientID, CallbackOptions, Settings) {
    var Self = this;
    Self.HidenField = $('#' + ClientID + '_hd');
    Self.Control = $('#' + ClientID);
    Self.Settings = Settings;
    Self.Themes;
    Self.Xml;
    Self.SelectedTheme;

    Self.Serialize = function () {
        var xml = '<ThemeSelector>';
        for (var i = 0; i < Self.Themes.length; i++) {
            xml += '<TH>';
            var Selected = Self.Themes[i].Selected; if (Selected) xml += '<S>true</S>';
            var Text = Self.Themes[i].Text; if (Text != '') xml += '<T>' + htmlEncode(Text) + '</T>';
            var Value = Self.Themes[i].Value; if (Value != '') xml += '<V>' + htmlEncode(Value) + '</V>';
            if (!Self.Themes[i].Enabled) xml += '<E>' + Self.Themes[i].Enabled + '</E>';
            var CssClass = Self.Themes[i].CssClass; if (CssClass != '') xml += '<C>' + CssClass + '</C>';
            var CssClass_Selected = Self.Themes[i].CssClass_Selected; if (CssClass_Selected != '') xml += '<CS>' + CssClass_Selected + '</CS>';
            xml += '</TH>';
        }
        xml += '</ThemeSelector>';
        return xml;
    }

    Self.Deserialize = function () {
        var DOM = XMLtoDOM(Self.Xml);
        Self.Themes = new Array();
        var Themes = $('TH', DOM);
        for (var i = 0; i < Themes.length; i++) {
            var t = new Object();
            t.ClientId = $('ID', Themes[i]).text();
            t.Text = $('T', Themes[i]).text();
            t.Value = $('V', Themes[i]).text();
            var E = $('E', Themes[i]).text(); if (E == '') { t.Enabled = true; } else { t.Enabled = (E.toLowerCase() == 'true'); }
            t.Selected = $('S', Themes[i]).text().toLowerCase() == 'true';
            t.CssClass = $('C', Themes[i]).text();
            t.CssClass_Selected = $('CS', Themes[i]).text();
            Self.Themes[i] = t;
            if (t.Selected) { Self.SelectedTheme = t; }
        }
    }

    Self.Init = function () {
        Self.Xml = $.base64Decode(Self.HidenField.val());
        Self.Deserialize();
    }

    Self.ThemeSelected = function (Value) {
        UpdateSelection(Value);
        //Call Server
        var FunBeforeCall = null;
        var FunAfterCall = null;
        var e_BeforeCall = { 'CancelCall': !Self.Settings.ServerEvents_ThemeSelected, 'ArgFromClient': '', 'CommandArg': Self.Settings.CommandArg, 'Instance': Self, 'EventTriggerHierarchyID': Self.Settings.EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
        e_BeforeCall.ControlReserved = Self.Xml;
        e_BeforeCall.SelectedThemeValue = Value;
        if (Self.Settings.ClientEvent_BeforeCallback_ThemeSelected != '') { FunBeforeCall = eval(Self.Settings.ClientEvent_BeforeCallback_ThemeSelected); }
        if (Self.Settings.ClientEvent_AfterCallback_ThemeSelected != '') { FunAfterCall = eval(Self.Settings.ClientEvent_AfterCallback_ThemeSelected); }
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, '20', FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);
    }

    Self.AfterCallProcessing = function (id, e_AfterCall) {
        switch (e_AfterCall.CallbackOptions.AfterCallAction) {
            case 0: //'NA':
            case 11: //'RecallToSynchro':
                var xml = Self.Serialize();
                if (e_AfterCall.Context.e_BeforeCall.ControlReserved != xml) {
                    e_AfterCall.Context.e_BeforeCall.ControlReserved = xml;
                    e_AfterCall.ReFiredCallback = true;
                    e_AfterCall.CallbackManager_ClientInstance.DelayReFireCallback(e_AfterCall.Context);
                    return;
                }
                else {
                    RenderSelection();
                }
                break;
            case 1: //'RefreshToSynchro':
                Self.Xml = e_AfterCall.ControlReserved;
                Self.HidenField.val($.base64Encode(Self.Xml));
                Self.Deserialize();
                RenderSelection();
                break;
        }
        for (var i = 0; i < Self.Themes.length; i++) {
            if (Self.Themes[i].Selected) { e_AfterCall.SelectedThemeValue = Self.Themes[i].Value; return; }
        }
    }

    function UpdateSelection(Value) {
        for (var i = 0; i < Self.Themes.length; i++) {
            Self.Themes[i].Selected = false;
            if (Self.Themes[i].Value == Value) { Self.Themes[i].Selected = true; }
        }
        Self.Xml = Self.Serialize();
        Self.HidenField.val($.base64Encode(Self.Xml));
    }

    function RenderSelection() {
        for (var i = 0; i < Self.Themes.length; i++) {
            var Theme = $('#' + Self.Themes[i].ClientId, Self.Control); Theme.attr('class', Self.Themes[i].CssClass);
            if (Self.Themes[i].Selected) { Theme.attr('class', Self.Themes[i].CssClass_Selected); }
        }
    }
}