<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AsyncCallOptions_Demo.aspx.cs"
    Inherits="Tester_ZYH.NET_V.Demo.AsyncCallOptions_Demo" %>

<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var CallIndex = 0;

        function a(id, e) {
            $('#result').append('<br/>' + e.ArgToClient);
        }

        function b(id, e) {
            CallIndex++;
            e.ArgFromClient = CallIndex;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:CallbackManager ID="CallbackManager1" runat="server" CallWaitingTimeOut="90">
    </cc1:CallbackManager>
    <asp:Label ID="Label1" runat="server" Text="AsyncCallOptions" Font-Bold="True" 
        Font-Size="XX-Large"></asp:Label>
    <br />
    <br />
    <asp:Image ID="Image4" runat="server" 
        ImageUrl="~/Demo/Img/AsyncCallOptions_Demo/Location.png" />
    <br />
    <br />
    Please open this page using FireFox and active FireBug, swich to "Net" tab in FireBug. click links below continuously to see behaviors for AsyncCallOptions. Each call will sleep 3 seconds on server side event handlers.
    <br />
    <br />
    <br />
    <cc1:LinkButton ID="LinkButton_BlockLaterCalls" runat="server" 
        AsyncCallOption="BlockLaterCalls" onclick="LinkButton_BlockLaterCalls_Click">Block Later Calls</cc1:LinkButton>
    <br />
    During the request is pending status (after request sent, before result returned),
    the control will ignore user input.<br />
    <br />
    <cc1:LinkButton ID="LinkButton_AbortPrevios" runat="server" AsyncCallOption="AbortPreviousCalls"
        OnClick="LinkButton_AbortPrevios_Click" 
        Text="Abort Previos call (default)"></cc1:LinkButton>
    <br />
    All &quot;CanBeAborted = true&quot; previous requests will be aborted (including
    queued calls and pending calls) before the request send.<br />
    <asp:Image ID="Image1" runat="server" 
        ImageUrl="~/Demo/Img/AsyncCallOptions_Demo/AbortPreviousCall.png" />
    <br />
    <br />
    <cc1:LinkButton ID="LinkButton_QueueCalls" runat="server" 
        AsyncCallOption="QueueCalls" OnClick="LinkButton_QueueCalls_Click"
        Text="QueueCalls"></cc1:LinkButton>
    <br />
    Multiple requests will be queued on client side and they will be sent one by one
    after each previous call result returned.<br />
    <asp:Image ID="Image2" runat="server" 
        ImageUrl="~/Demo/Img/AsyncCallOptions_Demo/QueuedCalls.png" />
    <br />
    <cc1:LinkButton ID="LinkButton_MultipleCalls" runat="server" 
        AsyncCallOption="MultipleCalls" OnClick="LinkButton_MultipleCalls_Click"
        Text="Multiple Calls" ClientEvent_AfterCallback="a" 
        ClientEvent_BeforeCallback="b"></cc1:LinkButton>
    &nbsp;<br />
    Multiple requests will send to server immediately without waiting for previous 
    calls result returned. Requests will be processed on server in parallel. But 
    server multiple processing threads will executed in lock mode if any server side 
    code accesses Session object. Such as Page.Session and Global.ascx.cs <span style="color: #00AA00">"protected void Session_Start(object
        sender, EventArgs e)"</span>&nbsp; and <span style="color: #00AA00">"protected void
            Session_End(object sender, EventArgs e)"</span><br />
    <asp:Image ID="Image3" runat="server" 
        ImageUrl="~/Demo/Img/AsyncCallOptions_Demo/MultipleCalls.png" />
    <br />
    <cc1:LinkButton ID="LinkButton_QueuedCallsCannotBeAborted" runat="server" AsyncCallOption="QueueCalls"
        CanBeAborted="False" OnClick="LinkButton_QueuedCallsCannotBeAborted_Click">Queue Calls (Cannot be aborted)</cc1:LinkButton>
    <br />
    Set CanBeAborted property to false, requests sent by this control will not be 
    aborted by other later AsyncCallOption=&quot;AbortPreviousCalls&quot; requests. Request 
    sent by this control and other later requests will become MultipleCalls or QueueCalls mode depend on setting and
    timing.
    <br />
    <div id="result" style="position: absolute; background-color: White;">
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    </form>
</body>
</html>
