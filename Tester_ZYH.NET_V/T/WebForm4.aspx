<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="Tester_ZYH.NET_V.T.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .MicTestHeader {
            height: 200px;
            line-height: 200px;
            font-size: 200%;
            font-weight: bold;
            font-family: Arial;
            text-align: center;
        }

        div {
            height: 200px;
            line-height: 200px;
            text-align: center;
            border: 2px dashed #f69c55;
        }

        span {
            display: inline-block;
            vertical-align: middle;
            line-height: normal;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        Volume:
        <input type='range' id='volume' min='0' max='1' value='1' step='0.01' />
        <div class="MicTestHeader">Test Your Microphone</div>
        <div>
            <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Haec et tu ita posuisti, et verba vestra sunt. Non enim iam stirpis bonum quaeret, sed animalis. </span>
        </div>
    <script type="text/javascript">
        //var recognition = new webkitSpeechRecognition();
        //recognition.start();

        if (!navigator.getUserMedia) {
            navigator.getUserMedia = navigator.webkitGetUserMedia ||
                navigator.mozGetUserMedia;
        }
        navigator.getUserMedia({
            audio: true
        }, function (stream) {
            var ctx = new AudioContext();
            var source = ctx.createMediaStreamSource(stream);
            var dest = ctx.createMediaStreamDestination();
            var gainNode = ctx.createGain();

            source.connect(gainNode);
            gainNode.connect(dest);
            document.getElementById('volume').onchange = function () {
                gainNode.gain.value = this.value; // Any number between 0 and 1.
            };
            gainNode.gain.value = document.getElementById('volume').value;

            // Example: play the audio
            // Or if you use WebRTC, use peerConnection.addStream(dest.stream);
            new Audio(URL.createObjectURL(dest.stream)).play();

            // Store the source and destination in a global variable
            // to avoid losing the audio to garbage collection.
            window.leakMyAudioNodes = [source, dest];
        }, function (e) {
            console.log(e); // TODO: Handle error.
        });

        // For the demo only:
        document.getElementById('volume').onchange = function () {
            alert('Please provide access to the microhone before using this.');
        };
    </script>
    </form>
</body>
</html>
