var ProcessingIcon = document.createElement('img');
var Cover = document.createElement('div');
var MousePosition = { x: 0, y: 0 };
var NumberPendingCalls = 0;
$(document).mousedown(DocumentMouseDown);

function DocumentMouseDown(e) {
    MousePosition.x = e.pageX;
    MousePosition.y = e.pageY;
}

function CallServer(Data, Url, Success, Error, Complete, Context) {
    var p = {
        timeout: 25000,
        data: Data,
        url: Url,
        success: Success,
        beforeSend: SetWaitingStatus
    };
    if (Error) { p.error = Error; } else { p.error = OnError; }
    if (Complete) { p.complete = Complete; } else { p.complete = CancelWaitingStatus; }
    if (Context) { p.context = Context; }
    $.ajax(p);
}

function SetWaitingStatus() {
    NumberPendingCalls++;
    ProcessingIcon.style.zIndex = 9999;
    ProcessingIcon.src = '../../../../Content/Reports/images/preloader.gif';
    ProcessingIcon.border = '0';
    ProcessingIcon.style.position = 'absolute';
    ProcessingIcon.style.backgrandColor = "Transparent";

    $(document).bind('mousemove', MoveProcessingIcon);
    document.body.appendChild(ProcessingIcon);
    MoveProcessingIcon();
}

function CancelWaitingStatus() {
    NumberPendingCalls--;
    if (NumberPendingCalls <= 0) {
        NumberPendingCalls = 0;
        $(document).unbind('mousemove', MoveProcessingIcon);
        try { document.body.removeChild(ProcessingIcon); } catch (err) { }
    }
}

function MoveProcessingIcon(e) {
    if (e) { MousePosition.x = e.pageX; MousePosition.y = e.pageY; }
    ProcessingIcon.style.top = (MousePosition.y + 5) + 'px';
    ProcessingIcon.style.left = (MousePosition.x + 12) + 'px';
}

function ResetZindex(ActivePanel) {
    $('#PlaceHolderChart').css('z-index', '1000');
    $('#PlaceHolderListingDetail').css('z-index', '1000');
    $('#PlaceHolderCommunityLargeChart').css('z-index', '1000');
    $('#PlaceHolderChangeSchedul').css('z-index', '1000');
    $('#PlaceHolderSchoolDetail').css('z-index', '1000');
    $('#PlaceHolderSchoolDistrict').css('z-index', '1000');
    ActivePanel.css('z-index', '1005');
}

function LoadCover() {
    Cover.style.zIndex = 9998;
    Cover.style.top = '0px';
    Cover.style.left = '0px';
    Cover.style.background = '#000000';
    Cover.style.filter = 'alpha(opacity=30)';
    Cover.style.opacity = 0.3;
    Cover.style.align = 'center';
    Cover.style.position = 'absolute';
    ResizeCover();
    $(window).bind('resize', ResizeCover);
    document.body.appendChild(Cover);
}

function UnloadCover() {
    $(window).unbind('resize', ResizeCover);
    try { document.body.removeChild(Cover); } catch (err) { }
}

function ResizeCover() {
    Cover.style.width = $(document).width() + "px";
    Cover.style.height = $(document).height() + "px";
}

function OnError(e) {
    //            alert(e.statusText + ": " + e.status);
    var p = $('#ErrorMessageBox');
    if (p.css('display') == 'none') {
        SetPopupPosition(p);
        p.show();
    }
    $('#ErrorMessageBoxHeader', p).bind('mousedown', OnStartDrag_ErrorMessageBox);
}

function CloseErrorMessageBox(e) {
    var p = $('#ErrorMessageBox');
    $('#ErrorMessageBoxHeader', p).unbind('mousedown', OnStartDrag_ErrorMessageBox);
    p.hide()
}

function LoadMarketSnapshotReport() {
    UnloadMap();
    CallServer({ ReportInstanceId: $('#ReportInstanceId').val() }, "/Reports/MarketSnapshot/GetMarketSnapshotReport", LoadReport_AfterCall);
}

function LoadCommunityReport() {
    UnloadMap();
    CallServer({ ReportInstanceId: $('#ReportInstanceId').val() }, "/Reports/Community/GetCommunityReport", LoadReport_AfterCall);
}

function LoadSchoolReport(Radius) {
    UnloadMap();
    if (Radius)
    { tracker.trackEvent('School List', 'Radius Select', Radius + ' mile radius'); }
    else
    { Radius = 2; }
    CallServer({ ReportInstanceId: $('#ReportInstanceId').val(), Radius: Radius }, "/Reports/School/GetSchoolReport", LoadReport_AfterCall);
}

function LoadReport_AfterCall(Result, Status) {
    var p = $('#divReportContent');
    p.html('');
    p.append(Result);
}

function UnloadMap() {
    if (typeof (MSMap) != "undefined" && MSMap) { try { MSMap.Dispose(); MSMap = null; } catch (ex) { } }
    if (typeof (CommunityMap) != "undefined" && CommunityMap) { try { CommunityMap.Dispose(); CommunityMap = null; } catch (ex) { } }
    if (typeof (SchoolMap) != "undefined" && SchoolMap) { try { SchoolMap.Dispose(); SchoolMap = null; } catch (ex) { } }
}

function CommunityReport_ShowMenu(senderId) {
    var m = $('#CommunityMenu');
    var p = $('#' + senderId).position();
    //            var pc = $('#slides').position();
    m.css('top', p.top);
    m.css('left', p.left);
    //m.mouseenter(function () { m.mouseleave(function () { m.hide(); }) });
    m.mouseleave(function () { m.hide(); });
    m.slideDown(150);
}

var EmailMeSenderId = '';
function LoadEmailMe(senderId, Subject) {
    EmailMeSenderId = senderId;
    CallServer({ Subject: Subject }, "/Reports/Report/GetEmailMe", LoadEmailMe_AfterCall, senderId);
}

function LoadEmailMe_AfterCall(Result, Status) {
    var sender = $('#' + EmailMeSenderId);
    var p = $('#PlaceHolderEmailMe');
    var positioin;
    p.html('');
    p.append(Result);
    switch (EmailMeSenderId) {
        case '':
        case 'BannerEmailMe':
            positioin = sender.position();
            positioin.left -= 250;
            positioin.top += 50;
            break;
        case 'MarketSnapshotEmailMe':
        case 'CommunityEmailMe':
            positioin = sender.position();
            positioin.left -= 380;
            positioin.top -= 200;
            break;
        case 'SchoolEmailMe':
            positioin = sender.position();
            positioin.left -= 400;
            positioin.top += 30;
            break;
        case 'ListingDetailEmailMe':
            positioin = sender.position();
            positioin.left -= 180;
            positioin.top += 100;
            break;
    }
    p.css('top', positioin.top);
    p.css('left', positioin.left);
    p.show();
    $('#EmailMeHeader', p).bind('mousedown', OnStartDrag_EmailMe);
}

function CloseEmailMe() {
    var p = $('#PlaceHolderEmailMe');
    $('#EmailMeHeader', p).unbind('mousedown', OnStartDrag_EmailMe);
    p.hide();
}

function SendEmailMe() {
    var ReportInstanceId = $('#ReportInstanceId').val();
    var Subject = $('#EmailMeSubject').val();
    var Message = $('#EmailMeMessage').val();
    var ReceiveCopy = $('#EmailMeReceiveCopy')[0].checked;
    var cnsmr = $('#cnsmr').val();
    if (Message == '') { $('#EmailMeErrorMsg').text('Message body is required.'); return; } else { $('#EmailMeErrorMsg').text(''); }
    CallServer({ ReportInstanceId: ReportInstanceId, Subject: Subject, Message: Message, ReceiveCopy: ReceiveCopy, cnsmr: cnsmr }, "/Reports/Report/SendEmailMe", SendEmailMe_AfterCall);
}

function SendEmailMe_AfterCall(Result, Status) {
    if (Result == "True") {
        CloseEmailMe();
    }
    else {
        OnError();
    }
}

function LoadCommunityLargeChart(Url, Title) {
    $('#ImgCommunityLargeChart').attr('src', Url);
    $('#largeChartTitle').html(Title);
    var p = $('#PlaceHolderCommunityLargeChart');
    if (p.css('display') == 'none') {
        SetPopupPosition(p);
        p.show();
        p.bind('click', OnClick_CommunityLargeChart);
    }
    $('#CommunityChartHeader', p).bind('mousedown', OnStartDrag_CommunityLargeChart);
    ResetZindex(p);
}

function CloseCommunityLargeChart() {
    var p = $('#PlaceHolderCommunityLargeChart');
    p.hide();
    $('#CommunityChartHeader', p).unbind('mousedown', OnStartDrag_CommunityLargeChart);
    p.unbind('click', OnClick_CommunityLargeChart);
}

function LoadChangeSchedul() {
    var p = $('#PlaceHolderChangeSchedul');
    if (p.css('display') == 'none') {
        var positioin = $('#btnChnageSchedule').position();
        p.css('top', positioin.top - 150);
        p.css('left', positioin.left);
        p.show();
        p.bind('click', OnClick_ChangeSchedul);
    }
    $('#ChangeSchedulHeader', p).bind('mousedown', OnStartDrag_ChangeSchedul);
    ResetZindex(p);
}

function CloseChangeSchedul() {
    var p = $('#PlaceHolderChangeSchedul');
    p.hide();
    $('#ChangeSchedulHeader', p).unbind('mousedown', OnStartDrag_ChangeSchedul);
    p.unbind('click', OnClick_ChangeSchedul);
}

function ChangeSchedule(SelectedValue) {
    var ReportInstanceId = $('#ReportInstanceId').val();
    var cnsmr = $('#cnsmr').val();
    CallServer({ ReportInstanceId: ReportInstanceId, cnsmr: cnsmr, SelectedValue: SelectedValue }, "/Reports/Report/ChangeSchedul", ChangeSchedul_AfterCall);
    CloseChangeSchedul();
    tracker.trackEvent('Market Info', 'Change Schedule Save Button Click', SelectedValue + ' weeks');
}

function ChangeSchedul_AfterCall(Result, Status) {
    if (Result != 'Operation Faild') {
        $('#NextReportDate').html(Result);
        CloseChangeSchedul();
    }
    else {
        OnError();
    }
}

function LoadChart(ChartType) {
    CallServer({ ChartType: ChartType, ReportInstanceId: $('#ReportInstanceId').val() }, "/Reports/MarketSnapshot/LoadChart", LoadChart_AfterCall);
}

function LoadChart_AfterCall(Result, Status) {
    var p = $('#PlaceHolderChart');
    p.html('');
    p.append(Result);
    if (p.css('display') == 'none') {
        SetPopupPosition(p);
        p.show();
        p.bind('click', OnClick_Chart);
    }
    $('#ChartHeader', p).bind('mousedown', OnStartDrag_Chart);
    ResetZindex(p);
}

function CloseChart() {
    var p = $('#PlaceHolderChart');
    $('#ChartHeader', p).unbind('mousedown', OnStartDrag_Chart);
    p.unbind('click', OnClick_Chart);
    p.html('');
    p.hide();
}

function LoadListingDetail(PinId) {
    var Sample = $('#Sample').val();
    Sample = (Sample == '1') ? 1 : 0;
    CallServer({ ReportInstanceId: $('#ReportInstanceId').val(), PinId: PinId, Sample: Sample }, "/Reports/MarketSnapshot/LoadListingDetail", LoadListingDetail_AfterCall);
}

function LoadListingDetail_AfterCall(Result, Status) {
    var p = $('#PlaceHolderListingDetail');
    p.html('');
    p.append(Result);
    if (p.css('display') == 'none') {
        SetPopupPosition(p);
        p.show();
        p.bind('click', OnClick_ListingDetail);
    }
    $('#ListingDetailHeader', p).bind('mousedown', OnStartDrag_ListingDetail);
    ResetZindex(p);
}

function CloseListingDetail() {
    var p = $('#PlaceHolderListingDetail');
    $('#ListingDetailHeader', p).unbind('mousedown', OnStartDrag_ListingDetail);
    p.unbind('click', OnClick_ListingDetail);
    p.html('');
    p.hide();
}

function LoadCommunityGrid() {
    CallServer({ ReportInstanceId: $('#ReportInstanceId').val() }, "/Reports/Community/LoadCommunityGrid", LoadCommunityGrid_AfterCall);
}

function LoadCommunityGrid_AfterCall(Result, Status) {
    var p = $('#PlaceHolderCommunityGrid');
    p.html('');
    p.append(Result);
}

function LoadOtherReport() {
    var ReportInstanceId = $("#ddlOtherReports option:selected").val();
    var cnsmr = $('#cnsmr').val();
    window.location = "/Reports?cnsmr=" + cnsmr + "&ReportInstanceId=" + ReportInstanceId;
}

function LoadSchoolDetail(SchoolId) {
    CallServer({ ReportInstanceId: $('#ReportInstanceId').val(), SchoolId: SchoolId }, "/Reports/School/LoadSchoolDetail", LoadSchoolDetail_AfterCall);
}

function LoadSchoolDetail_AfterCall(Result, Status) {
    var p = $('#PlaceHolderSchoolDetail');
    p.html('');
    p.append(Result);
    if (p.css('display') == 'none') {
        SetPopupPosition(p);
        p.show();
        p.bind('click', OnClick_SchoolDetail);
    }
    $('#SchoolDetailHeader', p).bind('mousedown', OnStartDrag_SchoolDetail);
    ResetZindex(p);
}

function CloseSchoolDetail() {
    var p = $('#PlaceHolderSchoolDetail');
    $('#SchoolDetailHeader', p).unbind('mousedown', OnStartDrag_SchoolDetail);
    p.unbind('click', OnClick_SchoolDetail);
    p.html('');
    p.hide();
}

function LoadSchoolDetailGeneral(SchoolId) {
    $('#lk_Subjects').attr('class', '');
    $('#lk_TestResult').attr('class', '');
    $('#lk_General').attr('class', 'this');
    CallServer({ ReportInstanceId: $('#ReportInstanceId').val(), SchoolId: SchoolId }, "/Reports/School/LoadSchoolDetailGeneral", LoadSchoolDetailGeneral_AfterCall);
}

function LoadSchoolDetailGeneral_AfterCall(Result, Status) {
    var p = $('#SchoolDetailContent');
    p.html('');
    p.append(Result);
}

function LoadSchoolDetailSubjects(SchoolId) {
    $('#lk_General').attr('class', '');
    $('#lk_TestResult').attr('class', '');
    $('#lk_Subjects').attr('class', 'this');
    CallServer({ ReportInstanceId: $('#ReportInstanceId').val(), SchoolId: SchoolId }, "/Reports/School/LoadSchoolDetailSubjects", LoadSchoolDetailSubjects_AfterCall);
}

function LoadSchoolDetailSubjects_AfterCall(Result, Status) {
    var p = $('#SchoolDetailContent');
    p.html('');
    p.append(Result);
}

function LoadSchoolDetailTestResult(SchoolId) {
    $('#lk_General').attr('class', '');
    $('#lk_Subjects').attr('class', '');
    $('#lk_TestResult').attr('class', 'this');
    CallServer({ ReportInstanceId: $('#ReportInstanceId').val(), SchoolId: SchoolId }, "/Reports/School/LoadSchoolDetailTestResult", LoadSchoolDetailTestResult_AfterCall);
}

function LoadSchoolDetailTestResult_AfterCall(Result, Status) {
    var p = $('#SchoolDetailContent');
    p.html('');
    p.append(Result);
}

function SetPopupPosition(PlaceHolder) {
    var scrollTop = $(window).scrollTop();
    var top = ($(window).height() / 2) - (PlaceHolder.height() / 2);
    var left = ($(window).width() / 2) - (PlaceHolder.width() / 2);
    if (top < 0) top = 0;
    if (left < 0) left = 0;
    PlaceHolder[0].style.top = top + scrollTop + 'px';
    PlaceHolder[0].style.left = left + 'px';
}

var IsUnderDrag = false;
var UnderDragWidget;
var DragOffSet = { x: 0, y: 0 };

function OnClick_Chart(ee) {
    ResetZindex($('#PlaceHolderChart'));
}

function OnStartDrag_Chart(ee) {
    OnStartDrag(ee, 'PlaceHolderChart');
    ResetZindex($('#PlaceHolderChart'));
    return false;
}

function OnStartDrag_EmailMe(ee) {
    OnStartDrag(ee, 'PlaceHolderEmailMe');
    return false;
}

function OnClick_SchoolDetail(ee) {
    ResetZindex($('#PlaceHolderSchoolDetail'));
}

function OnClick_ListingDetail(ee) {
    ResetZindex($('#PlaceHolderListingDetail'));
}

function OnStartDrag_SchoolDetail(ee) {
    OnStartDrag(ee, 'PlaceHolderSchoolDetail');
    ResetZindex($('#PlaceHolderSchoolDetail'));
    return false;
}

function OnStartDrag_ListingDetail(ee) {
    OnStartDrag(ee, 'PlaceHolderListingDetail');
    ResetZindex($('#PlaceHolderListingDetail'));
    return false;
}

function OnClick_CommunityLargeChart(ee) {
    ResetZindex($('#PlaceHolderCommunityLargeChart'));
}

function OnStartDrag_CommunityLargeChart(ee) {
    OnStartDrag(ee, 'PlaceHolderCommunityLargeChart');
    ResetZindex($('#PlaceHolderCommunityLargeChart'));
    return false;
}

function OnStartDrag_ErrorMessageBox(ee) {
    OnStartDrag(ee, 'ErrorMessageBox');
    return false;
}

function OnStartDrag_ChangeSchedul(ee) {
    ResetZindex($('#PlaceHolderChangeSchedul'));
}

function OnClick_ChangeSchedul(ee) {
    ResetZindex($('#PlaceHolderChangeSchedul'));
}

function OnStartDrag_ChangeSchedul(ee) {
    OnStartDrag(ee, 'PlaceHolderChangeSchedul');
    ResetZindex($('#PlaceHolderChangeSchedul'));
    return false;
}

function OnStartDrag(ee, TargetId) {
    if (ee.which == 1) {
        if (!IsUnderDrag) {
            IsUnderDrag = true;
            UnderDragWidget = $('#' + TargetId);
            MousePosition.x = ee.pageX; MousePosition.y = ee.pageY;
            DragOffSet.x = MousePosition.x - UnderDragWidget.position().left;
            DragOffSet.y = MousePosition.y - UnderDragWidget.position().top;
            OnWidgetDrag(ee);
            if ($.browser.msie) {
                UnderDragWidget[0].style.filter = 'alpha(opacity=80)';
                document.onselectstart = function () { return false; }
            }
            else {
                UnderDragWidget[0].style.opacity = '0.8';
            }
            $(document).bind('mousemove', OnWidgetDrag);
            $(document).bind('mouseup', OnWidgetDrop);
        }
    }
}

function OnWidgetDrag(ee) {
    MousePosition.x = ee.pageX; MousePosition.y = ee.pageY;
    var top = MousePosition.x - DragOffSet.x; if (top < 0) { top = 0; }
    var left = MousePosition.y - DragOffSet.y; if (left < 0) { left = 0; }
    UnderDragWidget[0].style.left = top + "px";
    UnderDragWidget[0].style.top = left + "px";
}

function OnWidgetDrop(ee) {
    $(document).unbind('mousemove', OnWidgetDrag);
    $(document).unbind('mouseup', OnWidgetDrop);
    if ($.browser.msie) {
        UnderDragWidget[0].style.filter = '';
        document.onselectstart = null;
    }
    else {
        UnderDragWidget[0].style.opacity = '1';
    }
    IsUnderDrag = false;
}

function ShowStudentPerGrade() {
    var lk = $('#Link_StudentsPerGrade');
    if (lk.attr('status') == 'close') {
        lk.text('close');
        lk.attr('class', 'icon sortUp')
        $('#StudentsPerGrade').slideDown(500);
        lk.attr('status', 'open');
    }
    else {
        lk.text('Students per Grade');
        lk.attr('class', 'icon sortDown')
        $('#StudentsPerGrade').slideUp(500);
        lk.attr('status', 'close');
    }
}

function getLocalDate(argDate) {
    var monthArray = new Array("January", "Febuary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");
    var AMPM = " AM";
    var dtUTC = new Date(argDate);
    var dtLocal = new Date(dtUTC.getTime() - (dtUTC.getTimezoneOffset() * 60000));
    var iMonth = dtLocal.getMonth();
    var iDay = dtLocal.getDate();
    var iYear = dtLocal.getFullYear();
    var iHour = dtLocal.getHours();
    if (iHour == 12) {
        AMPM = " PM";
    }
    if (iHour > 12) {
        AMPM = " PM";
        iHour -= 12;
    }
    var iMinute = dtLocal.getMinutes();
    var minPrefix = "";
    if (iMinute < 10)
        minPrefix = "0";
    return monthArray[iMonth] + " " + iDay + ", " + iYear + " " + " at " + iHour + ":" + minPrefix + iMinute + AMPM;
}


// Start school district pop-up
function OnClick_SchoolDistrict(ee) {
    ResetZindex($('#PlaceHolderSchoolDistrict'));
}

function OnStartDrag_SchoolDistrict(ee) {
    OnStartDrag(ee, 'PlaceHolderSchoolDistrict');
    ResetZindex($('#PlaceHolderSchoolDistrict'));
    return false;
}

function LoadSchoolDistrict(districtId) {
    CallServer({ districtId: districtId }, "/Reports/School/LoadSchoolDistrict", LoadSchoolDistrict_AfterCall);
}

function LoadSchoolDistrict_AfterCall(Result, Status) {
    var p = $('#PlaceHolderSchoolDistrict');
    p.html('');
    p.append(Result);
    if (p.css('display') == 'none') {
        SetPopupPosition(p);
        p.show();
        p.bind('click', OnClick_SchoolDistrict);
    }
    $('#SchoolDistrictHeader', p).bind('mousedown', OnStartDrag_SchoolDistrict);
    ResetZindex(p);
}

function CloseSchoolDistrict() {
    var p = $('#PlaceHolderSchoolDistrict');
    $('#SchoolDistrictHeader', p).unbind('mousedown', OnStartDrag_SchoolDistrict);
    p.unbind('click', OnClick_SchoolDistrict);
    p.html('');
    p.hide();
}

function LoadSchoolDistrictGeneral(districtId) {
    CallServer({ districtId: districtId }, "/Reports/School/LoadSchoolDistrictGeneral", LoadSchoolDistrictBodyAfterCall);
    $('#TabGeneral').addClass('this');
    $('#TabSchools').removeClass('this');
}

function LoadSchoolDistrictSchools(districtId) {
    CallServer({ districtId: districtId }, "/Reports/School/LoadSchoolDistrictSchools", LoadSchoolDistrictBodyAfterCall);
    $('#TabGeneral').removeClass('this');
    $('#TabSchools').addClass('this');
}

function LoadSchoolDistrictBodyAfterCall(result, status) {
    var p = $('#SchoolDistrictContent');
    p.html('');
    p.append(result);
}
// End school district pop-up


