<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpeechBubble2.aspx.cs" Inherits="Tester_ZYH.NET_V.css.SpeechBubble2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .chat {
            width: 400px;
        }

        .bubble {
            background-color: #F2F2F2;
            border-radius: 5px;
            box-shadow: 0 0 6px #B2B2B2;
            display: inline-block;
            padding: 10px 18px;
            position: relative;
            vertical-align: top;
        }

            .bubble::before {
                background-color: #F2F2F2;
                content: "\00a0";
                display: block;
                height: 16px;
                position: absolute;
                top: 11px;
                transform: rotate( 29deg ) skew( -35deg );
                -moz-transform: rotate( 29deg ) skew( -35deg );
                -ms-transform: rotate( 29deg ) skew( -35deg );
                -o-transform: rotate( 29deg ) skew( -35deg );
                -webkit-transform: rotate( 29deg ) skew( -35deg );
                width: 20px;
            }

        .me {
            float: left;
            margin: 5px 45px 5px 20px;
        }

            .me::before {
                box-shadow: -2px 2px 2px 0 rgba( 178, 178, 178, .4 );
                left: -9px;
            }

        .you {
            float: right;
            margin: 5px 20px 5px 45px;
        }

            .you::before {
                box-shadow: 2px -2px 2px 0 rgba( 178, 178, 178, .4 );
                right: -9px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="chat">
                <div class="bubble me">Hello there!</div>
                <div class="bubble you">Hi. I'm an expandeable chat box with box shadow. How are you? I expand horizontally and vertically, as you can see here.</div>
                <div class="bubble me">Awesome.</div>
            </div>
        </div>
    </form>
</body>
</html>
