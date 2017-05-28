<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tester_ZYH.NET_V.Demo.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Main.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .frame {
            padding: 15px;
        }

        .firstRow {
            /*line-height:250%;*/
            padding-bottom: 8px;
        }

        .txt {
            font: 400 14px arial;
            color: #18308e;
            line-height: 150%;
            margin-left: 20px;
	}

        .feed {
            margin-left: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="frame">
            <h2>ZYH.NET_V UI Framework Introduction</h2>
            <div class="txt">
                <div class="firstRow">
                    This is a powerful UI framework capable to build large scale single page web application with unique OOP UI generation advantages. Enable to build fancy Ajax page quickly. It includes a client side javascript library and a server side C# control library and provides following capabilities.
                </div>
                1. Works in all major browsers and mobile devices.<br />
                2. Support large scale single page web application development.plication development.<br />
                <div class="feed">
                    a. 
                    One or multiple UI objects can be loaded in one AJAX call. Multiple page areas can be rendered / updated in one ajax call. Capable render and update any part(s) of an UI object.<br />
                    b. <a href="DynamicLoading_Demo.aspx">Incremental dynamic loading</a>. A large scale web application can be built on a single page without initial loading pressure. Resources in a UI object (HTML, javascript, image, and CSS) can be incrementally loaded later depend on user&#39;s operation. When a fUI object is first time requested to be present on the client side by an AJAX call, it will automatically register all required resources to the client via the same AJAX call when the UI object is rendered and sent to the client.<br />
                    b. Automatic resource registration management. Same resource will be only register once.<br />
                    c. <a href="AsyncCallOptions_Demo.aspx">Semi-automatic handle multiple asynchronous calls conflict.</a> Options: AbortPreviousCalls, BlockLaterCalls, QueueCalls, MultipleCalls, and CanBeAborted.<br />
                    d. Developer is free to choose client side render or server side render.<br />
                </div>
                3. Highly abstracted object oriented UI rendering, build powerful UED without compromise using very little code.
                <div class="feed">
                    a. Object oriented UI elements can be 100% self-contain, automatically register required resources to client side.<br />
                    b. Any AJAX control can be added into the frame work.<br />
                    c. Any piece of UI part can be loaded into any position of any page.<br />
                    d. A same piece of UI part can be used for both desktop browser and mobile browser.<br />
                    e. Any piece of UI part can be provided as UI service to 3rd party.<br />
                    f. Can handle complicated UI loading combination natrually. For example, a piece of UI loads itself (contains itself repeatedly).<br />
                </div>
                4. Maintain browser history in AJAX mode. Browser Back button and Add to Favorite can work 
                correctly with the AJAX page.<br />
                5. Automatic states persistence. Or can be set to Off for state less web application<br />
                5. Error handling.<br />
                <div class="feed">
                    a. Built in general error handler function, shows friendly general error message in a popup layer.<br />
                    b. Built in debug mode error handler function, shows detail of server side error message in a browser new tab.<br />
                    c. Custom error event, allows developer to write custom javascript functions to handle specified error types.<br />
                </div>7. Maintain pending call status, including multiple pending calls. Developer can choose pending call behaviors. The Default behaviors is that show an waiting icon to user. At same time, framework will automatically coordinate all related functions working correctly. Such as AbortPreviousCalls, BlockLaterCalls, QueueCalls, MultipleCalls, and CanBeAborted calls.<br />
                9. Provide client (before and after call server) events and server events. Developer can easily add delegate functions to handle every detail of an user action.<br />
                12. High performance, minimize bandwidth useage, a typical user action can complet in 100ms. (under 7Mbps home internet connection)<br />
                14. Automatically collect all or partial user input data into a Post method AJAX call. By set PostDataContainer option, Framework will automatically collect values in &quot;Input&quot; tags in those specified areas and includ these values into a Post method AJAX call. All posted values will automatically restored on server side into a corresponding control if that &quot;Input&quot; tag was generated be the server control.<br />
                16. Compiler error check.<br />
                17. Directly call static page method (equivalent to call web service).<br />
            </div>
    </div>
    </form>
</body>
</html>
