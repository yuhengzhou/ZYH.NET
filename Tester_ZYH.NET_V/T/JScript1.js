var FollowUpSetting = {
    Settings: null,
    HighlightRowId: 1011,
    OriginalSettings: null,
    GetSetting: function (a) {
        for (var b = 0; b < FollowUpSetting.Settings.length; b++) {
            if (FollowUpSetting.Settings[b].PreferenceTypeId === a) {
                return FollowUpSetting.Settings[b]
            }
        }
        return null
    },
    GetOriginalSetting: function (a) {
        for (var b = 0; b < FollowUpSetting.OriginalSettings.length; b++) {
            if (FollowUpSetting.OriginalSettings[b].PreferenceTypeId === a) {
                return FollowUpSetting.OriginalSettings[b]
            }
        }
        return null
    },
    Init: function () {
        FollowUpSetting.HighlightRowId = 1011;
        dataServiceGate({
            url: "/DataService/Preference.svc/GetFollowUpSettings",
            httpMethod: "GET",
            onSuccess: function (a) {
                FollowUpSetting.Settings = a;
                FollowUpSetting.OriginalSettings = a;
                FollowUpSetting.Create()
            },
            onError: function (a) {
                console.log(a)
            },
            onCommonError: function (a) {
                console.log(a)
            }
        })
    },
    EnableClicked: function (a) {
        if ($("#a" + a).attr("class") == "enabled") {
            $("#a" + a).attr("class", "disabled")
        } else {
            $("#a" + a).attr("class", "enabled")
        }
    },
    RowClicked: function (a) {
        if (FollowUpSetting.HighlightRowId == a) {
            return
        }
        var c = $("#tableFollowUpSettings");
        $("#tr" + FollowUpSetting.HighlightRowId, c).attr("class", "");
        $("#tr" + a, c).attr("class", "row-highlight");
        var d = $("#text" + FollowUpSetting.HighlightRowId, c);
        d.hide();
        var f = $("#text" + a, c);
        f.show().focus();
        var b = f.val();
        f.val("");
        f.val(b);
        var e = $("#label" + FollowUpSetting.HighlightRowId, c);
        e.html(d.val());
        e.show();
        $("#label" + a, c).hide();
        $("#rule" + FollowUpSetting.HighlightRowId).hide();
        $("#rule" + a).show();
        FollowUpSetting.HighlightRowId = a
    },
    SaveClick: function () {
        var a = false;
        var m = true;
        var d = $("#tableFollowUpSettings");
        var r = -1;
        var l = "";
        var j = $("#text1011", d).val().trim();
        if (j == "") {
            l = " This information is required."
        } else {
            if (!(/^\d+$/.test(j))) {
                l = " Must be between 1 and 999 days."
            } else {
                j = parseInt(j, 10);
                if (j < 1 || j > 999) {
                    l = "Must be between 1 and 999 days."
                }
            }
        }
        $("#msg1011", d).html(l);
        if (l != "") {
            m = false;
            r = 1011
        } else {
            var t = FollowUpSetting.GetOriginalSetting(1011);
            var q = FollowUpSetting.GetOriginalSetting(1012);
            var p = FollowUpSetting.GetOriginalSetting(1013);
            var n = FollowUpSetting.GetOriginalSetting(1014);
            if (t.Value != j) {
                t.Value = j;
                a = true;
                q.Value = j;
                p.Value = j;
                n.Value = j
            }
            var k = $("#a1011").attr("class") == "enabled";
            if (t.Enable != k) {
                t.Enable = k;
                a = true;
                q.Enable = k;
                p.Enable = k;
                n.Enable = k
            }
        }
        l = "";
        var g = $("#text1021", d).val().trim();
        if (g == "") {
            l = "This information is required."
        } else {
            if (!(/^\d+$/.test(g))) {
                l = "Must be between 1 and 999 days."
            } else {
                g = parseInt(g, 10);
                if (g < 1 || g > 999) {
                    l = "Must be between 1 and 999 days."
                }
            }
        }
        $("#msg1021", d).html(l);
        if (l != "") {
            m = false;
            if (r == -1) {
                r = 1021
            }
        } else {
            var t = FollowUpSetting.GetOriginalSetting(1021);
            if (t.Value != g) {
                t.Value = g;
                a = true
            }
            var k = $("#a1021").attr("class") == "enabled";
            if (t.Enable != k) {
                t.Enable = k;
                a = true
            }
        }
        l = "";
        var c = $("#text1031", d).val().trim();
        if (c == "") {
            l = "This information is required."
        } else {
            if (!(/^\d+$/.test(c))) {
                l = "Must be between 1 and 999 days."
            } else {
                c = parseInt(c, 10);
                if (c < 1 || c > 999) {
                    l = "Must be between 1 and 999 days."
                }
            }
        }
        $("#msg1031", d).html(l);
        if (l != "") {
            m = false;
            if (r == -1) {
                r = 1031
            }
        } else {
            var t = FollowUpSetting.GetOriginalSetting(1031);
            var q = FollowUpSetting.GetOriginalSetting(1032);
            var p = FollowUpSetting.GetOriginalSetting(1033);
            if (t.Value != c) {
                t.Value = c;
                a = true;
                q.Value = c;
                p.Value = c
            }
            var k = $("#a1031").attr("class") == "enabled";
            if (t.Enable != k) {
                t.Enable = k;
                a = true;
                q.Enable = k;
                p.Enable = k
            }
        }
        l = "";
        var b = $("#text1041", d).val().trim();
        if (b == "") {
            l = " This information is required."
        } else {
            if (!(/^\d+$/.test(b))) {
                l = "Must be between 1 and 999 days."
            } else {
                b = parseInt(b, 10);
                if (b < 1 || b > 999) {
                    l = "Must be between 1 and 999 days."
                }
            }
        }
        $("#msg1041", d).html(l);
        if (l != "") {
            m = false;
            if (r == -1) {
                r = 1041
            }
        } else {
            var t = FollowUpSetting.GetOriginalSetting(1041);
            if (t.Value != b) {
                t.Value = b;
                a = true
            }
            var k = $("#a1041").attr("class") == "enabled";
            if (t.Enable != k) {
                t.Enable = k;
                a = true
            }
        }
        l = "";
        var o = $("#text1051", d).val().trim();
        if (o == "") {
            l = " This information is required."
        } else {
            if (!(/^\d+$/.test(o))) {
                l = "Must be between 1 and 999 days."
            } else {
                o = parseInt(o, 10);
                if (o < 1 || o > 999) {
                    l = "Must be between 1 and 999 days."
                }
            }
        }
        $("#msg1051", d).html(l);
        if (l != "") {
            m = false;
            if (r == -1) {
                r = 1051
            }
        } else {
            var t = FollowUpSetting.GetOriginalSetting(1051);
            if (t.Value != o) {
                t.Value = o;
                a = true
            }
            t.Enable = true
        } if (!m) {
            FollowUpSetting.RowClicked(r);
            return
        }
        if (!a) {
            FollowUpSetting.CancelClick(2);
            return
        }
        var h = [];
        for (var f = 0; f < FollowUpSetting.Settings.length; f++) {
            var t = FollowUpSetting.Settings[f];
            h.push({
                PreferenceTypeId: t.PreferenceTypeId,
                Value: t.Value,
                Enable: t.Enable,
                RankId: t.RankId
            })
        }
        dataServiceGate({
            url: "/DataService/Preference.svc/SetFollowUpSettings",
            httpMethod: "POST",
            data: h,
            onSuccess: function (e) {
                FollowUp.initFollowUp("next");
                FollowUpSetting.CancelClick(3)
            },
            onError: function (e) {
                console.log(e)
            },
            onCommonError: function (e) {
                console.log(e)
            }
        })
    },
    CancelClick: function (a) {
        addGAEvent("FollowUpSetting", a === 1 ? "xIconClicked" : "CancelClicked");
        FollowUpSetting.Destroy()
    },
    RestorDefaultClick: function () {
        dataServiceGate({
                url: "/DataService/Preference.svc/GetDefaultFollowUpSettings",
                httpMethod: "GET",
                onSuccess: function (a) {
                    FollowUpSetting.HighlightRowId = 1011;
                    FollowUpSetting.Settings = a;
                    FollowUpSetting.Destroy();
                    FollowUpSetting.Create()
                },
                onError: function (a) {
                    con...avascript: window.open(\'' + FollowUpSettingHelpLink + '\');addGAEvent(\'FollowUpSettings\', \'FollowUpSettingsHelp\');" title="Why follow up with these contacts?"><span class="ir icon actionHelpCenter">help</span></a></h2>\r\n</div>\r\n</div>\r\n<div class="modalBody modalSettings" >\r\n<div class="body">\r\n<p>Consistent follow-up provides a valuable return on your time investment. That’s why we’ve taken these 5 core groups and will prompt you when it’s time to touch base. We’ll also suggest actions or conversations to speed things up. </p>\r\n<p>All you have to do is set how often you want to connect with each group and make sure you’ve assigned contact types. We’ll do the rest.</p>\r\n<div >\r\n<div class="left-rail">\r\n<div id="rule1011" class="sp-box sp-coaching rule1 ">\r\n<div class="sp-nub sp-nubRightTop"></div>\r\n<p><span id="Coaching1011">Rule 1 Coaching Text</span></p>\r\n</div>\r\n<div id="rule1021" class="sp-box sp-coaching rule2" style="display:none;">\r\n<div class="sp-nub sp-nubRightTop"></div>\r\n<p><span id="Coaching1021">Rule 2 Coaching Text</span></p>\r\n</div>\r\n<div id="rule1031" class="sp-box sp-coaching rule3" style="display:none;">\r\n<div class="sp-nub sp-nubRightTop"></div>\r\n<p><span id="Coaching1031">Rule 3 Coaching Text</span></p>\r\n</div>\r\n<div id="rule1041" class="sp-box sp-coaching rule4" style="display:none;">\r\n<div class="sp-nub sp-nubRightTop"></div>\r\n<p><span id="Coaching1041">Rule 4 Coaching Text</span></p>\r\n</div>\r\n<div id="rule1051" class="sp-box sp-coaching rule5" style="display:none;">\r\n<div class="sp-nub sp-nubRightTop"></div>\r\n<p><span id="Coaching1051">Rule 5 Coaching Text</span></p>\r\n</div>\r\n</div>\r\n<table id="tableFollowUpSettings" border="0" cellpadding="0" cellspacing="0" class="settings-table">\r\n<tr>\r\n<th>Rule</th>\r\n<th class="column-wide">Last action was more than</th>\r\n<th>On/Off</th>\r\n</tr>\r\n<tr id="tr1011" class="row-highlight" onclick="FollowUpSetting.RowClicked(1011);">\r\n<td><em class="rule-box rule1"></em>Past Client</td>\r\n<td><label id="label1011" style="display:none;"></label>\r\n<input name="text1011" type="text" id="text1011" size="5" />\r\n<span>days ago</span><span class="error" id="msg1011"></span></td>\r\n<td><a id="a1011" href="javascript:FollowUpSetting.EnableClicked(1011);addGAEvent(\'FollowUpSettings\', \'FollowUpSettingsPastClicked\');">1011</a></td>\r\n</tr>\r\n<tr id="tr1021" onclick="FollowUpSetting.RowClicked(1021);">\r\n<td><em class="rule-box rule2"></em>Sphere of Influence</td>\r\n<td><label id="label1021"></label>\r\n<input name="text1021" type="text" id="text1021" size="5" style="display:none;" />\r\n<span>days ago</span><span class="error" id="msg1021"></span></td>\r\n<td><a id="a1021" href="javascript:FollowUpSetting.EnableClicked(1021);addGAEvent(\'FollowUpSettings\', \'FollowUpSettingsSphereClicked\');">1021</a></td>\r\n</tr>\r\n<tr id="tr1031" onclick="FollowUpSetting.RowClicked(1031);">\r\n<td><em class="rule-box rule3"></em>New</td>\r\n<td><label id="label1031"></label>\r\n<input name="text1031" type="text" id="text1031" size="5" style="display:none;" />\r\n<span>days ago</span><span class="error" id="msg1031"></span></td>\r\n<td><a id="a1031" href="javascript:FollowUpSetting.EnableClicked(1031);addGAEvent(\'FollowUpSettings\', \'FollowUpSettingsNewClicked\');">1031</a></td>\r\n</tr>\r\n<tr id="tr1041" onclick="FollowUpSetting.RowClicked(1041);">\r\n<td><em class="rule-box rule4"></em>Imports</td>\r\n<td><label id="label1041"></label>\r\n<input name="text1041" type="text" id="text1041" size="5" style="display:none;" />\r\n<span>days ago</span><span class="error" id="msg1041"></span></td>\r\n<td><a id="a1041" href="javascript:FollowUpSetting.EnableClicked(1041);addGAEvent(\'FollowUpSettings\', \'FollowUpSettingsImportClicked\');">1041</a></td>\r\n</tr>\r\n<tr id="tr1051" onclick="FollowUpSetting.RowClicked(1051);">\r\n<td><em class="rule-box rule5"></em>Everyone Else</td>\r\n<td><label id="label1051"></label>\r\n<input name="text1051" type="text" id="text1051" size="5" style="display:none;" />\r\n<span>days ago</span><span class="error" id="msg1051"></span></td>\r\n<td><span class="enabled no-toggle"></span></td>\r\n</tr>\r\n</table>\r\n</div>\r\n</div>\r\n<div class="buttonRow" id="FollowUpSettingButtonRow">\r\n<a href="javascript:FollowUpSetting.RestorDefaultClick();addGAEvent(\'FollowUpSettings\', \'FollowUpSettingsRestoreDefault\');" class="themeLink reset-link">Restore Defaults</a>\r\n<input type="button" onclick="FollowUpSetting.SaveClick();addGAEvent(\'FollowUpSettings\', \'FollowUpSettingsSave\');" value="Save" style="margin-left: 3px" class="button">\r\n<input type="button" onclick="FollowUpSetting.CancelClick(2);addGAEvent(\'FollowUpSettings\', \'FollowUpSettingsCancel\');" value="Cancel" style="margin-left: 3px" class="button">\r\n</div>\r\n</div>\r\n<div class="section_footerbar">\r\n<div class="section_footerbarRightCorner">&nbsp;</div>\r\n</div>\r\n</div>';
                        return c
                    }
                };;