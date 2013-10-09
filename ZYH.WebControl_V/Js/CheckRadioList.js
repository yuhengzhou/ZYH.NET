//for CheckBoxList and RadioButtonList
function Class_CheckRadioList(ClientID, CallbackOptions, Settings) {
    var Self = this;
    Self.HidenField = $('#' + ClientID + '_hd');
    Self.Control = $('#' + ClientID);
    Self.Settings = Settings;
    Self.Items;
    Self.Xml;

    Self.Serialize = function () {
        var xml = '<ListControl>';
        for (var i = 0; i < Self.Items.length; i++) {
            var Control = $('#' + ClientID + ' input[_Position=' + i + ']');
            xml += '<IT>';
            if (Self.Items[i].ImgUrl != '') xml += '<I>' + Self.Items[i].ImgUrl + '</I>';
            if (Self.Items[i].Text != '') xml += '<T>' + htmlEncode(Self.Items[i].Text) + '</T>';
            var Seleted = Control[0].checked.toString(); if (Seleted != '' && Seleted.toLowerCase() == 'true') xml += '<S>' + Seleted + '</S>';
            var Value = Control.attr('_Value'); if (Value != '') xml += '<V>' + htmlEncode(Value) + '</V>';
            if (!Self.Items[i].Enabled) xml += '<E>' + Self.Items[i].Enabled + '</E>';
            xml += '</IT>';
        }
        xml += '</ListControl>';
        //Self.HidenField.val($.base64Encode(xml));
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
        Self.Xml = $.base64Decode(Self.HidenField.val());
        var DOM = XMLtoDOM(Self.Xml);
        Self.Items = Self.DeserializeItems(DOM);
        Self.Render();
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
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, (Self.Settings.ControlType == 3 ? '13' : '15'), FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);
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
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, (Self.Settings.ControlType == 3 ? '4' : '2'), FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);
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
                Self.Items = Self.DeserializeItems(DOM);
                Self.Render();
                break;
        }
    }

    Self.Render = function () {
        Self.ClearItems();
        //        var T = $('<table>'); Self.Control.append(T);
        if (Self.Settings.CellPadding != '-1') { Self.Control.attr({ cellPadding: Self.Settings.CellPadding }); }
        if (Self.Settings.CellSpacing != '-1') { Self.Control.attr({ cellSpacing: Self.Settings.CellSpacing }); }
        var ColumnCount = 0;
        var RepeatColumns;
        var ControlTypeHtml = (Self.Settings.ListControlType == 3) ? 'checkbox' : 'radio';
        var tr = $('<tr>'); Self.Control.append(tr);
        switch (Self.Settings.RepeatDirection) {
            case 1: //'Vertical':
                RepeatColumns = parseInt(Self.Settings.RepeatColumns); if (RepeatColumns == 0) { RepeatColumns = 1; }
                var RowCount = 0; var RowLength = parseInt(Self.Items.length / RepeatColumns) + 1; var NumberOfItemOnLastRow = Self.Items.length % RepeatColumns;
                for (var i = 0; i < Self.Items.length; i++) {
                    if (ColumnCount >= RepeatColumns) { ColumnCount = 0; RowCount++; tr = $('<tr>'); Self.Control.append(tr); }
                    var Index;
                    if (ColumnCount <= NumberOfItemOnLastRow)
                    { Index = ColumnCount * RowLength + RowCount; }
                    else
                    { Index = ColumnCount * (RowLength - 1) + NumberOfItemOnLastRow + RowCount }

                    ColumnCount++;
                    var td = $('<td>'); td.css('whiteSpace', 'nowrap'); tr.append(td);
                    var ckName = (ControlTypeHtml == 'checkbox') ? Self.Settings.UniqueID + "$" + i : Self.Settings.UniqueID;
                    var ck = $('<input type="' + ControlTypeHtml + '" name="' + ckName + '" />').attr('id', ClientID + "_" + i).attr('_Value', Self.Items[Index].Value).attr('_Position', Index);
                    if (Self.Items[Index].Selected) { ck.attr('checked', 'checked'); }
                    if (!Self.Items[Index].Enabled) ck.attr('disabled', 'disabled');
                    ck.bind('click', Self.OnChange);
                    var label = $('<label>').attr('for', ck.attr('id')).text(Self.Items[Index].Text);
                    if (Self.Settings.TextAlign == 'Left') {
                        if (Self.Items[Index].ImgUrl_Client != '') { var img = $('<img>').attr('src', Self.Items[Index].ImgUrl_Client).attr('border', '0'); td.append(img); } td.append(label); td.append(ck);
                    } else {
                        td.append(ck); if (Self.Items[Index].ImgUrl_Client != '') { var img = $('<img>').attr('src', Self.Items[Index].ImgUrl_Client).attr('border', '0'); td.append(img); } td.append(label);
                    }
                }
                break;
            case 0: //'Horizontal':
                RepeatColumns = parseInt(Self.Settings.RepeatColumns); if (RepeatColumns == 0) { RepeatColumns = 9999; }
                for (var i = 0; i < Self.Items.length; i++) {
                    if (ColumnCount >= RepeatColumns) { ColumnCount = 0; tr = $('<tr>'); Self.Control.append(tr); }
                    ColumnCount++;
                    var td = $('<td>'); td.css('whiteSpace', 'nowrap'); tr.append(td);
                    var ckName = (ControlTypeHtml == 'checkbox') ? Self.Settings.UniqueID + "$" + i : Self.Settings.UniqueID;
                    var ck = $('<input type="' + ControlTypeHtml + '" name="' + ckName + '" />').attr('id', ClientID + "_" + i).attr('_Value', Self.Items[i].Value).attr('_Position', i);
                    if (Self.Items[i].Selected) { ck.attr('checked', 'checked'); }
                    if (!Self.Items[i].Enabled) ck.attr('disabled', 'disabled');
                    ck.bind('click', Self.OnChange);
                    var label = $('<label>').attr('for', ck.attr('id')).text(Self.Items[i].Text);
                    if (Self.Settings.TextAlign == 'Left') {
                        if (Self.Items[i].ImgUrl_Client != '') { var img = $('<img>').attr('src', Self.Items[i].ImgUrl_Client).attr('border', '0'); td.append(img); } td.append(label); td.append(ck);
                    } else {
                        td.append(ck); if (Self.Items[i].ImgUrl_Client != '') { var img = $('<img>').attr('src', Self.Items[i].ImgUrl_Client).attr('border', '0'); td.append(img); } td.append(label);
                    }
                }
                break;
        }
    }

    Self.ClearItems = function () {
        $('tr', Self.Control).remove();
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

    Self.SelectedValue = function () {
        if (Self.Settings.ListControlType == 3) {
            var rt = '';
            var cks = $('input:checked', Self.Control);
            for (var i = 0; i < cks.length; i++) {
                if (rt != '') { rt += ','; }
                rt += $(cks[i]).attr('_Value');
            }
            return rt;
        }
        else {
            return $('input:checked', Self.Control).attr('_Value');
        }
    }
}