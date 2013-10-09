<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServerDate.aspx.cs" Inherits="Tester_ZYH.NET_V.ComponentArtTest.ServerDate" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function updateServerDateTime(param) {
            CallBack1.callback(param);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="DemoArea">
      <ComponentArt:CallBack id="CallBack1" runat="server" Height="16px" 
            oncallback="CallBack1_Callback"  >
        <Content>
          <asp:Label id="lblServerDateTime" runat="server" />
        </Content>
      </ComponentArt:CallBack>
      <br/><br/>
      <b><a href="javascript:updateServerDateTime('date');">Get Server Date</a></b> |
      <b><a href="javascript:updateServerDateTime('weekDay');">Get Server Week Day</a></b> |
      <b><a href="javascript:updateServerDateTime('time');">Get Server Time</a></b>

    </div>

    </div>
    </form>
</body>
</html>
