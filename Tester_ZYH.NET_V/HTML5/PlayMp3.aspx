<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayMp3.aspx.cs" Inherits="Tester_ZYH.NET_V.HTML5.PlayMp3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var a = new Audio('阿兰 - 霸道天下.mp3');
        function Play() {
            a.play();
            a.onended = function () { alert('done'); };
        }
        function Stop() {
            a.pause();
            a = null;
        }
        function FF() {
            a.currentTime += 30;
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="javascript:Play();">play mp3</a>
            <br />
            <a href="javascript:Stop();">stop mp3</a>
            <br />
            <a href="javascript:FF();">Fast Forward</a>
        </div>
    </form>
</body>
</html>
