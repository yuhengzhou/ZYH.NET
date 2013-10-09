var MousePosition = { x: 0, y: 0 };
$(document).mousedown(DocumentMouseDown);

function DocumentMouseDown(e) {
    MousePosition.x = e.pageX;
    MousePosition.y = e.pageY;
}
function notifyScriptLoaded() {
    var x = arguments;
}

function Class_CallbackManager(ClientID, Settings) {
    var Self = this;
    var _PendingCalls = new Array();
    var _QueuedCalls = new Array();

    Self.Settings = Settings;
    Self.ControlStates;
    Self.ViewStates;
    Self.RegisteredScriptKeys;
    Self.Scripts;
    Self._CallbackResult = new Object();
    Self.PageMethodPath = '';

    var ProcessingIcon = document.createElement('img');
    var Cover = document.createElement('div');

    Self.Init = function () {
        //Self.Settings = $.evalJSON($('#' + ClientID).attr('Settings'));
        //var t = $.toJSON(Self.Settings);
        Self.ControlStates = Self.DeserializeControlState($.base64Decode($('#' + ClientID + '_ControlStates').val()));
        Self.ViewStates = Self.DeserializeViewState($.base64Decode($('#' + ClientID + '_ViewStates').val()));
        Self.RegisteredScriptKeys = $.evalJSON($('#' + ClientID + '_ScriptKeys').val());
    }

    //////////////////////////////////////////////////
    //History
    Self.CurrentEntry = '';
    var Timer_OnIdle;
    Self.ClientID_History;
    Self.UniqueID_History;
    Self.CallbackOptions_History;

    Self.EnableHistory = function (ClientID, UniqueID, CallbackOptions) {
        Self.ClientID_History = ClientID;
        Self.UniqueID_History = UniqueID;
        Self.CallbackOptions_History = CallbackOptions;
        Timer_OnIdle = window.setTimeout(OnIdle, 100);
    }

    Self.AddHistoryPoint = function (entry, title) {
        if (Self.CurrentEntry !== entry) {
            Self.CurrentEntry = entry;
            window.location.hash = entry;
            if ((typeof (title) !== 'undefined') && (title !== null)) { document.title = title; }
        }
    }

    Self.OnHistoryNavigate = function (entry) {
        Self.CurrentEntry = entry;
        var e_BeforeCall = { 'CancelCall': !Self.Settings.ServerEvents_Navigate, 'ArgFromClient': '', 'CallbackOptions': Self.CallbackOptions_History };
        e_BeforeCall.ControlReserved = entry;
        var FunBeforeCall = null;
        var FunAfterCall = null;
        if (Self.Settings.ClientEvent_BeforeCall_Navigate != '') { FunBeforeCall = eval(Self.Settings.ClientEvent_BeforeCall_Navigate); }
        if (Self.Settings.ClientEvent_AfterCall_Navigate != '') { FunAfterCall = eval(Self.Settings.ClientEvent_AfterCall_Navigate); }
        Self.CallServer(Self.UniqueID_History, Self.ClientID_History, 21, FunBeforeCall, e_BeforeCall, Self.HistoryNavigate_AfterCallProcessing, FunAfterCall);
    }

    Self.HistoryNavigate_AfterCallProcessing = function (id, e_AfterCall) {
        e_AfterCall.HistoryPoint = Get_HistoryPoint(e_AfterCall.ControlReserved);
    }

    function Get_HistoryPoint(HistoryPoint_Bin) {
        var s = HistoryPoint_Bin.indexOf("|");
        var n = parseInt(HistoryPoint_Bin.substring(0, s));
        HistoryPoint_Bin = HistoryPoint_Bin.substring(s + 1);
        var Title = HistoryPoint_Bin.substring(0, n);
        HistoryPoint_Bin = HistoryPoint_Bin.substring(n + 1);
        s = HistoryPoint_Bin.indexOf("|");
        n = parseInt(HistoryPoint_Bin.substring(0, s));
        HistoryPoint_Bin = HistoryPoint_Bin.substring(s + 1);
        var Entry = HistoryPoint_Bin.substring(0, n);
        return { 'Title': Title, 'Entry': Entry };
    }

    function OnIdle() {
        var entry = get_stateString();
        if (entry != Self.CurrentEntry) {
            Self.OnHistoryNavigate(entry);
        }
        Timer_OnIdle = window.setTimeout(OnIdle, 100);
    }

    function get_stateString() {
        var hash = null;
        if ($.browser.mozilla) {
            var href = window.location.href;
            var hashIndex = href.indexOf('#');
            if (hashIndex !== -1) {
                hash = href.substring(hashIndex + 1);
            }
            else {
                hash = "";
            }
            return hash;
        }
        else {
            hash = window.location.hash;
        }
        if ((hash.length > 0) && (hash.charAt(0) === '#')) {
            hash = hash.substring(1);
        }
        return hash;
    }

    //////////////////////////////////////////////////
    //Scripts
    var CurrentRegisterScriptIndex = 0;

    Self.DeserializeJavascripts = function (xml) {
        var DOM = XMLtoDOM(xml);
        var ss = new Array();
        var cs = $('Script', DOM);
        for (var i = 0; i < cs.length; i++) {
            var AlwaysForceRefresh = $('r', cs[i]).text() == 'true';
            var VersionForceRefresh = $('v', cs[i]).text();
            var key = $('k', cs[i]).text();
            var type = $('t', cs[i]).text();
            var js = $('js', cs[i]).text();
            var s = new Object(); s.Key = key; s.Type = type; s.Javascript = js; s.AlwaysForceRefresh = AlwaysForceRefresh; s.VersionForceRefresh = VersionForceRefresh;
            ss[i] = s;
        }
        return ss;
    }

    Self.SerializeRegisteredJavascriptKeys = function (keys) {
        var xml = '<ArrayOfString>';
        for (var i = 0; i < keys.length; i++) {
            xml += '<string>' + keys[i] + '</string>';
        }
        xml += '</ArrayOfString>';
        return xml;
    }

    Self.ScriptRegisterReady = function () {
        if (this.readyState == 'complete' || this.readyState == 'loaded' || $.browser.safari || $.browser.mozilla || $.browser.opera || $.browser.chrome) {
            CurrentRegisterScriptIndex++;
            Self.RegsterAllScripts_Synchro();
        }
    }

    Self.RegisterCss = function (Key, Script) {
        var script = document.getElementById(Key);
        if (script == null) { script = document.createElement('link'); }
        script.href = Script;
        script.id = Key;
        script.type = 'text/css';
        script.rel = 'stylesheet';
        script.charset = 'utf-8';
        script.appendChild;

        var head = document.getElementsByTagName('head')[0];
        head.appendChild(script);
    }

    //Self.RegisterCss_Synchro = function (Key, Script) {
    //    var path = Script + "?" + (new Date()).valueOf();
    //    //$.get(path, function (response) {
    //    //    var css = $('<style id="' + Key + '" type="text/css"></style>');
    //    //    $('head').append(css);
    //    //    css.text(response);
    //    //    CurrentRegisterScriptIndex++;
    //    //    Self.RegsterAllScripts_Synchro();
    //    //});
    //    $.ajax({
    //        url: path,
    //        type: 'get',
    //        beforeSend: function (request) {
    //            request.setRequestHeader('Connection', 'keep-alive');
    //        },
    //        success: function (response, Status, jqXHR) {
    //            var css = $('<style id="' + Key + '" type="text/css"></style>');
    //            $('head').append(css);
    //            css.text(response);
    //            CurrentRegisterScriptIndex++;
    //            Self.RegsterAllScripts_Synchro();
    //        },
    //        error: function (jqXHR, Status, ErrorThrown, Context) {
    //            CurrentRegisterScriptIndex++;
    //            Self.RegsterAllScripts_Synchro();
    //        }
    //    });
    //}

    Self.RegisterCss_Synchro = function (Key, Script) {
        var script = document.getElementById(Key);
        if (script == null) { script = document.createElement('link'); }
        script.href = Script;
        script.id = Key;
        script.type = 'text/css';
        script.rel = 'stylesheet';
        script.charset = 'utf-8';
        script.appendChild;
        if ($.browser.safari || $.browser.mozilla || $.browser.opera || $.browser.chrome) {
            if ($.browser.safari)
            { this.ScriptRegisterReady(); }
            else
            { script.onload = this.ScriptRegisterReady; }
        }
        else { script.onreadystatechange = this.ScriptRegisterReady; }
        var head = document.getElementsByTagName('head')[0];
        head.appendChild(script);
    }

    Self.RegisterClientScriptInclude = function (Key, Script) {
        var script = document.getElementById(Key);
        if (script == null) { script = document.createElement('script'); }
        script.src = Script;
        script.id = Key;
        script.type = 'text/javascript';
        script.charset = 'utf-8';
        script.appendChild;

        var head = document.getElementsByTagName('head')[0];
        head.appendChild(script);
    }

    Self.RegisterClientScriptInclude_Synchro = function (Key, Script) {
        var script = document.getElementById(Key);
        if (script == null) { script = document.createElement('script'); }
        script.src = Script;
        script.id = Key;
        script.type = 'text/javascript';
        script.charset = 'utf-8';
        script.appendChild;
        if ($.browser.safari || $.browser.mozilla || $.browser.opera || $.browser.chrome)
        { script.onload = this.ScriptRegisterReady; }
        else
        { script.onreadystatechange = this.ScriptRegisterReady; }
        var head = document.getElementsByTagName('head')[0];
        head.appendChild(script);
    }

    Self.RegisterClientScriptBlock = function (Key, Script) {
        var head = document.getElementsByTagName('head')[0];
        var s = document.getElementById(Key);
        if (s) { head.removeChild(s); }
        var script = document.createElement('script');
        script.id = Key;
        script.type = 'text/javascript';
        script.charset = 'utf-8';
        script.defer = true;

        script.text = Script;
        head.appendChild(script);
    }

    function AddRegisteredScriptKey(Key) {
        if ($.inArray(Key, Self.RegisteredScriptKeys) == -1) {
            Self.RegisteredScriptKeys[Self.RegisteredScriptKeys.length] = Key;
        }
    }

    Self.RegsterAllScripts = function () {
        if ($.browser.msie || $.browser.safari || ($.browser.mozilla && parseFloat($.browser.version) > 3.0) || $.browser.opera || $.browser.chrome) { CurrentRegisterScriptIndex = 0; Self.RegsterAllScripts_Synchro(); return; }
        for (var i = 0; i < Self.Scripts.length; i++) {
            var AlwaysForceRefresh = Self.Scripts[i].AlwaysForceRefresh;
            var VersionForceRefresh = Self.Scripts[i].VersionForceRefresh;
            var type = Self.Scripts[i].Type;
            var key = Self.Scripts[i].Key;
            var script = Self.Scripts[i].Javascript;
            switch (type) {
                case "Css": //Css = -1
                    if (AlwaysForceRefresh) { script += '#t=' + new Date().valueOf(); }
                    if (VersionForceRefresh != '') { script += '#v=' + VersionForceRefresh; }
                    Self.RegisterCss(key, script);
                    AddRegisteredScriptKey(key);
                    break;
                case "Include": //Include = 1
                    if (AlwaysForceRefresh) { script += '#t=' + new Date().valueOf(); }
                    if (VersionForceRefresh != '') { script += '#v=' + VersionForceRefresh; }
                    Self.RegisterClientScriptInclude(key, script);
                    AddRegisteredScriptKey(key);
                    break;
                case "Block": //Block = 2
                    Self.RegisterClientScriptBlock(key, script);
                    AddRegisteredScriptKey(key);
                case "Startup": //Startup = 3
                    Self.RegisterClientScriptBlock(key, script);
                    AddRegisteredScriptKey(key);
                    break;
            }
        }
    }

    Self.RegsterAllScripts_Synchro = function () {
        if (Self.Scripts[CurrentRegisterScriptIndex] && Self.Scripts[CurrentRegisterScriptIndex].Type != '') {
            var AlwaysForceRefresh = Self.Scripts[CurrentRegisterScriptIndex].AlwaysForceRefresh;
            var VersionForceRefresh = Self.Scripts[CurrentRegisterScriptIndex].VersionForceRefresh;
            var type = Self.Scripts[CurrentRegisterScriptIndex].Type;
            var key = Self.Scripts[CurrentRegisterScriptIndex].Key;
            var script = Self.Scripts[CurrentRegisterScriptIndex].Javascript;
            switch (type) {
                case "Css": //Css = -1
                    if (AlwaysForceRefresh) { script += '#t=' + new Date().valueOf(); }
                    if (VersionForceRefresh != '') { script += '#v=' + VersionForceRefresh; }
                    this.RegisterCss_Synchro(key, script);
                    AddRegisteredScriptKey(key);
                    break;
                case "Include": //Include = 1
                    if (AlwaysForceRefresh) { script += '#t=' + new Date().valueOf(); }
                    if (VersionForceRefresh != '') { script += '#v=' + VersionForceRefresh; }
                    this.RegisterClientScriptInclude_Synchro(key, script);
                    AddRegisteredScriptKey(key);
                    break;
                case "Block": //Block = 2
                    this.RegisterClientScriptBlock(key, script);
                    AddRegisteredScriptKey(key);
                    CurrentRegisterScriptIndex++;
                    this.RegsterAllScripts_Synchro();
                    break;
                case "Startup": //Startup = 3
                    this.RegisterClientScriptBlock(key, script);
                    AddRegisteredScriptKey(key);
                    CurrentRegisterScriptIndex++;
                    this.RegsterAllScripts_Synchro();
                    break;
            }
        }
    }
    //////////////////////////////////////////////////
    //ControlStates
    Self.DeserializeControlState = function (xml) {
        var DOM = XMLtoDOM(xml);
        var controlStates = new Array();
        var states;
        var cs = $('ControlState', DOM);
        for (var i = 0; i < cs.length; i++) {
            states = new Array();
            var id = $('id', cs[i]).text();
            var s = $('s', cs[i]);
            for (var j = 0; j < s.length; j++) {
                var n = $('n', s[j]).text();
                var v = $('v', s[j]).text();
                var state = new Object(); state.Name = n; state.Value = v;
                states[j] = state;
            }
            var controlState = new Object(); controlState.ClientID = id; controlState.States = states;
            controlStates[i] = controlState;
        }
        return controlStates;
    }

    Self.SerializeControlState = function (controlStates) {
        var xml = '<ControlStates>';
        for (var i = 0; i < controlStates.length; i++) {
            xml += '<ControlState>';
            xml += '<id>' + controlStates[i].ClientID + '</id>';
            for (var j = 0; j < controlStates[i].States.length; j++) {
                xml += '<s>';
                xml += '<n>' + controlStates[i].States[j].Name + '</n>';
                xml += '<v>' + controlStates[i].States[j].Value + '</v>';
                xml += '</s>';
            }
            xml += '</ControlState>';
        }
        xml += '</ControlStates>';
        return xml;
    }

    Self.GetControlState = function (clientID, stateName) {
        for (var i = 0; i < Self.ControlStates.length; i++) {
            if (Self.ControlStates[i].ClientID == clientID) {
                for (var j = 0; j < Self.ControlStates[i].States.length; j++) {
                    if (Self.ControlStates[i].States[j].Name == stateName) {
                        return Self.ControlStates[i].States[j].Value;
                    }
                }
            }
        }
    }

    Self.SetControlState = function (clientID, stateName, Value) {
        for (var i = 0; i < Self.ControlStates.length; i++) {
            if (Self.ControlStates[i].ClientID == clientID) {
                for (var j = 0; j < Self.ControlStates[i].States.length; j++) {
                    if (Self.ControlStates[i].States[j].Name == stateName) {
                        Self.ControlStates[i].States[j].Value = Value;
                        $('#' + ClientID + '_ControlStates').val($.base64Encode(Self.SerializeControlState(Self.ControlStates)));
                        return;
                    }
                }
            }
        }
    }
    //////////////////////////////////////////////////
    //ViewStates
    Self.DeserializeViewState = function (xml) {
        var DOM = XMLtoDOM(xml);
        var viewStates = new Array();
        var vs = $('ViewState', DOM);
        for (var i = 0; i < vs.length; i++) {
            var k = $('k', vs[i]).text();
            var v = $('v', vs[i]).text();
            var viewState = new Object(); viewState.Key = k; viewState.Value = v;
            viewStates[i] = viewState;
        }
        return viewStates;
    }

    Self.SerializeViewState = function (viewStates) {
        var xml = '<ViewStates>';
        for (var i = 0; i < viewStates.length; i++) {
            xml += '<ViewState>';
            xml += '<k>' + viewStates[i].Key + '</k>';
            xml += '<v>' + viewStates[i].Value + '</v>';
            xml += '</ViewState>';
        }
        xml += '</ViewStates>';
        return xml;
    }

    Self.GetViewState = function (Key) {
        for (var i = 0; i < Self.ViewStates.length; i++) {
            if (Self.ViewStates[i].Key == Key) {
                return Self.ViewStates[i].Value;
            }
        }
    }

    Self.SetViewState = function (Key, Value) {
        for (var i = 0; i < Self.ViewStates.length; i++) {
            if (Self.ViewStates[i].Key == Key) {
                Self.ViewStates[i].Value = Value;
                $('#' + ClientID + '_ViewStates').val($.base64Encode(Self.SerializeViewState(Self.ViewStates)));
                return;
            }
        }
    }
    //////////////////////////////////////////////////
    //Call PageMethod
    Self.CallPageMethod = function (PageMethod, Data, Function_BeforeCall, Function_AfterCall) {
        var CallbackOptions = { "AsyncCallOption": 4, "AfterCallAction": 0, "CanBeAborted": false, "UpdateViewState": false, "UpdateControlState": false };
        var e_BeforeCall = { "CancelCall": false, "CallbackOptions": CallbackOptions, "OverrideCallbackManagerSettings": CloneSettings() };
        var Context = { "Function_BeforeCall": Function_BeforeCall, "e_BeforeCall": e_BeforeCall, "Function_AfterCall": Function_AfterCall, "PageMethod": PageMethod, "Data": $.toJSON(Data) };
        Self.FireCall(Context);
    }
    //////////////////////////////////////////////////
    //Delay/Re/Queue-Call server
    Self.DelayReFireCallback = function (Context, DelayMillisec) {
        if (!DelayMillisec) { DelayMillisec = 50; }
        setTimeout(function () { Self.FireCall(Context) }, DelayMillisec);
    }

    Self.FireQueuedCall = function () {
        if (_QueuedCalls.length > 0) {
            Self.FireCall(_QueuedCalls[0]);
        }
    }

    //CallServer
    Self.CallServer = function (SenderUniqueID, SenderClientID, Command, Function_BeforeCall, e_BeforeCall, Function_AfterCallProcessing, Function_AfterCall) {
        var Context = new Object();
        Context.SenderUniqueID = SenderUniqueID;
        Context.SenderClientID = SenderClientID;
        Context.Command = Command;
        Context.Function_BeforeCall = Function_BeforeCall;
        Context.e_BeforeCall = e_BeforeCall;
        Context.Function_AfterCallProcessing = Function_AfterCallProcessing;
        Context.Function_AfterCall = Function_AfterCall;

        Self.FireCall(Context);
    }

    Self.FireCall = function (Context) {
        switch (Context.e_BeforeCall.CallbackOptions.AsyncCallOption) {
            case 1: //'AbortPreviousCalls':
                var RemoveList = new Array();
                for (var i = 0; i < _PendingCalls.length; i++) {
                    if (_PendingCalls[i].e_BeforeCall.CallbackOptions.CanBeAborted) { RemoveList.push(_PendingCalls[i]); }
                }
                for (var j = 0; j < RemoveList.length; j++) { RemoveList[j].XHR.abort(); _PendingCalls.remove(RemoveList[j]); }
                RemoveList = new Array();
                for (var k = 0; k < _QueuedCalls.length; k++) {
                    if (_QueuedCalls[k].e_BeforeCall.CallbackOptions.CanBeAborted) { RemoveList.push(_QueuedCalls[k]); }
                }
                for (var l = 0; l < RemoveList.length; l++) { _QueuedCalls.remove(RemoveList[l]); }
                break;
            case 2: //'BlockLaterCalls':
                if (_PendingCalls.length > 0) { return; }
                break;
            case 3: //'QueueCalls':
                if (_PendingCalls.length > 0) { if (!_QueuedCalls.contains(Context)) { _QueuedCalls.push(Context); } return; }
                if (_QueuedCalls.length > 0) _QueuedCalls.remove(Context);
                break;
            case 4: //'MultipleCalls':
                break;
        }

        Context.e_BeforeCall.OverrideCallbackManagerSettings = CloneSettings();
        if (Context.Function_BeforeCall) { Context.Function_BeforeCall(Context.SenderClientID, Context.e_BeforeCall); }

        if (!Context.e_BeforeCall.CancelCall) {
            var d;
            var p = { type: 'post', timeout: Context.e_BeforeCall.OverrideCallbackManagerSettings.CallWaitingTimeOut * 1000, success: Self.OnSuccess, beforeSend: Self.SetWaitingStatus, context: Context, error: Self.OnError };
            if (Context.PageMethod) {
                d = Context.Data;
                p.url = Self.PageMethodPath + '/' + Context.PageMethod;
                p.dataType = "json";
                p.contentType = "application/json; charset=utf-8";
            } else {
                d = { __EVENTTARGET: '', __EVENTARGUMENT: '', __CALLBACKPARAM: Context.e_BeforeCall.ControlReserved, __CALLBACKID: Context.SenderUniqueID };
                var __EVENTVALIDATION = $('#__EVENTVALIDATION').val();
                var PD = Context.e_BeforeCall.CallbackOptions.PostDataContainer;
                if (PD != 'NA') {
                    var Inputs;
                    if (PD == '' || PD == 'All' || PD == 'Form') {
                        Inputs = $(theForm).serializeArray();
                    } else {
                        PD = PD.replace(/,/g, ' :input,#');
                        Inputs = $('#' + PD + ' :input').serializeArray();
                    }
                    for (var i = 0; i < Inputs.length; i++) {
                        var n = Inputs[i].name;
                        if (n.indexOf('CallbackManager') == -1 && n != '_ViewStates' && n != '_ControlStates' && n != '__EVENTTARGET' & n != '__EVENTARGUMENT' & n != '__VIEWSTATE') {
                            if (Inputs[i].value != '') d[Inputs[i].name] = Inputs[i].value;
                        }
                    }
                }
                if (typeof (__EVENTVALIDATION) != 'undefined' && __EVENTVALIDATION != '') {
                    d.__EVENTVALIDATION = __EVENTVALIDATION;
                }
                var __VIEWSTATE = $('#__VIEWSTATE').val();
                if (typeof (__VIEWSTATE) != 'undefined' && __VIEWSTATE != '') {
                    d.__VIEWSTATE = __VIEWSTATE;
                }
                if (Context.e_BeforeCall.CallbackOptions.UpdateViewState) {
                    d._ViewStates = $('#' + ClientID + '_ViewStates').val();
                    //d._ViewStates = $.base64Encode(Self.SerializeViewState(Self.ViewStates));
                }
                if (Context.e_BeforeCall.CallbackOptions.UpdateControlState) {
                    d._ControlStates = $('#' + ClientID + '_ControlStates').val();
                    //d._ControlStates = $.base64Encode(Self.SerializeControlState(Self.ControlStates));
                }
                if (typeof (Context.e_BeforeCall.CommandArg) != 'undefined' && Context.e_BeforeCall.CommandArg != '') { d._CommandArg = Context.e_BeforeCall.CommandArg; }
                d._RegisteredScriptKeys = $.toJSON(Self.RegisteredScriptKeys);
                d._Command = Context.Command;
                d._EventTriggerHierarchyID = Context.e_BeforeCall.EventTriggerHierarchyID;
                d._ArgFromClient = $.base64Encode(Context.e_BeforeCall.ArgFromClient.toString());
                d._CallbackOptions = $.toJSON(Context.e_BeforeCall.CallbackOptions);
            }
            p.data = d;
            Context.XHR = $.ajax(p);
        }
    }

    Self.OnSuccess = function (Response, Status, jqXHR) {
        var Context = $(this)[0];
        if (Response.d) {
            var e_AfterCall = { "CallbackManager_ClientInstance": Self, "Context": Context, "Return": Response.d };
            Self.CancelWaitingStatus(Context);
            if (Context.Function_AfterCall) { Context.Function_AfterCall(Context.SenderClientID, e_AfterCall); }
        } else {
            if (Response.charAt(0) == "e") {
                Self.OnError(jqXHR, 'OnExceptionActions.ExceptionObject', Response.substring(1), Context);
            }
            else {
                var Result = '';
                if (Response.charAt(0) == "s") {
                    Result = Response.substring(1);
                }
                else {
                    var separatorIndex = Response.indexOf("|");
                    if (separatorIndex != -1) {
                        var validationFieldLength = parseInt(Response.substring(0, separatorIndex));
                        var validationField = Response.substring(separatorIndex + 1, separatorIndex + validationFieldLength + 1);
                        if (validationField != '') { $('#__EVENTVALIDATION').val(validationField); }
                        Result = Response.substring(separatorIndex + validationFieldLength + 1);
                    }
                }
                if (Result != '') {
                    Self._CallbackResult = new Object();
                    Get_CallbackResult(Result);
                    if (typeof (Self._CallbackResult.Exception) != 'undefined') {
                        Self.OnError(jqXHR, 'CustomRaisedException', Self._CallbackResult.Exception, Context);
                        return;
                    }
                    if (typeof (Self._CallbackResult.AddedHistoryPoint) != 'undefined') {
                        Self.AddHistoryPoint(Self._CallbackResult.AddedHistoryPoint.Entry, Self._CallbackResult.AddedHistoryPoint.Title);
                    }
                    var e_AfterCall = new Object();
                    e_AfterCall.CallbackOptions = $.evalJSON(Self._CallbackResult.CallbackOptions);
                    if (e_AfterCall.CallbackOptions.UpdateViewState) {
                        Self.ViewStates = Self.DeserializeViewState(Self._CallbackResult.ViewStates);
                        $('#' + ClientID + '_ViewStates').val($.base64Encode(Self._CallbackResult.ViewStates));
                    }
                    if (e_AfterCall.CallbackOptions.UpdateControlState) {
                        Self.ControlStates = Self.DeserializeControlState(Self._CallbackResult.ControlStates);
                        $('#' + ClientID + '_ControlStates').val($.base64Encode(Self._CallbackResult.ControlStates));
                    }
                    Self.Scripts = Self.DeserializeJavascripts(Self._CallbackResult.Scripts);
                    e_AfterCall.ControlReserved = Self._CallbackResult.ControlReserved;
                    e_AfterCall.ArgToClient = Self._CallbackResult.ArgToClient;
                    e_AfterCall.CallbackManager_ClientInstance = Self;
                    e_AfterCall.Context = Context;

                    Self.CancelWaitingStatus(Context);

                    if (Context.Function_AfterCallProcessing) { Context.Function_AfterCallProcessing(Context.SenderClientID, e_AfterCall); }
                    if (e_AfterCall.ReFiredCallback != true) {
                        if (Context.Function_AfterCall) { Context.Function_AfterCall(Context.SenderClientID, e_AfterCall); }
                        Self.RegsterAllScripts();
                    }
                }
            }
        }
        Self.FireQueuedCall();
    }

    Self.OnError = function (jqXHR, Status, ErrorThrown, Context) {
        if (typeof (Context) == 'undefined') { Context = $(this)[0]; }
        Self.CancelWaitingStatus(Context);
        if (Status == 'abort') { return; }
        if (Status == 'timeout') {
            if (Context.e_BeforeCall.OverrideCallbackManagerSettings.ClientEvent_OnCallTimeOut != '')
            { eval(Context.e_BeforeCall.OverrideCallbackManagerSettings.ClientEvent_OnCallTimeOut)(Context.SenderClientID, Context); }
            else
            { alert('Processing time out! Please press "Ctrl + F5" to refresh your brower and try again later.'); }
        }
        else {
            switch (Context.e_BeforeCall.OverrideCallbackManagerSettings.OnExceptionActions) {
                case -1: //'NA'
                    break;
                case 1: //'FriendlyMessage'
                    if (Status == 'CustomRaisedException') {
                        alert(ErrorThrown);
                    } else {
                        alert('Error occured! please try again later.');
                    }
                    break;
                case 2: //'DebugDetail'
                    var w = window.open();
                    var doc = w.document; //If "w" is null, set Pop-Up option to "Always allow pop-ups from localhost".
                    doc.open();
                    doc.write("Status: " + Status + "<br/>");
                    if (Status == 'CustomRaisedException') { doc.write(ErrorThrown); } else { doc.write(jqXHR.responseText); }
                    doc.close();
                    break;
                case 10: //'ClientEvent'
                    if (Context.e_BeforeCall.OverrideCallbackManagerSettings.ClientEvent_OnException == '') { throw 'Please set CallbackManage.ClientEvent_OnException to handle exceptions.'; }
                    eval(Context.e_BeforeCall.OverrideCallbackManagerSettings.ClientEvent_OnException)(Context.SenderClientID, Status, ErrorThrown, Context);
                    break;
            }

        }
        Self.FireQueuedCall();
    }

    Self.SetWaitingStatus = function (jqXHR, settings) {
        var Context = $(this)[0];
        _PendingCalls.push(Context);
        switch (Context.e_BeforeCall.OverrideCallbackManagerSettings.CallWaitingBehavior) {
            case -1: //'NA'
                break;
            case 3: //'WaitingCursor'
                document.body.style.cursor = 'wait';
                break;
            case 2: //'CenteredProcessingIcon'
                LoadCenteredProcessingIcon(Context);
                break;
            case 4: //'Cover'
                Self.LoadCover(Context);
                break;
            case 5: //'CoverAndProcessingIcon'
                Self.LoadCover(Context);
                LoadProcessingIcon(Context);
                break;
            case 6: //'CoverAndWaitingCursor'
                document.body.style.cursor = 'wait';
                Self.LoadCover(Context);
                break;
            case 7: //'CoverAndCenteredProcessingIcon'
                LoadCenteredProcessingIcon(Context);
                Self.LoadCover(Context);
                break;
            case 1: //'MovingProcessingIcon'
            default:
                LoadProcessingIcon(Context);
                break;
        }
    }

    Self.CancelWaitingStatus = function (Context) {
        _PendingCalls.remove(Context);
        if (_PendingCalls.length > 0) return;
        switch (Context.e_BeforeCall.OverrideCallbackManagerSettings.CallWaitingBehavior) {
            case -1: //'NA'
                break;
            case 3: //'WaitingCursor'
                document.body.style.cursor = 'default';
                break;
            case 2: //'CenteredProcessingIcon'
                UnloadCenteredProcessingIcon();
                break;
            case 4: //'Cover'
                Self.UnloadCover();
                break;
            case 5: //'CoverAndProcessingIcon'
                Self.UnloadCover();
                UnloadProcessingIcon();
                break;
            case 6: //'CoverAndWaitingCursor'
                document.body.style.cursor = 'default';
                Self.UnloadCover();
                break;
            case 7: //'CoverAndCenteredProcessingIcon'
                UnloadCenteredProcessingIcon();
                Self.UnloadCover();
                break;
            case 1: //'MovingProcessingIcon'
            default:
                UnloadProcessingIcon();
                break;
        }
    }

    function MouseMove_ProcessingState(e) {
        MousePosition.x = e.pageX; MousePosition.y = e.pageY;
        MoveProcessingIcon();
    }

    function CloneSettings() {
        var json = $.toJSON(Self.Settings);
        var s = $.evalJSON(json);
        delete s.EnableHistory; delete s.ServerEvents_Navigate; delete s.ClientInstanceName;
        return s;
    }

    function Get_CallbackResult(Result) {
        var separatorIndex = Result.indexOf("|");
        if (separatorIndex == -1) { return; }
        var Length = parseInt(Result.substring(0, separatorIndex));
        Result = Result.substring(separatorIndex + 1);
        separatorIndex = Result.indexOf("|");
        var it = Result.substring(0, separatorIndex);
        Result = Result.substring(separatorIndex + 1);
        switch (it) {
            case 'CallbackOptions':
                Self._CallbackResult.CallbackOptions = Result.substring(0, Length);
                break;
            case 'ControlStates':
                Self._CallbackResult.ControlStates = Result.substring(0, Length);
                break;
            case 'ViewStates':
                Self._CallbackResult.ViewStates = Result.substring(0, Length);
                break;
            case 'Scripts':
                Self._CallbackResult.Scripts = Result.substring(0, Length);
                break;
            case 'ControlReserved':
                Self._CallbackResult.ControlReserved = Result.substring(0, Length);
                break;
            case 'ArgToClient':
                Self._CallbackResult.ArgToClient = Result.substring(0, Length);
                break;
            case 'Exception':
                Self._CallbackResult.Exception = Result.substring(0, Length);
            case 'HistoryPoint':
                Self._CallbackResult.AddedHistoryPoint = Get_HistoryPoint(Result.substring(0, Length));
                break;
        }
        Get_CallbackResult(Result.substring(Length + 1));
    }

    function MoveProcessingIcon() {
        ProcessingIcon.style.top = (MousePosition.y + 5) + 'px';
        ProcessingIcon.style.left = (MousePosition.x + 12) + 'px';
    }

    function LoadProcessingIcon(Context) {
        ProcessingIcon.style.zIndex = 9999;
        ProcessingIcon.src = Context.e_BeforeCall.OverrideCallbackManagerSettings.CallWaitingIcon;
        ProcessingIcon.border = '0';
        ProcessingIcon.style.position = 'absolute';
        ProcessingIcon.style.backgrandColor = "Transparent";

        $(document).bind('mousemove', MouseMove_ProcessingState);
        document.body.appendChild(ProcessingIcon);
        MoveProcessingIcon();
    }

    function UnloadProcessingIcon() {
        $(document).unbind('mousemove', MouseMove_ProcessingState);
        try { document.body.removeChild(ProcessingIcon); } catch (err) { }
    }

    function LoadCenteredProcessingIcon(Context) {
        ProcessingIcon.style.zIndex = 9999;
        ProcessingIcon.src = Context.e_BeforeCall.OverrideCallbackManagerSettings.CallWaitingIcon;
        ProcessingIcon.border = '0';
        ProcessingIcon.style.position = 'absolute';
        ProcessingIcon.style.backgrandColor = "Transparent";
        document.body.appendChild(ProcessingIcon);
        ProcessingIcon.style.top = ($(document).height() / 2 - $(ProcessingIcon).height() / 2) + 'px';
        ProcessingIcon.style.left = ($(document).width() / 2 - $(ProcessingIcon).width() / 2) + 'px';
    }

    function UnloadCenteredProcessingIcon() {
        try { document.body.removeChild(ProcessingIcon); } catch (err) { }
    }

    Self.LoadCover = function (Context) {
        var ContextHasVaule = true;
        if (typeof Context === 'undefined') { ContextHasVaule = false; }
        Cover.style.zIndex = 9990;
        Cover.style.top = '0px';
        Cover.style.left = '0px';
        Cover.style.background = ContextHasVaule ? Context.e_BeforeCall.OverrideCallbackManagerSettings.CallWaitingCoverColor : Self.Settings.CallWaitingCoverColor;
        Cover.style.filter = 'alpha(opacity=' + parseFloat(ContextHasVaule ? Context.e_BeforeCall.OverrideCallbackManagerSettings.CallWaitingCoverTransparency : Self.Settings.CallWaitingCoverTransparency) * 100 + ')';
        Cover.style.opacity = ContextHasVaule ? Context.e_BeforeCall.OverrideCallbackManagerSettings.CallWaitingCoverTransparency : Self.Settings.CallWaitingCoverTransparency;
        Cover.style.align = 'center';
        Cover.style.position = 'absolute';
        Self.ResizeCover();
        $(window).bind('resize', Self.ResizeCover);
        document.body.appendChild(Cover);
    }

    Self.UnloadCover = function () {
        $(window).unbind('resize', Self.ResizeCover);
        try { document.body.removeChild(Cover); } catch (err) { }
    }

    Self.ResizeCover = function () {
        Cover.style.width = $(document).width() + "px";
        Cover.style.height = $(document).height() + "px";
    }
}

///////Common functions///////
//function WebForm_EncodeCallback(parameter) { if (encodeURIComponent) { return encodeURIComponent(parameter); } else { return escape(parameter); } }
function htmlEncode(value) { return $('<div/>').text(value).html(); } function htmlDecode(value) { return $('<div/>').html(value).text(); }
String.prototype.trim = function () { return (this.replace(/^[\s\xA0]+/, "").replace(/[\s\xA0]+$/, "")) }
String.prototype.startsWith = function (str) { return (this.match("^" + str) == str) }
Array.prototype.remove = function (e) { for (var i = 0; i < this.length; i++) { if (e === this[i]) this.splice(i, 1); } };
Array.prototype.contains = function (e) { for (var i = 0; i < this.length; i++) { if (e === this[i]) return true; } return false; };

function XMLtoDOM(XML) {
    var xmlDoc;
    try {
        if ($.browser.msie) {
            xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
            xmlDoc.async = this.async;
            xmlDoc["loadXM" + "L"](XML);
        }
        else {
            var parser = new DOMParser();
            xmlDoc = parser.parseFromString(XML, "text/xml");
        }
    }
    catch (e) {
        alert('Your broswer does not support DOM.');
    }
    return xmlDoc;
}

/*
jQuery BASE64 functions
$.base64Encode ( String str )
$.base64Decode ( String str )
*/
(function ($) {

    var keyString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

    var uTF8Encode = function (string) {
        string = string.replace(/\x0d\x0a/g, "\x0a");
        var output = "";
        for (var n = 0; n < string.length; n++) {
            var c = string.charCodeAt(n);
            if (c < 128) {
                output += String.fromCharCode(c);
            } else if ((c > 127) && (c < 2048)) {
                output += String.fromCharCode((c >> 6) | 192);
                output += String.fromCharCode((c & 63) | 128);
            } else {
                output += String.fromCharCode((c >> 12) | 224);
                output += String.fromCharCode(((c >> 6) & 63) | 128);
                output += String.fromCharCode((c & 63) | 128);
            }
        }
        return output;
    };

    var uTF8Decode = function (input) {
        var string = "";
        var i = 0;
        var c = c1 = c2 = 0;
        while (i < input.length) {
            c = input.charCodeAt(i);
            if (c < 128) {
                string += String.fromCharCode(c);
                i++;
            } else if ((c > 191) && (c < 224)) {
                c2 = input.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            } else {
                c2 = input.charCodeAt(i + 1);
                c3 = input.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }
        }
        return string;
    }

    $.extend({
        base64Encode: function (input) {
            var output = "";
            var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
            var i = 0;
            input = uTF8Encode(input);
            while (i < input.length) {
                chr1 = input.charCodeAt(i++);
                chr2 = input.charCodeAt(i++);
                chr3 = input.charCodeAt(i++);
                enc1 = chr1 >> 2;
                enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
                enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
                enc4 = chr3 & 63;
                if (isNaN(chr2)) {
                    enc3 = enc4 = 64;
                } else if (isNaN(chr3)) {
                    enc4 = 64;
                }
                output = output + keyString.charAt(enc1) + keyString.charAt(enc2) + keyString.charAt(enc3) + keyString.charAt(enc4);
            }
            return output;
        },
        base64Decode: function (input) {
            var output = "";
            var chr1, chr2, chr3;
            var enc1, enc2, enc3, enc4;
            var i = 0;
            input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");
            while (i < input.length) {
                enc1 = keyString.indexOf(input.charAt(i++));
                enc2 = keyString.indexOf(input.charAt(i++));
                enc3 = keyString.indexOf(input.charAt(i++));
                enc4 = keyString.indexOf(input.charAt(i++));
                chr1 = (enc1 << 2) | (enc2 >> 4);
                chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
                chr3 = ((enc3 & 3) << 6) | enc4;
                output = output + String.fromCharCode(chr1);
                if (enc3 != 64) {
                    output = output + String.fromCharCode(chr2);
                }
                if (enc4 != 64) {
                    output = output + String.fromCharCode(chr3);
                }
            }
            output = uTF8Decode(output);
            return output;
        }
    });
})(jQuery);

// jQuery.toJSON(json - serializble)
// jQuery.evalJSON(src)
// jQuery.secureEvalJSON(src)
(function ($) {
    $.toJSON = function (o) {
        if (typeof (JSON) == 'object' && JSON.stringify)
            return JSON.stringify(o); var type = typeof (o); if (o === null)
                return "null"; if (type == "undefined")
                    return undefined; if (type == "number" || type == "boolean")
                        return o + ""; if (type == "string")
                            return $.quoteString(o); if (type == 'object') {
                                if (typeof o.toJSON == "function")
                                    return $.toJSON(o.toJSON()); if (o.constructor === Date) {
                                        var month = o.getUTCMonth() + 1; if (month < 10) month = '0' + month; var day = o.getUTCDate(); if (day < 10) day = '0' + day; var year = o.getUTCFullYear(); var hours = o.getUTCHours(); if (hours < 10) hours = '0' + hours; var minutes = o.getUTCMinutes(); if (minutes < 10) minutes = '0' + minutes; var seconds = o.getUTCSeconds(); if (seconds < 10) seconds = '0' + seconds; var milli = o.getUTCMilliseconds(); if (milli < 100) milli = '0' + milli; if (milli < 10) milli = '0' + milli; return '"' + year + '-' + month + '-' + day + 'T' +
                                                                                                hours + ':' + minutes + ':' + seconds + '.' + milli + 'Z"';
                                    }
                                if (o.constructor === Array) {
                                    var ret = []; for (var i = 0; i < o.length; i++)
                                        ret.push($.toJSON(o[i]) || "null"); return "[" + ret.join(",") + "]";
                                }
                                var pairs = []; for (var k in o) {
                                    var name; var type = typeof k; if (type == "number")
                                        name = '"' + k + '"'; else if (type == "string")
                                            name = $.quoteString(k); else
                                            continue; if (typeof o[k] == "function")
                                                continue; var val = $.toJSON(o[k]); pairs.push(name + ":" + val);
                                }
                                return "{" + pairs.join(", ") + "}";
                            }
    }; $.evalJSON = function (src) {
        if (typeof (JSON) == 'object' && JSON.parse)
            return JSON.parse(src); return eval("(" + src + ")");
    }; $.secureEvalJSON = function (src) {
        if (typeof (JSON) == 'object' && JSON.parse)
            return JSON.parse(src); var filtered = src; filtered = filtered.replace(/\\["\\\/bfnrtu]/g, '@'); filtered = filtered.replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']'); filtered = filtered.replace(/(?:^|:|,)(?:\s*\[)+/g, ''); if (/^[\],:{}\s]*$/.test(filtered))
                return eval("(" + src + ")"); else
                throw new SyntaxError("Error parsing JSON, source is not valid.");
    }; $.quoteString = function (string) {
        if (string.match(_escapeable)) {
            return '"' + string.replace(_escapeable, function (a)
            { var c = _meta[a]; if (typeof c === 'string') return c; c = a.charCodeAt(); return '\\u00' + Math.floor(c / 16).toString(16) + (c % 16).toString(16); }) + '"';
        }
        return '"' + string + '"';
    }; var _escapeable = /["\\\x00-\x1f\x7f-\x9f]/g; var _meta = { '\b': '\\b', '\t': '\\t', '\n': '\\n', '\f': '\\f', '\r': '\\r', '"': '\\"', '\\': '\\\\' };
})(jQuery);