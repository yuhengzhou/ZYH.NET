<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="History Test.aspx.cs" Inherits="Tester_ZYH.NET_V.ProtType.History_Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>kkk</title>
    <script src="jquery-1.8.3.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        var CurrentEntry;
        var CurrentHistoryPoint = null;
        var ThrowEvent = false;
        var _timerCookie;
        var HistoryPoints = new Array();

        function OnStepClick(step) {
            setState(step, step + ' clicked');
            GoStep(step);
        }

        function setState(entry, title, state) {
            if (CurrentEntry !== entry) {
                CurrentHistoryPoint = { Entry: entry, Title: title, State: state };
                HistoryPoints.push(CurrentHistoryPoint);
                ThrowEvent = false;
                if ($.browser.msie) {
                    CurrentEntry = entry;
                    var _historyFrame = document.getElementById('__historyFrame');
                    var frameDoc = _historyFrame.contentWindow.document;
                    frameDoc.open("javascript:'<html></html>'");
                    frameDoc.write("<html><head><title>" + (title || document.title) + "</title><scri" + "pt type=\"text/javascript\">parent.OnHistoryNavigate('" + entry + "');</scri" + "pt></head><body></body></html>");
                    frameDoc.close();
                }
                else {
                    CurrentEntry = entry;
                    window.location.hash = entry;
                }
                ThrowEvent = true;
                if ((typeof (title) !== 'undefined') && (title !== null)) { document.title = title; }
            }
        }

        function OnHistoryNavigate(entry) {
            if (ThrowEvent) {
                CurrentEntry = entry;
                CurrentHistoryPoint = null;
                for (var i = 0; i < HistoryPoints.length; i++) {
                    if (HistoryPoints[i].Entry == entry) { CurrentHistoryPoint = HistoryPoints[i]; break; }
                }
                if ((CurrentHistoryPoint != null) && (typeof (CurrentHistoryPoint.Title) !== 'undefined') && (CurrentHistoryPoint.Title !== null)) { document.title = CurrentHistoryPoint.Title; }
                GoStep(entry);
            }
        }

        function _onIdle() {
            var entry = get_stateString();
            if (entry != CurrentEntry) {
                OnHistoryNavigate(entry);
            }
            _timerCookie = window.setTimeout(_onIdle, 100);
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


        function GoStep(step) {
            document.getElementById('span1').innerHTML = 'You are on step: ' + step;
        }
    </script>
    <iframe id="__historyFrame"></iframe>
    <div>
        <a id="step1" href="javascript:OnStepClick(1);">step1</a>
        <br />
        <a id="step2" href="javascript:OnStepClick(2);">step2</a>
        <br />
        <a id="step3" href="javascript:OnStepClick(3);">step3</a>
        <br />
        <a id="step4" href="javascript:OnStepClick(4);">step4</a>
        <br />
        <a id="step5" href="javascript:OnStepClick(5);">step5</a>
        <br />
        <span id="span1"></span>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    </div>
    <script type="text/javascript">
        //        var _historyFrame = document.getElementById('__historyFrame');
        //        _historyFrame.title = document.title;
        //        var a = 1;

        if ($.browser.msie) {
            setState('', document.title);
        } else {
            CurrentHistoryPoint = { Entry: '', Title: document.title, State: null };
            HistoryPoints.push(CurrentHistoryPoint);
            _timerCookie = window.setTimeout(_onIdle, 100);
        }
    </script>
    </form>
</body>
</html>
