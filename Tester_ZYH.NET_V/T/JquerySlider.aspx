<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JquerySlider.aspx.cs" Inherits="Tester_ZYH.NET_V.T.JquerySlider" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>jQuery UI Slider - Default functionality</title>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script>
        $(function () { $("#slider").slider(); }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="slider">
        </div>
    </div>
    </form>
</body>
</html>
