<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="beat_Angular.aspx.cs" Inherits="Tester_ZYH.NET_V.T.beat_Angular" %>

<%@ Register assembly="ZYH.WebControl_V" namespace="ZYH.WebControl_V" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <cc1:CallbackManager ID="CallbackManager1" runat="server">
        </cc1:CallbackManager>
    
    </div>
        <cc1:TextBox ID="tb1" runat="server" ClientEvent_BeforeCallback_KeyUp="(function(){return function(id,e){$('#s1').html(e.Text);}})()" />
        <br/>
        <span id="s1"></span>
    </form>
</body>
</html>
