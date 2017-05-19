<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Tester_ZYH.NET_V.T.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function Fun(a, b) {
            if (a) {
                alert(a.toString());
            }
        }

        function Fun_b(a, b) {
            alert('fun_b called.');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="javascript:Fun();">click no p</a>
            <a href="javascript:Fun(null);">click null</a>
            <a href="javascript:Fun(false);">click false</a>
            <a href="javascript:Fun(0);">click 0</a>
            <a href="javascript:Fun(1);">click 1</a>
            <a href="javascript:Fun(Fun_b);">click Fun_b</a>
        </div>
    </form>
</body>
</html>
