<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Tester_ZYH.NET_V.T.WebForm1" %>

<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #xxx
        {
            height: 211px;
            width: 429px;
        }
    </style>
    <script type="text/javascript">
        var a1;
        var a2 = 3;
        var b=a1+a2;


        document.writeln(b);
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    <iframe id="xxx" height="400" src="../Demo/Default.aspx" width="600"></iframe>
        <asp:HiddenField ID="HiddenField1" runat="server" Value="5" />
    </form>
</body>
</html>
