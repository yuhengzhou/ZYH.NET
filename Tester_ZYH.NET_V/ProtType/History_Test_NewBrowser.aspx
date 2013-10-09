<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="History_Test_NewBrowser.aspx.cs"
    Inherits="Tester_ZYH.NET_V.ProtType.History_Test_NewBrowser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TTT</title>
    <script src="http://localhost:49345/ProtType/jquery-1.8.3.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        var CurrentEntry = '';
        var _timerCookie;

        function OnStepClick(step) {
            setState(step, step + ' clicked');
            GoStep(step);
        }

        function setState(entry, title) {
            if (CurrentEntry !== entry) {
                CurrentEntry = entry;
                window.location.hash = entry;
                if ((typeof (title) !== 'undefined') && (title !== null)) { document.title = title; }
            }
        }

        function OnHistoryNavigate(entry) {
            CurrentEntry = entry;
            GoStep(entry);
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
            if (step == '') { document.title = 'NNN'; } else { document.title = step + ' navigate'; }
        }
    </script>
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
    </div>
    <script type="text/javascript">
        _timerCookie = window.setTimeout(_onIdle, 100);
    </script>
    </form>
</body>
</html>
