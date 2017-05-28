<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calendar_Test.aspx.cs" Inherits="Tester_ZYH.NET_V.Test_V.Calendar_Test" %>

<%@ Register assembly="ZYH.WebControl_V" namespace="ZYH.WebControl_V" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <cc1:CallbackManager ID="CallbackManager1" runat="server">
        </cc1:CallbackManager>
        <br />
        <br />
        <cc1:Calendar ID="Calendar1" runat="server" 
            ondatechanged="Calendar1_DateChanged" Theme="PepperGrinder" Value="2013-03-29" />
    
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Postback" />
    
    </div>
    </form>
</body>
</html>
