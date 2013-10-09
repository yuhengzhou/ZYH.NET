function Class_TextBox(ClientID, CallbackOptions, Settings) {
    var Self = this;
    Self.Control = $('#' + ClientID);
    Self.Settings = Settings;

    var TempTextBoxBackColor;
    var TextBoxLastKeyUp_e_BeforeCall;
    var TextBoxLastKeyUp_Time;

    Self.Serialize = function (e) {
        var xml = '<TextBox>';
        var Text = Self.Control.val(); if (Text != '') xml += '<Text>' + htmlEncode(Text) + '</Text>';
        if (e.keyCode && e.keyCode != 0) xml += '<KeyCode>' + e.keyCode + '</KeyCode>';
        if (e.ctrlKey) xml += '<Ctrl>' + e.ctrlKey + '</Ctrl>';
        if (e.altKey) xml += '<Alt>' + e.ctrlKey + '</Alt>';
        if (e.shiftKey) xml += '<Shift>' + e.shiftKey + '</Shift>';
        if (e.type != 'blur') var CP = Self.GetCursorPosition(); if (CP) xml += '<CP>' + CP + '</CP>';
        xml += '</TextBox>';
        return xml;
    }

    Self.Init = function () {
        Self.Control.bind('focus', Self.GetFocus);
        Self.Control.bind('blur', Self.LostFocus);
        Self.Control.bind('keydown', Self.KeyDown);
        Self.Control.bind('keyup', Self.KeyUp);
        Self.Control.bind('paste', function (e) { setTimeout(function () { Self.KeyUp(e); }, 5); });
    }

    Self.AfterCallProcessing = function (id, e_AfterCall) {
        e_AfterCall.CommandArg = Self.Settings.CommandArg;
        var DOM = XMLtoDOM(e_AfterCall.ControlReserved);
        e_AfterCall.Text = $('Text', DOM).text();
        var KeyCode = $('KeyCode', DOM).text(); if (KeyCode == '') e_AfterCall.KeyCode = 0; else e_AfterCall.KeyCode = parseInt(KeyCode);
        var Ctrl = $('Ctrl', DOM).text(); if (Ctrl == '') e_AfterCall.Ctrl = false; else e_AfterCall.Ctrl = true;
        var Alt = $('Alt', DOM).text(); if (Alt == '') e_AfterCall.Alt = false; else e_AfterCall.Alt = true;
        var Shift = $('Shift', DOM).text(); if (Shift == '') e_AfterCall.Shift = false; else e_AfterCall.Shift = true;
        var CP = $('CP', DOM).text(); if (CP == '') e_AfterCall.CursorPosition = 0; else e_AfterCall.CursorPosition = parseInt(CP);
        var SCPT = $('SCPT', DOM).text(); if (SCPT == '') e_AfterCall.SetCursorPositionTo = -1; else e_AfterCall.SetCursorPositionTo = parseInt(SCPT);
        switch (e_AfterCall.CallbackOptions.AfterCallAction) {
            case 0://'NA':
                break;
            case 11://'RefreshToSynchro':
                if (Self.Control.val() != e_AfterCall.Text) {
                    Self.Control.val(e_AfterCall.Text);
                    Self.SetCursorPosition(e_AfterCall.CursorPosition);
                }
                break;
            case 1://'RecallToSynchro':
                if (Self.Control.val() != e_AfterCall.Text) { Self.OnKeyUpDeply(); }
                break;
        }
        if (e_AfterCall.SetCursorPositionTo != -1) Self.SetCursorPosition(e_AfterCall.SetCursorPositionTo); //not for LostFocus
    }

    Self.GetFocus = function (e) {
        Self.GFCC();
        //Call Server
        var FunBeforeCall = null;
        var FunAfterCall = null;
        var e_BeforeCall = { 'CancelCall': !Self.Settings.ServerEvents_GetFocus, 'ArgFromClient': '', 'CommandArg': Self.Settings.CommandArg, 'Instance': Self, 'EventTriggerHierarchyID': Self.Settings.EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
        e_BeforeCall.ControlReserved = Self.Serialize(e);
        e_BeforeCall.Text = Self.Control.val();
        if (Self.Settings.ClientEvent_BeforeCallback_GetFocus != '') { FunBeforeCall = eval(Self.Settings.ClientEvent_BeforeCallback_GetFocus); }
        if (Self.Settings.ClientEvent_AfterCallback_GetFocus != '') { FunAfterCall = eval(Self.Settings.ClientEvent_AfterCallback_GetFocus); }
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, '8', FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);

    }

    Self.GFCC = function () {
        TempTextBoxBackColor = Self.Control.css('background-color');
        Self.Control.css('background-color', Self.Settings.OnFocusBackColor);
    }

    Self.LostFocus = function (e) {
        Self.LFCC();
        //Call Server
        var FunBeforeCall = null;
        var FunAfterCall = null;
        var e_BeforeCall = { 'CancelCall': !Self.Settings.ServerEvents_LostFocus, 'ArgFromClient': '', 'CommandArg': Self.Settings.CommandArg, 'Instance': Self, 'EventTriggerHierarchyID': Self.Settings.EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
        e_BeforeCall.ControlReserved = Self.Serialize(e);
        e_BeforeCall.Text = Self.Control.val();
        if (Self.Settings.ClientEvent_BeforeCallback_LostFocus != '') { FunBeforeCall = eval(Self.Settings.ClientEvent_BeforeCallback_LostFocus); }
        if (Self.Settings.ClientEvent_AfterCallback_LostFocus != '') { FunAfterCall = eval(Self.Settings.ClientEvent_AfterCallback_LostFocus); }
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, '9', FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);

    }

    Self.LFCC = function () {
        Self.Control.css('background-color', TempTextBoxBackColor);
    }

    Self.KeyDown = function (e) {
        //Call Server
        var FunBeforeCall = null;
        var FunAfterCall = null;
        var e_BeforeCall = { 'CancelCall': !Self.Settings.ServerEvents_KeyDown, 'ArgFromClient': '', 'CommandArg': Self.Settings.CommandArg, 'Instance': Self, 'EventTriggerHierarchyID': Self.Settings.EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
        e_BeforeCall.ControlReserved = Self.Serialize(e);
        e_BeforeCall.Text = Self.Control.val();
        e_BeforeCall.KeyCode = e.keyCode;
        e_BeforeCall.Ctrl = e.ctrlKey;
        e_BeforeCall.Alt = e.altKey;
        e_BeforeCall.Shift = e.shiftKey;
        if (Self.Settings.ClientEvent_BeforeCallback_KeyDown != '') { FunBeforeCall = eval(Self.Settings.ClientEvent_BeforeCallback_KeyDown); }
        if (Self.Settings.ClientEvent_AfterCallback_KeyDown != '') { FunAfterCall = eval(Self.Settings.ClientEvent_AfterCallback_KeyDown); }
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, '6', FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);
    }

    Self.KeyUp = function (e) {
        TextBoxLastKeyUp_e_BeforeCall = { 'CancelCall': !Self.Settings.ServerEvents_KeyUp, 'ArgFromClient': '', 'CommandArg': Self.Settings.CommandArg, 'Instance': Self, 'EventTriggerHierarchyID': Self.Settings.EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
        TextBoxLastKeyUp_e_BeforeCall.ControlReserved = Self.Serialize(e);
        TextBoxLastKeyUp_e_BeforeCall.Text = Self.Control.val();
        TextBoxLastKeyUp_e_BeforeCall.KeyCode = e.keyCode;
        TextBoxLastKeyUp_e_BeforeCall.Ctrl = e.ctrlKey;
        TextBoxLastKeyUp_e_BeforeCall.Alt = e.altKey;
        TextBoxLastKeyUp_e_BeforeCall.Shift = e.shiftKey;

        TextBoxLastKeyUp_Time = new Date();
        setTimeout(Self.OnKeyUpDeply, Self.Settings.KeyUpEventDelay + 50);
    }

    Self.OnKeyUpDeply = function () {
        if (new Date().getTime() - TextBoxLastKeyUp_Time.getTime() < Self.Settings.KeyUpEventDelay) { return; }

        var FunBeforeCall = null;
        var FunAfterCall = null;
        if (Self.Settings.ClientEvent_BeforeCallback_KeyUp != '') { FunBeforeCall = eval(Self.Settings.ClientEvent_BeforeCallback_KeyUp); }
        if (Self.Settings.ClientEvent_AfterCallback_KeyUp != '') { FunAfterCall = eval(Self.Settings.ClientEvent_AfterCallback_KeyUp); }
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, '7', FunBeforeCall, TextBoxLastKeyUp_e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);
    }

    Self.GetText = function () {
        return Self.Control.val();
    }

    Self.SetText = function (txt) {
        Self.Control.val(txt);
    }

    Self.GetCursorPosition = function () {
        var ctrl = Self.Control[0];
        var CaretPos = 0;
        // IE 
        if (document.selection) {
            ctrl.focus();
            var Sel = document.selection.createRange();
            Sel.moveStart('character', -ctrl.value.length);
            CaretPos = Sel.text.length;
        }
            // Firefox 
        else if (ctrl.selectionStart || ctrl.selectionStart == '0')
            CaretPos = ctrl.selectionStart;
        return (CaretPos);
    }

    Self.SetCursorPosition = function (pos) {
        var ctrl = Self.Control[0];
        if (ctrl.setSelectionRange) {
            ctrl.focus();
            ctrl.setSelectionRange(pos, pos);
        } else if (ctrl.createTextRange) {
            var range = ctrl.createTextRange();
            range.collapse(true);
            range.moveEnd('character', pos);
            range.moveStart('character', pos);
            range.select();
        }
    }

}
