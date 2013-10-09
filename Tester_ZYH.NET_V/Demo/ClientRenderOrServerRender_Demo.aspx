<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientRenderOrServerRender_Demo.aspx.cs"
    Inherits="Tester_ZYH.NET_V.Demo.ClientRenderOrServerRender_Demo" %>

<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function Button1_onclick() {
            var d1 = new Date();
            var T = $('<table>');
            for (var i = 0; i < 10000; i++) {
                T.append($('<tr>').append($('<td>').width(100).height(25).css("border", "solid red 1px").html(i.toString())));
            }
            $('#div').append(T);
            var d2 = new Date();
            var x = d2 - d1;
            $('#TimeSpent1').html(x.toString());
        }

        function Button2_onclick() {
            var d1 = new Date();
            var s = '<table>';
            for (var i = 0; i < 10000; i++) {
                s += '<tr><td style="height:25px;width:100px;border:solid red 1px;">' + i + '</td></tr>';
            }
            s += '</table>'
            $('#div').html(s);
            var d2 = new Date();
            var x = d2 - d1;
            $('#TimeSpent2').html(x.toString());

        }

        var ServerRenderStart;
        var ServerRenderEnd;
        function lk_b(id, e) {
            ServerRenderStart = new Date();
        }
        function lk_a(id, e) {
            $('#div').append(e.ArgToClient);
            ServerRenderEnd = new Date();
            var x = ServerRenderEnd - ServerRenderStart;
            $('#TimeSpent3').html(x.toString());
        }

        function Button3_onclick() {
            $('#div').html('');
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:CallbackManager ID="CallbackManager1" runat="server" CallWaitingTimeOut="90">
        </cc1:CallbackManager>
        <asp:Label ID="Label1" runat="server" Text="Using Client Side Render or Server Side Render"
            Font-Bold="True" Font-Size="XX-Large"></asp:Label>
        <br />
        <br />
        This page will demonstrate client side and server side rendering performance difference
        through test, for discussion advantage between using client side and server side
        rendering under different conditions. The server side rendering performance is constantly
        similar during many years technology improvement. Firebug shows that total time
        spend on sever side between 70~200 milliseconds (for test below between 2008~2013).
        But the client side rendering performance is very different depending on CPU, browser
        types, and browser versions. Using DOM tech, IE6, Q8400 CPU, it takes over 30 seconds.
        IE10, i7-2600 CPU, it is 7~14 seconds. Chrome26, i7-2600 CPU, it spent 1~2 seconds.
        Champion is that, client side js generates html using text composition, FireFox
        20, i7-2600 CPU, it takes only 35 milliseconds. But the same thing takes 7 seconds
        using IE10.
        <br />
        <br />
        <b>Client side rendering has advantage on</b>
        <br />
        &nbsp;&nbsp;&nbsp; • When network band width is limited
        <br />
        &nbsp;&nbsp;&nbsp; • Smaller area to be render
        <br />
        <b>Server side rendering has advantage on</b>
        <br />
        &nbsp;&nbsp;&nbsp; • When using fast network
        <br />
        &nbsp;&nbsp;&nbsp; • Larger size and complicated UI to be render
        <br />
        &nbsp;&nbsp;&nbsp; • Client side CPU is slow
        <br />
        &nbsp;&nbsp;&nbsp; • OOP html generation and corresponding javascript auto-registration.
        (less restriction for architecture design)
        <br />
        &nbsp;&nbsp;&nbsp; • View logic on server side. (Do not need to transfer to client.
        Lower development and maintenance cost)
        <br />
        <br />
        <b>Using this UI framework, developers are free to choose server side render or 
        client side render on each action.</b>
        <br />
    </div>
    <table style="width: 100%;">
        <tr>
            <td colspan="3" width="33%">
                Following tests will create 1000 (table) rows and one cells in each rows.
                <input id="Button3" type="button" value="clear test result table" onclick="return Button3_onclick()" />
            </td>
        </tr>
        <tr>
            <td width="33%">
                <input id="Button1" type="button" value="Client side render using DMO" onclick="return Button1_onclick()" />
            </td>
            <td width="33%">
                <input id="Button2" type="button" value="Client side render using text HTML" 
                    onclick="return Button2_onclick()" />
            </td>
            <td width="33%">
                <cc1:LinkButton ID="LinkButton1" runat="server" ClientEvent_AfterCallback="lk_a"
                    ClientEvent_BeforeCallback="lk_b" OnClick="LinkButton1_Click">Server render</cc1:LinkButton>
            </td>
        </tr>
        <tr>
            <td width="33%">
                <span id="TimeSpent1"></span>
            </td>
            <td width="33%">
                <span id="TimeSpent2"></span>
            </td>
            <td>
                <span id="TimeSpent3"></span>
            </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
    <div id="div">
    </div>
    </form>
</body>
</html>
