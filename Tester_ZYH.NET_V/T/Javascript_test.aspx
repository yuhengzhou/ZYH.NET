<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Javascript_test.aspx.cs" Inherits="Tester_ZYH.NET_V.T.Javascript_test" %>

<!DOCTYPE html>
555555
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var scope = "I am global";
        function whatismyscope() {
            var scope = "I am just a local";
            function func() {
                console.log(scope);
                console.log(window.scope);
            }
            return func;
        }
        whatismyscope()()

    </script>
<%--    <script type="text/javascript">
        var b = typeof a === 'undefined';
        var b1 = a1 === undefined;
        var b2 = a1 === void (0);
        var c = 2;

        document.write("This is \
\
a program");

        var s = 3 + 2 + '7';
        var n = navigator;

        var t = prompt('hi', 'z');
        var c =
            1;
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
