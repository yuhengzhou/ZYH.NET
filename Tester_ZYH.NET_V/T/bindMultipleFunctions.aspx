<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bindMultipleFunctions.aspx.cs" Inherits="Tester_ZYH.NET_V.T.bindMultipleFunctions" %>

<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>

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

            <p>
            </p>
            <input id="Text1" type="text" />
        </div>

        <script type="text/javascript">
            function a() {
                alert('a');
            }
            function b() {
                alert('b');
            }
            var txt = $('#Text1');
            txt.on('focus', a);
            txt.on('focus', b);
        </script>
    </form>
</body>
</html>
