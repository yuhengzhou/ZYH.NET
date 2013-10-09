//for DropDownList and ListBox
function Class_ListControl(ClientID, CallbackOptions, Settings) {
    var Self = this;
    Self.HidenField = $('#' + ClientID + '_hd');
    Self.Control = $('#' + ClientID);
    Self.Settings = Settings;
    Self.Items;
    Self.Xml;
    Self.Html;

    Self.Serialize = function () {
        var xml = '<ListControl>';
        var Items = $('option', Self.Control);
        var j = 0;
        for (var i = 0; i < Self.Items.length; i++) {
            xml += '<IT>';
            var Selected = Items[j].selected; if (Selected) xml += '<S>true</S>';
            var Text = Self.Items[i].Text; if (Text != '') xml += '<T>' + htmlEncode(Text) + '</T>';
            var Value = Self.Items[i].Value; if (Value != '') xml += '<V>' + htmlEncode(Value) + '</V>';
            if (!Self.Items[i].Enabled) xml += '<E>' + Self.Items[i].Enabled + '</E>'; else j++;
            xml += '</IT>';
        }
        xml += '</ListControl>';
        return xml;
    }

    Self.DeserializeItems = function (DOM) {
        var its = new Array();
        var items = $('IT', DOM);
        for (var i = 0; i < items.length; i++) {
            var it = new Object();
            it.ImgUrl_Client = $('IC', items[i]).text();
            it.ImgUrl = $('I', items[i]).text();
            it.Text = $('T', items[i]).text();
            it.Value = $('V', items[i]).text();
            var E = $('E', items[i]).text(); if (E == '') { it.Enabled = true; } else { it.Enabled = (E.toLowerCase() == 'true'); }
            it.Selected = $('S', items[i]).text().toLowerCase() == 'true';
            its[i] = it;
        }
        return its;
    }

    Self.Init = function () {
        //Self.Settings = $.evalJSON($('#' + ClientID).attr('Settings'));
        Self.Xml = $.base64Decode(Self.HidenField.val());
        var DOM = XMLtoDOM(Self.Xml);
        Self.Items = Self.DeserializeItems(DOM);
        Self.Control.bind('change', Self.OnChange);
    }

    Self.OnChange = function (e) {
        //Call Server
        var FunBeforeCall = null;
        var FunAfterCall = null;
        var e_BeforeCall = { 'CancelCall': !Self.Settings.ServerEvents_SelectedIndexChanged, 'ArgFromClient': '', 'CommandArg': Self.Settings.CommandArg, 'Instance': Self, 'EventTriggerHierarchyID': Self.Settings.EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
        e_BeforeCall.ControlReserved = Self.Serialize();
        Self.HidenField.val($.base64Encode(e_BeforeCall.ControlReserved));
        if (Self.Settings.ClientEvent_BeforeCallback_SelectedIndexChanged != '') { FunBeforeCall = eval(Self.Settings.ClientEvent_BeforeCallback_SelectedIndexChanged); }
        if (Self.Settings.ClientEvent_AfterCallback_SelectedIndexChanged != '') { FunAfterCall = eval(Self.Settings.ClientEvent_AfterCallback_SelectedIndexChanged); }
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, (Self.Settings.ControlType == 1 ? '5' : '3'), FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);
    }

    Self.OnFill = function (ArgFromClient) {
        if (typeof (ArgFromClient) === 'undefined') { ArgFromClient = ''; }
        //Call Server
        var FunBeforeCall = null;
        var FunAfterCall = null;
        var e_BeforeCall = { 'CancelCall': !Self.Settings.ServerEvents_Fill, 'ArgFromClient': ArgFromClient, 'CommandArg': Self.Settings.CommandArg, 'Instance': Self, 'EventTriggerHierarchyID': Self.Settings.EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
        e_BeforeCall.ControlReserved = Self.Serialize();
        if (Self.Settings.ClientEvent_BeforeCallback_Fill != '') { FunBeforeCall = eval(Self.Settings.ClientEvent_BeforeCallback_Fill); }
        if (Self.Settings.ClientEvent_AfterCallback_Fill != '') { FunAfterCall = eval(Self.Settings.ClientEvent_AfterCallback_Fill); }
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, (Self.Settings.ControlType == 1 ? '4' : '2'), FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);
    }

    Self.AfterCallProcessing = function (id, e_AfterCall) {
        e_AfterCall.CommandArg = Self.Settings.CommandArg;
        switch (e_AfterCall.CallbackOptions.AfterCallAction) {
            case 0: //'NA':
                break;
            case 11: //'RecallToSynchro': should only work with 'AsynchronousCallOptions = MultipleCalls or BlockLaterCalls'
                var xml = Self.Serialize();
                if (e_AfterCall.Context.e_BeforeCall.ControlReserved != xml) {
                    e_AfterCall.Context.e_BeforeCall.ControlReserved = xml;
                    e_AfterCall.ReFiredCallback = true;
                    e_AfterCall.CallbackManager_ClientInstance.DelayReFireCallback(e_AfterCall.Context);
                }
                else {
                }
                break;
            case 1: //'RefreshToSynchro':
                Self.Xml = e_AfterCall.ControlReserved;
                Self.HidenField.val($.base64Encode(Self.Xml));
                var DOM = XMLtoDOM(Self.Xml);
                Self.Html = $('Html', DOM).text();
                Self.Items = Self.DeserializeItems(DOM);
                Self.ClearItems();
                Self.Control.append(Self.Html);
                break;
        }
    }

    Self.ClearItems = function () {
        Self.Control[0].options.length = 0;
    }

    Self.SelectAll = function () {
        for (var i = 0; i < List.rows.length; i++) {
            var ck = (List.rows[i].cells[0].childNodes[0].tagName == 'INPUT') ? List.rows[i].cells[0].childNodes[0] : List.rows[i].cells[0].childNodes[1];
            var label = (List.rows[i].cells[0].childNodes[0].tagName == 'LABEL') ? List.rows[i].cells[0].childNodes[0] : List.rows[i].cells[0].childNodes[1];
            ck.checked = true;
        }
    }

    Self.UnselectAll = function () {
        for (var i = 0; i < List.rows.length; i++) {
            var ck = (List.rows[i].cells[0].childNodes[0].tagName == 'INPUT') ? List.rows[i].cells[0].childNodes[0] : List.rows[i].cells[0].childNodes[1];
            var label = (List.rows[i].cells[0].childNodes[0].tagName == 'LABEL') ? List.rows[i].cells[0].childNodes[0] : List.rows[i].cells[0].childNodes[1];
            ck.checked = false;
        }
    }

    Self.SelectedItems = function () { return $("option:selected", Self.Control); }
}
