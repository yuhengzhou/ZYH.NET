<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AMD.aspx.cs" Inherits="Tester_ZYH.NET_V.T.AMD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/require.js"></script>
    <script type="text/javascript">

        function a_click() {
            require(["AMD2"], function (adm2) {
                adm2.log();
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="Javascript:a_click();">aaa</a>
        </div>
    </form>
</body>
</html>
