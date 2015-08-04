<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LinkButton_Test.aspx.cs" Inherits="Tester_ZYH.NET_V.Test_V.LinkButton_Test" %>

<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function a(id, e) {

        }

        function b(id, e) {

        }
    </script>
    <link href="../css/base.css" rel="stylesheet" type="text/css" />
    <link href="../css/quickSetup.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:CallbackManager ID="CallbackManager1" runat="server">
        </cc1:CallbackManager>
    </div>
    <cc1:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" UpdateViewState="True"
        CommandArg="xxx" UpdateControlState="True" ClientIDMode="Static"></cc1:LinkButton>
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    <br />
    <cc1:LinkButton ID="LinkButton2" runat="server" ClientEvent_AfterCallback="a" ClientEvent_BeforeCallback="b"
        OnClick="LinkButton2_Click" OnRestoreState="LinkButton2_RestoreState" OnSaveState="LinkButton2_SaveState"
        AsyncCallOption="AbortPreviousCalls" CommandArg="XxX">Before/After Call</cc1:LinkButton>
    <br />
    <br />
    <cc1:LinkButton ID="LinkButton6" runat="server" 
        AsyncCallOption="AbortPreviousCalls" onclick="LinkButton6_Click">Abort Previous Calls</cc1:LinkButton>
    <br />
    <br />
    <cc1:LinkButton ID="LinkButton7" runat="server" AsyncCallOption="MultipleCalls" 
        onclick="LinkButton7_Click">Mulltiple Calls</cc1:LinkButton>
    <br />
    <br />
    <br />
    <cc1:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" 
        UpdateControlSettings="True" UpdateControlState="True" UpdateViewState="True">Register Js</cc1:LinkButton>
    <br />
    <br />
    <br />
    <br />
    inner HTML
    <div class="wrap-step">
        <div class="step-nav ">
            <ul class="group">
                <li class="step1">
                    <cc1:LinkButton ID="LinkButton4" runat="server" AsyncCallOption="QueueCalls" OnClick="LinkButton4_Click"><span class="pointer pointerRightTop"><span class="pointerInner"></span></span><span class="step-number select">1</span> <span class="step-text"><em>Edit Your</em><br/> Branding</span>
                    </cc1:LinkButton>
    <asp:LinkButton ID="LinkButton5" runat="server" EnableViewState="False"><span class="pointer pointerRightTop"><span class="pointerInner"></span></span><span class="step-number select">1</span> <span class="step-text"><em>Edit Your</em><br/> Branding</span>
    </asp:LinkButton>
                </li>
            </ul>
        </div>
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    </form>
</body>
</html>
