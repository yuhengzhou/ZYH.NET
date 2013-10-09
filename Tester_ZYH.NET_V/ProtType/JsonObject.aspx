<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JsonObject.aspx.cs" Inherits="Prottype_JsonObject" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <%--<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.6.2.min.js" type="text/javascript"></script>--%>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.6.2-vsdoc.js" type="text/javascript"></script>
    <%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.14/jquery-ui.js" type="text/javascript"></script>--%>
    <%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.14/jquery-ui.min.js" type="text/javascript"></script>--%>
    <script src="../JQ_Plug/jquery.json-2.2.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
// <![CDATA[


        function Button2_onclick() {
            var s = '{"ID":"c1","Text":"mmx","Expire":"\/Date(1310771112377)\/","IsSomething":true,"Cost":5.1,"Scripts":[{"Key":"sss","IsValid":true,"Type":1},{"Key":"56","IsValid":false,"Type":3}]}';
            //            var o = eval("(" + s + ")");
            var o = $.parseJSON(s);
            var i = parseInt(o.Expire.substring(6, o.Expire.length - 2));
            var d = new Date(i);
            var txt = d.toLocaleString();
            var D = new Date(txt);
            var ts = D.valueOf();

            var oo = $.evalJSON(s);
            var ss = $.toJSON(oo);

        }

        function Button3_onclick() {
            var d = parseInt(new Date().valueOf());
            alert(d);

            var d2 = new Date(d);
            alert(d2.toString());
        }

// ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Server Side" />
        &nbsp;<asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Server Side (JavaScriptSerializer)" />
    </div>
    </form>
    <p>
        <input id="Button2" type="button" value="Client Side" onclick="return Button2_onclick()" /></p>
    <p>
        <input id="Button3" type="button" value="TimeStamp" onclick="return Button3_onclick()" /></p>
</body>
</html>
