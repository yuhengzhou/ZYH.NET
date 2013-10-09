<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tester_ZYH.NET_V.Demo.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Main.css" rel="stylesheet" type="text/css" />

    <style type="text/css">

 p.MsoNormal
	{margin-bottom:.0001pt;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	        margin-left: 0in;
            margin-right: 0in;
            margin-top: 0in;
        }
p.MsoListParagraph
	{margin-top:0in;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:.5in;
	margin-bottom:.0001pt;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-size: xx-large">
    
        Introduction<br />
        <br />
        <p class="MsoNormal">
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;
color:#1F497D">This UI framework provides following capabilities.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">1.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            Supports all browsers and mobile device.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">2.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            <a href="DynamicLoading_Demo.aspx">Incremental dynamic loading.</a><o:p></o:p></span></p>
        <p class="MsoListParagraph" style="margin-left:1.0in;text-indent:-.25in;
mso-list:l0 level2 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-bidi-font-family:
Calibri;color:#1F497D"><span style="mso-list:Ignore">a.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;
color:#1F497D">Whole web app can be built in one page without initial loading pressure. 
            Resources (HTML, Js and Css) will be automatically loaded to client only before 
            they will be used.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="margin-left:1.0in;text-indent:-.25in;
mso-list:l0 level2 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-bidi-font-family:
Calibri;color:#1F497D"><span style="mso-list:Ignore">b.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;
color:#1F497D">Any piece of UI part can be loaded into any position of any page.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="margin-left:1.0in;text-indent:-.25in;
mso-list:l0 level2 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-bidi-font-family:
Calibri;color:#1F497D"><span style="mso-list:Ignore">c.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;
color:#1F497D">A same piece of UI part can be used for both desktop version app and mobile 
            version app.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="margin-left:1.0in;text-indent:-.25in;
mso-list:l0 level2 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-bidi-font-family:
Calibri;color:#1F497D"><span style="mso-list:Ignore">d.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;
color:#1F497D">Any piece of UI part can be provided as UI service to 3<sup>rd</sup> party.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">3.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            Automatic resource (Js and Css) registration management.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="margin-left:1.0in;text-indent:-.25in;
mso-list:l0 level2 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-bidi-font-family:
Calibri;color:#1F497D"><span style="mso-list:Ignore">a.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;
color:#1F497D">Will not register same piece of resource repeatedly.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">4.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            Automatic state persistence.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">5.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            Error handling.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">6.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            <a href="AsyncCallOptions_Demo.aspx">Handle multiple asynchronous calls conflict.</a><o:p></o:p></span></p>
        <p class="MsoListParagraph" style="margin-left:1.0in;text-indent:-.25in;
mso-list:l0 level2 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-bidi-font-family:
Calibri;color:#1F497D"><span style="mso-list:Ignore">a.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;
color:#1F497D">Options including, AbortPreviousCalls, BlockLaterCalls, QueueCalls, 
            MultipleCalls, and CanBeAborted.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">7.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            Maintain pending call status.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">8.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            Maintain browser history in AJAX mode.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">9.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            Provide client event and server event.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">10.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp; </span></span>
            </span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            Developer is able to choose client side render or server side render.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">11.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp; </span></span>
            </span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            Support powerful UED without compromise. <o:p></o:p></span>
        </p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">12.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp; </span></span>
            </span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            Typical user action completion time spend is 200ms. (under 7Mbps home internet 
            condition)<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">13.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp; </span></span>
            </span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            OOP UI rendering.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="margin-left:1.0in;text-indent:-.25in;
mso-list:l0 level2 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-bidi-font-family:
Calibri;color:#1F497D"><span style="mso-list:Ignore">a.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;
color:#1F497D">Able to render any part of an UI object.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="margin-left:1.0in;text-indent:-.25in;
mso-list:l0 level2 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-bidi-font-family:
Calibri;color:#1F497D"><span style="mso-list:Ignore">b.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;
color:#1F497D">Automatically register required resources when render.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="margin-left:1.0in;text-indent:-.25in;
mso-list:l0 level2 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-bidi-font-family:
Calibri;color:#1F497D"><span style="mso-list:Ignore">c.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </span></span></span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;
color:#1F497D">Can handle complicated UI situation without special treatment. For example, a 
            piece of UI loads itself (contains itself) repeatedly.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">14.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp; </span></span>
            </span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            Automatically collect all or partial user input data on post call.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">15.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp; </span></span>
            </span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            Code is shorter than using other solution (not including auto-generated code).<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">16.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp; </span></span>
            </span><![endif]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">
            Compiler error check normally covers 90% of total code.<o:p></o:p></span></p>
        <p class="MsoListParagraph" style="text-indent:-.25in;mso-list:l0 level1 lfo1">
            <![if !supportLists]>
            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-bidi-font-family:Calibri;color:#1F497D"><span style="mso-list:Ignore">17.<span 
                style="font:7.0pt &quot;Times New Roman&quot;">&nbsp;&nbsp;&nbsp;</span></span></span><![endif]><span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D">Directly call static method (web service).<o:p></o:p></span></p>
    </div>
    </form>
</body>
</html>
