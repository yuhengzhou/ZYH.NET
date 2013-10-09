function Class_Map(ReportType) {
    var Self = this;
    Self.Map;
    Self.ContainerId;
    Self.Container;
    Self.ImagePath;

    Self.PinsList;
    Self.homeLat;
    Self.homeLong;

    Self.Pins_Sold = new Array();
    Self.Pins_New = new Array();
    Self.Pins_ForSale = new Array();
    Self.Pins_Expired = new Array();
    Self.Pins_Pending = new Array();

    Self.Pins_Community = new Array();

    Self.Pins_Public = new Array();
    Self.Pins_Private = new Array();
    Self.Pins_Other = new Array();

    Self.CurrentActivedPinId;
    Self.CurrentActivedInforBox = document.createElement('div');

    var viewchangeEventHandler;
    var PinMouseOverDelayTrack = new Object();
    var PinMouseOverDelayhandler;

    Self.Init = function () {
        switch (ReportType) {
            case 'MarketSnapshot':
                Self.ContainerId = 'divMSMap';
                break;
            case 'CommunityReport':
                Self.ContainerId = 'divCommunityMap';
                break;
            case 'SchoolReport':
                Self.ContainerId = 'divSchoolMap';
                break;
        }
        Self.Container = $('#' + Self.ContainerId);
        var myOptions = {
            credentials: "Ap86Y-FzZQaeGeJUYh15QP8raKwrW84GVCIArIkA7HFwmKmo8czz75ZqeNxqT3UO",
            enableClickableLogo: false,
            enableSearchLogo: false,
            showMapTypeSelector: false,
            //showScalebar: false,
            //showDashboard: false,
            zoom: 13,
            mapTypeId: Microsoft.Maps.MapTypeId.automatic,
            center: new Microsoft.Maps.Location(Self.homeLat, Self.homeLong)
        };

        Self.Map = new Microsoft.Maps.Map(document.getElementById(Self.ContainerId), myOptions);
    }

    Self.Dispose = function () {
        Self.HideInfoBox();
        if (Self.Map) Self.Map.dispose();
    }

    Self.SoldChecked = function (checked) {
        Self.HideInfoBox();
        for (var i = 0; i < Self.Pins_Sold.length; i++) {
            Self.Pins_Sold[i].setOptions({ visible: checked });
        }
    }

    Self.NewLilstingChecked = function (checked) {
        Self.HideInfoBox();
        for (var i = 0; i < Self.Pins_New.length; i++) {
            Self.Pins_New[i].setOptions({ visible: checked });
        }
    }

    Self.ForSaleChecked = function (checked) {
        Self.HideInfoBox();
        for (var i = 0; i < Self.Pins_ForSale.length; i++) {
            Self.Pins_ForSale[i].setOptions({ visible: checked });
        }
    }

    Self.PendingChecked = function (checked) {
        Self.HideInfoBox();
        for (var i = 0; i < Self.Pins_Pending.length; i++) {
            Self.Pins_Pending[i].setOptions({ visible: checked });
        }
    }

    Self.ExpiredChecked = function (checked) {
        Self.HideInfoBox();
        for (var i = 0; i < Self.Pins_Expired.length; i++) {
            Self.Pins_Expired[i].setOptions({ visible: checked });
        }
    }

    Self.PublicChecked = function (checked) {
        Self.HideInfoBox();
        for (var i = 0; i < Self.Pins_Public.length; i++) {
            Self.Pins_Public[i].setOptions({ visible: checked });
        }
    }

    Self.PrivateChecked = function (checked) {
        Self.HideInfoBox();
        for (var i = 0; i < Self.Pins_Private.length; i++) {
            Self.Pins_Private[i].setOptions({ visible: checked });
        }
    }

    Self.OtherChecked = function (checked) {
        Self.HideInfoBox();
        for (var i = 0; i < Self.Pins_Other.length; i++) {
            Self.Pins_Other[i].setOptions({ visible: checked });
        }
    }

    Self.CategoryClick_Community = function (Index, name) {
        Self.HideInfoBox();
        var state;
        var icon = $('#CommunityCategory' + Index.toString());
        var v = (icon.css('display') != 'none');
        if (v) { icon.hide(); state = ' (hide)'; } else { icon.show(); state = ' (show)' }
        for (var i = 0; i < Self.Pins_Community[Index].length; i++) {
            Self.Pins_Community[Index][i].setOptions({ visible: !v });
        }
        Self.AutoZoom();
        tracker.trackEvent('Community Amenities', 'Amenity Link Click', name + state);
    }

    Self.ClearAllPins_Community = function () {
        Self.HideInfoBox();
        var icon = $('span[id*="CommunityCategory"]');
        icon.hide();
        for (var i = 0; i < Self.Pins_Community.length; i++) {
            if (Self.Pins_Community[i].length > 0) {
                for (var j = 0; j < Self.Pins_Community[i].length; j++) {
                    Self.Pins_Community[i][j].setOptions({ visible: false });
                }
            }
        }
        tracker.trackEvent('Community Amenities', 'Clear All Link Click', '');
    }

    Self.AutoZoom = function () {
        var ly = 90.0, hy = -90.0, lx = 180.0, hx = -180.0, NumberOfVisiblePins = 0, center;
        switch (ReportType) {
            case 'CommunityReport':
                for (var i = 0; i < Self.Pins_Community.length; i++) {
                    if (Self.Pins_Community[i].length > 0 && Self.Pins_Community[i][0].getVisible()) {
                        for (var j = 0; j < Self.Pins_Community[i].length; j++) {
                            NumberOfVisiblePins++;
                            if (NumberOfVisiblePins == 1) { center = new Microsoft.Maps.Location(Self.PinsList[i].Businesses[j].Latitude, Self.PinsList[i].Businesses[j].Longitude); }
                            if (Self.PinsList[i].Businesses[j].Latitude < ly) { ly = Self.PinsList[i].Businesses[j].Latitude; }
                            if (Self.PinsList[i].Businesses[j].Latitude > hy) { hy = Self.PinsList[i].Businesses[j].Latitude; }
                            if (Self.PinsList[i].Businesses[j].Longitude < lx) { lx = Self.PinsList[i].Businesses[j].Longitude; }
                            if (Self.PinsList[i].Businesses[j].Longitude > hx) { hx = Self.PinsList[i].Businesses[j].Longitude; }
                        }
                    }
                }
                break;
            case 'MarketSnapshot':
                // Include 'Your Home' in the centre
                NumberOfVisiblePins++;
                if (Self.homeLat < ly) { ly = Self.homeLat }
                if (Self.homeLay > hy) { hy = Self.homeLat }
                if (Self.homeLong < lx) { lx = Self.homeLong }
                if (Self.homeLong > hx) { hx = Self.homeLong }

            case 'SchoolReport':
                for (var i = 0; i < Self.PinsList.length; i++) {
                    NumberOfVisiblePins++;
                    if (NumberOfVisiblePins == 1) { center = new Microsoft.Maps.Location(Self.PinsList[i].Latitude, Self.PinsList[i].Longitude); }
                    if (Self.PinsList[i].Latitude < ly) { ly = Self.PinsList[i].Latitude; }
                    if (Self.PinsList[i].Latitude > hy) { hy = Self.PinsList[i].Latitude; }
                    if (Self.PinsList[i].Longitude < lx) { lx = Self.PinsList[i].Longitude; }
                    if (Self.PinsList[i].Longitude > hx) { hx = Self.PinsList[i].Longitude; }
                }
                break;
        }
        if (NumberOfVisiblePins > 1) {
            var DistanceY = hy - ly; hy += DistanceY * .05;
            Self.Map.setView({ bounds: Microsoft.Maps.LocationRect.fromLocations(new Microsoft.Maps.Location(ly, lx), new Microsoft.Maps.Location(ly, hx), new Microsoft.Maps.Location(hy, lx)) });
        }
        else if (NumberOfVisiblePins == 1) {
            Self.Map.setView({ center: center });
        }
    }

    function LoadHomePin() {
        var homeMarker = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(Self.homeLat, Self.homeLong), {
            icon: "/Content/Reports/images/pin-target.png",
            height: 50,
            width: 50,
            text: 'home',
            textOffset: new Microsoft.Maps.Point(-50, 0),
            zIndex: 0
        });
        Self.Map.entities.push(homeMarker);
        return homeMarker;
    }

    Self.LoadPins_MS = function () {

        var homeMarker = LoadHomePin();
        Microsoft.Maps.Events.addHandler(homeMarker, 'mouseover', PinMouseOver);
        Microsoft.Maps.Events.addHandler(homeMarker, 'mouseout', PinMouseOut);

        var pin, typeName;
        for (i = 0; i < Self.PinsList.length; i++) {
            switch (Self.PinsList[i].Status) {//Sold = 1, NewListing = 2, ForSale = 3, Expired=4, Pending=5
                case 1:
                    typeName = "iconMap pinSold";
                    break;
                case 2:
                    typeName = "iconMap pinNewListing";
                    break;
                case 3:
                    typeName = "iconMap pinForSale";
                    break;
                case 4:
                    typeName = "iconMap pinExpired";
                    break;
                case 5:
                    typeName = "iconMap pinPending";
                    break;
            }
            pin = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(Self.PinsList[i].Latitude, Self.PinsList[i].Longitude), {
                icon: "/Content/Reports/images/pin-noimage.png",
                typeName: typeName,
                text: Self.PinsList[i].Id.toString(),
                textOffset: new Microsoft.Maps.Point(0, 5),
                zIndex: -5, height: 32, width: 28
            });
            Self.Map.entities.push(pin);
            Microsoft.Maps.Events.addHandler(pin, 'click', Self.SelectPinInGrid);
            Microsoft.Maps.Events.addHandler(pin, 'mouseover', PinMouseOver);
            Microsoft.Maps.Events.addHandler(pin, 'mouseout', PinMouseOut);
            switch (Self.PinsList[i].Status) {//Sold = 1, NewListing = 2, ForSale = 3, Expired=4, Pending=5
                case 1:
                    Self.Pins_Sold[Self.Pins_Sold.length] = pin;
                    break;
                case 2:
                    Self.Pins_New[Self.Pins_New.length] = pin;
                    break;
                case 3:
                    Self.Pins_ForSale[Self.Pins_ForSale.length] = pin;
                    break;
                case 4:
                    Self.Pins_Expired[Self.Pins_Expired.length] = pin;
                    break;
                case 5:
                    Self.Pins_Pending[Self.Pins_Pending.length] = pin;
                    break;
            }
        }
        Self.AutoZoom();
    }

    Self.LoadPins_Community = function () {
        LoadHomePin();
        for (var i = 0; i < Self.PinsList.length; i++) {
            Self.Pins_Community[i] = new Array();
            for (var j = 0; j < Self.PinsList[i].Businesses.length; j++) {
                var pin = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(Self.PinsList[i].Businesses[j].Latitude, Self.PinsList[i].Businesses[j].Longitude), {
                    icon: "/Content/Reports/images/pin-noimage.png",
                    typeName: "iconMap " + Self.PinsList[i].Name.toLowerCase(),
                    text: i.toString() + '|' + j.toString(),
                    textOffset: new Microsoft.Maps.Point(0, 0),
                    zIndex: -5, height: 19, width: 18,
                    visible: false
                });
                Self.Map.entities.push(pin);
                Microsoft.Maps.Events.addHandler(pin, 'click', Self.ShowInfoBox_Click);
                Microsoft.Maps.Events.addHandler(pin, 'mouseover', PinMouseOver);
                Microsoft.Maps.Events.addHandler(pin, 'mouseout', PinMouseOut);
                Self.Pins_Community[i][j] = pin;
            }
        }
    }

    Self.LoadPins_School = function () {

        var IsSeller = $('#SellerHomeInfor').length == 1;
        var homeMarker = LoadHomePin();

        if (IsSeller) {
            Microsoft.Maps.Events.addHandler(homeMarker, 'mouseover', PinMouseOver);
            Microsoft.Maps.Events.addHandler(homeMarker, 'mouseout', PinMouseOut);
        }

        var pin, typeName;
        for (i = 0; i < Self.PinsList.length; i++) {
            switch (Self.PinsList[i].Type) {
                case 'Public':
                    typeName = "iconMap mapPublic";
                    break;
                case 'Private':
                    typeName = "iconMap mapPrivate";
                    break;
                default:
                    typeName = "iconMap mapOther";
                    break;
            }
            pin = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(Self.PinsList[i].Latitude, Self.PinsList[i].Longitude), {
                icon: "/Content/Reports/images/pin-noimage.png",
                typeName: typeName,
                id: (Self.PinsList[i].Index + 1),
                text: (Self.PinsList[i].Index + 1).toString(),
                textOffset: new Microsoft.Maps.Point(0, 0),
                zIndex: -5, height: 19, width: 18
            });
            Self.Map.entities.push(pin);
            Microsoft.Maps.Events.addHandler(pin, 'click', Self.SelectPinInGrid);
            Microsoft.Maps.Events.addHandler(pin, 'mouseover', PinMouseOver);
            Microsoft.Maps.Events.addHandler(pin, 'mouseout', PinMouseOut);
            switch (Self.PinsList[i].Type) {
                case 'Public':
                    Self.Pins_Public[Self.Pins_Public.length] = pin;
                    break;
                case 'Private':
                    Self.Pins_Private[Self.Pins_Private.length] = pin;
                    break;
                default:
                    Self.Pins_Other[Self.Pins_Other.length] = pin;
                    break;
            }
        }
        Self.AutoZoom();
    }

    Self.SelectPinInGrid = function (e) {
        var id = e.target.getText();
        switch (Self.ContainerId) {
            case 'divMSMap':
                MSGrid.OnExternalCommand_MapSelected(id);
                CreateInfoWindow(id);
                break;
            case 'divCommunityMap':
                break;
            case 'divSchoolMap':
                SchoolGrid.OnExternalCommand_MapSelected(id - 1);
                CreateInfoWindow(id);
                break;
        }
    }

    Self.HideInfoBox = function (e) {
        try {
            $('#ThemeWrap')[0].removeChild(Self.CurrentActivedInforBox);
            Microsoft.Maps.Events.removeHandler(viewchangeEventHandler);
            $(window).unbind('resize', MoveInforBox);
        } catch (ex) { }
    }

    var OriginalPinClass = '';
    function PinMouseOver(e) {
        if (typeof (PinMouseOverDelayTrack[e.target.getText()]) == 'undefined' || PinMouseOverDelayTrack[e.target.getText()] != true) {
            OriginalPinClass = e.target.getTypeName();
            //e.target.setOptions({ typeName: 'iconMap pinSoldOn' });
            PinMouseOverDelayTrack[e.target.getText()] = true;
            PinMouseOverDelayhandler = setTimeout(function () { Self.ShowInfoBox(e); }, 300);
        }
    }

    function PinMouseOut(e) {
        //        e.target.setOptions({ typeName: OriginalPinClass });
        PinMouseOverDelayTrack[e.target.getText()] = false;
        clearTimeout(PinMouseOverDelayhandler);
    }

    Self.ShowInfoBox = function (e) {
        if (PinMouseOverDelayTrack[e.target.getText()] == true) {
            CreateInfoWindow(e.target.getText());
        }
    }

    Self.ShowInfoBox_Click = function (e) {
        CreateInfoWindow(e.target.getText());
    }

    Self.ShowInfoBoxCenter = function (Id) {
        var PinIndex = Id - 1;
        var center = new Microsoft.Maps.Location(Self.PinsList[PinIndex].Latitude, Self.PinsList[PinIndex].Longitude);
        Self.Map.setView({ center: center });
        CreateInfoWindow(Id);
        //window.setTimeout(function () { CreateInfoWindow(Id, center); }, 500);
    }

    function CreateInfoWindow(Id) {
        Self.HideInfoBox();
        Self.CurrentActivedPinId = Id
        var infoContent;
        switch (Self.ContainerId) {
            case 'divMSMap':
                infoContent = getInfoContent_MS(Id);
                break;
            case 'divCommunityMap':
                infoContent = getInfoContent_Community(Id);
                break;
            case 'divSchoolMap':
                infoContent = getInfoContent_School(Id);
                break;
        }
        Self.CurrentActivedInforBox.innerHTML = infoContent;
        Self.CurrentActivedInforBox.style.zIndex = 99;
        Self.CurrentActivedInforBox.style.position = 'absolute';
        $('#ThemeWrap')[0].appendChild(Self.CurrentActivedInforBox);
        MoveInforBox();
        viewchangeEventHandler = Microsoft.Maps.Events.addHandler(Self.Map, 'viewchange', MoveInforBox);
        $(window).bind('resize', MoveInforBox);
    }

    function MoveInforBox() {
        if (Self.CurrentActivedPinId) {
            var Offset, coords, MapWidth = Self.Container.width();
            switch (Self.ContainerId) {
                case 'divMSMap':
                    if (Self.CurrentActivedPinId == 'home') {
                        coords = new Microsoft.Maps.Location(Self.homeLat, Self.homeLong);
                        Offset = { x: -175, y: -130 };
                    }
                    else {
                        coords = new Microsoft.Maps.Location(Self.PinsList[Self.CurrentActivedPinId - 1].Latitude, Self.PinsList[Self.CurrentActivedPinId - 1].Longitude);
                        Offset = { x: -175, y: -145 };
                    }
                    break;
                case 'divCommunityMap':
                    var id = Self.CurrentActivedPinId.split('|');
                    var i = id[0]; var j = id[1];
                    coords = new Microsoft.Maps.Location(Self.PinsList[i].Businesses[j].Latitude, Self.PinsList[i].Businesses[j].Longitude);
                    Offset = { x: -175, y: -107 };
                    break;
                case 'divSchoolMap':
                    if (Self.CurrentActivedPinId == 'home') {
                        coords = new Microsoft.Maps.Location(Self.homeLat, Self.homeLong);
                        Offset = { x: -175, y: -130 };
                    }
                    else {
                        coords = new Microsoft.Maps.Location(Self.PinsList[Self.CurrentActivedPinId - 1].Latitude, Self.PinsList[Self.CurrentActivedPinId - 1].Longitude);
                        Offset = { x: -180, y: 10 };
                    }
                    break;
            }
            var PinPosition = Self.Map.tryLocationToPixel(coords, Microsoft.Maps.PixelReference.control);
            if (PinPosition.y < 0 + 5 || PinPosition.y > 310 + 35 || PinPosition.x < 0 - 20 || PinPosition.x > MapWidth + 10) {
                Self.CurrentActivedInforBox.style.display = 'none';
            }
            else {
                Self.CurrentActivedInforBox.style.display = '';
            }
            var MapPosition = $('#' + Self.ContainerId).position();
            //var BoxHeight = Self.CurrentActivedInforBox.height();
            Self.CurrentActivedInforBox.style.top = (PinPosition.y + MapPosition.top + Offset.y).toString() + 'px';
            Self.CurrentActivedInforBox.style.left = (PinPosition.x + MapPosition.left + Offset.x).toString() + 'px';
        }
    }

    function getInfoContent_MS(ID) {
        var infoContent = '';
        if (ID == 'home') {
            var homeInfor = $('#SellerHomeInfor').val();
            var homeAddress = $('#SellerAddress').text();
            var popupTitle = 'Your Home';

            if (typeof homeInfor == 'undefined') {
                popupTitle = 'Of Interest'
                homeInfor = $('#BuyerHomeInfor').val();
                homeAddress = $('#BuyerAddress').text();
            }

            infoContent = '<div class="infoBoxContainer"><div class="popup seller" ><div class="title"><a href="javascript:MSMap.HideInfoBox();" class="ir icon close">close</a><h4 class="ellipsis">' + popupTitle + '</h4></div>' +
            '<div class="dataRow"><p>' + homeInfor + '</p><p>' + homeAddress + '</p>' +
             '<p class="location">Location may be approximate</p></div>' +
              '<div class="pointer pointerBottomRight"><div class="pointerInner"></div></div></div></div>';
            return infoContent;
        }

        infoContent = '<div class="infoBoxContainer $STATUS$"><div class="popup" ><div class="title"><a href="javascript:MSMap.HideInfoBox();" class="ir icon close">close</a><h4 class="ellipsis">$ADDRESS$</h4></div>' +
        '<div class="dataRow"><a href="$DETAIL$" class="photo"><img src="$IMAGE$" /></a>' +
        '<p class="price"><span class="status">$STATUS1$</span>$$PRICE$</p>' +

        '<p>$BED$ beds, $BATH$ baths</p><p> $SQFT$ sq.ft., $$PRICEPERSQFT$ / S.F.</p>' +
         '$MlsLogo$' +
        '<div><a class=\'viewDetails\' href="$DETAIL1$">View Details</a>' +
        '<span class="location">Location may be approximate</span></div></div>' +
        '<div class="pointer pointerBottomRight"><div class="pointerInner"></div></div></div></div>';

        ID--;
        var className = 'Sold'; var StatusName = 'Sold';
        switch (Self.PinsList[ID].Status) {//Sold = 1, NewListing = 2, ForSale = 3, Expired=4, Pending=5
            case 1:
                className = "Sold";
                StatusName = "Sold";
                break;
            case 2:
                className = "NewListing";
                StatusName = "New Listing";
                break;
            case 3:
                className = "ForSale";
                StatusName = "For Sale";
                break;
            case 4:
                className = "Expired";
                StatusName = "Expired";
                break;
            case 5:
                className = "Pending";
                StatusName = "Pending";
                break;
        }
        infoContent = infoContent.replace("$ADDRESS$", Self.PinsList[ID].Address)
   .replace("$PRICE$", addCommas(Self.PinsList[ID].DisplayPrice))
   .replace("$IMAGE$", Self.ImagePath + Self.PinsList[ID].ImageFile)
   .replace("$BED$", Self.PinsList[ID].Bed)
   .replace("$BATH$", Self.PinsList[ID].Bath)
   .replace("$SQFT$", Self.PinsList[ID].Sq)
   .replace("$PRICEPERSQFT$", Self.PinsList[ID].PricePerSq)
   .replace("$DETAIL$", "javascript:LoadListingDetail(" + Self.PinsList[ID].Id + ");")
   .replace("$DETAIL1$", "javascript:LoadListingDetail(" + Self.PinsList[ID].Id + ");")
   .replace("$STATUS$", className)
   .replace("$STATUS1$", StatusName);

        var MlsLogoFileUrl = $('#MlsLogoFileUrl').val();
        if (MlsLogoFileUrl != null && MlsLogoFileUrl != '') { infoContent = infoContent.replace("$MlsLogo$", "<img class='mlsLogo' src='" + MlsLogoFileUrl + "' />"); } else { infoContent = infoContent.replace("$MlsLogo$", ""); }

        return infoContent;
    }

    function getInfoContent_Community(ids) {
        var id = ids.split('|');
        var i = id[0]; var j = id[1];
        var infoContent = '<div class="infoBoxContainer"><div class="popup"><div class="title"><a href="javascript:CommunityMap.HideInfoBox();" class="ir icon close">close</a><h4>$NAME$</h4></div><div class="dataRow"><p>$ADDRESS$</p><p>$Phone$</p>' +
'</div><div class="pointer pointerBottomRight"><div class="pointerInner"></div></div></div></div>';
        infoContent = infoContent.replace("$NAME$", Self.PinsList[i].Businesses[j].Name)
       .replace("$ADDRESS$", Self.PinsList[i].Businesses[j].Address)
       .replace("$Phone$", Self.PinsList[i].Businesses[j].Phone);

        return infoContent;
    }

    function getInfoContent_School(ID) {
        var infoContent = '';
        if (ID == 'home') {
            var SellerHomeInfor = $('#SellerHomeInfor').val();
            var SellerAddress = $('#SellerAddress').text();
            infoContent = '<div class="infoBoxContainer"><div class="popup seller" ><div class="title"><a href="javascript:SchoolMap.HideInfoBox();" class="ir icon close">close</a><h4 class="ellipsis">Your Home</h4></div>' +
            '<div class="dataRow"><p>' + SellerHomeInfor + '</p><p>' + SellerAddress + '</p>' +
             '<p class="location">Location may be approximate</p></div>' +
              '<div class="pointer pointerBottomRight"><div class="pointerInner"></div></div></div></div>';
            return infoContent;
        }

        ID--;
        infoContent = '<div class="infoBoxContainer"><div class="popup"><div class="title"><a href="javascript:SchoolMap.HideInfoBox();" class="ir icon close">close</a><h4><a style="color:White;" onclick="tracker.trackEvent(\'School\',\'View Detail Link Click\',Self.PinsList[ID].SchoolId);" href="javascript:LoadSchoolDetail(' + Self.PinsList[ID].SchoolId + ');">$NAME$</a></h4></div><div class="dataRow">' +
'<table class="dataTable"><tr><th>Grades</th><td>$GRADE$</td></tr><tr><th>Type</th><td>$TYPE$</td></tr><tr><th>Students</th><td>$STUDENTS$</td></tr>';
        if (Self.PinsList[ID].Url != '') {
            infoContent += '<tr><th>Website</th><td><a class="webLink" href="javascript:void window.open(\'' + Self.PinsList[ID].Url + '\');">visit website<span class="icon"></span></a></td></tr>';
        }
        infoContent += '<tr><td><a href="javascript:LoadSchoolDetail(' + Self.PinsList[ID].SchoolId + ');">View Detail</a></td></tr></table>' +
'</div><div class="pointer pointerTopRight"><div class="pointerInner"></div></div></div></div>';
        infoContent = infoContent.replace("$NAME$", Self.PinsList[ID].Name)

   .replace("$GRADE$", Self.PinsList[ID].Grade)
   .replace("$TYPE$", Self.PinsList[ID].Type)
   .replace("$STUDENTS$", Self.PinsList[ID].StudentCount)

        return infoContent;
    }

    function addCommas(nStr) {
        nStr += '';
        x = nStr.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        return x1 + x2;
    }
}
