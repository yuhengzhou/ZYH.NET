<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExceptionHandler_Test.aspx.cs"
    Inherits="Tester_ZYH.NET_V.Test_V.ExceptionHandler_Test" %>

<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function timeOutHandl(id) {

        }
        function OnError(SenderClientID, Status, ErrorThrown, Context) {

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:CallbackManager ID="CallbackManager1" runat="server" CallWaitingTimeOut="33"
            ClientEvent_OnCallTimeOut="timeOutHandl" 
            ServerSideExceptionAction="DebugDetail" ClientEvent_OnException="OnError" 
            onexceptionactions="ClientEvent">
        </cc1:CallbackManager>
    </div>
    <br />
    
        <cc1:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">ClientEvent_OnCallTimeOut</cc1:LinkButton>
    </p>
    
        <cc1:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Error in Event handler</cc1:LinkButton>
    </p>
    
        <cc1:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">Error in PageLoad</cc1:LinkButton>
    </p>
    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click">Error in Postback</asp:LinkButton>
    <br />
    <br />
    <cc1:LinkButton ID="LinkButton_SessionTimeout" runat="server" 
        onclick="LinkButton_SessionTimeout_Click">Simulate Session Timeout</cc1:LinkButton>
    <br />
    <br />
    <asp:PlaceHolder ID="PlaceHolder_ExceptionEventTrigerControlNotLoaded" 
        runat="server"></asp:PlaceHolder>
    
        <asp:PlaceHolder ID="PlaceHolder_ExceptionEventTrigerControlNotLoaded_postback" 
            runat="server"></asp:PlaceHolder>
    </p>
    </form>
</body>
</html>
