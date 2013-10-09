<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JsRegularExpression.aspx.cs" Inherits="Tester_ZYH.NET_V.T.JsRegularExpression" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
// <![CDATA[

        function Button1_onclick() {
            var x = "Joe's dog and Jin's baby."
            var r = x.replace(/'/g, "''")
        }

// ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <input id="Button1" type="button" value="button" onclick="return Button1_onclick()" /></div>
    </form>
</body>
</html>
