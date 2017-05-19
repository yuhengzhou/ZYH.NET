<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpeechBubble.aspx.cs" Inherits="Tester_ZYH.NET_V.css.SpeechBubble" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        p.thought {
            position: relative;
            width: 130px;
            height: 100px;
            text-align: center;
            line-height: 100px;
            background-color: #fff;
            border: 8px solid #666;
            -webkit-border-radius: 58px;
            -moz-border-radius: 58px;
            border-radius: 58px;
            -webkit-box-shadow: 2px 2px 4px #888;
            -moz-box-shadow: 2px 2px 4px #888;
            box-shadow: 2px 2px 4px #888;
        }

            p.thought:before, p.thought:after {
                left: 10px;
                top: 70px;
                width: 40px;
                height: 40px;
                background-color: #fff;
                border: 8px solid #666;
                -webkit-border-radius: 28px;
                -moz-border-radius: 28px;
                border-radius: 28px;
            }

            p.thought:after {
                width: 20px;
                height: 20px;
                left: 5px;
                top: 100px;
                -webkit-border-radius: 18px;
                -moz-border-radius: 18px;
                border-radius: 18px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <div>
            <p class="thought">
                Hi Z!
            </p>
        </div>
    </form>
</body>
</html>
