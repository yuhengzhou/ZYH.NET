<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Closure.aspx.cs" Inherits="Tester_ZYH.NET_V.T.Cloosure" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var addTo = function (p) {
            this. add = function (i) {
                return p + i;
            }
        }

        var addThree = new addTo(3);
        var x = addThree.add(2);


        var a = 0;
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
