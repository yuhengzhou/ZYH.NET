<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="js_test.aspx.cs" Inherits="Tester_ZYH.NET_V.T.js_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        "use strict"
        fa();

        (function () {
            alert('started.')
            var a = b = 3;
        })();

        function fa() {
            b1 = 5;
            var a1 = 5;
        }

        console.log("a defined? " + (typeof a !== 'undefined'));
        console.log("b defined? " + (typeof b !== 'undefined'));


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
