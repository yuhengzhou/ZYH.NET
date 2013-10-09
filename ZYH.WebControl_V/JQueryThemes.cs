using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ZYH.WebControl_V
{
    public class JQueryThemesUrl
    {
        Hashtable _ThemeUrl = new Hashtable();
        JQueryThemes _theme;

        public JQueryThemesUrl() : this(JQueryThemes.Darkness) { }
        public JQueryThemesUrl(JQueryThemes theme)
        {
            _theme = theme;
            Init();
        }

        public string Url { get { return _ThemeUrl[_theme].ToString(); } }

        private void Init()
        {
            _ThemeUrl.Add(JQueryThemes.BlackTie, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/black-tie/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.Blitzer, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/blitzer/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.Cupertino, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/cupertino/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.DarkHive, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/dark-hive/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.DotLuv, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/dot-luv/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.Eggplant, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/eggplant/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.ExciteBike, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/excite-bike/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.Flick, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/flick/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.HotSneaks, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/hot-sneaks/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.Humanity, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/humanity/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.LeFrog, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/le-frog/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.MintChoc, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/mint-choc/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.Overcast, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/overcast/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.PepperGrinder, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/pepper-grinder/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.Redmond, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/redmond/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.Smoothness, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/smoothness/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.SouthStreet, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/south-street/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.Start, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/start/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.Sunny, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/sunny/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.SwankyPurse, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/swanky-purse/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.Trontastic, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/trontastic/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.Darkness, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/ui-darkness/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.Lightness, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/ui-lightness/jquery-ui.css");
            _ThemeUrl.Add(JQueryThemes.Vader, "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/vader/jquery-ui.css");
        }
    }
}
