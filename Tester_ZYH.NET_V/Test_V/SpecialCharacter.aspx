<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecialCharacter.aspx.cs" Inherits="Tester_ZYH.NET_V.Test_V.SpecialCharacter" %>

<%@ Register assembly="ZYH.WebControl_V" namespace="ZYH.WebControl_V" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" >

        function b(id, e) { e.ArgFromClient = $('#TextBox1').val(); }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <cc1:CallbackManager ID="CallbackManager1" runat="server">
        </cc1:CallbackManager>
        <br />
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="255px" TextMode="MultiLine" 
            Width="636px">&lt;script&gt;
&amp; ; &lt; ? &gt; , ‘ “ |  ]]&gt;
ເອົາໃຈໃສ່ປຸ່ມເຊັ່ນດຽວກັນກັບບາງເວັບໄຊທ໌ກ່ຽວກັບທ່ານເພື່ອປະກອບຜູ້ໃຊ້ຂອງທ່ານ. ລາຍລະອຽດສາມາດພົບໄດ້ທີ່ນີ້.
ನಿಮ್ಮ ಬಳಕೆದಾರರು ತೊಡಗಿಸಿಕೊಳ್ಳಲು ನಿಮ್ಮ ವೆಬ್ಸೈಟ್ನಲ್ಲಿ ಕೆಲವು ಲೈಕ್ ಬಟನ್ ಇರಿಸಿ. ವಿವರಗಳನ್ನು ಇಲ್ಲಿ ಕಾಣಬಹುದು.
有些人喜歡按鈕放在您的網站上進行用戶。詳細信息可以在這裡找到。
ใส่ปุ่มเช่นบางบนเว็บไซต์ของคุณเพื่อดึงดูดผู้ใช้ของคุณ รายละเอียดสามารถพบได้ที่นี่
Đặt một số nút Like trên trang web của bạn để thu hút người dùng của bạn. Thông tin chi tiết có thể được tìm thấy ở đây.
وضع بعض الأزرار مثل على موقع الويب الخاص بك لإشراك المستخدمين. ويمكن الاطلاع على التفاصيل هنا.
Daj nekaj podobnega gumbe na vaši spletni strani, da se vključijo uporabnikom. Podrobnosti najdete tukaj.
Doe wat Net als knoppen op uw website naar uw gebruikers in te schakelen. Details kunt u hier vinden.
Ponga algunos botones como en su página web para contratar a sus usuarios. Los detalles se pueden encontrar aquí.
તમારી વેબસાઇટ પર કેટલાક જેમ બટનો મૂકો તમારા વપરાશકર્તાઓ સંલગ્ન થાય છે. વિગતો અહીં મળી શકે છે.
Coloque alguns botões como em seu site para engajar seus usuários. Detalhes podem ser encontrados aqui.
あなたのユーザーを魅了するためにあなたのウェブサイト上のいくつかのようにボタンを配置します。詳細はここで見つけることができます。
사용자 참여를 유도하기 위해 웹 사이트의 일부처럼 버튼을 놔. 자세한 내용은 여기에서 찾아보실 수 있습니다.
Покладіть деякі, як кнопки на вашому сайті, щоб займатися своїм користувачам. Подробиці можна знайти тут.
</asp:TextBox>
        <br />
        <br />
        <cc1:LinkButton ID="LinkButton1" runat="server" ClientEvent_BeforeCallback="b" 
            onclick="LinkButton1_Click"></cc1:LinkButton>
    
    </div>
    </form>
</body>
</html>
