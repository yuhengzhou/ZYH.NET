function Class_Calendar(ClientID, CallbackOptions, Settings) {
    var Self = this;
    Self.HidenField = $('#' + ClientID + '_hd');
    Self.Control = $('#' + ClientID);
    Self.Settings = Settings;

    Self.Init = function () {
        Self.Value = Self.HidenField.val();
        Self.Control.datepicker({ onSelect: Self.OnDateChange, dateFormat: Self.Settings.DateFormat, altField: '#' + ClientID + '_hd', altFormat: 'mm/dd/yy', maxDate: Self.Settings.MaxDate, minDate: Self.Settings.MinDate, beforeShow: Self.GetFocus, onClose: Self.LostFocus });

        if (Self.Value != '') {
            try {
                var d = new Date(Self.Value);
                if (isNaN(d)) throw "Invalid Date";
                Self.Control.val($.datepicker.formatDate(Self.Settings.DateFormat, d));
            }
            catch (ex) {
                Self.Control.val(Self.Value);
                Self.Control.css('background-color', Self.Settings.InvalidValueBackColor);
            }
        }
    }

    Self.OnDateChange = function (dateText, inst) {
        Self.Value = Self.HidenField.val();

        var FunBeforeCall = null;
        var FunAfterCall = null;

        var e_BeforeCall = { 'CancelCall': !Self.Settings.ServerEvents_DateChanged, 'ArgFromClient': '', 'CommandArg': Self.Settings.CommandArg, 'Instance': Self, 'EventTriggerHierarchyID': Self.Settings.EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
        e_BeforeCall.ControlReserved = Self.Value;
        if (Self.Settings.ClientEvent_BeforeCallback_DateChanged != '') { FunBeforeCall = eval(Self.Settings.ClientEvent_BeforeCallback_DateChanged); }
        if (Self.Settings.ClientEvent_AfterCallback_DateChanged != '') { FunAfterCall = eval(Self.Settings.ClientEvent_AfterCallback_DateChanged); }
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, '17', FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);
    }

    Self.AfterCallProcessing = function (id, e_AfterCall) {
        switch (e_AfterCall.CallbackOptions.AfterCallAction) {
            case 0://'NA':
                break;
            case 11://'RecallToSynchro':
                if (e_AfterCall.Context.e_BeforeCall.ControlReserved != Self.Value) {
                    e_AfterCall.Context.e_BeforeCall.ControlReserved = Self.Value;
                    e_AfterCall.ReFiredCallback = true;
                    e_AfterCall.CallbackManager_ClientInstance.DelayReFireCallback(e_AfterCall.Context);
                }
                break;
            case 1://'RefreshToSynchro':
                if (e_AfterCall.Context.e_BeforeCall.ControlReserved != Self.Value) {
                    e_AfterCall.Value = e_AfterCall.Context.e_BeforeCall.ControlReserved;
                    Self.Value = e_AfterCall.Value;
                    Self.HidenField.val(Self.Value);
                    var ds = '';
                    if (Self.Value != '') {
                        var d = new Date(Self.Value);
                        ds = $.datepicker.formatDate(Self.Settings.DateFormat, d);
                        Self.Control.val($.datepicker.formatDate(Self.Settings.DateFormat, d));
                    }
                    Self.Control.val(ds);
                }
                break;
        }
    }

    Self.GetFocus = function (e) {
        //        TempTextBoxBackColor = Self.Control.css('background-color');
        Self.Control.css('background-color', Self.Settings.OnFocusBackColor);
    }

    Self.LostFocus = function (e) {
        try {
            var d = $.datepicker.parseDate(Self.Settings.DateFormat, Self.Control.val());
            Self.Control.css('background-color', Self.Settings.BackColor);
            if (Self.Value != Self.HidenField.val()) {
                //Self.Value = $.datepicker.formatDate('mm/dd/yy', d);
                Self.OnDateChange();
            }
        }
        catch (ex) {
            Self.HidenField.val(Self.Control.val());
            Self.Control.css('background-color', Self.Settings.InvalidValueBackColor);
        }
    }

    Self.TicksToTimeStamp = function (ticks) {
        return (ticks - 621355968000000000) / 10000;
    }

    Self.GetValue = function () {
        try { return new Date(Self.Value); } catch (ex) { return null; }
    }

    //value is Date type
    Self.SetValue = function (value) {
        Self.Control.val($.datepicker.formatDate(Self.Settings.DateFormat, value));
        var s = $.datepicker.formatDate('mm/dd/yy', value);
        Self.HidenField.val(s);
    }
}

