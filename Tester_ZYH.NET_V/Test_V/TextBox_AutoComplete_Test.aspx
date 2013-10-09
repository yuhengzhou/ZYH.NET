<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TextBox_AutoComplete_Test.aspx.cs"
    Inherits="Tester_ZYH.NET_V.Test_V.TextBox_AutoComplete_Test" %>

<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var TB;
        var Sel;
        function TB1_KD_B(id, e) {
            Sel = document.getElementById(id + 'Sel');
            if (Sel) {
                switch (e['KeyCode']) {
                    case 40:
                        if (Sel.options.length > 0) {
                            if (Sel.selectedIndex < Sel.options.length - 1) {
                                if (Sel.selectedIndex >= 0)
                                { Sel.selectedIndex++; }
                                else
                                { Sel.selectedIndex = 0; }
                            }
                            if (Sel.selectedIndex != -1) {
                                Sel.options[Sel.selectedIndex].selected = true;
                                TB[0].value = Sel.options[Sel.selectedIndex].text;
                            }
                        }
                        break;
                    case 38:
                        if (Sel.options.length > 0) {
                            if (Sel.selectedIndex > 0 && Sel.selectedIndex <= Sel.options.length)
                            { Sel.selectedIndex--; }
                            if (Sel.selectedIndex != -1) {
                                Sel.options[Sel.selectedIndex].selected = true;
                                TB[0].value = Sel.options[Sel.selectedIndex].text;
                            }
                        }
                        break;
                }
            }
        }

        function TB1_KD_A(id, e) {

        }

        function TB1_KU_B(id, e) {
            if ((e['KeyCode'] < 48 || e['KeyCode'] > 90) && e['KeyCode'] != 8 && e['KeyCode'] != 13) {
                e['CancelCall'] = true;
            }
        }

        function TB1_KU_A(id, e) {
            HideSel();
            if (e['Text'] == '') return;
            TB = $('#' + id);
            Sel = document.createElement('select');
            Sel.id = id + 'Sel';
            //            Sel.multiple = 'multiple';
            Sel.onchange = Sel_onchange;
            Sel.onblur = Sel_onLostFocus;
            Sel.size = '8';

            Sel.style.position = 'absolute';
            Sel.style.top = TB.position().top + TB[0].offsetHeight + 'px';
            Sel.style.left = TB.position().left + 'px';
            Sel.style.width = TB[0].offsetWidth + 'px';
            document.body.appendChild(Sel);
            //            $('#' + Sel.id).bind('change', Sel_onchange);
            //            $('#' + Sel.id).bind('blur', Sel_onLostFocus);

            var args = e['ArgToClient'].split(String.fromCharCode(255));
            for (var i = 0; i < args.length; i++) {
                Sel.options.add(new Option(args[i], args[i]));
            }
        }

        function Sel_onchange(e) {
            TB[0].value = Sel.options[Sel.selectedIndex].text;
        }
        function Sel_onLostFocus() {
            HideSel();
        }

        function TB1_LF_B(id, e) {
            setTimeout('HideSel()', 100);
        }

        function HideSel() {
            if (document.activeElement !== Sel) {
                try { document.body.removeChild(Sel); Sel = null; } catch (e) { }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:CallbackManager ID="CallbackManager1" runat="server">
        </cc1:CallbackManager>
        <br />
        <cc1:TextBox ID="TextBox1" runat="server" BackColor="#FFCCFF"
            OnFocusBackColor="Gold" ClientEvent_BeforeCallback_KeyDown="TB1_KD_B" ClientEvent_BeforeCallback_KeyUp="TB1_KU_B"
            ClientEvent_AfterCallback_KeyUp="TB1_KU_A" Style="margin-bottom: 18px" ClientEvent_BeforeCallback_LostFocus="TB1_LF_B"
            Text="" ForeColor="#00CC00" Font-Bold="False" Font-Italic="True" Font-Overline="False"
            Font-Strikeout="False" Font-Underline="False" OnKeyUp="TextBox1_KeyUp"/>
    </div>
    </form>
</body>
</html>
